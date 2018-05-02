using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objectClassLibrary
{
    public class DownloadInfo
    {
        private string fileName;
        private long size;
        private int sourceNum;
        private string status;
        private string time;
        private int kbps =0;

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

        public long Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public int SourceNum
        {
            get
            {
                return sourceNum;
            }

            set
            {
                sourceNum = value;
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

        public string Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }

        public int Kbps
        {
            get
            {
                return kbps;
            }

            set
            {
                kbps = value;
            }
        }

        public DownloadInfo(string fileName, long size, int sourceNum, string status, string time)
        {
            this.FileName = fileName;
            this.Size = size;
            this.SourceNum = sourceNum;
            this.Status = status;
            this.Time = time;
        }
    }
}
