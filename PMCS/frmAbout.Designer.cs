namespace PMCS
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAbout = new System.Windows.Forms.Label();
            this.lnkEmails = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.lblBannerText1 = new System.Windows.Forms.Label();
            this.lblBannerText2 = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(22, 153);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(124, 13);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "PMCS Version : v1.1";
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbout.ForeColor = System.Drawing.Color.Black;
            this.lblAbout.Location = new System.Drawing.Point(22, 170);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(97, 13);
            this.lblAbout.TabIndex = 6;
            this.lblAbout.Text = "PMCS : Written by ";
            // 
            // lnkEmails
            // 
            this.lnkEmails.AutoSize = true;
            this.lnkEmails.Location = new System.Drawing.Point(125, 170);
            this.lnkEmails.Name = "lnkEmails";
            this.lnkEmails.Size = new System.Drawing.Size(65, 13);
            this.lnkEmails.TabIndex = 8;
            this.lnkEmails.TabStop = true;
            this.lnkEmails.Text = "Ermira Daka";
            this.toolTip1.SetToolTip(this.lnkEmails, "Email us");
            this.lnkEmails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEmails_LinkClicked);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(278, 236);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(66, 23);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "OK";
            this.toolTip1.SetToolTip(this.btnOk, "Close this form");
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblBannerText1
            // 
            this.lblBannerText1.AutoSize = true;
            this.lblBannerText1.Font = new System.Drawing.Font("Arial Black", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBannerText1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblBannerText1.Location = new System.Drawing.Point(88, 15);
            this.lblBannerText1.Name = "lblBannerText1";
            this.lblBannerText1.Size = new System.Drawing.Size(182, 38);
            this.lblBannerText1.TabIndex = 9;
            this.lblBannerText1.Text = "PM-C# Sys";
            // 
            // lblBannerText2
            // 
            this.lblBannerText2.AutoSize = true;
            this.lblBannerText2.ForeColor = System.Drawing.Color.Black;
            this.lblBannerText2.Location = new System.Drawing.Point(94, 53);
            this.lblBannerText2.Name = "lblBannerText2";
            this.lblBannerText2.Size = new System.Drawing.Size(168, 13);
            this.lblBannerText2.TabIndex = 10;
            this.lblBannerText2.Text = "Parsing and Modeling C# Systems";
            // 
            // picLogo
            // 
            this.picLogo.ErrorImage = global::PMCS.Properties.Resources.AboutImg;
            this.picLogo.Image = global::PMCS.Properties.Resources.AboutImg;
            this.picLogo.InitialImage = global::PMCS.Properties.Resources.AboutImg;
            this.picLogo.Location = new System.Drawing.Point(12, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(70, 70);
            this.picLogo.TabIndex = 2;
            this.picLogo.TabStop = false;
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(354, 270);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblBannerText2);
            this.Controls.Add(this.lblBannerText1);
            this.Controls.Add(this.lnkEmails);
            this.Controls.Add(this.lblAbout);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Parsing and Modeling C# Systems";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.LinkLabel lnkEmails;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblBannerText1;
        private System.Windows.Forms.Label lblBannerText2;
        private System.Windows.Forms.Button btnOk;

    }
}