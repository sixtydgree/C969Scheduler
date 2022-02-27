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
    public partial class CustomerCount : UserControl
    {

        Collections cusCountCollection = new Collections();
        public CustomerCount()
        {
            InitializeComponent();
        }

        private void CustomerCount_Load(object sender, EventArgs e)
        {
            
            customerTableAdapter.Fill(dataSet1.customer);
            addressTableAdapter.Fill(dataSet1.address);
            cityTableAdapter.Fill(dataSet1.city);
            countryTableAdapter.Fill(dataSet1.country);
            addToCollection();
            FillCustomerCount();
        }


        private void FillCustomerCount()
        {
            dataGridView1.DataSource = cusCountCollection.customerCountByCountry.ToList();
        }

        private void addToCollection()
        {
            // query required data for the datagrid
            var customerCount = from c in dataSet1.customer
                                join d in dataSet1.address
                                on c.addressId equals d.addressId
                                join f in dataSet1.city
                                on d.cityId equals f.cityId
                                join e in dataSet1.country
                                on f.countryId equals e.countryId
                                group c.customerName.Count() by e.country into g
                                orderby g.Key
                                select new { CustomerCount = g.Count(), Country = g.Key };
            

            // put data into the collection and fill the datagrid
            if (cusCountCollection.customerCountByCountry.Count() > 0)
            {
                cusCountCollection.emptyCusBC();
            }
            for (int i = 0; i < customerCount.Count(); i++)
            {
                CustomerByCountry customer = new CustomerByCountry(customerCount.ElementAt(i).CustomerCount, customerCount.ElementAt(i).Country);
                cusCountCollection.addCusBC(customer);
            }
        }
    }
}
