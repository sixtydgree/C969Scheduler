using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969Scheduler
{
    class CustomerByCountry
    {
        public int Count { get; set; }
        public string Country { get; set; }

        // Constructor

        public CustomerByCountry(int _count, string _country)
        {
            Count = _count;
            Country = _country;
        }
    }
}
