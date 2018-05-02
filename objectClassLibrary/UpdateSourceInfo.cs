using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objectClassLibrary
{
    public class UpdateSourceInfo
    {
        private string username;
        private string filename;
        private string ip;
        private int filesize;

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }

        public string Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }

        public int Filesize
        {
            get
            {
                return filesize;
            }

            set
            {
                filesize = value;
            }
        }

        public UpdateSourceInfo(string username, string filename, string ip, int filesize)
        {
            this.Username = username;
            this.Filename = filename;
            this.Ip = ip;
            this.Filesize = filesize;
        }
    }
}
