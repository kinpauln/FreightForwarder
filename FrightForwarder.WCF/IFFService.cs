using FreightForwarder.Domain.Entities;
using FreightForwarder.MessageCompression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FrightForwarder.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFFService" in both code and config file together.
    [ServiceContract(Namespace = "FrightForwarder")]
    public interface IFFService
    {
        [OperationContract]
        [CompressionOperationBehavior]
        IList<RouteInformationItem> GetRouteInformationItems(int? companyId);

        [OperationContract]
        [CompressionOperationBehavior]
        IEnumerable<Company> GetAllCompanyList();

        [OperationContract]
        [CompressionOperationBehavior]
        Dictionary<string, int> GetAllCompanies();

        [OperationContract]
        [CompressionOperationBehavior]
        bool ImportRouteInformationItems(IList<RouteInformationItem> rlist);

        [OperationContract]
        [CompressionOperationBehavior]
        IList<RouteInformationItem> GetRoutItems(string shipName, string startPort, string destinationPort, bool? isSingleContainer);

        //[OperationContract]
        //byte[] GetRoutItems(string shipName, string startPort, string destinationPort, bool? isSingleContainer);

        [OperationContract]
        bool AssociatMachineAndRegCode(string machineCode, string regcode, int companyId);

        [OperationContract]
        bool AddMachineCode(string machineCode, string description, int companyId);

        [OperationContract]
        RegisterCode IsRegistered(string machineCode);

        [OperationContract]
        bool ValidateMachineCode(string machineCode);

        [OperationContract]
        bool AddCompany(string companyName, string companyCode);
    }
}
