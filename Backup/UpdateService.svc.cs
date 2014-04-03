﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Linq;
using System.Web.Configuration;

namespace BigzoneBusinessCenterService {
    // 注意: 如果更改此处的类名“Service1”，也必须更新 Web.config 和关联的 .svc 文件中对“Service1”的引用。
    public class UpdateService : IUpdateService {
        public IEnumerable<string> CheckUpdate(string version) {
            using (DataContext dc = new DataContext(
                WebConfigurationManager.ConnectionStrings["TransPad"].ConnectionString))
            {
                return (from file in dc.GetTable<UpdateFile>()
                        where file.FileVersion.CompareTo(version) > 0
                        select file.FileVersion).ToList();
            }
        }

        public UpdateFile GetUpdate(string version) {
            using (DataContext dc = new DataContext(
                WebConfigurationManager.ConnectionStrings["TransPad"].ConnectionString))
            {
                return (from file in dc.GetTable<UpdateFile>()
                        where file.FileVersion == version
                        select file).FirstOrDefault();
            }
        }
    }
}
