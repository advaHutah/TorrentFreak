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

    public class FileService : IFileService
    {


        public string findListOfFilesByFileName(string fileName)
        {
            List<SearchResult> result = FilesDBClass.Instance.findListOfFilesByFileName(fileName);
                if (result.Count() != 0)
                    return JsonConvert.SerializeObject(result);
                return null;

            
        }

        public async Task<string> ClientFileRequestAsync(string msg)
        {

                string fileName = JsonConvert.DeserializeObject<string>(msg);
                return await Task.Factory.StartNew(() => findListOfFilesByFileName(fileName));
        }

        public async void UpdateFileSourceAsync(string msg)
        {
            UpdateSourceInfo info = JsonConvert.DeserializeObject<UpdateSourceInfo>(msg);
             await Task.Factory.StartNew(() => updateUserAsFileSource(info));

        }
        public void updateUserAsFileSource(UpdateSourceInfo info)
        {
            FilesDBClass.Instance.AddFileSource(info); 
        }
    }
}
