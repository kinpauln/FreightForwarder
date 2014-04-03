using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "Steer")]
    [Serializable]
    [DataContract]
    public class Steer
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public int IndentType { get; set; }
        [Column]
        [DataMember]
        public string IndentNo { get; set; }
        [Column]
        [DataMember]
        public string FileId { get; set; }
        [Column]
        [DataMember]
        public string ApplyName { get; set; }
        [Column]
        [DataMember]
        public string IdCard { get; set; }
        [Column]
        [DataMember]
        public string Tel { get; set; }
        [Column]
        [DataMember]
        public string OfficeTel { get; set; }
        [Column]
        [DataMember]
        public string FamilyTel { get; set; }
        [Column]
        [DataMember]
        public string Mail { get; set; }
        [Column]
        [DataMember]
        public string Code { get; set; }
        [Column]
        [DataMember]
        public string Addr { get; set; }
        [Column(DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        [DataMember]
        public string Cause { get; set; }
        [Column]
        [DataMember]
        public string CardInnerCode { get; set; }
    }
}
