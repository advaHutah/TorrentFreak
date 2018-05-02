using DatabaseLibrary;
using MediationServerSrevices;
using Newtonsoft.Json;
using objectClassLibrary;
using System.Threading.Tasks;

namespace MediationServerServices
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class SignInService : ISignInService
    {
        
        public async Task<bool> ClientSignInAsync(string msg)
        {
            return await Task.Factory.StartNew(()=>checkUser(msg));
        }


        public bool checkUser(string msg)
        {
            UserInfo u = JsonConvert.DeserializeObject<UserInfo>(msg);
            //check user in DB
            return UsersDBClass.Instance.AccountSearchAndConnectOrDisconnect(u,true);
           
        }

    }
}
