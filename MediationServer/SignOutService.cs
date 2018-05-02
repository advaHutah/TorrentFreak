using DatabaseLibrary;
using MediationServerSrevices;
using Newtonsoft.Json;
using objectClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MediationServerServices
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class SignOutService : ISignOutService
    {

        public void SignOutUserAndRemoveAllHisFiles(string msg)
        {
            UserInfo u = JsonConvert.DeserializeObject<UserInfo>(msg);
            //disconnect user and remove his files on DB
             UsersDBClass.Instance.AccountSearchAndConnectOrDisconnect(u,false);
           
        }

        public async Task ClientSignOutAsync(string msg)
        {
             await Task.Factory.StartNew(() => SignOutUserAndRemoveAllHisFiles(msg));
        }
    }
}
