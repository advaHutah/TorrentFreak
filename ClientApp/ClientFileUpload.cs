using Newtonsoft.Json;
using objectClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace ClientApp
{
    public delegate void UploadEvent(UploadInfo info, bool isFinished);
    class ClientFileUpload
    {
        public UploadEvent uploadEventSchedule;

        private string uploadFolderPath;
        private int port;
        private TcpListener listener;



        public void StartUploadsListener(string uploadFolderPath, int port)
        {
            this.port = port;
            this.uploadFolderPath = uploadFolderPath;
            listener = new TcpListener(IPAddress.Any, port);
            listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            //start listening to peers downloads requsts
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Factory.StartNew(() => UploadFile(client));
            }

        }

        public void Stop()
        {
            listener.Stop();
        }

        private void UploadFile(TcpClient client)
        {

            NetworkStream networkStream = client.GetStream();
            StreamReader streamReader = new StreamReader(networkStream);
            //get the file requset as json from downloader
            FileRequest req = JsonConvert.DeserializeObject<FileRequest>(streamReader.ReadLine());
          

            //updating ui listeners
            UploadInfo uploadInfo = new UploadInfo(req.FileName, long.Parse(req.FileSize.ToString()), "Uploading");
            uploadEventSchedule(uploadInfo, false);

            //bytes array to be filled with bits requsted from downloading peer
            byte[] bytesToUplaod = new byte[int.Parse(req.PieceSize.ToString())];
            string fullUploadPath = uploadFolderPath + "\\" + req.FileName;

            bool finishedReading = false;
            //reading the bits from the file that the peer requsted
            while (!finishedReading)
            {
                try
                {
                    using (BinaryReader binaryReader = new BinaryReader(new FileStream(fullUploadPath, FileMode.Open)))
                    {
                        binaryReader.BaseStream.Seek(long.Parse(req.FileOffset.ToString()), SeekOrigin.Begin);
                        binaryReader.Read(bytesToUplaod, 0, bytesToUplaod.Length);
                    }
                    finishedReading = true;
                    //writing bits to stream - sending to the request peer
                    networkStream.Write(bytesToUplaod, 0, bytesToUplaod.Length);
                }
                catch 
                {
                    finishedReading = false;
                }
            }

            uploadInfo.Status = "Finished Uploading";
            uploadEventSchedule(uploadInfo, true);
        }
    }
}
