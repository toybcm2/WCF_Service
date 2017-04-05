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
        public Users CreateUser(string FirstName, string LastName, string Phone, string Email, string Address, string UserName, string Hash)
        {
            Users newUser = new Users();
            newUser.FirstName = FirstName;
            newUser.LastName = LastName;
            newUser.UserName = UserName;
            int clientReturn = context.InsertUser(FirstName, LastName, Email, Phone, Address, null, UserName, Hash);
            newUser.ClientID = clientReturn;
            return newUser;
        }
        public void AddUserToMeeting(string FirstName, string LastName, int TaskID)
        {

        }
        public void CancelMeeting(int MeetingID)
        {

        }
        public void DeleteMeeting(int ClientID, int TaskID)
        {

        }
        public void GetAllUsersInvitedToMeeting(int TaskID)
        {

        }
        public void GetContactInfo(int ClientId)
        {

        }
        public void GetMeetingInfoForUser(int ClientID)
        {

        }
        public void GetSpecificMeetingInfo(int TaskID)
        {

        }
        public void GetUser(int ClientID)
        {

        }
        public void InsertContact(int ClientID, string FirstName, string LastName, string Phone, string Email, string Address)
        {

        }
        public void InsertTask(int ClientID, string TypeName, DateTime Time, string Address, string TaskName)
        {

        }
        public void RemoveUserFromMeeting(int MeetingID, int ClientID)
        {

        }
        public void UpdateClient(int ClientID, string Phone, string Address, string UserName)
        {

        }
        public void UpdatePassword(int ClientID)
        {

        }
    }
}
