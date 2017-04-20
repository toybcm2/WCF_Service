using SchedngoService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SchedngoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SchedService : ISchedService
    {
        private const string initVector = "pemgail9uzpgzl88";
        private const string plainText = "DragonQuest8";
        private const int keysize = 256;
        SchedngoEntities context = new SchedngoEntities();
        public Users CreateUser(string FirstName, string LastName, string Phone, string Email, string Address, string UserName, string Hash)
        {
            int? rawResult;
            Users newUser = new Users();
            newUser.FirstName = FirstName;
            newUser.LastName = LastName;
            newUser.UserName = UserName;
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(Hash, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            Hash = Convert.ToBase64String(cipherTextBytes);
            rawResult = context.InsertUser(FirstName, LastName, Email, Phone, Address, null, UserName, Hash).FirstOrDefault();
            newUser.ClientID = Convert.ToInt32(rawResult);
            if(newUser.ClientID == 0)
            {
                newUser.Error = "Email Address Already in Use.";
            }
            return newUser;
        }
        public string LoginCheck(string Email, string Hash)
        {
            string result;
            string dbHash;
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(Hash, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            Hash = Convert.ToBase64String(cipherTextBytes);
            dbHash = context.LoginCheck(Email).FirstOrDefault();

            if(dbHash == Hash)
            {
                result = "Login Successful";
            }
            else
            {
                result = "Login Failed";
            }
            return result;
        }
        public string AddUserToMeeting(string FirstName, string LastName, int TaskID, string Email)
        {
            string Error = "";
            try
            {
                context.AddUserToMeeting(FirstName, LastName, TaskID, Email);
                return Error;
            }
            catch (Exception e)
            {
                if(e.InnerException.ToString().StartsWith("Cannot insert the value NULL into column 'ClientID'"))
                {
                    Error = "User doesn't exist.";
                }
                return Error;
            }
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
                    TaskID = task.TaskID,
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
                result.Topic = rawresult.Topic;
                result.ChatID = rawresult.ChatID;
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
        public string InsertTask(int ClientID, string TypeName, DateTime Time, string Address, string TaskName, string ChatID, string Topic)
        {
            string Error = "";
            try
            {
                context.InsertTask(ClientID, TypeName, Time, Address, TaskName, Topic, ChatID);
                Error = "Successful";
            }
            catch(Exception e)
            {
                Error = e.InnerException.ToString();
            }
            
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
