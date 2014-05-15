namespace FreightForwarder.UI.Winform
{
    partial class FrmContainer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContainer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsddBtnImport = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlspBtnExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnQuery = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemTool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegCodeViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsItemBtnAddCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCompanyMgr = new System.Windows.Forms.ToolStripMenuItem();
            this.tsItemBtnRegCode = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSoftInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLblCompanyInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddBtnImport,
            this.toolStripSeparator1,
            this.tlspBtnExport,
            this.toolStripSeparator2,
            this.toolStripBtnQuery});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 64);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsddBtnImport
            // 
            this.tsddBtnImport.Image = global::FreightForwarder.UI.Winform.Properties.Resources.database_up;
            this.tsddBtnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddBtnImport.Name = "tsddBtnImport";
            this.tsddBtnImport.ShowDropDownArrow = false;
            this.tsddBtnImport.Size = new System.Drawing.Size(60, 61);
            this.tsddBtnImport.Text = "导入数据";
            this.tsddBtnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsddBtnImport.Click += new System.EventHandler(this.tsddBtnImport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 64);
            // 
            // tlspBtnExport
            // 
            this.tlspBtnExport.Image = global::FreightForwarder.UI.Winform.Properties.Resources.database_down;
            this.tlspBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlspBtnExport.Name = "tlspBtnExport";
            this.tlspBtnExport.Size = new System.Drawing.Size(60, 61);
            this.tlspBtnExport.Text = "导出数据";
            this.tlspBtnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlspBtnExport.Click += new System.EventHandler(this.tlspBtnExport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 64);
            // 
            // toolStripBtnQuery
            // 
            this.toolStripBtnQuery.Image = global::FreightForwarder.UI.Winform.Properties.Resources.database_search;
            this.toolStripBtnQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnQuery.Name = "toolStripBtnQuery";
            this.toolStripBtnQuery.Size = new System.Drawing.Size(44, 61);
            this.toolStripBtnQuery.Text = "查询";
            this.toolStripBtnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnQuery.ToolTipText = "    查询    ";
            this.toolStripBtnQuery.Click += new System.EventHandler(this.toolStripBtnQuery_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTool,
            this.工具ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemAdded);
            // 
            // toolStripMenuItemTool
            // 
            this.toolStripMenuItemTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRegCodeViewer});
            this.toolStripMenuItemTool.Name = "toolStripMenuItemTool";
            this.toolStripMenuItemTool.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItemTool.Text = "工具";
            // 
            // toolStripMenuItemRegCodeViewer
            // 
            this.toolStripMenuItemRegCodeViewer.Name = "toolStripMenuItemRegCodeViewer";
            this.toolStripMenuItemRegCodeViewer.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItemRegCodeViewer.Text = "注册码查看器";
            this.toolStripMenuItemRegCodeViewer.Click += new System.EventHandler(this.toolStripMenuItemRegCodeViewer_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSearch,
            this.tsItemBtnAddCompany,
            this.toolStripMenuItemCompanyMgr,
            this.tsItemBtnRegCode});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工具ToolStripMenuItem.Text = "编辑";
            // 
            // toolStripMenuItemSearch
            // 
            this.toolStripMenuItemSearch.Name = "toolStripMenuItemSearch";
            this.toolStripMenuItemSearch.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemSearch.Text = "查询";
            this.toolStripMenuItemSearch.Click += new System.EventHandler(this.toolStripMenuItemSearch_Click);
            // 
            // tsItemBtnAddCompany
            // 
            this.tsItemBtnAddCompany.Name = "tsItemBtnAddCompany";
            this.tsItemBtnAddCompany.Size = new System.Drawing.Size(136, 22);
            this.tsItemBtnAddCompany.Text = "添加公司";
            this.tsItemBtnAddCompany.Click += new System.EventHandler(this.tsItemBtnAddCompany_Click);
            // 
            // toolStripMenuItemCompanyMgr
            // 
            this.toolStripMenuItemCompanyMgr.Name = "toolStripMenuItemCompanyMgr";
            this.toolStripMenuItemCompanyMgr.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemCompanyMgr.Text = "公司管理";
            this.toolStripMenuItemCompanyMgr.Click += new System.EventHandler(this.toolStripMenuItemCompanyMgr_Click);
            // 
            // tsItemBtnRegCode
            // 
            this.tsItemBtnRegCode.Name = "tsItemBtnRegCode";
            this.tsItemBtnRegCode.Size = new System.Drawing.Size(136, 22);
            this.tsItemBtnRegCode.Text = "注册机器码";
            this.tsItemBtnRegCode.Click += new System.EventHandler(this.tsItemBtnRegCode_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAboutUs,
            this.toolStripMenuItemSoftInfo});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // toolStripMenuItemAboutUs
            // 
            this.toolStripMenuItemAboutUs.Name = "toolStripMenuItemAboutUs";
            this.toolStripMenuItemAboutUs.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemAboutUs.Text = "关于";
            this.toolStripMenuItemAboutUs.Click += new System.EventHandler(this.toolStripMenuItemAboutUs_Click);
            // 
            // toolStripMenuItemSoftInfo
            // 
            this.toolStripMenuItemSoftInfo.Name = "toolStripMenuItemSoftInfo";
            this.toolStripMenuItemSoftInfo.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemSoftInfo.Text = "机器码";
            this.toolStripMenuItemSoftInfo.Click += new System.EventHandler(this.toolStripMenuItemSoftInfo_Click);
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLblCompanyInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLblCompanyInfo
            // 
            this.toolStripStatusLblCompanyInfo.Name = "toolStripStatusLblCompanyInfo";
            this.toolStripStatusLblCompanyInfo.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLblCompanyInfo.Text = "公司名";
            // 
            // FrmContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::FreightForwarder.UI.Winform.Properties.Resources.青岛;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmContainer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代贸通物流平台-客户端";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ContainerForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MdiChildActivate += new System.EventHandler(this.FrmContainer_MdiChildActivate);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContainerForm_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripDropDownButton tsddBtnImport;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAboutUs;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton tlspBtnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsItemBtnAddCompany;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSearch;
        private System.Windows.Forms.ToolStripMenuItem tsItemBtnRegCode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSoftInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTool;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRegCodeViewer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblCompanyInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripBtnQuery;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCompanyMgr;

    }
}

