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
        public bool ImportExcelData(Stream stream) {
            DataTable dt = NPOIHelper.ReadFromExcel(stream);

            IList<RouteInformationItem> rlist = dt.ToRoutItemList();

            IList<RouteInformationItem> insertlist = rlist.Where(ri=>ri.Id == 0).ToList();
            IList<RouteInformationItem> updatelist = rlist.Where(ri => ri.Id != 0).ToList();

            // bool result = DBHelper.AddRouteInformationItems(importlist);
            bool result = DBHelper.ImportRouteInformationItems(rlist);

            return result;
        }

        public bool ImportExcelData(string filepath)
        {
            StreamReader reader = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(filepath, FileMode.Open);
                reader = new StreamReader(fs);
                return ImportExcelData(fs);
            }
            catch (Exception excep)
            {
                //MessageBox.Show(excep.Message);
                return false;
            }
            finally
            {
                reader.Close();
                fs.Close();
            }
        }

        public bool AddCompany(string companyName, string companyCode) {
            bool result = DBHelper.AddCompany(companyName, companyCode);
            return result;
        }

        public bool RegCode(string machineCode, string regCode, int companyId)
        {
            bool result = DBHelper.AddRegCode(machineCode, regCode, companyId);
            return result;
        }

        public IEnumerable<Company> GetAllCompanies() {
            return DBHelper.GetAllCompanies();
        }
    }
}
