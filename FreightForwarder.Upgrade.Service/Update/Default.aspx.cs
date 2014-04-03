using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace FreightForwarder.Upgrade.Service {
    public partial class _Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e) {
            FileUpload fileUpload = FormView1.FindControl("fileUpload") as FileUpload;
            e.Values.Add("FileVersion", Path.GetFileNameWithoutExtension(fileUpload.FileName));
            e.Values.Add("FileName", Path.GetFileName(fileUpload.FileName));
            e.Values.Add("FileBytes", fileUpload.FileBytes);
            e.Values.Add("PostTime", DateTime.Now);
        }

        //protected void ObjectDataSource1_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
            
        //    ObjectDataSource1.InsertParameters.Add("FileVersion", 
        //        Path.GetFileNameWithoutExtension(fileUpload.FileName));
        //    ObjectDataSource1.InsertParameters.Add("FileName",
        //        Path.GetFileName(fileUpload.FileName));
        //    //ObjectDataSource1.InsertParameters
        //    //new Parameter("FileBytes", System.Data.DbType.Binary)
        //    //ObjectDataSource1.InsertParameters["PostTime"].DefaultValue = DateTime.Now;
            
        //}

    }
}
