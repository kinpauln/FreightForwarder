using FreightForwarder.Common;
using FreightForwarder.Data;
using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Business
{
    public class PackageBusinesses
    {
        public bool DeletePackage(int pid)
        {
            return (new DBHelper()).DeletePackage(pid);
        }

        public IEnumerable<string> CheckUpdate(string version)
        {
            return (new DBHelper()).CheckUpdate(version);
        }

        public UpgradePackage GetUpdate(string version)
        {
            return (new DBHelper()).GetUpdate(version);
        }

        public int GetUpgradePackageCount()
        {
            return (new DBHelper()).GetUpgradePackageCount();
        }

        public bool AddUpgradePackage(UpgradePackage entity)
        {
            return (new DBHelper()).AddUpgradePackage(entity);
        }

        public IEnumerable<UpgradePackage> GetUpgradePackages()
        {
            return (new DBHelper()).GetUpgradePackages();
        }
    }
}
