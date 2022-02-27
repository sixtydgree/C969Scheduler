using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969Scheduler
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        

        private void Reports_Load(object sender, EventArgs e)
        {
            
            FillAppointmentData();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FillAppointmentData()
        {
            if (consultantRadio.Checked)
            {
                flowLayoutPanel1.Controls.Clear();
                ConsultantUserControl newConsultant = new ConsultantUserControl();
                flowLayoutPanel1.Controls.Add(newConsultant);
            }
            if (typeRadio.Checked)
            {
                flowLayoutPanel1.Controls.Clear();

                TypeCount newCount = new TypeCount();
                flowLayoutPanel1.Controls.Add(newCount);
                

            }
            if (customerRadio.Checked)
            {
                flowLayoutPanel1.Controls.Clear();
                CustomerCount customerCount = new CustomerCount();
                flowLayoutPanel1.Controls.Add(customerCount);
            }
        }

        private void consultantRadio_CheckedChanged(object sender, EventArgs e)
        {
            FillAppointmentData();
        }

        private void typeRadio_CheckedChanged(object sender, EventArgs e)
        {
            FillAppointmentData();
        }

        private void customerRadio_CheckedChanged(object sender, EventArgs e)
        {
            FillAppointmentData();
        }

        private void consultantComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillAppointmentData();
        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillAppointmentData();
        }
    }
}
