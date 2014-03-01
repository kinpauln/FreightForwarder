namespace FreightForwarder.UI.Winform
{
    partial class FrmRegCodeViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegCodeViewer));
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtMachineCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRegCode = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "机器码：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(77, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "查看";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.Location = new System.Drawing.Point(77, 24);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Size = new System.Drawing.Size(243, 21);
            this.txtMachineCode.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "注册码：";
            // 
            // lblRegCode
            // 
            this.lblRegCode.AutoSize = true;
            this.lblRegCode.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRegCode.ForeColor = System.Drawing.Color.Red;
            this.lblRegCode.Location = new System.Drawing.Point(75, 58);
            this.lblRegCode.Name = "lblRegCode";
            this.lblRegCode.Size = new System.Drawing.Size(72, 20);
            this.lblRegCode.TabIndex = 0;
            this.lblRegCode.Text = "注册码";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(380, 58);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Visible = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // FrmRegCodeViewer
            // 
            this.ClientSize = new System.Drawing.Size(496, 143);
            this.Controls.Add(this.txtMachineCode);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblRegCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegCodeViewer";
            this.ShowIcon = false;
            this.Text = "注册码查看器";
            this.Load += new System.EventHandler(this.FrmRegCodeViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtMachineCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRegCode;
        private System.Windows.Forms.Button btnCopy;
    }
}