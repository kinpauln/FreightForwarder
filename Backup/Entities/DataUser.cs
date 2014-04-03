using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService
{
    [Table(Name = "DataUser")]
    [Serializable]
    [DataContract]
    public class DataUser
    {
        [Column(IsPrimaryKey = true)]
        [DataMember]
        public string UserName { get; set; }
        [Column]
        [DataMember]
        public string UserPwd { get; set; }
    }
}
