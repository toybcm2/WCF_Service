﻿using System;
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
        public int OrganiserID { get; set; }
        [DataMember]
        public DateTime TaskTime { get; set; }
        [DataMember]
        public string TaskAddress { get; set; }
        [DataMember]
        public string TaskName { get; set; }
        [DataMember]
        public Boolean Cancelled { get; set; }

    }
}