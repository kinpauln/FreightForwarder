namespace FreightForwarder.Client
{
    partial class FrmUserRegister
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
            this.txtPart1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPart2 = new System.Windows.Forms.TextBox();
            this.txtPart3 = new System.Windows.Forms.TextBox();
            this.txtPart4 = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPart1
            // 
            this.txtPart1.Location = new System.Drawing.Point(13, 25);
            this.txtPart1.Name = "txtPart1";
            this.txtPart1.Size = new System.Drawing.Size(100, 21);
            this.txtPart1.TabIndex = 0;
            this.txtPart1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPart1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "-";
            // 
            // txtPart2
            // 
            this.txtPart2.Location = new System.Drawing.Point(136, 25);
            this.txtPart2.Name = "txtPart2";
            this.txtPart2.Size = new System.Drawing.Size(100, 21);
            this.txtPart2.TabIndex = 1;
            // 
            // txtPart3
            // 
            this.txtPart3.Location = new System.Drawing.Point(266, 25);
            this.txtPart3.Name = "txtPart3";
            this.txtPart3.Size = new System.Drawing.Size(100, 21);
            this.txtPart3.TabIndex = 2;
            // 
            // txtPart4
            // 
            this.txtPart4.Location = new System.Drawing.Point(404, 25);
            this.txtPart4.Name = "txtPart4";
            this.txtPart4.Size = new System.Drawing.Size(100, 21);
            this.txtPart4.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(430, 62);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReg
            // 
            this.btnReg.Location = new System.Drawing.Point(349, 62);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(75, 23);
            this.btnReg.TabIndex = 4;
            this.btnReg.Text = "注册";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // FrmUserRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 110);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPart4);
            this.Controls.Add(this.txtPart3);
            this.Controls.Add(this.txtPart2);
            this.Controls.Add(this.txtPart1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUserRegister";
            this.ShowIcon = false;
            this.Text = "注册码";
            this.Click += new System.EventHandler(this.FrmUserRegister_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPart2;
        private System.Windows.Forms.TextBox txtPart3;
        private System.Windows.Forms.TextBox txtPart4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReg;
    }
}