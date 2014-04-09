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
            try
            {
                return (new PackageBusinesses()).GetUpgradePackages();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SelectCount()
        {
            try
            {
                return (new PackageBusinesses()).GetUpgradePackageCount();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public static void Insert(UpgradePackage UpgradePackage)
        {
            try
            {
                (new PackageBusinesses()).AddUpgradePackage(UpgradePackage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Delete(UpgradePackage UpgradePackage)
        {
            try
            {
                bool result = (new PackageBusinesses()).DeletePackage(UpgradePackage.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
