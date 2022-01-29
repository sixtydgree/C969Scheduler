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
    public partial class Customers : Form
    {
        public Customers()
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
                saveBtn.Enabled = true;
                cancelBtn.Enabled = true;
                customerDataGridView.Enabled = false;
                addressDataGridView.Enabled = true;
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
            }
            else
            {
                addBtn.Enabled = true;
                updateBtn.Enabled = true;
                deleteBtn.Enabled = true;
                saveBtn.Enabled = false;
                cancelBtn.Enabled = false;
                customerDataGridView.Enabled = true;
                addressDataGridView.Enabled = false;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
            }
        }

        public bool AllFieldsFilled()
        {
            if (customerNameTextBox.Text != "" && addressIdTextBox.Text != "")
            {
                return true;
            }
            else
            {
                if(customerNameTextBox.Text == "")
                {
                    customerNameTextBox.BackColor = Color.Coral;
                }
                if(addressIdTextBox.Text == "")
                {
                    addressIdTextBox.BackColor = Color.Coral;
                }
                MessageBox.Show(Properties.Resources.Fieldsincomplete, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void Customers_Load(object sender, EventArgs e)
        {
            this.customerTableAdapter.Fill(this.dataSet1.customer);
            this.addressTableAdapter.Fill(this.dataSet1.address);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            addressTableAdapter.FillByAddress(dataSet1.address, '%' + textBox1.Text + '%');
        }

        private void customerDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(customerDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() != null)
            {
                int addressId;
                int.TryParse(customerDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out addressId);
                addressTableAdapter.FillById(dataSet1.address, addressId);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            EnableDisableBtns();
            // add new customer
            customerBindingSource.AddNew();
            createDateDateTimePicker.Value = DateTime.Now;
            lastUpdateDateTimePicker.Value = DateTime.Now;
            createdByTextBox.Text = GVariables.userName;
            lastUpdateByTextBox.Text = GVariables.userName;
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            int rows;
            rows = dataSet1.customer.Count;
            if(rows == 0)
            {
                MessageBox.Show(Properties.Resources.NoCustomerSelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            EnableDisableBtns();
            lastUpdateByTextBox.Text = GVariables.userName;
            lastUpdateDateTimePicker.Value = DateTime.Now;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int rows;
            rows = dataSet1.customer.Count;
            if (rows == 0)
            {
                MessageBox.Show(Properties.Resources.NoCustomerSelected, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(MessageBox.Show(Properties.Resources.AreYouSureCustomer, Properties.Resources.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                customerBindingSource.RemoveCurrent();
                int r;
                r = customerTableAdapter.Update(dataSet1.customer);
                if(r > 0)
                {
                    MessageBox.Show(Properties.Resources.CustomerDeleted);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.NothingDeleted);
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if(AllFieldsFilled() == true)
            {
                EnableDisableBtns();
                // save changes
                customerBindingSource.EndEdit();
                int r;
                r = customerTableAdapter.Update(dataSet1.customer);
                if (r > 0)
                {
                    MessageBox.Show(Properties.Resources.Saved);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.NothingSaved);
                }
            }
            
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            EnableDisableBtns();
            dataSet1.customer.RejectChanges();
            customerBindingSource.CancelEdit();
        }

        private void addressDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(addressDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() != null)
            {
                addressIdTextBox.Text = addressDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            dataSet1.customer.RejectChanges();
            customerBindingSource.CancelEdit();
            this.Hide();
        }

        private void addAddressBtn_Click(object sender, EventArgs e)
        {
            AddAddress newAddress = new AddAddress();
            newAddress.ShowDialog();
        }
    }
}
