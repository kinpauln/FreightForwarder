using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService{
    [Table(Name = "UserBelong")]
    [Serializable]
    [DataContract]
    public class UserBelong
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public int UserId { get; set; }
        [Column]
        [DataMember]
        public string BusName { get; set; }
    }
}
