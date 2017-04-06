using SchedngoService.Models;
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
            int? rawResult;
            Users newUser = new Users();
            newUser.FirstName = FirstName;
            newUser.LastName = LastName;
            newUser.UserName = UserName;
            rawResult = context.InsertUser(FirstName, LastName, Email, Phone, Address, null, UserName, Hash).FirstOrDefault();
            newUser.ClientID = Convert.ToInt32(rawResult);
            return newUser;
        }
        public Users LoginCheck(string Email)
        {
            string dbHash;
            dbHash = context.LoginCheck(Email).FirstOrDefault();
            return new Users();
        }
        public string AddUserToMeeting(string FirstName, string LastName, int TaskID, string Email)
        {
            string Error = "";
            context.AddUserToMeeting(FirstName, LastName, TaskID, Email);
            return Error;
        }
        public string CancelMeeting(int MeetingID)
        {
            string Error = "";
            context.CancelMeeting(MeetingID);
            return Error;
        }
        public List<Users> GetAllUsersInvitedToMeeting(int TaskID)
        {
            List<GetAllUsersInvitedToMeeting_Result> rawResult = new List<GetAllUsersInvitedToMeeting_Result>();
            List<Users> result = new List<Users>();
            rawResult = context.GetAllUsersInvitedToMeeting(TaskID).ToList();
            foreach (var person in rawResult)
            {
                result.Add(new Users
                {
                    ClientID = person.ClientID,
                    FirstName = person.FirstName,
                    LastName = person.LastName
                });
            }

            return result;

        }
        public List<Contacts> GetContactInfo(int ClientId)
        {
            List<GetContactInfo_Result> rawResult = new List<GetContactInfo_Result>();
            List<Contacts> result = new List<Contacts>();
            rawResult = context.GetContactInfo(ClientId).ToList();

            foreach (var contact in rawResult)
            {
                result.Add(new Contacts
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Address = contact.Address,
                    Phone = contact.Phone,
                    Email = contact.Email
                });
            }

            return result;
        }
        public List<Tasks> GetMeetingInfoForUser(int ClientID)
        {
            List<GetMeetingInfoForUser_Result> rawResult = new List<GetMeetingInfoForUser_Result>();
            List<Tasks> result = new List<Tasks>();
            rawResult = context.GetMeetingInfoForUser(ClientID).ToList();

            foreach(var task in rawResult)
            {
                result.Add(new Tasks
                {
                    TaskName = task.TaskName,
                    TypeName = task.Name,
                    TaskTime = task.TaskTime,
                    Cancelled = task.Cancelled
                });
            }

            return result;
        }
        public Tasks GetSpecificMeetingInfo(int TaskID)
        {
            Tasks result = new Tasks();
            GetSpecficMeetingInfo_Result rawresult;
            try
            {
                rawresult = context.GetSpecficMeetingInfo(TaskID).FirstOrDefault();

                result.TaskID = rawresult.TaskID;
                result.TaskName = rawresult.TaskName;
                result.TaskTime = rawresult.TaskTime;
                result.TaskAddress = rawresult.TaskAddress;
                result.Cancelled = rawresult.Cancelled;
                result.OrganizerFirstName = rawresult.FirstName;
                result.OrganizerLastName = rawresult.LastName;
                result.TypeName = rawresult.Name;
                result.TaskID = rawresult.TaskID;
            }
            catch (Exception e)
            {
                if(e.Message == "Object reference not set to an instance of an object.")
                {
                    result.Error = "No Meeting Found";
                }
            }
            return result;
        }
        public Users GetUser(string Email)
        {
            Users result = new Users();
            GetUser_Result rawResult = new GetUser_Result();
            try
            {
                rawResult = context.GetUser(Email).FirstOrDefault();

                result.ClientID = rawResult.ClientID;
                result.FirstName = rawResult.FirstName;
                result.LastName = rawResult.LastName;
                result.Phone = rawResult.Phone;
                result.Email = rawResult.Email;
                result.Address = rawResult.Address;
                result.UserName = rawResult.UserName;
                result.Avatar = rawResult.Avatar;
            }
            catch(Exception e)
            {
                if (e.Message == "Object reference not set to an instance of an object.")
                {
                    result.Error = "User not Found";
                    return result;
                }
            }

            return result;
            
        }
        public string InsertContact(int ClientID, string FirstName, string LastName, string Phone, string Email, string Address)
        {
            string Error = "";
            context.InsertContact(ClientID, FirstName, LastName, Phone, Email, Address);
            return Error;
        }
        public string InsertTask(int ClientID, string TypeName, DateTime Time, string Address, string TaskName)
        {
            string Error = "";
            context.InsertTask(ClientID, TypeName, Time, Address, TaskName);
            return Error;
        }
        public string RemoveUserFromMeeting(int MeetingID, int ClientID)
        {
            string Error = "";
            context.RemoveUserFromMeeting(MeetingID, ClientID);
            return Error;
        }
        public string UpdateUser(int ClientID, string Phone, string Address, string UserName, byte[] Avatar)
        {
            string Error = "";
            try
            {
                context.UpdateUser(ClientID, Phone, Avatar, Address, UserName);
            }
            catch(Exception e)
            {
                Error = "Database Error Please Try Again.";
            }
            return Error;
        }
        public string UpdatePassword(int ClientID, string Hash)
        {
            string Error = "";
            try
            {
                context.UpdatePassword(ClientID, Hash);
            }
            catch(Exception e)
            {
                Error = "Database Error Please Try Again.";
            }
            return Error;
        }
    }
}
