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

        
        //load variables and user controls -------------------------------------------------------------------------------------------------
        private void Main_Load(object sender, EventArgs e)
        {

            // set date variables
            GVariables.nowDate = DateTime.Now;
            GVariables.month = GVariables.nowDate.Month;
            GVariables.year = GVariables.nowDate.Year;
            GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
            GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);

            GVariables.monthNums = new List<int>();
            int[] months =
            {
                1,2,3,4,5,6,7,8,9,10,11,12
            };
            if (GVariables.monthNums.Count == 0)
            {
                GVariables.monthNums.AddRange(months);
            }

            // Load month view as default
            LoadMonthView();
            
        }
        //----------------------------------------------------------------------------------------------------------------------------------

        //create user control load----------------------------------------------------------------------------------------------------------
        private void LoadMonthView()
        {
            MonthUserControl month = new MonthUserControl();
            groupBox1.Controls.Add(month);
        }
        //----------------------------------------------------------------------------------------------------------------------------------
       
        // ensure app closes when form is closed--------------------------------------------------------------------------------------------
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //----------------------------------------------------------------------------------------------------------------------------------
    }
}
