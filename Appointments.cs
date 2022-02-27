using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969Scheduler
{
    class Appointments
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        // Constructor
        public Appointments(int _appointmentId, int _customerId, int _userId, string _title, string _description, string _location, string _contact, string _type, string _url, 
            DateTime _start, DateTime _end, DateTime _createDate, string _createdBy, DateTime _lastUpdate, string _lastUpdateBy)
        {
            AppointmentId = _appointmentId;
            CustomerId = _customerId;
            UserId = _userId;
            Title = _title;
            Description = _description;
            Location = _location;
            Contact = _contact;
            Type = _type;
            Url = _url;
            Start = _start;
            End = _end;
            CreateDate = _createDate;
            CreatedBy = _createdBy;
            LastUpdate = _lastUpdate;
            LastUpdateBy = _lastUpdateBy;
        }

        
    }
}
