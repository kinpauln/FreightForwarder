using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "dbo.CustomerServiceWorkList")]
    [DataContract]
    public class CustomerServiceWorkListEntity
    {

        [Column(AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        [DataMember]
        public int Id { get; set; }

        [Column(DbType = "Int")]
        [DataMember]
        public System.Nullable<int> UserId { get; set; }

        [Column(DbType = "Int")]
        [DataMember]
        public System.Nullable<int> BusinessId { get; set; }

        [Column(DbType = "DateTime")]
        [DataMember]
        public System.Nullable<DateTime> EntryTime { get; set; }
    }
}
