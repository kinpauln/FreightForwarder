namespace FreightForwarder
{
    partial class ContainerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContainerForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.tsItemBtnAddCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddBtnImport = new System.Windows.Forms.ToolStripDropDownButton();
            this.tlspBtnExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenuItemSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsItemBtnRegCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddBtnImport,
            this.toolStripSeparator1,
            this.tlspBtnExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(745, 64);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 64);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.工具ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(745, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSearch,
            this.tsItemBtnAddCompany,
            this.tsItemBtnRegCode});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "database.png");
            this.imageList1.Images.SetKeyName(1, "database_accept.png");
            this.imageList1.Images.SetKeyName(2, "database_add.png");
            this.imageList1.Images.SetKeyName(3, "database_down.png");
            this.imageList1.Images.SetKeyName(4, "database_lock.png");
            this.imageList1.Images.SetKeyName(5, "database_next.png");
            this.imageList1.Images.SetKeyName(6, "database_previous.png");
            this.imageList1.Images.SetKeyName(7, "database_process.png");
            this.imageList1.Images.SetKeyName(8, "database_remove.png");
            this.imageList1.Images.SetKeyName(9, "database_search.png");
            this.imageList1.Images.SetKeyName(10, "database_up.png");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 89);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(745, 283);
            this.panelContainer.TabIndex = 4;
            this.panelContainer.Visible = false;
            // 
            // tsItemBtnAddCompany
            // 
            this.tsItemBtnAddCompany.Name = "tsItemBtnAddCompany";
            this.tsItemBtnAddCompany.Size = new System.Drawing.Size(152, 22);
            this.tsItemBtnAddCompany.Text = "添加公司";
            this.tsItemBtnAddCompany.Click += new System.EventHandler(this.tsItemBtnAddCompany_Click);
            // 
            // tsddBtnImport
            // 
            this.tsddBtnImport.Image = global::FreightForwarder.Server.Properties.Resources.database_up;
            this.tsddBtnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddBtnImport.Name = "tsddBtnImport";
            this.tsddBtnImport.ShowDropDownArrow = false;
            this.tsddBtnImport.Size = new System.Drawing.Size(72, 61);
            this.tsddBtnImport.Text = "导入数据库";
            this.tsddBtnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsddBtnImport.Click += new System.EventHandler(this.tsddBtnImport_Click);
            // 
            // tlspBtnExport
            // 
            this.tlspBtnExport.Image = global::FreightForwarder.Server.Properties.Resources.database_down;
            this.tlspBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlspBtnExport.Name = "tlspBtnExport";
            this.tlspBtnExport.Size = new System.Drawing.Size(60, 61);
            this.tlspBtnExport.Text = "导出数据";
            this.tlspBtnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlspBtnExport.Click += new System.EventHandler(this.tlspBtnExport_Click);
            // 
            // toolStripMenuItemSearch
            // 
            this.toolStripMenuItemSearch.Name = "toolStripMenuItemSearch";
            this.toolStripMenuItemSearch.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemSearch.Text = "查询";
            this.toolStripMenuItemSearch.Click += new System.EventHandler(this.toolStripMenuItemSearch_Click);
            // 
            // tsItemBtnRegCode
            // 
            this.tsItemBtnRegCode.Name = "tsItemBtnRegCode";
            this.tsItemBtnRegCode.Size = new System.Drawing.Size(152, 22);
            this.tsItemBtnRegCode.Text = "生成注册码";
            this.tsItemBtnRegCode.Click += new System.EventHandler(this.tsItemBtnRegCode_Click);
            // 
            // ContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 372);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "ContainerForm";
            this.Text = "货代";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripDropDownButton tsddBtnImport;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton tlspBtnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsItemBtnAddCompany;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSearch;
        private System.Windows.Forms.ToolStripMenuItem tsItemBtnRegCode;

    }
}

