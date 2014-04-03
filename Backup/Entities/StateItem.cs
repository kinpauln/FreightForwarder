using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "State")]
    [Serializable]
    [DataContract]
    public class StateItem
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public int State { get; set; }
        [Column]
        [DataMember]
        public string Per { get; set; }
        [Column]
        [DataMember]
        public string Description { get; set; }
    }
}
