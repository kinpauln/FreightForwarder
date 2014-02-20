namespace FreightForwarder.Server
{
    partial class FrmStart
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
            this.lblQQInfo = new System.Windows.Forms.Label();
            this.lblSoftDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblQQInfo
            // 
            this.lblQQInfo.AutoSize = true;
            this.lblQQInfo.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.lblQQInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQQInfo.ForeColor = System.Drawing.Color.White;
            this.lblQQInfo.Location = new System.Drawing.Point(364, 260);
            this.lblQQInfo.Name = "lblQQInfo";
            this.lblQQInfo.Size = new System.Drawing.Size(163, 20);
            this.lblQQInfo.TabIndex = 1;
            this.lblQQInfo.Text = "QQ : 657237151";
            // 
            // lblSoftDescription
            // 
            this.lblSoftDescription.AutoSize = true;
            this.lblSoftDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblSoftDescription.Font = new System.Drawing.Font("SketchFlow Print", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSoftDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblSoftDescription.Location = new System.Drawing.Point(334, 149);
            this.lblSoftDescription.Name = "lblSoftDescription";
            this.lblSoftDescription.Size = new System.Drawing.Size(177, 40);
            this.lblSoftDescription.TabIndex = 2;
            this.lblSoftDescription.Text = "货代Mini";
            // 
            // FrmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FreightForwarder.Server.Properties.Resources.青岛_small;
            this.ClientSize = new System.Drawing.Size(512, 280);
            this.Controls.Add(this.lblSoftDescription);
            this.Controls.Add(this.lblQQInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmStart";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQQInfo;
        private System.Windows.Forms.Label lblSoftDescription;
    }
}