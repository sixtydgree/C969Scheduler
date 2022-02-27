using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969Scheduler
{
    class Collections
    {
        public List<Appointments> appointmentsByConsultant = new List<Appointments>();
        public List<Appointments> typeCountByMonth = new List<Appointments>();
        public List<CustomerByCountry> customerCountByCountry = new List<CustomerByCountry>();

        public void addAppBC(Appointments appointments)
        {
            appointmentsByConsultant.Add(appointments);
            return;
        }

        public void addAppCountByM(Appointments appointments)
        {
            typeCountByMonth.Add(appointments);
            return;
        }

        public void addCusBC(CustomerByCountry customerByCountry)
        {
            customerCountByCountry.Add(customerByCountry);
            return;
        }

        public void emptyCusBC()
        {
            customerCountByCountry.Clear();
            return;
        }

        public void emptyAppBC()
        {
            appointmentsByConsultant.Clear();
        }

        public void emptyAppByM()
        {
            typeCountByMonth.Clear();
        }
    }
}
