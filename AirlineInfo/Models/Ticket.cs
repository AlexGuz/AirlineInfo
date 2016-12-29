using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineInfo
{
    class Ticket
    {
        public string FlNumber { get; set; }
        public TicketClass Type { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }          
        public Passenger Passenger { get; set; }
    }
}
