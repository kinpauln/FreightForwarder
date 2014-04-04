using FreightForwarder.Business;
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
            return (new PackageBusinesses()).CheckUpdate(version);
        }

        public UpgradePackage GetUpdate(string version)
        {
            return (new PackageBusinesses()).GetUpdate(version);
        }
    }
}
