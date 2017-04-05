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
        Users CreateUser(string FirstName, string LastName, string Phone, string Email, string Address, string UserName, string Hash);
        void AddUserToMeeting(string FirstName, string LastName, int TaskID);
        void CancelMeeting(int MeetingID);
        void DeleteMeeting(int ClientID, int TaskID);
        void GetAllUsersInvitedToMeeting(int TaskID);
        void GetContactInfo(int ClientId);
        void GetMeetingInfoForUser(int ClientID);
        void GetSpecificMeetingInfo(int TaskID);
        void GetUser(int ClientID);
        void InsertContact(int ClientID, string FirstName, string LastName, string Phone, string Email, string Address);
        void InsertTask(int ClientID, string TypeName, DateTime Time, string Address, string TaskName);
        void RemoveUserFromMeeting(int MeetingID, int ClientID);
        void UpdateClient(int ClientID, string Phone, string Address, string UserName);
        void UpdatePassword(int ClientID);

    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}
