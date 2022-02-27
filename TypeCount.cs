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
    public partial class TypeCount : UserControl
    {
        Collections typeCollection = new Collections();
        public TypeCount()
        {
            InitializeComponent();
        }

        

        private void TypeCount_Load(object sender, EventArgs e)
        {
            appointmentTableAdapter.Fill(dataSet1.appointment);
            foreach(string type in GVariables.type)
            {
                comboBox1.Items.Add(type);
            }
            comboBox1.SelectedIndex = 0;
            FillTypeCollection();
            FillTypeCount();
        }


        // Meathod for filling the datagrid -----------------------------------------------------------------------------------------------------------------
        private void FillTypeCount()
        {
            //this lambda was the only way I could get the proper counts and grid view. It was also the most efficiant way to make this happen
            var typeCount = typeCollection.typeCountByMonth.Where(t => t.Type == comboBox1.Text)
                .GroupBy(n => new { Type = n.Type, Month = n.Start.Month })
                .OrderBy(s => s.Key.Month)
                .Select(t => new { Count = t.Count(), Type = t.Key.Type, Month = t.Key.Month });
            
            dataGridView1.DataSource = typeCount.ToList();

        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------

        // combobox behaviour ------------------------------------------------------------------------------------------------------------------------------
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTypeCount();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------
        
        private void FillTypeCollection()
        {
            var appointments = from a in dataSet1.appointment
                               select a;
            if (typeCollection.typeCountByMonth.Count() > 0)
            {
                typeCollection.emptyAppByM();
            }
            foreach(var a in appointments)
            {
                Appointments app = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start, a.end, a.createDate, a.createdBy,
                    a.lastUpdate, a.lastUpdateBy);
                typeCollection.addAppCountByM(app);
            }
        }
    }
}
