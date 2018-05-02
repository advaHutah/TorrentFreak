using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace objectClassLibrary
{
    public class UserInfo
    {

        private string userName;
        private string password;
        private string ip;
        private int portIn;
        private int portOut;
        private Dictionary<string, long> fileDict = new Dictionary<string, long>();

        public UserInfo()
        {

        }
        public UserInfo(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
        public UserInfo(string userName, string password, string ip, int portIn, int portOut)
        {
            this.UserName = userName;
            this.Password = password;
            this.Ip = ip;
            this.PortIn = portIn;
            this.PortOut = portOut;
        }
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
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

        public int PortIn
        {
            get
            {
                return portIn;
            }

            set
            {
                portIn = value;
            }
        }

        public int PortOut
        {
            get
            {
                return portOut;
            }

            set
            {
                portOut = value;
            }
        }

        public Dictionary<string, long> FileDict
        {
            get
            {
                return fileDict;
            }

            set
            {
                fileDict = value;
            }
        }

        public void fillMyFiles(string uploadDirPath)
        {
            FileInfo[] myFiles = new DirectoryInfo(uploadDirPath).GetFiles();

            clearMyFiles();
            for (int i = 0; i < myFiles.Length; i++)
            {
                fileDict.Add((myFiles[i].Name),(myFiles[i].Length));
            }
        }
        public void clearMyFiles()
        {
            fileDict.Clear();
        }
    }
}
