using objectClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    public class FilesDBClass
    {
        public static readonly FilesDBClass instance = new FilesDBClass();
        public static FilesDBClass Instance { get { return instance; } }
        public string myConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\afeka\\Desktop\\TorrentFreakLiranAndAdva\\DatabaseLibrary\\Database.mdf; Integrated Security = True; Connect Timeout = 30";


        private FilesDBClass() { }

        public void AddUserFileList(UserInfo userInfo)
        {
            using (TorrentFreakDBLinkDataContext db = new TorrentFreakDBLinkDataContext())
            {
                db.Connection.ConnectionString = myConnectionString;
                foreach (KeyValuePair<string, long> file in userInfo.FileDict)
                {
                    File f = new File();
                    f.FileName = file.Key;
                    f.FileSize = file.Value;
                    f.IP = userInfo.Ip;
                    f.UserName = userInfo.UserName;
                    db.Files.InsertOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }
        public void AddFileSource(UpdateSourceInfo info)
        {
            using (TorrentFreakDBLinkDataContext db = new TorrentFreakDBLinkDataContext())
            {
                db.Connection.ConnectionString = myConnectionString;

                File f = new File();
                f.FileName = info.Filename;
                f.FileSize = info.Filesize;
                f.IP = info.Ip;
                f.UserName = info.Username;
                db.Files.InsertOnSubmit(f);
                db.SubmitChanges();

            }

        }

        public void RemoveUserFileList(UserInfo userInfo)
        {
            using (TorrentFreakDBLinkDataContext db = new TorrentFreakDBLinkDataContext())
            {
                db.Connection.ConnectionString = myConnectionString;

                var files = from file in db.Files
                            where file.UserName == userInfo.UserName
                            select file;

                foreach (var file in files)
                {
                    db.Files.DeleteOnSubmit(file);
                    db.SubmitChanges();
                }
            }

        }

        public List<SearchResult> findListOfFilesByFileName(string fileName)
        {
            using (TorrentFreakDBLinkDataContext db = new TorrentFreakDBLinkDataContext())
            {
                db.Connection.ConnectionString = myConnectionString;
                var files = from f in db.Files
                            where f.FileName == fileName
                            select f;

                var sizes = (from f in db.Files
                             where f.FileName == fileName
                             select f.FileSize).Distinct();


                List<SearchResult> answer = new List<SearchResult>();

                foreach (var size in sizes)
                {
                    List<String> ips = new List<string>();
                    foreach (var file in files)
                    {
                        if (file.FileSize == size)
                        {
                            ips.Add(file.IP);
                        }
                    }
                    answer.Add(new SearchResult(fileName, size, ips));

                }
                return answer;
            }

        }
    }
}

