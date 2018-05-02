using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objectClassLibrary
{
    public class FileRequest
    {
        private string fileName;
        private int fileSize;
        private int fileOffset;
        private int pieceSize;

        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
            }
        }

        public int FileSize
        {
            get
            {
                return fileSize;
            }

            set
            {
                fileSize = value;
            }
        }

        public int FileOffset
        {
            get
            {
                return fileOffset;
            }

            set
            {
                fileOffset = value;
            }
        }

        public int PieceSize
        {
            get
            {
                return pieceSize;
            }

            set
            {
                pieceSize = value;
            }
        }

        public FileRequest(string fileName, int fileSize, int fileOffset, int pieceSize)
        {
            this.FileName = fileName;
            this.FileSize = fileSize;
            this.FileOffset = fileOffset;
            this.PieceSize = pieceSize;
        }
    }
}
