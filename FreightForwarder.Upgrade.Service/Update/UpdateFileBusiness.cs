using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Web.Configuration;
using System.Transactions;
using FreightForwarder.Domain.Entities;
using FreightForwarder.Business;

namespace FreightForwarder.Upgrade.Service
{
    public static class UpgradePackageBusiness
    {
        public static IEnumerable<UpgradePackage> Select(int? startRowIndex, int? maximumRows)
        {
            return (new ServerBusinesses()).GetUpgradePackages();
        }

        public static int SelectCount()
        {
            return (new ServerBusinesses()).GetUpgradePackageCount();
        }

        public static void Insert(UpgradePackage UpgradePackage)
        {
            (new ServerBusinesses()).AddUpgradePackage(UpgradePackage);
        }

        public static void Delete(UpgradePackage UpgradePackage)
        {
            using (DataContext dc = new DataContext(
                WebConfigurationManager.ConnectionStrings["FFDB"].ConnectionString))
            {
                UpgradePackage fileToDelete = dc.GetTable<UpgradePackage>().Where(k => k.FileVersion == UpgradePackage.FileVersion).FirstOrDefault();
                if (fileToDelete != default(UpgradePackage))
                {
                    dc.GetTable<UpgradePackage>().DeleteOnSubmit(fileToDelete);

                    using (TransactionScope ts = new TransactionScope())
                    {
                        dc.SubmitChanges();
                        ts.Complete();
                    }
                }
            }
        }
    }
}
