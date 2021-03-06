﻿namespace FreightForwarder.UI.Winform
{
    partial class FrmRegCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegCode));
            this.label1 = new System.Windows.Forms.Label();
            this.txtMachineCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRegCode = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBoxCompanies = new System.Windows.Forms.ComboBox();
            this.lblRegCode = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.lblCompanyInfo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "机器码：";
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.Location = new System.Drawing.Point(108, 29);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Size = new System.Drawing.Size(205, 21);
            this.txtMachineCode.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "注册码：";
            // 
            // btnRegCode
            // 
            this.btnRegCode.Location = new System.Drawing.Point(108, 185);
            this.btnRegCode.Name = "btnRegCode";
            this.btnRegCode.Size = new System.Drawing.Size(75, 23);
            this.btnRegCode.TabIndex = 2;
            this.btnRegCode.Text = "注册";
            this.btnRegCode.UseVisualStyleBackColor = true;
            this.btnRegCode.Click += new System.EventHandler(this.btnRegCode_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "公司：";
            // 
            // cbBoxCompanies
            // 
            this.cbBoxCompanies.FormattingEnabled = true;
            this.cbBoxCompanies.Location = new System.Drawing.Point(108, 118);
            this.cbBoxCompanies.Name = "cbBoxCompanies";
            this.cbBoxCompanies.Size = new System.Drawing.Size(121, 20);
            this.cbBoxCompanies.TabIndex = 1;
            // 
            // lblRegCode
            // 
            this.lblRegCode.AutoSize = true;
            this.lblRegCode.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRegCode.ForeColor = System.Drawing.Color.Red;
            this.lblRegCode.Location = new System.Drawing.Point(106, 150);
            this.lblRegCode.Name = "lblRegCode";
            this.lblRegCode.Size = new System.Drawing.Size(177, 20);
            this.lblRegCode.TabIndex = 0;
            this.lblRegCode.Text = "等待生成中。。。";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(431, 149);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 3;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Visible = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // lblCompanyInfo
            // 
            this.lblCompanyInfo.AutoSize = true;
            this.lblCompanyInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCompanyInfo.ForeColor = System.Drawing.Color.Red;
            this.lblCompanyInfo.Location = new System.Drawing.Point(244, 123);
            this.lblCompanyInfo.Name = "lblCompanyInfo";
            this.lblCompanyInfo.Size = new System.Drawing.Size(187, 12);
            this.lblCompanyInfo.TabIndex = 4;
            this.lblCompanyInfo.Text = "正在初始化货代公司信息。。。";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "描述：";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(108, 61);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(323, 51);
            this.txtDescription.TabIndex = 0;
            // 
            // FrmRegCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 249);
            this.Controls.Add(this.lblCompanyInfo);
            this.Controls.Add(this.cbBoxCompanies);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnRegCode);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtMachineCode);
            this.Controls.Add(this.lblRegCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegCode";
            this.ShowIcon = false;
            this.Text = "注册码";
            this.Load += new System.EventHandler(this.RegCodeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMachineCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRegCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbBoxCompanies;
        private System.Windows.Forms.Label lblRegCode;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label lblCompanyInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescription;
    }
}