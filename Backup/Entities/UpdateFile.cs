using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService {
    [Table(Name = "UpdateFiles")]
    [Serializable]
    [DataContract]
    public class UpdateFile {
        [Column(IsPrimaryKey = true)]
        [DataMember]
        public string FileVersion { get; set; }
        [Column]
        [DataMember]
        public string FileName { get; set; }
        [Column]
        [DataMember]
        public byte[] FileBytes { get; set; }
        [Column]
        [DataMember]
        public DateTime PostTime { get; set; }
        [Column(IsVersion = true, IsDbGenerated = true)]
        [DataMember]
        public byte[] RowVersion { get; set; }
    }
}
