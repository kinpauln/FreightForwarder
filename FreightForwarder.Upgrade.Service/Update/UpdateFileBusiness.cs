using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using FreightForwarder.Domain.Entities;
using FreightForwarder.Business;

namespace FreightForwarder.Upgrade.Service
{
    public static class UpgradePackageBusiness
    {
        public static IEnumerable<UpgradePackage> Select(int? startRowIndex, int? maximumRows)
        {
            return (new PackageBusinesses()).GetUpgradePackages();
        }

        public static int SelectCount()
        {
            return (new PackageBusinesses()).GetUpgradePackageCount();
        }

        public static void Insert(UpgradePackage UpgradePackage)
        {
            (new PackageBusinesses()).AddUpgradePackage(UpgradePackage);
        }

        public static void Delete(UpgradePackage UpgradePackage)
        {
            //using (DataContext dc = new DataContext(
            //    WebConfigurationManager.ConnectionStrings["FFDB"].ConnectionString))
            //{
            //    UpgradePackage fileToDelete = dc.GetTable<UpgradePackage>().Where(k => k.FileVersion == UpgradePackage.FileVersion).FirstOrDefault();
            //    if (fileToDelete != default(UpgradePackage))
            //    {
            //        dc.GetTable<UpgradePackage>().DeleteOnSubmit(fileToDelete);

            //        using (TransactionScope ts = new TransactionScope())
            //        {
            //            dc.SubmitChanges();
            //            ts.Complete();
            //        }
            //    }
            //}
        }
    }
}
