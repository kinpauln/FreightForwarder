using FreightForwarder.Data;
using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Business
{
    public class CompanyBusiness
    {
        public static Dictionary<string, int> GetAllCompanies() {
            IEnumerable<Company> companies = (new DBHelper()).GetAllCompanies();

            Dictionary<string, int> results = new Dictionary<string, int>();

            foreach(Company item in companies){
                results.Add(item.Code, item.Id);
            }

            return results;
        }
    }
}
