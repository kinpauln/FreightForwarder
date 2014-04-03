using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "Log")]
    [Serializable]
    [DataContract]
    public class Log
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public int UserId { get; set; }
        [Column]
        [DataMember]
        public int OrderId { get; set; }
        [Column]
        [DataMember]
        public int StartState { get; set; }
        [Column]
        [DataMember]
        public int LastState { get; set; }
        [Column(UpdateCheck = UpdateCheck.Never, IsDbGenerated = true)]
        [DataMember]
        public DateTime? Entry { get; set; }
    }
}
