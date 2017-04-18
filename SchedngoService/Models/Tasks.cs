using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SchedngoService.Models
{
    [DataContract]
    public class Tasks
    {
        [DataMember]
        public int TaskID { get; set; }
        [DataMember]
        public int TypeID { get; set; }
        [DataMember]
        public int OrganizerID { get; set; }
        [DataMember]
        public DateTime TaskTime { get; set; }
        [DataMember]
        public string TaskAddress { get; set; }
        [DataMember]
        public string TaskName { get; set; }
        [DataMember]
        public Boolean Cancelled { get; set; }
        [DataMember]
        public string OrganizerFirstName { get; set; }
        [DataMember]
        public string OrganizerLastName { get; set; }
        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public string Error { get; set; }
        [DataMember]
        public string Topic { get; set; }
        [DataMember]
        public string ChatID { get; set; }
    }
}