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
            bool result = (new PackageBusinesses()).DeletePackage(UpgradePackage.Id);
        }
    }
}
