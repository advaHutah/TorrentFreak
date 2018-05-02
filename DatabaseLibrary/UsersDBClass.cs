using objectClassLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    public class UsersDBClass
    {
        private static readonly UsersDBClass instance = new UsersDBClass();
        public static UsersDBClass Instance { get { return instance; } }
        public string myConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\afeka\\Desktop\\TorrentFreakLiranAndAdva\\DatabaseLibrary\\Database.mdf; Integrated Security = True; Connect Timeout = 30";


        private UsersDBClass() { }

        public bool AccountSearchAndConnectOrDisconnect(UserInfo userInfo, bool isConnect)
        {
            if (AccountSearch(userInfo))
            {
                if (isConnect)
                    ConnectUser(userInfo);
                else
                    DisconnectUser(userInfo);

                return true;
            }
            else return false;
        }

        public bool AccountSearch(UserInfo userInfo)
        {
            using (TorrentFreakDBLinkDataContext db = new TorrentFreakDBLinkDataContext())
            {
                db.Connection.ConnectionString = myConnectionString;

                var userAccount = from users in db.Users
                                  where users.Username == userInfo.UserName
                                  select users;
                if (userAccount.Count() == 0)
                    return false;
                else
                    return true;
            }
        }

        public void ConnectUser(UserInfo userInfo)
        {

            using (TorrentFreakDBLinkDataContext db = new TorrentFreakDBLinkDataContext())
            {
                db.Connection.ConnectionString = myConnectionString;
                var user = db.Users.Single(u => u.Username == userInfo.UserName);
                user.isConnected = true;
                db.SubmitChanges();
            }
            FilesDBClass.Instance.AddUserFileList(userInfo);
        }


        public void DisconnectUser(UserInfo userInfo)
        {
            using (TorrentFreakDBLinkDataContext db = new TorrentFreakDBLinkDataContext())
            {
                db.Connection.ConnectionString = myConnectionString;
                var user = db.Users.Single(u => u.Username == userInfo.UserName);
                user.isConnected = false;
                db.SubmitChanges();
            }
            RemoveUserFiles(userInfo);
        }

        public void RemoveUserFiles(UserInfo userInfo)
        {
            using (TorrentFreakDBLinkDataContext db = new TorrentFreakDBLinkDataContext())
            {
                db.Connection.ConnectionString = myConnectionString;
                var files = from f in db.Files
                            where f.UserName == userInfo.UserName
                            select f;

                foreach (var item in files)
                {
                    db.Files.DeleteOnSubmit(item);
                    db.SubmitChanges();
                }
            }
        }

        public int getNumberOfUsers(bool isConnected)
        {
            using (TorrentFreakDBLinkDataContext db = new TorrentFreakDBLinkDataContext())
            {
                db.Connection.ConnectionString = myConnectionString;
                if (isConnected)
                {
                    var activeUserAccount = from u in db.Users
                                            where u.isConnected == true
                                            select u;
                    return activeUserAccount.Count();
                }
                else
                {
                    var userAccount = from u in db.Users
                                      select u;
                    return userAccount.Count();
                }

            }
        }

        public bool addNewUser(string userName, string password)
        {
            UserInfo newUser = new UserInfo(userName, password);

            if (!AccountSearch(newUser))
            {

                using (TorrentFreakDBLinkDataContext db = new TorrentFreakDBLinkDataContext())
                {
                    db.Connection.ConnectionString = myConnectionString;

                    User user = new User();
                    user.Username = userName;
                    user.Password = password;
                    user.isConnected = false;

                    db.Users.InsertOnSubmit(user);
                    db.SubmitChanges();
                }
                return true;
            }
            else
                return false;
        }

    }


}
