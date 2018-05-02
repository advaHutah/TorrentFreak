using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objectClassLibrary
{
    public class UploadInfo
    {
        private string fileName;
        private long fileSize;
        private string status;

        public UploadInfo(string fileName, long fileSize, string status)
        {
            this.FileName = fileName;
            this.FileSize = fileSize;
            this.Status = status;
        }

        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }

        public long FileSize
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

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}
