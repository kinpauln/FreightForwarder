using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "UserGroup")]
    [Serializable]
    [DataContract]
    public class UserGroup
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public int GroupId { get; set; }
        [Column]
        [DataMember]
        public string GroupName { get; set; }
        [Column]
        [DataMember]
        public string Desc { get; set; }
    }
}
