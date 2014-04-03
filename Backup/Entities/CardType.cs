using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "CardType")]
    [Serializable]
    [DataContract]
    public class CardType
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public string Code { get; set; }
        [Column]
        [DataMember]
        public string Name { get; set; }
    }
}
