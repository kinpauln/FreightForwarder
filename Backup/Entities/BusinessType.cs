using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService{
    [Table(Name = "BusinessType")]
    [Serializable]
    [DataContract]
    public class BusinessType
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public string Type { get; set; }
        [Column]
        [DataMember]
        public string Description { get; set; }
    }
}
