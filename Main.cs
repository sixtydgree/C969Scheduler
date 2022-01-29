using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace C969Scheduler
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        

        private void Main_Load(object sender, EventArgs e)
        {

            // find and use dates
            GVariables.nowDate = DateTime.Now;
            GVariables.month = GVariables.nowDate.Month;
            GVariables.year = GVariables.nowDate.Year;
            GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
            GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
            

            // Load month view as default
            LoadMonthView();
            
        }

        private void LoadMonthView()
        {
            MonthUserControl month = new MonthUserControl();
            groupBox1.Controls.Add(month);
        }

       

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
