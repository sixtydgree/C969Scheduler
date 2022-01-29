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
    public partial class MonthUserControl : UserControl
    {
        private bool isMonth;
        private int sevenDay;
        public MonthUserControl()
        {
            InitializeComponent();
        }

        private void EnableDisableBtns()
        {
            if(addBtn.Enabled == true && updateBtn.Enabled == true && deleteBtn.Enabled == true)
            {
                addBtn.Enabled = false;
                updateBtn.Enabled = false;
                deleteBtn.Enabled = false;
                updateCusBtn.Enabled = false;
                saveBtn.Enabled = true;
                cancelBtn.Enabled = true;
                prevBtn.Enabled = false;
                nextBtn.Enabled = false;
                todayBtn.Enabled = false;
                reportsBtn.Enabled = false;
                appointmentDataGridView.Enabled = false;
                groupBox1.Enabled = true;
                groupBox3.Enabled = true;
            }
            else
            {
                addBtn.Enabled = true;
                updateBtn.Enabled = true;
                deleteBtn.Enabled = true;
                updateCusBtn.Enabled = true;
                saveBtn.Enabled = false;
                cancelBtn.Enabled = false;
                prevBtn.Enabled = true;
                nextBtn.Enabled = true;
                todayBtn.Enabled = true;
                reportsBtn.Enabled = true;
                appointmentDataGridView.Enabled = true;
                groupBox1.Enabled = false;
                groupBox3.Enabled = false;
            }
        }

        private void MonthUserControl_Load(object sender, EventArgs e)
        {
            label3.Text = Properties.Resources.Current + " " + GVariables.userName;
            isMonth = true;
            GVariables.type = new List<string>();
            GVariables.contact = new List<string>();
            GVariables.monthNums = new List<int>();
            string[] types =
            {
                "Scrum",
                "Presentation",
                "Lunch",
                "Interview"
            };
            if(GVariables.type.Count == 0)
            {
                GVariables.type.AddRange(types);
            }
            string[] contacts =
            {
                "Paul J.",
                "Paul T.",
                "Trisha J.",
                "Keri S.",
                "Carra L.",
                "Steve N.",
                "Patty T."
            };
            if(GVariables.contact.Count == 0)
            {
                GVariables.contact.AddRange(contacts);
            }
            int[] months =
            {
                1,2,3,4,5,6,7,8,9,10,11,12
            };
            if(GVariables.monthNums.Count == 0)
            {
                GVariables.monthNums.AddRange(months);
            }
            GVariables.nowDate = DateTime.Now;
            GVariables.month = GVariables.nowDate.Month;
            GVariables.year = GVariables.nowDate.Year;
            GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
            GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
            GVariables.day = GVariables.firstOfMonth.Day;
            DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
            GVariables.endOfMonth = DateTime.DaysInMonth(GVariables.year, GVariables.month);
            DateTime.TryParse(GVariables.month + "-" + GVariables.endOfMonth + "-" + GVariables.year, out GVariables.date2);
            appointmentTableAdapter.FillByDate(dataSet1.appointment, GVariables.date1, GVariables.date2);
            monthYearTxt.Text = GVariables.monthNm + " " + GVariables.year;
            this.saveBtn.Enabled = false;
            this.cancelBtn.Enabled = false;
            this.appointmentDataGridView.Enabled = true;
            this.groupBox1.Enabled = false;
            appointmentDataGridView.ClearSelection();
            foreach(string type in GVariables.type)
            {
                typeComboBox.Items.Add(type);
            }
            foreach(string contact in GVariables.contact)
            {
                contactComboBox.Items.Add(contact);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            customerInformationTableAdapter.Fill(dataSet1.customerInformation);
            EnableDisableBtns();
            //add new appointment
            appointmentBindingSource.AddNew();
            userIdTextBox.Text = GVariables.userId.ToString();
            lastUpdateByTextBox.Text = GVariables.userName;
            createdByTextBox.Text = GVariables.userName;
            createDateDateTimePicker.Value = DateTime.Now;
            lastUpdateDateTimePicker.Value = DateTime.Now;
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            customerInformationTableAdapter.Fill(dataSet1.customerInformation);
            int rows;
            rows = dataSet1.appointment.Rows.Count;
            if(rows == 0)
            {
                MessageBox.Show(Properties.Resources.NoAppointmentSelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            EnableDisableBtns();
            userIdTextBox.Text = GVariables.userId.ToString();
            lastUpdateByTextBox.Text = GVariables.userName;
            lastUpdateDateTimePicker.Value = DateTime.Now;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int rows;
            rows = dataSet1.appointment.Rows.Count;
            if(rows == 0)
            {
                MessageBox.Show(Properties.Resources.NoAppointmentSelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(MessageBox.Show(Properties.Resources.AreYouSure, Properties.Resources.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                appointmentBindingSource.RemoveCurrent();
                int r;
                r = appointmentTableAdapter.Update(dataSet1.appointment);
                if (r > 0)
                {
                    MessageBox.Show(Properties.Resources.AppDeleted);
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.NothingDeleted);
                return;
            }

        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // Save and cancel buttons ----------------------------------------------------------------------------------------------------------------------------------
        private void saveBtn_Click(object sender, EventArgs e)
        {
            if(DateIsFuture() == true && DateNotTaken() == true && CheckInputs() == true && InsideWorkHours() == true)
            {
                EnableDisableBtns();
                // save changed or new appointment
                appointmentBindingSource.EndEdit();
                int r;
                r = appointmentTableAdapter.Update(dataSet1.appointment);
                if (r > 0)
                {
                    MessageBox.Show(Properties.Resources.Saved);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.NothingSaved);
                    return;
                }
            }
            
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            EnableDisableBtns();
            // cancel changes to appointments
            dataSet1.appointment.RejectChanges();
            appointmentBindingSource.CancelEdit();
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------

        private void appointmentDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(appointmentDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString() != null)
            {
                int.TryParse(appointmentDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString(), out GVariables.customerId);
                customerInformationTableAdapter.FillById(dataSet1.customerInformation, GVariables.customerId);
            }
        }

        // Navigation buttons "next week", "previous week", etc.-------------------------------------------------------------------------------------------
        private void prevBtn_Click(object sender, EventArgs e)
        {
            if (isMonth)
            {
                GVariables.month--;
                if (GVariables.month < 1)
                {
                    GVariables.year--;
                    GVariables.month = 12;
                }
                GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
                GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
                GVariables.day = GVariables.firstOfMonth.Day;
                DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
                GVariables.endOfMonth = DateTime.DaysInMonth(GVariables.year, GVariables.month);
                DateTime.TryParse(GVariables.month + "-" + GVariables.endOfMonth + "-" + GVariables.year, out GVariables.date2);
                appointmentTableAdapter.FillByDate(dataSet1.appointment, GVariables.date1, GVariables.date2);
                monthYearTxt.Text = GVariables.monthNm + " " + GVariables.year;
            }
            else
            {
                GVariables.day = GVariables.day - 8;
                if (GVariables.day < 1)
                {
                    GVariables.month--;
                    if (GVariables.month < 1)
                    {
                        GVariables.year--;
                        GVariables.month = 12;
                    }
                    GVariables.day = DateTime.DaysInMonth(GVariables.year, GVariables.month) - 7;
                }
                GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
                DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
                sevenDay = GVariables.day + 7;
                if (sevenDay < 1)
                {
                    sevenDay = DateTime.DaysInMonth(GVariables.year, GVariables.month);
                }
                DateTime.TryParse(GVariables.month + "-" + sevenDay + "-" + GVariables.year, out GVariables.date2);
                appointmentTableAdapter.FillByDate(dataSet1.appointment, GVariables.date1, GVariables.date2);
                monthYearTxt.Text = "Days of " + GVariables.day + " - " + sevenDay + " " + GVariables.monthNm + " " + GVariables.year;
            }
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (isMonth)
            {
                GVariables.month++;
                if (GVariables.month > 12)
                {
                    GVariables.year++;
                    GVariables.month = 1;
                }
                GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
                GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
                GVariables.day = GVariables.firstOfMonth.Day;
                DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
                GVariables.endOfMonth = DateTime.DaysInMonth(GVariables.year, GVariables.month);
                DateTime.TryParse(GVariables.month + "-" + GVariables.endOfMonth + "-" + GVariables.year, out GVariables.date2);
                appointmentTableAdapter.FillByDate(dataSet1.appointment, GVariables.date1, GVariables.date2);
                monthYearTxt.Text = GVariables.monthNm + " " + GVariables.year;
            }
            else
            {
                GVariables.day = GVariables.day + 8;
                if(GVariables.day > DateTime.DaysInMonth(GVariables.year, GVariables.month))
                {
                    GVariables.month++;
                    GVariables.day = 1;
                    if(GVariables.month > 12)
                    {
                        GVariables.year++;
                        GVariables.month = 1;
                    }
                }
                GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
                DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
                sevenDay = GVariables.day + 7;
                if(sevenDay > DateTime.DaysInMonth(GVariables.year, GVariables.month))
                {
                    sevenDay = DateTime.DaysInMonth(GVariables.year, GVariables.month);
                }
                DateTime.TryParse(GVariables.month + "-" + sevenDay + "-" + GVariables.year, out GVariables.date2);
                appointmentTableAdapter.FillByDate(dataSet1.appointment, GVariables.date1, GVariables.date2);
                monthYearTxt.Text = "Days of " + GVariables.day + " - " + sevenDay + " " + GVariables.monthNm + " " + GVariables.year;
            }
            
        }

        private void todayBtn_Click(object sender, EventArgs e)
        {
            if (isMonth)
            {
                GVariables.nowDate = DateTime.Now;
                GVariables.month = GVariables.nowDate.Month;
                GVariables.year = GVariables.nowDate.Year;
                GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
                GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
                GVariables.day = GVariables.firstOfMonth.Day;
                DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
                GVariables.endOfMonth = DateTime.DaysInMonth(GVariables.year, GVariables.month);
                DateTime.TryParse(GVariables.month + "-" + GVariables.endOfMonth + "-" + GVariables.year, out GVariables.date2);
                appointmentTableAdapter.FillByDate(dataSet1.appointment, GVariables.date1, GVariables.date2);
                monthYearTxt.Text = GVariables.monthNm + " " + GVariables.year;
            }
            else
            {
                GVariables.nowDate = DateTime.Now;
                GVariables.month = GVariables.nowDate.Month;
                GVariables.year = GVariables.nowDate.Year;
                GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
                GVariables.day = GVariables.nowDate.Day;
                DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
                sevenDay = GVariables.day + 7;
                if (sevenDay > DateTime.DaysInMonth(GVariables.year, GVariables.month))
                {
                    sevenDay = DateTime.DaysInMonth(GVariables.year, GVariables.month);
                }
                DateTime.TryParse(GVariables.month + "-" + sevenDay + "-" + GVariables.year, out GVariables.date2);
                appointmentTableAdapter.FillByDate(dataSet1.appointment, GVariables.date1, GVariables.date2);
                monthYearTxt.Text = "Days of " + GVariables.day + " - " + sevenDay + " " + GVariables.monthNm + " " + GVariables.year;
            }
            
        }

        private void searchTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (nameRadio.Checked)
            {
                customerInformationTableAdapter.FillByName(dataSet1.customerInformation, '%' + searchTxtBox.Text + '%');
            }
            else if (addressRadio.Checked)
            {
                customerInformationTableAdapter.FillByAddress(dataSet1.customerInformation, '%' + searchTxtBox.Text + '%');
            }else if (cityRadio.Checked)
            {
                customerInformationTableAdapter.FillByCity(dataSet1.customerInformation, '%' + searchTxtBox.Text + '%');
            }else if (phoneRadio.Checked)
            {
                customerInformationTableAdapter.FillByPhone(dataSet1.customerInformation, '%' + searchTxtBox.Text + '%');
            }
        }

        private void customerInformationDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(customerInformationDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() != null)
            {
                customerIdTextBox.Text = customerInformationDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void updateCusBtn_Click(object sender, EventArgs e)
        {
            Customers newCustomers = new Customers();
            newCustomers.ShowDialog();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------

        // Check values and inputs -----------------------------------------------------------------------------------------------------------------
        private bool CheckInputs()
        {
            if (customerIdTextBox.Text != "" && titleTextBox.Text != "" && descriptionTextBox.Text != ""
                && locationTextBox.Text != "" && contactComboBox.SelectedIndex > -1 && typeComboBox.SelectedIndex > -1 && urlTextBox.Text != "")
            {
                return true;
            }
            else
            {
                if (customerIdTextBox.Text == "")
                {
                    customerIdTextBox.BackColor = Color.Coral;
                }
                if (titleTextBox.Text == "")
                {
                    titleTextBox.BackColor = Color.Coral;
                }
                if (descriptionTextBox.Text == "")
                {
                    descriptionTextBox.BackColor = Color.Coral;
                }
                if (locationTextBox.Text == "")
                {
                    locationTextBox.BackColor = Color.Coral;
                }
                if (contactComboBox.SelectedIndex == -1)
                {
                    contactComboBox.BackColor = Color.Coral;
                }
                if (typeComboBox.SelectedIndex == -1)
                {
                    typeComboBox.BackColor = Color.Coral;
                }
                if (urlTextBox.Text == "")
                {
                    urlTextBox.BackColor = Color.Coral;
                }
                MessageBox.Show(Properties.Resources.Fieldsincomplete, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool DateIsFuture()
        {
            if(startDateTimePicker.Value > DateTime.Now && endDateTimePicker.Value > DateTime.Now)
            {
                return true;
            }
            else
            {
                MessageBox.Show(Properties.Resources.CannotBookInPast, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                startDateTimePicker.CalendarMonthBackground = Color.Coral;
                endDateTimePicker.CalendarMonthBackground = Color.Coral;
                return false;
            }
        }

        private bool DateNotTaken()
        {
            DateTime date;
            DateTime.TryParse(startDateTimePicker.Value.ToString(), out date);
            DateTime endDate;
            DateTime.TryParse(endDateTimePicker.Value.ToString(), out endDate);

            if (dataSet1.appointment.Any(a => a.start >= date && a.start <= endDate) == true && dataSet1.appointment.Any(a => a.contact == contactComboBox.Text) == true)
            {
                MessageBox.Show(Properties.Resources.DateTaken, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                startDateTimePicker.CalendarMonthBackground = Color.Coral;
                contactComboBox.BackColor = Color.Coral;
                return false;
            }else if(dataSet1.appointment.Any(a => a.end >= date && a.end <= endDate) == true && dataSet1.appointment.Any(a => a.contact == contactComboBox.Text) == true)
            {
                MessageBox.Show(Properties.Resources.DateTaken, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                endDateTimePicker.CalendarMonthBackground = Color.Coral;
                contactComboBox.BackColor = Color.Coral;
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool InsideWorkHours()
        {
            DateTime time = startDateTimePicker.Value;
            DateTime time2 = endDateTimePicker.Value;
            if (time.Hour < 9 && time.Hour > 17)
            {
                MessageBox.Show(Properties.Resources.OutsideHours, Properties.Resources.Closed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                startDateTimePicker.CalendarMonthBackground = Color.Coral;
                return false;
            }else if(time2.Hour < 9 && time2.Hour > 17)
            {
                MessageBox.Show(Properties.Resources.OutsideHours, Properties.Resources.Closed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                endDateTimePicker.CalendarMonthBackground = Color.Coral;
                return false;
            }
            else
            {
                return true;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        // buttons to change between month and week views
        private void monthBtn_Click(object sender, EventArgs e)
        {
            isMonth = true;
            nextBtn.Text = Properties.Resources.NextM;
            prevBtn.Text = Properties.Resources.PrevM;
            GVariables.nowDate = DateTime.Now;
            GVariables.month = GVariables.nowDate.Month;
            GVariables.year = GVariables.nowDate.Year;
            GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
            GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
            GVariables.day = GVariables.firstOfMonth.Day;
            DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
            GVariables.endOfMonth = DateTime.DaysInMonth(GVariables.year, GVariables.month);
            DateTime.TryParse(GVariables.month + "-" + GVariables.endOfMonth + "-" + GVariables.year, out GVariables.date2);
            appointmentTableAdapter.FillByDate(dataSet1.appointment, GVariables.date1, GVariables.date2);
            monthYearTxt.Text = GVariables.monthNm + " " + GVariables.year;
        }

        private void weekBtn_Click(object sender, EventArgs e)
        {
            isMonth = false;
            nextBtn.Text = Properties.Resources.NextW;
            prevBtn.Text = Properties.Resources.PrevW;
            GVariables.nowDate = DateTime.Now;
            GVariables.month = GVariables.nowDate.Month;
            GVariables.year = GVariables.nowDate.Year;
            GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
            GVariables.day = GVariables.nowDate.Day;
            DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
            sevenDay = GVariables.day + 7;
            if (sevenDay > DateTime.DaysInMonth(GVariables.year, GVariables.month))
            {
                sevenDay = DateTime.DaysInMonth(GVariables.year, GVariables.month);
            }
            DateTime.TryParse(GVariables.month + "-" + sevenDay + "-" + GVariables.year, out GVariables.date2);
            appointmentTableAdapter.FillByDate(dataSet1.appointment, GVariables.date1, GVariables.date2);
            monthYearTxt.Text = "Days of " + GVariables.day + " - " + sevenDay + " " + GVariables.monthNm + " " + GVariables.year;
        }
    }
}
