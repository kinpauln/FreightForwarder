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
    [ServiceContract]
    public interface IUpdateService
    {
        [OperationContract]
        IEnumerable<string> CheckUpdate(string version);

        [OperationContract]
        UpgradePackage GetUpdate(string version);
    }
}
