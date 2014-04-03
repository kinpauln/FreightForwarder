using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BigzoneBusinessCenterService{
    public class UserDetailInfo
    {
        public User User { get; set; }
        public List<UserBelong> UserBelongs { get; set; }
        public UserGroup UserGroup { get; set; }
        public List<BusinessType> BusinessType { get; set; }
    }
}
