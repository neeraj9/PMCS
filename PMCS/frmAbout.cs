using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PMCS
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

      

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void startProcess(string target)
        {
            //try to start the correct app for the target
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem with starting process " + target + " in frmAbout : " +
                    "\r\n\r\n\r\n" + ex.Message);

            }
        }

        private void lnkEmails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // LinkData property of the Link object.
            string target = e.Link.LinkData as string;
            //start the link process
            startProcess(target);
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            this.lnkEmails.Links.Add(0, 12, "mailto:ermiradaka@hotmail.com");
        }
    }
}