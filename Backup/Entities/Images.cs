using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "Images")]
    [Serializable]
    [DataContract]
    public class Images
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public string BarCode { get; set; }
        [Column]
        [DataMember]
        public string FileName { get; set; }
        [Column]
        [DataMember]
        public string FileType { get; set; }
        [Column]
        [DataMember]
        public int FileSize { get; set; }
        [Column]
        [DataMember]
        public byte[] Image { get; set; }
        [Column(IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        [DataMember]
        public DateTime? Entry { get; set; }
    }
}
