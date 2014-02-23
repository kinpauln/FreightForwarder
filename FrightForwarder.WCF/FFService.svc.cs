using FreightForwarder.Business;
using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FrightForwarder.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FFService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FFService.svc or FFService.svc.cs at the Solution Explorer and start debugging.
    public class FFService : IFFService
    {
        public IList<RouteInformationItem> GetRoutItems(string shipName, string startPort, string destinationPort, bool? isSingleContainer) {
            return BusinessBase.GetRoutItems(shipName, startPort, destinationPort, isSingleContainer);
        }

        public bool AssociatMachineAndRegCode(string machineCode, string regcode, int companyId)
        {
            return (new ServerBusinesses()).AssociatMachineAndRegCode(machineCode, regcode, companyId);
        }

        public bool AddMachineCode(string machineCode, int companyId)
        {
            // 机器码数据库中已经存在
            if ((new ServerBusinesses()).ExistedEntity(machineCode, companyId)) {
                return false;
            }
            return (new ServerBusinesses()).AddMachineCode(machineCode, companyId);
        }

        public RegisterCode IsRegistered(string machineCode)
        {             
            return BusinessBase.IsRegistered(machineCode);
        }

        public bool ValidateMachineCode(string machineCode) {
            return true;
        }

        public bool AddCompany(string companyName, string companyCode) {
            ServerBusinesses sb = new ServerBusinesses();
            return sb.AddCompany(companyName, companyCode);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
