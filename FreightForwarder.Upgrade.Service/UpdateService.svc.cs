using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FreightForwarder.Upgrade.Service
{
    public class UpdateService : IUpdateService
    {
        public IEnumerable<string> CheckUpdate(string version)
        {
            return null;
            //using (DataContext dc = new DataContext(
            //    WebConfigurationManager.ConnectionStrings["FFDB"].ConnectionString))
            //{
            //    return (from file in dc.GetTable<UpgradePackage>()
            //            where file.FileVersion.CompareTo(version) > 0
            //            select file.FileVersion).ToList();
            //}
        }

        public UpgradePackage GetUpdate(string version)
        {
            return null;
            //using (DataContext dc = new DataContext(
            //    WebConfigurationManager.ConnectionStrings["FFDB"].ConnectionString))
            //{
            //    return (from file in dc.GetTable<UpgradePackage>()
            //            where file.FileVersion == version
            //            select file).FirstOrDefault();
            //}
        }
    }
}
