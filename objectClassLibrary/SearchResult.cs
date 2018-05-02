using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objectClassLibrary
{
   public class SearchResult
    {
        string fileName;
        long fileSize;
        List<string> sourcesIps;

        public SearchResult(string fileName, long fileSize, List<string> sourcesIps)
        {
            this.FileName = fileName;
            this.FileSize = fileSize;
            this.SourcesIps = sourcesIps;
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

        public List<string> SourcesIps
        {
            get
            {
                return sourcesIps;
            }

            set
            {
                sourcesIps = value;
            }
        }
    }
}
