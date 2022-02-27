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
    public partial class ConsultantUserControl : UserControl
    {
        Collections consultantColl = new Collections();
        public ConsultantUserControl()
        {
            InitializeComponent();
        }

        

        private void ConsultantUserControl_Load(object sender, EventArgs e)
        {
            appointmentTableAdapter.Fill(dataSet1.appointment);
            foreach(string contact in GVariables.contact)
            {
                comboBox1.Items.Add(contact);
            }
            comboBox1.SelectedIndex = 0;
            FillContactAppointments();
        }

        private void FillContactAppointments()
        {
            FillConsultantAppCollection();
            dataGridView1.DataSource = consultantColl.appointmentsByConsultant.ToList();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillContactAppointments();
        }

        private void FillConsultantAppCollection()
        {
            var cons = dataSet1.appointment.Where(a => a.contact == comboBox1.Text);
            if(consultantColl.appointmentsByConsultant.Count() > 0)
            {
                consultantColl.emptyAppBC();
            }
            foreach(var c in cons)
            {
                Appointments byConsulstant = new Appointments(c.appointmentId, c.customerId, c.userId, c.title, c.description, c.location, c.contact, c.type, c.url,
                    c.start, c.end, c.createDate, c.createdBy, c.lastUpdate, c.lastUpdateBy);
                consultantColl.addAppBC(byConsulstant);
            }
        }
    }
}
