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
    public partial class AddAddress : Form
    {
        private bool isAdd, isCity, isCountry;

        public AddAddress()
        {
            InitializeComponent();
        }

        private void addressBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.addressBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet1);

        }

        

        private void AddAddress_Load(object sender, EventArgs e)
        {
            isAdd = false;
            isCity = false;
            isCountry = false;
            // TODO: This line of code loads data into the 'dataSet1.country' table. You can move, or remove it, as needed.
            this.countryTableAdapter.Fill(this.dataSet1.country);
            // TODO: This line of code loads data into the 'dataSet1.city' table. You can move, or remove it, as needed.
            this.cityTableAdapter.Fill(this.dataSet1.city);
            // TODO: This line of code loads data into the 'dataSet1.address' table. You can move, or remove it, as needed.
            this.addressTableAdapter.Fill(this.dataSet1.address);

        }

        

        private void closeBtn_Click(object sender, EventArgs e)
        {
            isAdd = false;
            isCity = false;
            isCountry = false;
            dataSet1.address.RejectChanges();
            addressBindingSource.CancelEdit();
            dataSet1.city.RejectChanges();
            cityBindingSource.CancelEdit();
            dataSet1.country.RejectChanges();
            countryBindingSource.CancelEdit();
            this.Hide();
        }

        

        private void EnableDisableBtns()
        {
            if(addAddressBtn.Enabled && addCityBtn.Enabled && addCountryBtn.Enabled)
            {
                saveBtn.Enabled = true;
                cancelBtn.Enabled = true;
                addAddressBtn.Enabled = false;
                addCountryBtn.Enabled = false;
                addCityBtn.Enabled = false;
                updateAddBtn.Enabled = false;
                updateCityBtn.Enabled = false;
                updateCountryBtn.Enabled = false;
                deleteAddBtn.Enabled = false;
                deleteCityBtn.Enabled = false;
                deleteCountryBtn.Enabled = false;
                if (isAdd)
                {
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = true;
                    groupBox5.Enabled = false;
                    countryDataGridView.Enabled = false;
                    cityDataGridView.Enabled = true;
                    addressDataGridView.Enabled = true;
                }
                else if (isCity)
                {
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = false;
                    groupBox5.Enabled = true;
                    addressDataGridView.Enabled = false;
                    countryDataGridView.Enabled = true;
                    cityDataGridView.Enabled = true;
                }else if (isCountry)
                {
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = true;
                    groupBox4.Enabled = false;
                    groupBox5.Enabled = false;
                    countryDataGridView.Enabled = true;
                    cityDataGridView.Enabled = false;
                    addressDataGridView.Enabled = false;
                }
            }
            else
            {
                saveBtn.Enabled = false;
                cancelBtn.Enabled = false;
                addAddressBtn.Enabled = true;
                addCountryBtn.Enabled = true;
                addCityBtn.Enabled = true;
                updateAddBtn.Enabled = true;
                updateCityBtn.Enabled = true;
                updateCountryBtn.Enabled = true;
                deleteAddBtn.Enabled = true;
                deleteCityBtn.Enabled = true;
                deleteCountryBtn.Enabled = true;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
                countryDataGridView.Enabled = true;
                cityDataGridView.Enabled = true;
                addressDataGridView.Enabled = true;
            }
        }

        

        // Appointment Buttons --------------------------------------------------------------------------------------------------------------------------------------
        private void addAddressBtn_Click(object sender, EventArgs e)
        {
            isAdd = true;
            EnableDisableBtns();
            addressBindingSource.AddNew();
            createDateDateTimePicker.Value = DateTime.Now;
            createdByTextBox.Text = GVariables.userName;
            lastUpdateByTextBox.Text = GVariables.userName;
            lastUpdateDateTimePicker.Value = DateTime.Now;
        }


        private void updateAddBtn_Click(object sender, EventArgs e)
        {
            isAdd = true;
            this.cityTableAdapter.Fill(this.dataSet1.city);
            int rows;
            rows = dataSet1.address.Rows.Count;
            if (rows == 0)
            {
                MessageBox.Show(Properties.Resources.NoAddressSelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            EnableDisableBtns();
            lastUpdateByTextBox.Text = GVariables.userName;
            lastUpdateDateTimePicker.Value = DateTime.Now;
        }
                
        private void deleteAddBtn_Click(object sender, EventArgs e)
        {
            int rows;
            rows = dataSet1.address.Rows.Count;
            if (rows == 0)
            {
                MessageBox.Show(Properties.Resources.NoAddressSelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(Properties.Resources.AreYouSureAddress, Properties.Resources.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                addressBindingSource.RemoveCurrent();
                int r;
                r = addressTableAdapter.Update(dataSet1.address);
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
                
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // City Buttons -----------------------------------------------------------------------------------------------------------------------------------------------------------

        private void addCityBtn_Click(object sender, EventArgs e)
        {
            isCity = true;
            EnableDisableBtns();
            cityBindingSource.AddNew();
            createDateDateTimePicker1.Value = DateTime.Now;
            lastUpdateDateTimePicker1.Value = DateTime.Now;
            createdByTextBox1.Text = GVariables.userName;
            lastUpdateByTextBox1.Text = GVariables.userName;
        }


        private void updateCityBtn_Click(object sender, EventArgs e)
        {
            isCity = true;
            countryTableAdapter.Fill(dataSet1.country);
            int rows;
            rows = dataSet1.city.Rows.Count;
            if (rows == 0)
            {
                MessageBox.Show(Properties.Resources.NoCitySelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            EnableDisableBtns();
            lastUpdateByTextBox1.Text = GVariables.userName;
            lastUpdateDateTimePicker1.Value = DateTime.Now;
        }


        private void deleteCityBtn_Click(object sender, EventArgs e)
        {
            int rows;
            rows = dataSet1.city.Rows.Count;
            if(rows == 0)
            {
                MessageBox.Show(Properties.Resources.NoCitySelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(MessageBox.Show(Properties.Resources.AreYouSureCity, Properties.Resources.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cityBindingSource.RemoveCurrent();
                int r;
                r = cityTableAdapter.Update(dataSet1.city);
                if(r > 0)
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


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Country Buttons ----------------------------------------------------------------------------------------------------------------------------------------------------


        private void addCountryBtn_Click(object sender, EventArgs e)
        {
            isCountry = true;
            EnableDisableBtns();
            countryBindingSource.AddNew();
            createDateDateTimePicker2.Value = DateTime.Now;
            lastUpdateDateTimePicker2.Value = DateTime.Now;
            createdByTextBox2.Text = GVariables.userName;
            lastUpdateByTextBox2.Text = GVariables.userName;
        }


        private void updateCountryBtn_Click(object sender, EventArgs e)
        {
            isCountry = true;
            int rows;
            rows = dataSet1.country.Rows.Count;
            if (rows == 0)
            {
                MessageBox.Show(Properties.Resources.NoCountrySelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            EnableDisableBtns();
            lastUpdateByTextBox2.Text = GVariables.userName;
            lastUpdateDateTimePicker2.Value = DateTime.Now;
        }


        private void deleteCountryBtn_Click(object sender, EventArgs e)
        {
            int rows;
            rows = dataSet1.country.Rows.Count;
            if (rows == 0)
            {
                MessageBox.Show(Properties.Resources.NoCountrySelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(Properties.Resources.AreYouSureCountry, Properties.Resources.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                countryBindingSource.RemoveCurrent();
                int r;
                r = countryTableAdapter.Update(dataSet1.country);
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
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Save and Cancel buttons --------------------------------------------------------------------------------------------------------------------------------------------------


        private void saveBtn_Click(object sender, EventArgs e)
        {
            // Complete input checks and save accordingly.
            if (isAdd)
            {
                if (CheckInputs())
                {
                    EnableDisableBtns();
                    addressBindingSource.EndEdit();
                    int r;
                    r = addressTableAdapter.Update(dataSet1.address);
                    if (r > 0)
                    {
                        MessageBox.Show(Properties.Resources.Saved);
                        isAdd = false;
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.NothingSaved);
                        return;
                    }
                }
            }
            else if (isCity)
            {
                if (CheckInputs())
                {
                    EnableDisableBtns();
                    cityBindingSource.EndEdit();
                    int r;
                    r = cityTableAdapter.Update(dataSet1.city);
                    if (r > 0)
                    {
                        MessageBox.Show(Properties.Resources.Saved);
                        isCity = false;
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.NothingSaved);
                        return;
                    }
                }
            }
            else if (isCountry)
            {if (CheckInputs())
                {
                    EnableDisableBtns();
                    countryBindingSource.EndEdit();
                    int r;
                    r = countryTableAdapter.Update(dataSet1.country);
                    if (r > 0)
                    {
                        MessageBox.Show(Properties.Resources.Saved);
                        isCountry = false;
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.NothingSaved);
                        return;
                    }
                }
            }
        }

        private void countryIdLabel1_Click(object sender, EventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            if (isAdd)
            {
                dataSet1.address.RejectChanges();
                addressBindingSource.CancelEdit();
                isAdd = false;
            }
            else if (isCity)
            {
                dataSet1.city.RejectChanges();
                cityBindingSource.CancelEdit();
                isCity = false;
            }
            else if (isCountry)
            {
                dataSet1.country.RejectChanges();
                countryBindingSource.CancelEdit();
                isCountry = false;
            }

            EnableDisableBtns();
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------
        // Check inputs for data -----------------------------------------------------------------------------------------------------------------------------

        private bool CheckInputs()
        {
            if (isAdd)
            {
                if(addressTextBox.Text == "")
                {
                    addressTextBox.BackColor = Color.Coral;
                    MessageBox.Show(Properties.Resources.Fieldsincomplete, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(address2TextBox.Text == "")
                {
                    address2TextBox.BackColor = Color.Coral;
                    MessageBox.Show(Properties.Resources.Address2, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if(cityIdTextBox.Text == "")
                {
                    cityIdTextBox.BackColor = Color.Coral;
                    MessageBox.Show(Properties.Resources.CityId, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(postalCodeTextBox.Text == "")
                {
                    postalCodeTextBox.BackColor = Color.Coral;
                    MessageBox.Show(Properties.Resources.Fieldsincomplete, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(phoneTextBox.Text == "")
                {
                    phoneTextBox.BackColor = Color.Coral;
                    MessageBox.Show(Properties.Resources.Fieldsincomplete, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            else if (isCity)
            {
                if(cityTextBox.Text == "")
                {
                    cityTextBox.BackColor = Color.Coral;
                    MessageBox.Show(Properties.Resources.Fieldsincomplete, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(countryIdTextBox.Text == "")
                {
                    countryIdTextBox.BackColor = Color.Coral;
                    MessageBox.Show(Properties.Resources.CountryId, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            else if (isCountry)
            {
                if(countryTextBox.Text == "")
                {
                    countryTextBox.BackColor = Color.Coral;
                    MessageBox.Show(Properties.Resources.Fieldsincomplete, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}
