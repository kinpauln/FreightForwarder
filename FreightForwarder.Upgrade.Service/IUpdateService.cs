using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FreightForwarder.Upgrade.Service {
    // 注意: 如果更改此处的接口名称 "IService1"，也必须更新 Web.config 中对 "IService1" 的引用。
    [ServiceContract]
    public interface IUpdateService {
        [OperationContract]
        IEnumerable<string> CheckUpdate(string version);

        [OperationContract]
        UpgradePackage GetUpdate(string version);
    }
}
