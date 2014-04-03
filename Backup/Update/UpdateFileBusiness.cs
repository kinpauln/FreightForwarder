using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Web.Configuration;
using System.Transactions;

namespace BigzoneBusinessCenterService {
    public static class UpdateFileBusiness {
        public static IEnumerable<UpdateFile> Select(int? startRowIndex, int? maximumRows) {
            using (DataContext dc = new DataContext(
                WebConfigurationManager.ConnectionStrings["TransPad"].ConnectionString)) {
                return dc.GetTable<UpdateFile>()
                    .OrderByDescending(k => k.FileVersion)
                    .Skip(startRowIndex.Value)
                    .Take(maximumRows.Value)
                    .ToList();
            }
        }

        public static int SelectCount() {
            using (DataContext dc = new DataContext(
                WebConfigurationManager.ConnectionStrings["TransPad"].ConnectionString)) {
                return dc.GetTable<UpdateFile>().Count();
            }
        }

        public static void Insert(UpdateFile updateFile) {
            using (DataContext dc = new DataContext(
                WebConfigurationManager.ConnectionStrings["TransPad"].ConnectionString)) {
                dc.GetTable<UpdateFile>().InsertOnSubmit(updateFile);

                using (TransactionScope ts = new TransactionScope()) {
                    dc.SubmitChanges();
                    ts.Complete();
                }
            }
        }

        public static void Delete(UpdateFile updateFile) {
            using (DataContext dc = new DataContext(
                WebConfigurationManager.ConnectionStrings["TransPad"].ConnectionString)) {
                UpdateFile fileToDelete = dc.GetTable<UpdateFile>().Where(k => k.FileVersion == updateFile.FileVersion).FirstOrDefault();
                if (fileToDelete != default(UpdateFile)) {
                    dc.GetTable<UpdateFile>().DeleteOnSubmit(fileToDelete);

                    using (TransactionScope ts = new TransactionScope()) {
                        dc.SubmitChanges();
                        ts.Complete();
                    }
                }
            }
        }
    }
}
