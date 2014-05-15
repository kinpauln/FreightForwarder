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
            if (!IsPostBack)
            {
                BindDropdownList();
            }
        }

        protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e) {
            FileUpload fileUpload = FormView1.FindControl("fileUpload") as FileUpload;
            e.Values.Add("FileVersion", Path.GetFileNameWithoutExtension(fileUpload.FileName));
            e.Values.Add("FileName", Path.GetFileName(fileUpload.FileName));
            e.Values.Add("FileBytes", fileUpload.FileBytes);
            e.Values.Add("PostTime", DateTime.Now);

            string savingType = drpSavingType2.SelectedValue;
            e.Values.Add("SavingType", savingType);
        }

        private void BindDropdownList() {
            Dictionary<int, string> denums = FreightForwarder.Common.Extentions.GetEnumDescriptions<FreightForwarder.Common.PackageSavingType>();
            var dsource = new Dictionary<int, string>
            {
                {0, "请选择"}
            }.Union(denums);

            drpSavingType2.DataSource = denums;
            drpSavingType2.DataTextField = "Value";
            drpSavingType2.DataValueField = "Key";
            drpSavingType2.DataBind();
        }

        protected void Query(object sender, EventArgs e)
        {
            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("SavingType", drpSavingType2.SelectedValue);
            GridView1.DataSource = ObjectDataSource1;
            GridView1.DataBind();
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
