﻿namespace FreightForwarder.Client
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtnIsSingleContainer = new System.Windows.Forms.RadioButton();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.rbtnIsNotSingleContainer = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtDestinationPort = new System.Windows.Forms.TextBox();
            this.txtStartPort = new System.Windows.Forms.TextBox();
            this.txtShipName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gvRoutItems = new System.Windows.Forms.DataGridView();
            this.ShipName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price_20GP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price_40GP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price_40HQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nonstop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsNostopString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSingleContainer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSingleContainerString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SailDayLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValidDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoutItems)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtDestinationPort);
            this.panel1.Controls.Add(this.txtStartPort);
            this.panel1.Controls.Add(this.txtShipName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 84);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtnIsSingleContainer);
            this.panel2.Controls.Add(this.rbtnAll);
            this.panel2.Controls.Add(this.rbtnIsNotSingleContainer);
            this.panel2.Location = new System.Drawing.Point(277, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 21);
            this.panel2.TabIndex = 6;
            // 
            // rbtnIsSingleContainer
            // 
            this.rbtnIsSingleContainer.AutoSize = true;
            this.rbtnIsSingleContainer.Location = new System.Drawing.Point(6, 3);
            this.rbtnIsSingleContainer.Name = "rbtnIsSingleContainer";
            this.rbtnIsSingleContainer.Size = new System.Drawing.Size(47, 16);
            this.rbtnIsSingleContainer.TabIndex = 3;
            this.rbtnIsSingleContainer.Text = "整柜";
            this.rbtnIsSingleContainer.UseVisualStyleBackColor = true;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(112, 2);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(47, 16);
            this.rbtnAll.TabIndex = 4;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "所有";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // rbtnIsNotSingleContainer
            // 
            this.rbtnIsNotSingleContainer.AutoSize = true;
            this.rbtnIsNotSingleContainer.Location = new System.Drawing.Point(59, 3);
            this.rbtnIsNotSingleContainer.Name = "rbtnIsNotSingleContainer";
            this.rbtnIsNotSingleContainer.Size = new System.Drawing.Size(47, 16);
            this.rbtnIsNotSingleContainer.TabIndex = 4;
            this.rbtnIsNotSingleContainer.Text = "拼箱";
            this.rbtnIsNotSingleContainer.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(813, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 50);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtDestinationPort
            // 
            this.txtDestinationPort.Location = new System.Drawing.Point(328, 47);
            this.txtDestinationPort.Name = "txtDestinationPort";
            this.txtDestinationPort.Size = new System.Drawing.Size(186, 21);
            this.txtDestinationPort.TabIndex = 1;
            // 
            // txtStartPort
            // 
            this.txtStartPort.Location = new System.Drawing.Point(71, 47);
            this.txtStartPort.Name = "txtStartPort";
            this.txtStartPort.Size = new System.Drawing.Size(186, 21);
            this.txtStartPort.TabIndex = 1;
            // 
            // txtShipName
            // 
            this.txtShipName.Location = new System.Drawing.Point(71, 15);
            this.txtShipName.Name = "txtShipName";
            this.txtShipName.Size = new System.Drawing.Size(186, 21);
            this.txtShipName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "目的港";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "起运港";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "船名";
            // 
            // gvRoutItems
            // 
            this.gvRoutItems.AllowUserToAddRows = false;
            this.gvRoutItems.AllowUserToDeleteRows = false;
            this.gvRoutItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvRoutItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvRoutItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ShipName,
            this.StartPort,
            this.DestinationPort,
            this.StartDay,
            this.Price_20GP,
            this.Price_40GP,
            this.Price_40HQ,
            this.Nonstop,
            this.IsNostopString,
            this.IsSingleContainer,
            this.IsSingleContainerString,
            this.SailDayLength,
            this.ValidDate,
            this.Remarks});
            this.gvRoutItems.Location = new System.Drawing.Point(0, 84);
            this.gvRoutItems.Name = "gvRoutItems";
            this.gvRoutItems.RowTemplate.Height = 23;
            this.gvRoutItems.Size = new System.Drawing.Size(900, 321);
            this.gvRoutItems.TabIndex = 1;
            this.gvRoutItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvRoutItems_CellFormatting);
            // 
            // ShipName
            // 
            this.ShipName.DataPropertyName = "ShipName";
            this.ShipName.HeaderText = "船名";
            this.ShipName.Name = "ShipName";
            this.ShipName.ReadOnly = true;
            // 
            // StartPort
            // 
            this.StartPort.DataPropertyName = "StartPort";
            this.StartPort.HeaderText = "起运港";
            this.StartPort.Name = "StartPort";
            this.StartPort.ReadOnly = true;
            // 
            // DestinationPort
            // 
            this.DestinationPort.DataPropertyName = "DestinationPort";
            this.DestinationPort.HeaderText = "目的港";
            this.DestinationPort.Name = "DestinationPort";
            this.DestinationPort.ReadOnly = true;
            // 
            // StartDay
            // 
            this.StartDay.DataPropertyName = "StartDay";
            this.StartDay.HeaderText = "船期";
            this.StartDay.Name = "StartDay";
            this.StartDay.ReadOnly = true;
            // 
            // Price_20GP
            // 
            this.Price_20GP.DataPropertyName = "Price_20GP";
            this.Price_20GP.HeaderText = "20GP";
            this.Price_20GP.Name = "Price_20GP";
            this.Price_20GP.ReadOnly = true;
            // 
            // Price_40GP
            // 
            this.Price_40GP.DataPropertyName = "Price_40GP";
            this.Price_40GP.HeaderText = "40GP";
            this.Price_40GP.Name = "Price_40GP";
            this.Price_40GP.ReadOnly = true;
            // 
            // Price_40HQ
            // 
            this.Price_40HQ.DataPropertyName = "Price_40HQ";
            this.Price_40HQ.HeaderText = "40HQ";
            this.Price_40HQ.Name = "Price_40HQ";
            this.Price_40HQ.ReadOnly = true;
            // 
            // Nonstop
            // 
            this.Nonstop.DataPropertyName = "Nonstop";
            this.Nonstop.HeaderText = "是否直达";
            this.Nonstop.Name = "Nonstop";
            this.Nonstop.ReadOnly = true;
            this.Nonstop.Visible = false;
            // 
            // IsNostopString
            // 
            this.IsNostopString.HeaderText = "是否直达";
            this.IsNostopString.Name = "IsNostopString";
            // 
            // IsSingleContainer
            // 
            this.IsSingleContainer.DataPropertyName = "IsSingleContainer";
            this.IsSingleContainer.HeaderText = "整柜/拼箱";
            this.IsSingleContainer.Name = "IsSingleContainer";
            this.IsSingleContainer.ReadOnly = true;
            this.IsSingleContainer.Visible = false;
            // 
            // IsSingleContainerString
            // 
            this.IsSingleContainerString.HeaderText = "整柜/拼箱";
            this.IsSingleContainerString.Name = "IsSingleContainerString";
            // 
            // SailDayLength
            // 
            this.SailDayLength.DataPropertyName = "SailDayLength";
            this.SailDayLength.HeaderText = "航程";
            this.SailDayLength.Name = "SailDayLength";
            this.SailDayLength.ReadOnly = true;
            // 
            // ValidDate
            // 
            this.ValidDate.DataPropertyName = "ValidDate";
            this.ValidDate.HeaderText = "有效期";
            this.ValidDate.Name = "ValidDate";
            this.ValidDate.ReadOnly = true;
            // 
            // Remarks
            // 
            this.Remarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Remarks.DataPropertyName = "Remarks";
            this.Remarks.HeaderText = "备注";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 405);
            this.Controls.Add(this.gvRoutItems);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "查询";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoutItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtDestinationPort;
        private System.Windows.Forms.TextBox txtStartPort;
        private System.Windows.Forms.TextBox txtShipName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtnIsSingleContainer;
        private System.Windows.Forms.RadioButton rbtnIsNotSingleContainer;
        private System.Windows.Forms.DataGridView gvRoutItems;
        private System.Windows.Forms.RadioButton rbtnAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShipName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price_20GP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price_40GP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price_40HQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nonstop;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsNostopString;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsSingleContainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsSingleContainerString;
        private System.Windows.Forms.DataGridViewTextBoxColumn SailDayLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValidDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
    }
}