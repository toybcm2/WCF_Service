using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SchedngoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SchedService : ISchedService
    {
        SchedngoEntities context = new SchedngoEntities();
        public Client CreateUser(string FirstName, string LastName, string Phone, string Email, string Address, string UserName, string Hash)
        {
            Client newUser = new Client();
            newUser.FirstName = FirstName;
            newUser.LastName = LastName;
            newUser.UserName = UserName;
            int clientReturn = context.InsertUser(FirstName, LastName, Email, Phone, Address, null, UserName, Hash);
            newUser.ClientID = clientReturn;
            return newUser;
        }
        public Client FindUser(int ClientID)
        {
            Client foundUser = new Client();
            GetUser_Result found = GetUser_Result();
            found = context.GetUser(ClientID);
            return foundUser;
        }
    }
}
