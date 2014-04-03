using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "Cause")]
    [Serializable]
    [DataContract]
    public class Cause
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public int OrderId { get; set; }
        [Column(UpdateCheck = UpdateCheck.Never)]
        [DataMember]
        public string Memo { get; set; }
        [Column(IsDbGenerated = true)]
        [DataMember]
        public DateTime? Entry { get; set; }
    }
}
