using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineInfo
{
    class Passenger
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public Nationality Nationality { get; set; }
        public string Passport { get; set; }
        public DateTime Birthday { get; set; }
        public Sex Sex { get; set; }       
        public Ticket Ticket { get; set; }    

    }
}
