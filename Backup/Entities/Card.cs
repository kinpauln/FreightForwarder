using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "Card")]
    [Serializable]
    [DataContract]
    public class Card
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id { get; set; }
        [Column]
        [DataMember]
        public string Type { get; set; }
        [Column]
        [DataMember]
        public string CardNo { get; set; }
        [Column]
        [DataMember]
        public string ViewNo { get; set; }
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
    }
}
