using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService{

    [Table(Name = "dbo.TerminalReapplyBusiness")]
    public class TerminalReapplyBusinessEntity
    {
        [Column(AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(DbType = "Int")]
        public System.Nullable<int> BusinessType { get; set; }

        [Column(DbType = "Int")]
        public System.Nullable<int> State { get; set; }

        [Column(DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string LinkTel { get; set; }

        [Column(DbType = "VarChar(50)")]
        public string IdCardNo { get; set; }

        [Column(DbType = "VarChar(50)")]
        public string IdCardName { get; set; }

        [Column(DbType = "VarChar(10)")]
        public string IdCardNation { get; set; }

        [Column(DbType = "Bit")]
        public System.Nullable<bool> IdCardSex { get; set; }

        [Column(DbType = "DateTime")]
        public System.Nullable<System.DateTime> IdCardBirth { get; set; }

        [Column(DbType = "VarChar(500)")]
        public string IdCardAddr { get; set; }

        [Column(DbType = "Image", UpdateCheck = UpdateCheck.Never)]
        public System.Data.Linq.Binary IdCardPhoto { get; set; }

        [Column(DbType = "VarChar(200)")]
        public string IdCardIssuedBy { get; set; }

        [Column(DbType = "DateTime")]
        public System.Nullable<System.DateTime> IdCardIssuedDate { get; set; }

        [Column(DbType = "VarChar(50)")]
        public string IdCardValidPeriod { get; set; }

        [Column(DbType = "VarChar(50)")]
        public string MachineGuid { get; set; }

        [Column(DbType = "DateTime")]
        public System.Nullable<System.DateTime> EntryTime { get; set; }

        [Column]
        public string Remark { get; set; }
        
        [Column(DbType = "DateTime")]
        public System.Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
