using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SchedngoService;

namespace SchedngoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISchedService
    {
        // TODO: Add your service operations here
        [OperationContract]
        Client CreateUser(string FirstName, string LastName, string Phone, string Email, string Address, string UserName, string Hash);
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}
