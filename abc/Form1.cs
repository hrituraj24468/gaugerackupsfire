
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abc
{
    public partial class Form1 : Form
    {
        private dashboard dashboard; // Declare the dashboard instance

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenDashboard(); // Call the OpenDashboard method when button1 is clicked
        }

        private void OpenDashboard()
        {
            
                if (dashboard == null)
                {
                    dashboard = new dashboard();
                    dashboard.MdiParent = this;
                    dashboard.Show();
                }
                else
                {
                    dashboard.Show();
                }
            
        }
       


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            sidebartransition.Start();
        }

        bool sidebarexpand = true;

        private void sidebartransition_Tick_1(object sender, EventArgs e)
        {
            if (sidebarexpand)
            {
                sidebar.Width -= 30;
                if (sidebar.Width <= 90)
                {
                    sidebarexpand = false;
                    sidebartransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 30;
                if (sidebar.Width > 200)
                {
                    sidebarexpand = true;
                    sidebartransition.Stop();
                }
            }
        }
    }
}
