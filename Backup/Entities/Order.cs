using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "Order")]
    [Serializable]
    [DataContract]
    public class Order
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public string State { get; set; }
        [Column]
        [DataMember]
        public int BusinessType { get; set; }
        [Column]
        [DataMember]
        public int BusinessId { get; set; }
        [Column(UpdateCheck = UpdateCheck.Never, IsDbGenerated = true)]
        [DataMember]
        public DateTime? Entry { get; set; }
        [Column]
        [DataMember]
        public string BarCode { get; set; }
        [Column]
        [DataMember]
        public bool FileCorrect { get; set; }
        [Column]
        [DataMember]
        public bool ImageExists { get; set; }
        [Column]
        [DataMember]
        public bool ApplyDenial { get; set; }
        [Column]
        [DataMember]
        public bool Complete { get; set; }
        [Column]
        [DataMember]
        public bool Failed { get; set; }
        [Column]
        [DataMember]
        public bool ImageReload { get; set; }
    }
}
