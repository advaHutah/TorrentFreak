using Newtonsoft.Json;
using objectClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClientApp
{
    public delegate void DownloadEvent(DownloadInfo info,bool isFinished);
    class ClientFileRequest
    {
        //updates all ui related listeners
        public DownloadEvent downloadEventSchedule;

       
        private string downlodPath;
        private string uploadPath;
        private int port;
        byte[] downloadBytes;
        private string filename;
        private List<string> SourcesIps;
        private int fileSize;

        public ClientFileRequest(string filename, List<string> sourcesIps, int fileSize, string downlodPath,string uploadPath,int port)
        {
            this.port = port;
            this.filename = filename;
            SourcesIps = sourcesIps;
            this.fileSize = fileSize;
            this.downlodPath = downlodPath;
            this.uploadPath = uploadPath;
        }

        public void RequsetFileFromPeers()
        {
            DownloadInfo info = new DownloadInfo(filename, fileSize, SourcesIps.Count, "Downloading..", DateTime.Now.ToString());


            openStreamAndDownloadPieces(info);

            //copy file to upload folder for adding me as source
            addMeAsSourceFile(downlodPath, filename, uploadPath);

            //delgate ui information
            updateUIinfo(info);

        }

        void openStreamAndDownloadPieces(DownloadInfo info)
        {
            if (File.Exists(downlodPath + "\\" + filename))
                File.Delete(downlodPath + "\\" + filename);

            //Create file stram to start "making" the file from bits recived
            FileStream fileStream = File.Open(downlodPath + "\\" + filename, FileMode.Create);
            downloadBytes = new byte[fileSize];

            //list of file pieces
            List<DownloadSourcePiece> pieces = new List<DownloadSourcePiece>();

            //calculate every uploader piece size
            int pieceSize = fileSize / SourcesIps.Count;
            int downloadBytesRemaining = fileSize;

            //delgeate handling
           
            downloadEventSchedule(info, false);

            //start downloading pieces
            DownloadSourcePiece piece;
            for (int i = 0; i < SourcesIps.Count; i++)
            {
                int current = i;
                if (i == SourcesIps.Count - 1)
                {
                    piece = new DownloadSourcePiece(SourcesIps[current], filename, fileSize, pieceSize * current, downloadBytesRemaining, port);
                    //add piece to array
                    pieces.Add(piece);
                    //new thread for piece download
                    piece.downloadTask.Start();
                    //last piece
                    break;
                }
                piece = new DownloadSourcePiece(SourcesIps[current], filename, fileSize, pieceSize * current, pieceSize, port);
                downloadBytesRemaining -= pieceSize;
                //add piece to array
                pieces.Add(piece);
                //new thread for piece download
                piece.downloadTask.Start();
            }

            // Wait for all pieces to be downloaded
            for (int i = 0; i < pieces.Count; i++)
            {
                Task.WaitAll(pieces[i].downloadTask);
                //joining all file pieces together
                fileStream.Write(pieces[i].pieceBytesArr, 0, pieces[i].pieceBytesArr.Length);
            }
            fileStream.Close();
        }
        void updateUIinfo(DownloadInfo info)
        {
            TimeSpan timeTaken = DateTime.Now - DateTime.Parse(info.Time);
            info.Kbps = (int)((info.Size / 1000) / timeTaken.TotalSeconds);
            info.Status = "Finished";
            downloadEventSchedule(info, true);
        }
        void addMeAsSourceFile(string downlodPath , string filename, string uploadPath)
        {
            System.IO.File.Copy(downlodPath + "\\" + filename, uploadPath + "\\" + filename, true);
        }

        class DownloadSourcePiece
        {
            private string ip;
            private string fileName;
            private int fileSize;
            private int fileOffset;
            private int pieceSize;
            private int port;
            public Task downloadTask { get; }
            public byte[] pieceBytesArr { get; }

            public DownloadSourcePiece(string ip, string fileName, int fileSize, int fileOffset, int pieceSize,int port)
            {
                this.ip = ip;
                this.fileName = fileName;
                this.fileSize = fileSize;
                this.fileOffset = fileOffset;
                this.pieceSize = pieceSize;
                this.port = port;
                pieceBytesArr = new byte[pieceSize];
                downloadTask = new Task(() =>{pieceDownload();});
            }
            //file piece download
            private void pieceDownload()
           {
                try
                {
                    //connect to other peer
                    TcpClient tcpClient = new TcpClient(ip, port);
                    NetworkStream networkStream = tcpClient.GetStream();
                    StreamWriter streamWriter = new StreamWriter(networkStream);

                    //new file request object send as json to uploader peer
                    FileRequest req = new FileRequest(this.fileName, this.fileSize, this.fileOffset, this.pieceSize);
                    streamWriter.WriteLine((JsonConvert.SerializeObject(req)));
                    streamWriter.Flush();

                    //read bits sent from uploader peer
                    int byteReciveAmount = 0;

                    //add to bytes array the bits
                    byteReciveAmount += networkStream.Read(this.pieceBytesArr, 0, this.pieceBytesArr.Length);     
                    while (byteReciveAmount < this.pieceBytesArr.Length)
                        networkStream.Read(this.pieceBytesArr, 0, this.pieceBytesArr.Length);

                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
