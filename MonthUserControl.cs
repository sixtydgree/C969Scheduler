using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace C969Scheduler
{
    public partial class MonthUserControl : UserControl
    {
        private bool isMonth;
        private int sevenDay;
        private bool isUpdate;
        private Appointments newApp;
        public MonthUserControl()
        {
            InitializeComponent();
        }

        // Row select behaviours -----------------------------------------------------------------------------------------------------------------------

        private void appointmentDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (appointmentDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString() != null)
            {
                int.TryParse(appointmentDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString(), out GVariables.customerId);
                customerInformationTableAdapter.FillById(dataSet1.customerInformation, GVariables.customerId);
                appointmentIdTextBox.Text = appointmentDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                userIdTextBox.Text = appointmentDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                titleTextBox.Text = appointmentDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                descriptionTextBox.Text = appointmentDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                locationTextBox.Text = appointmentDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                contactComboBox.Text = appointmentDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                typeComboBox.Text = appointmentDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                urlTextBox.Text = appointmentDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
                DateTime date, date0, date1, date2;
                DateTime.TryParse(appointmentDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString(), out date);
                startDateTimePicker.Value = date;
                DateTime.TryParse(appointmentDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString(), out date0);
                endDateTimePicker.Value = date0;
                DateTime.TryParse(appointmentDataGridView.Rows[e.RowIndex].Cells[11].Value.ToString(), out date1);
                createDateDateTimePicker.Value = date1;
                createdByTextBox.Text = appointmentDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString();
                DateTime.TryParse(appointmentDataGridView.Rows[e.RowIndex].Cells[13].Value.ToString(), out date2);
                lastUpdateDateTimePicker.Value = date2;
                lastUpdateByTextBox.Text = appointmentDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString();

            }
        }

        private void customerInformationDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (customerInformationDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() != null)
            {
                customerIdTextBox.Text = customerInformationDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        // Enable or disable buttons when needed --------------------------------------------------------------------------------------------------------
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
        //----------------------------------------------------------------------------------------------------------------------------------------------------------

        //correct input colors -------------------------------------------------------------------------------------------------------------------------------------
        private void ReturnInputColors()
        {
            customerIdTextBox.BackColor = Color.White;
            titleTextBox.BackColor = Color.White;
            descriptionTextBox.BackColor = Color.White;
            locationTextBox.BackColor = Color.White;
            contactComboBox.BackColor = Color.White;
            typeComboBox.BackColor = Color.White;
            urlTextBox.BackColor = Color.White;
            startDateTimePicker.BackColor = Color.White;
            endDateTimePicker.BackColor = Color.White;
        }


        //----------------------------------------------------------------------------------------------------------------------------------------------------------

        // Load properties and dates -------------------------------------------------------------------------------------------------------------------------------
        private void MonthUserControl_Load(object sender, EventArgs e)
        {
            appointmentTableAdapter.Fill(dataSet1.appointment);
            customerTableAdapter.Fill(dataSet1.customer);
            label3.Text = Properties.Resources.Current + " " + GVariables.userName;
            isMonth = true;
            isUpdate = false;
            
            GVariables.type = new List<string>();
            GVariables.contact = new List<string>();
            customerInformationTableAdapter.Fill(dataSet1.customerInformation);
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



            TimeZone timeZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOffset = timeZone.GetUtcOffset(DateTime.Now);
            GVariables.nowDate = DateTime.Now;
            GVariables.month = GVariables.nowDate.Month;
            GVariables.year = GVariables.nowDate.Year;
            GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
            GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
            GVariables.day = GVariables.firstOfMonth.Day;
            DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
            int mnth = GVariables.month;
            GVariables.nextMonth = ++mnth;
            DateTime.TryParse(GVariables.nextMonth + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date2);
            //appointmentTableAdapter.FillByDate(dataSet1.appointment, GVariables.date1, GVariables.date2);

            // create and sort data for the datagrid ---------------------------------------------------------
            Collections appointments = new Collections();
            var apps = from a in dataSet1.appointment
                       select a;

            foreach (var a in apps)
            {
                newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                    a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                appointments.addAppBC(newApp);
            }

            var monthApps = from a in appointments.appointmentsByConsultant
                            where a.Start > GVariables.date1 && a.Start < GVariables.date2
                            select new {a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

            appointmentDataGridView.DataSource = monthApps.ToList();
            //-------------------------------------------------------------------------------------------------------------

            monthYearTxt.Text = GVariables.monthNm + " " + GVariables.year;
            this.saveBtn.Enabled = false;
            this.cancelBtn.Enabled = false;
            this.appointmentDataGridView.Enabled = true;
            this.groupBox1.Enabled = false;
            this.groupBox3.Enabled = false;
            appointmentDataGridView.ClearSelection();
            foreach(string type in GVariables.type)
            {
                typeComboBox.Items.Add(type);
            }
            foreach(string contact in GVariables.contact)
            {
                contactComboBox.Items.Add(contact);
            }
            AppointmentReminder();
            timer1.Start();
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        // add, update, delete buttons ------------------------------------------------------------------------------------------------------------------
        private void addBtn_Click(object sender, EventArgs e)
        {
            appointmentIdTextBox.Text = "";
            customerInformationTableAdapter.Fill(dataSet1.customerInformation);
            EnableDisableBtns();
            //add new appointment
            appointmentBindingSource.AddNew();
            userIdTextBox.Text = GVariables.userId.ToString();
            lastUpdateByTextBox.Text = GVariables.userName;
            createdByTextBox.Text = GVariables.userName;
            createDateDateTimePicker.Value = DateTime.UtcNow;
            lastUpdateDateTimePicker.Value = DateTime.UtcNow;
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
            isUpdate = true;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if(appointmentDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show(Properties.Resources.NoAppointmentSelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(MessageBox.Show(Properties.Resources.AreYouSure, Properties.Resources.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string appId = appointmentIdTextBox.Text;
                string sql = "DELETE FROM `appointment` WHERE (`appointmentId` = '" + appId + "');";
                try
                {
                    DbAccess dbAccess = new DbAccess();
                    dbAccess.Open();
                    dbAccess.ExecuteNonQuery(sql);
                    dbAccess.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Properties.Resources.NothingDeleted);
                    return;
                }
                MessageBox.Show(Properties.Resources.AppDeleted);
                appointmentTableAdapter.Fill(dataSet1.appointment);
                Collections appointments = new Collections();
                appointments.emptyAppBC();
                var apps = from a in dataSet1.appointment
                           select a;

                foreach (var a in apps)
                {
                    Appointments newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                         a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                    appointments.addAppBC(newApp);
                }

                var monthApps = from a in appointments.appointmentsByConsultant
                                where a.Start > GVariables.date1 && a.Start < GVariables.date2
                                select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

                appointmentDataGridView.DataSource = monthApps.ToList();
            }
            

        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // Save and cancel buttons ----------------------------------------------------------------------------------------------------------------------------------
        private void saveBtn_Click(object sender, EventArgs e)
        {
            if(isUpdate == true)
            {
                
                if(CheckInputs() == true && InsideWorkHours() == true)
                {
                    EnableDisableBtns();
                    ReturnInputColors();
                    int _appId, _cusId, _userId;
                    int.TryParse(appointmentIdTextBox.Text, out _appId);
                    int.TryParse(customerIdTextBox.Text, out _cusId);
                    int.TryParse(userIdTextBox.Text, out _userId);
                    
                    string _start, _end, _crD, _laU;
                    _start = startDateTimePicker.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    _end = endDateTimePicker.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    _crD = createDateDateTimePicker.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    _laU = lastUpdateDateTimePicker.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    
                    DbAccess dbAccess = new DbAccess();
                    dbAccess.Open();
                    string sql = "UPDATE `appointment` SET `customerId` = '" + customerIdTextBox.Text + "', `userId` = '" + userIdTextBox.Text + "', `title` = '" + titleTextBox.Text + "', `description` = '" + descriptionTextBox.Text + "', `location` = '" + locationTextBox.Text + "', `contact` = '" + contactComboBox.Text + "', " +
                        "`type` = '" + typeComboBox.Text + "', `url` = '" + urlTextBox.Text + "', `start` = '" + _start + "', `end` = '" + _end + "', `createDate` = '" + _crD + "'" +
                        ", `createdBy` = '" + createdByTextBox.Text + "', `lastUpdate` = '" + _laU + "', `lastUpdateBy` = '" + lastUpdateByTextBox.Text + "'  WHERE (`appointmentId` = '" + _appId + "');";
                    dbAccess.ExecuteNonQuery(sql);
                    dbAccess.Close();

                    
                    appointmentTableAdapter.Fill(dataSet1.appointment);
                    Collections appointments = new Collections();
                    appointments.emptyAppBC();
                    var apps = from a in dataSet1.appointment
                               select a;

                    foreach (var a in apps)
                    {
                       Appointments newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                            a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                        appointments.addAppBC(newApp);
                    }

                    var monthApps = from a in appointments.appointmentsByConsultant
                                    where a.Start > GVariables.date1 && a.Start < GVariables.date2
                                    select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

                    appointmentDataGridView.DataSource = monthApps.ToList();
                }
            }
            else
            {
                if (DateIsFuture() == true && DateNotTaken() == true && CheckInputs() == true && InsideWorkHours() == true)
                {
                    EnableDisableBtns();
                    ReturnInputColors();
                    // save changed or new appointment
                    startDateTimePicker.Value = startDateTimePicker.Value.ToUniversalTime();
                    endDateTimePicker.Value = endDateTimePicker.Value.ToUniversalTime();

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

                    appointmentTableAdapter.Fill(dataSet1.appointment);
                    Collections appointments = new Collections();
                    var apps = from a in dataSet1.appointment
                               select a;

                    foreach (var a in apps)
                    {
                        newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                            a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                        appointments.addAppBC(newApp);
                    }

                    var monthApps = from a in appointments.appointmentsByConsultant
                                    where a.Start > GVariables.date1 && a.Start < GVariables.date2
                                    select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

                    appointmentDataGridView.DataSource = monthApps.ToList();

                }
            }
            
            
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            EnableDisableBtns();
            ReturnInputColors();
            // cancel changes to appointments
            dataSet1.appointment.RejectChanges();
            appointmentBindingSource.CancelEdit();
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------

        

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
                TimeZone timeZone = TimeZone.CurrentTimeZone;
                TimeSpan currentOffset = timeZone.GetUtcOffset(DateTime.Now);
                GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
                GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
                GVariables.day = GVariables.firstOfMonth.Day;
                DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
                int mnth = GVariables.month;
                GVariables.nextMonth = ++mnth;
                DateTime.TryParse(GVariables.nextMonth + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date2);
                //appointmentTableAdapter.FillByDate(dataSet1.appointment, currentOffset.ToString().Substring(0, 6), GVariables.date1, GVariables.date2);

                // create and sort data for the datagrid ---------------------------------------------------------
                appointmentTableAdapter.Fill(dataSet1.appointment);
                Collections appointments = new Collections();
                var apps = from a in dataSet1.appointment
                           select a;

                foreach (var a in apps)
                {
                    newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                        a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                    appointments.addAppBC(newApp);
                }

                var monthApps = from a in appointments.appointmentsByConsultant
                                where a.Start > GVariables.date1 && a.Start < GVariables.date2
                                select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

                appointmentDataGridView.DataSource = monthApps.ToList();
                //-------------------------------------------------------------------------------------------------------------

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
                TimeZone timeZone = TimeZone.CurrentTimeZone;
                TimeSpan currentOffset = timeZone.GetUtcOffset(DateTime.Now);
                DateTime.TryParse(GVariables.month + "-" + sevenDay + "-" + GVariables.year, out GVariables.date2);
                //appointmentTableAdapter.FillByDate(dataSet1.appointment, currentOffset.ToString().Substring(0, 6), GVariables.date1, GVariables.date2);

                // create and sort data for the datagrid ---------------------------------------------------------
                appointmentTableAdapter.Fill(dataSet1.appointment);
                Collections appointments = new Collections();
                var apps = from a in dataSet1.appointment
                           select a;

                foreach (var a in apps)
                {
                    newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                        a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                    appointments.addAppBC(newApp);
                }

                var monthApps = from a in appointments.appointmentsByConsultant
                                where a.Start > GVariables.date1 && a.Start < GVariables.date2
                                select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

                appointmentDataGridView.DataSource = monthApps.ToList();
                //-------------------------------------------------------------------------------------------------------------

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
                TimeZone timeZone = TimeZone.CurrentTimeZone;
                TimeSpan currentOffset = timeZone.GetUtcOffset(DateTime.Now);
                GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
                GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
                GVariables.day = GVariables.firstOfMonth.Day;
                DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
                int mnth = GVariables.month;
                GVariables.nextMonth = ++mnth;
                DateTime.TryParse(GVariables.nextMonth + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date2);
                //appointmentTableAdapter.FillByDate(dataSet1.appointment, currentOffset.ToString().Substring(0, 6), GVariables.date1, GVariables.date2);

                // create and sort data for the datagrid ---------------------------------------------------------
                appointmentTableAdapter.Fill(dataSet1.appointment);
                Collections appointments = new Collections();
                var apps = from a in dataSet1.appointment
                           select a;

                foreach (var a in apps)
                {
                    newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                        a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                    appointments.addAppBC(newApp);
                }

                var monthApps = from a in appointments.appointmentsByConsultant
                                where a.Start > GVariables.date1 && a.Start < GVariables.date2
                                orderby a.Start
                                select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

                appointmentDataGridView.DataSource = monthApps.ToList();
                //-------------------------------------------------------------------------------------------------------------

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
                TimeZone timeZone = TimeZone.CurrentTimeZone;
                TimeSpan currentOffset = timeZone.GetUtcOffset(DateTime.Now);
                DateTime.TryParse(GVariables.month + "-" + sevenDay + "-" + GVariables.year, out GVariables.date2);
                //appointmentTableAdapter.FillByDate(dataSet1.appointment, currentOffset.ToString().Substring(0, 6), GVariables.date1, GVariables.date2);

                // create and sort data for the datagrid ---------------------------------------------------------
                appointmentTableAdapter.Fill(dataSet1.appointment);
                Collections appointments = new Collections();
                var apps = from a in dataSet1.appointment
                           select a;

                foreach (var a in apps)
                {
                    newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                        a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                    appointments.addAppBC(newApp);
                }

                var monthApps = from a in appointments.appointmentsByConsultant
                                where a.Start > GVariables.date1 && a.Start < GVariables.date2
                                orderby a.Start
                                select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

                appointmentDataGridView.DataSource = monthApps.ToList();
                //-------------------------------------------------------------------------------------------------------------


                monthYearTxt.Text = "Days of " + GVariables.day + " - " + sevenDay + " " + GVariables.monthNm + " " + GVariables.year;
            }
            
        }

        private void todayBtn_Click(object sender, EventArgs e)
        {
            if (isMonth)
            {
                TimeZone timeZone = TimeZone.CurrentTimeZone;
                TimeSpan currentOffset = timeZone.GetUtcOffset(DateTime.Now);
                GVariables.nowDate = DateTime.Now;
                GVariables.month = GVariables.nowDate.Month;
                GVariables.year = GVariables.nowDate.Year;
                GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
                GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
                GVariables.day = GVariables.firstOfMonth.Day;
                DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
                int mnth = GVariables.month;
                GVariables.nextMonth = ++mnth;
                DateTime.TryParse(GVariables.nextMonth + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date2);
                //appointmentTableAdapter.FillByDate(dataSet1.appointment, currentOffset.ToString().Substring(0, 6), GVariables.date1, GVariables.date2);

                // create and sort data for the datagrid ---------------------------------------------------------
                appointmentTableAdapter.Fill(dataSet1.appointment);
                Collections appointments = new Collections();
                var apps = from a in dataSet1.appointment
                           select a;

                foreach (var a in apps)
                {
                   newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                        a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                    appointments.addAppBC(newApp);
                }

                var monthApps = from a in appointments.appointmentsByConsultant
                                where a.Start > GVariables.date1 && a.Start < GVariables.date2
                                orderby a.Start
                                select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

                appointmentDataGridView.DataSource = monthApps.ToList();
                //-------------------------------------------------------------------------------------------------------------

                monthYearTxt.Text = GVariables.monthNm + " " + GVariables.year;
            }
            else
            {
                TimeZone timeZone = TimeZone.CurrentTimeZone;
                TimeSpan currentOffset = timeZone.GetUtcOffset(DateTime.Now);
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
                //appointmentTableAdapter.FillByDate(dataSet1.appointment, currentOffset.ToString().Substring(0, 6), GVariables.date1, GVariables.date2);

                // create and sort data for the datagrid ---------------------------------------------------------
                appointmentTableAdapter.Fill(dataSet1.appointment);
                Collections appointments = new Collections();
                var apps = from a in dataSet1.appointment
                           select a;

                foreach (var a in apps)
                {
                    newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                        a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                    appointments.addAppBC(newApp);
                }

                var monthApps = from a in appointments.appointmentsByConsultant
                                where a.Start > GVariables.date1 && a.Start < GVariables.date2
                                orderby a.Start
                                select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

                appointmentDataGridView.DataSource = monthApps.ToList();
                //-------------------------------------------------------------------------------------------------------------

                monthYearTxt.Text = "Days of " + GVariables.day + " - " + sevenDay + " " + GVariables.monthNm + " " + GVariables.year;
            }
            
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------

        // search box behaviour -------------------------------------------------------------------------------------------------------------------
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
        //------------------------------------------------------------------------------------------------------------------------------------------
        
        //view customers ---------------------------------------------------------------------------------------------------------------------------
        // this button no longer updates customers. It now loads the customers form.
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
                startDateTimePicker.BackColor = Color.Coral;
                endDateTimePicker.BackColor = Color.Coral;
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
                startDateTimePicker.BackColor = Color.Coral;
                contactComboBox.BackColor = Color.Coral;
                return false;
            }else if(dataSet1.appointment.Any(a => a.end >= date && a.end <= endDate) == true && dataSet1.appointment.Any(a => a.contact == contactComboBox.Text) == true)
            {
                MessageBox.Show(Properties.Resources.DateTaken, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                endDateTimePicker.BackColor = Color.Coral;
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
                startDateTimePicker.BackColor = Color.Coral;
                return false;
            }else if(time2.Hour < 9 && time2.Hour > 17)
            {
                MessageBox.Show(Properties.Resources.OutsideHours, Properties.Resources.Closed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                endDateTimePicker.BackColor = Color.Coral;
                return false;
            }
            else
            {
                return true;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        // buttons to change between month and week views -----------------------------------------------------------------------------------------------
        private void monthBtn_Click(object sender, EventArgs e)
        {
            appointmentTableAdapter.Fill(dataSet1.appointment);
            isMonth = true;
            nextBtn.Text = Properties.Resources.NextM;
            prevBtn.Text = Properties.Resources.PrevM;
            TimeZone timeZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOffset = timeZone.GetUtcOffset(DateTime.Now);
            GVariables.nowDate = DateTime.Now;
            GVariables.month = GVariables.nowDate.Month;
            GVariables.year = GVariables.nowDate.Year;
            GVariables.firstOfMonth = new DateTime(GVariables.year, GVariables.month, 1);
            GVariables.monthNm = DateTimeFormatInfo.CurrentInfo.GetMonthName(GVariables.month);
            GVariables.day = GVariables.firstOfMonth.Day;
            DateTime.TryParse(GVariables.month + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date1);
            int mnth = GVariables.month;
            GVariables.nextMonth = ++mnth;
            DateTime.TryParse(GVariables.nextMonth + "-" + GVariables.day + "-" + GVariables.year, out GVariables.date2);
            //appointmentTableAdapter.FillByDate(dataSet1.appointment, currentOffset.ToString().Substring(0, 6), GVariables.date1, GVariables.date2);

            // create and sort data for the datagrid ---------------------------------------------------------
            Collections appointments = new Collections();
            var apps = from a in dataSet1.appointment
                       select a;

            foreach (var a in apps)
            {
                newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                    a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                appointments.addAppBC(newApp);
            }

            var monthApps = from a in appointments.appointmentsByConsultant
                            where a.Start > GVariables.date1 && a.Start < GVariables.date2
                            orderby a.Start
                            select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

            appointmentDataGridView.DataSource = monthApps.ToList();
            //-------------------------------------------------------------------------------------------------------------

            monthYearTxt.Text = GVariables.monthNm + " " + GVariables.year;
        }

        private void weekBtn_Click(object sender, EventArgs e)
        {
            appointmentTableAdapter.Fill(dataSet1.appointment);
            isMonth = false;
            nextBtn.Text = Properties.Resources.NextW;
            prevBtn.Text = Properties.Resources.PrevW;
            TimeZone timeZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOffset = timeZone.GetUtcOffset(DateTime.Now);
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
            //appointmentTableAdapter.FillByDate(dataSet1.appointment, currentOffset.ToString().Substring(0, 6), GVariables.date1, GVariables.date2);

            // create and sort data for the datagrid ---------------------------------------------------------
            Collections appointments = new Collections();
            var apps = from a in dataSet1.appointment
                       select a;

            foreach (var a in apps)
            {
                newApp = new Appointments(a.appointmentId, a.customerId, a.userId, a.title, a.description, a.location, a.contact, a.type, a.url, a.start.ToLocalTime(), a.end.ToLocalTime(), a.createDate.ToLocalTime(), a.createdBy,
                    a.lastUpdate.ToLocalTime(), a.lastUpdateBy);
                appointments.addAppBC(newApp);
            }

            var monthApps = from a in appointments.appointmentsByConsultant
                            where a.Start > GVariables.date1 && a.Start < GVariables.date2
                            orderby a.Start
                            select new { a.AppointmentId, a.CustomerId, a.UserId, a.Title, a.Description, a.Location, a.Contact, a.Type, a.Url, a.Start, a.End, a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy };

            appointmentDataGridView.DataSource = monthApps.ToList();
            //-------------------------------------------------------------------------------------------------------------

            monthYearTxt.Text = "Days of " + GVariables.day + " - " + sevenDay + " " + GVariables.monthNm + " " + GVariables.year;
        }

        private void reportsBtn_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.ShowDialog();
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        // Appointment reminder -------------------------------------------------------------------------------------------------------------------------
        private void AppointmentReminder()
        {
            appointmentTableAdapter.Fill(dataSet1.appointment);
            var appDetails = from c in dataSet1.customer
                             join d in dataSet1.appointment
                             on c.customerId equals d.customerId
                             where d.start > DateTime.UtcNow
                             orderby d.start
                             select new { CustomerName = c.customerName, Consultant = d.contact, Time = d.start };
            if (appDetails.Any())
            {
                string time = appDetails.FirstOrDefault().Time.ToLocalTime().ToString();
                string cusName = appDetails.FirstOrDefault().CustomerName;
                string consultant = appDetails.FirstOrDefault().Consultant;
                MessageBox.Show(consultant + Properties.Resources.AppWith + cusName
                            + Properties.Resources.at + time);
            }
            


            
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        // 15 minute reminder ---------------------------------------------------------------------------------------------------------------------------------
        private void FifteenMinuteReminder()
        {

            var appDetails = from c in dataSet1.customer
                             join d in dataSet1.appointment
                             on c.customerId equals d.customerId
                             where d.start > DateTime.UtcNow
                             orderby d.start
                             select new { CustomerName = c.customerName, Consultant = d.contact, Time = d.start.TimeOfDay };
            if (appDetails.Any())
            {
                string timeStr = appDetails.FirstOrDefault().Time.ToString();
                string cusName = appDetails.FirstOrDefault().CustomerName;
                string consultant = appDetails.FirstOrDefault().Consultant;
                DateTime time = DateTime.UtcNow;
                DateTime xTime = time.AddMinutes(15);
                DateTime vTime = time.AddMinutes(17);

                if (appDetails.FirstOrDefault().Time >= xTime.TimeOfDay && appDetails.FirstOrDefault().Time <= vTime.TimeOfDay)
                {
                    MessageBox.Show(consultant + Properties.Resources.AppWith + cusName
                                + Properties.Resources.at + timeStr);
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FifteenMinuteReminder();
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
