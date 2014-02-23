namespace FreightForwarder.Server
{
    partial class FrmSoftInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblMachineCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRegCode = new System.Windows.Forms.Label();
            this.btnCopyMachineCode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "本机机器码：";
            // 
            // lblMachineCode
            // 
            this.lblMachineCode.AutoSize = true;
            this.lblMachineCode.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMachineCode.ForeColor = System.Drawing.Color.Red;
            this.lblMachineCode.Location = new System.Drawing.Point(110, 22);
            this.lblMachineCode.Name = "lblMachineCode";
            this.lblMachineCode.Size = new System.Drawing.Size(273, 20);
            this.lblMachineCode.TabIndex = 0;
            this.lblMachineCode.Text = "BFEBFBFF0001067A0004F0CF";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "本机注册码：";
            // 
            // lblRegCode
            // 
            this.lblRegCode.AutoSize = true;
            this.lblRegCode.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRegCode.ForeColor = System.Drawing.Color.Red;
            this.lblRegCode.Location = new System.Drawing.Point(110, 52);
            this.lblRegCode.Name = "lblRegCode";
            this.lblRegCode.Size = new System.Drawing.Size(273, 20);
            this.lblRegCode.TabIndex = 0;
            this.lblRegCode.Text = "EMKEMEMM3335368C3332M3GM";
            // 
            // btnCopyMachineCode
            // 
            this.btnCopyMachineCode.Location = new System.Drawing.Point(452, 21);
            this.btnCopyMachineCode.Name = "btnCopyMachineCode";
            this.btnCopyMachineCode.Size = new System.Drawing.Size(75, 23);
            this.btnCopyMachineCode.TabIndex = 1;
            this.btnCopyMachineCode.Text = "复制";
            this.btnCopyMachineCode.UseVisualStyleBackColor = true;
            this.btnCopyMachineCode.Click += new System.EventHandler(this.btnCopyMachineCode_Click);
            // 
            // FrmSoftInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 110);
            this.Controls.Add(this.btnCopyMachineCode);
            this.Controls.Add(this.lblRegCode);
            this.Controls.Add(this.lblMachineCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSoftInfo";
            this.ShowIcon = false;
            this.Text = "软件信息";
            this.Load += new System.EventHandler(this.FrmSoftInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMachineCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRegCode;
        private System.Windows.Forms.Button btnCopyMachineCode;
    }
}