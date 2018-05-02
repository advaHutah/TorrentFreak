using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MediationServerSrevices
{
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
       Task<string> ClientFileRequestAsync(string msg);
        [OperationContract]
        void UpdateFileSourceAsync(string msg);

        // TODO: Add your service operations here

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "MediationServer.ContractType".

}
