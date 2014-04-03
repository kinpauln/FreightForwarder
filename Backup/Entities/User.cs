using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService{
    [Table(Name = "Users")]
    [Serializable]
    [DataContract]
    public class User
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int UserId { get; set; }
        [Column]
        [DataMember]
        public string UserName { get; set; }
        [Column]
        [DataMember]
        public string UserPwd { get; set; }
        [Column]
        [DataMember]
        public int GroupId { get; set; }
        [Column]
        [DataMember]
        public int LogTimes { get; set; }
        [Column(UpdateCheck = UpdateCheck.Never)]
        [DataMember]
        public DateTime? LastLogin { get; set; }
    }
}
