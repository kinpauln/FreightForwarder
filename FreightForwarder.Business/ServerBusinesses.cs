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
    public class ServerBusinesses : BusinessBase
    {
        public bool ExistedEntity(string machineCode)
        {
            return (new DBHelper()).ExistedEntity(machineCode);
        }

        public bool ExistedEntity(string machineCode, int companyId)
        {
            return (new DBHelper()).ExistedEntity(machineCode, companyId);
        }

        public bool AssociatMachineAndRegCode(string machineCode, string regcode, int companyId)
        {
            return (new DBHelper()).AssociatMachineAndRegCode(machineCode, regcode, companyId);
        }

        public bool AddMachineCode(string machineCode, string description, int companyId) {
            return (new DBHelper()).AddMachineCode(machineCode,description, companyId);
        }

        public IList<RouteInformationItem> ImportExcelData(Stream stream, Dictionary<string, int> dicCompanies)
        {
            DataTable dt = NPOIHelper.ReadFromExcel(stream);
            IList<RouteInformationItem> rlist = dt.ToRoutItemList(dicCompanies);
            
            return rlist;
        }

        public IList<RouteInformationItem> GetExcelData(string filepath, Dictionary<string, int> companies)
        {
            StreamReader reader = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(filepath, FileMode.Open);
                reader = new StreamReader(fs);
                return ImportExcelData(fs, companies);
            }
            catch (Exception excep)
            {
                throw excep;
            }
            finally
            {
                reader.Close();
                fs.Close();
            }
        }

        public bool AddCompany(string companyName, string companyCode) {
            bool result = (new DBHelper()).AddCompany(companyName, companyCode);
            return result;
        }

        public bool ImportRouteInformationItems(IList<RouteInformationItem> rlist) {
            bool result = (new DBHelper()).ImportRouteInformationItems(rlist);
            return result;
        }

        public bool RegCode(string machineCode, string regCode, int companyId)
        {
            bool result = (new DBHelper()).AddRegCode(machineCode, regCode, companyId);
            return result;
        }

        public IEnumerable<Company> GetAllCompanies() {
            return (new DBHelper()).GetAllCompanies();
        }
    }
}
