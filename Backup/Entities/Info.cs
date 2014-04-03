using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "Info")]
    [Serializable]
    [DataContract]
    public class Info
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column(UpdateCheck = UpdateCheck.Never)]
        [DataMember]
        public string Notice { get; set; }
        [Column(IsDbGenerated = true)]
        [DataMember]
        public DateTime? Entry { get; set; }
    }
}
