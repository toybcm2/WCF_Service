using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SchedngoService;
using SchedngoService.Models;

namespace SchedngoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISchedService
    {
        // TODO: Add your service operations here
        [OperationContract]
        Users CreateUser(string FirstName, string LastName, string Phone, string Email, string Address, string UserName, string Hash);
        [OperationContract]
        string LoginCheck(string Email, string Hash);
        [OperationContract]
        string AddUserToMeeting(string FirstName, string LastName, int TaskID, string Email);
        [OperationContract]
        string CancelMeeting(int MeetingID);
        [OperationContract]
        List<Users> GetAllUsersInvitedToMeeting(int TaskID);
        [OperationContract]
        List<Contacts> GetContactInfo(int ClientId);
        [OperationContract]
        List<Tasks> GetMeetingInfoForUser(int ClientID);
        [OperationContract]
        Tasks GetSpecificMeetingInfo(int TaskID);
        [OperationContract]
        Users GetUser(string Email);
        [OperationContract]
        string InsertContact(int ClientID, string FirstName, string LastName, string Phone, string Email, string Address);
        [OperationContract]
        string InsertTask(int ClientID, string TypeName, DateTime Time, string Address, string TaskName);
        [OperationContract]
        string RemoveUserFromMeeting(int MeetingID, int ClientID);
        [OperationContract]
        string UpdateUser(int ClientID, string Phone, string Address, string UserName, byte[] Avatar);
        [OperationContract]
        string UpdatePassword(int ClientID, string Hash);

    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}
