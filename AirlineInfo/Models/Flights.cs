using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineInfo
{
    class Flights
    {
        public FlightsType Type { get; set; }
        public DateTime Time { get; set; }
        public string FlNumber { get; set; }
        public string Town { get; set; }
        public string Company { get; set; }
        public Terminal Terminal { get; set; }
        public FlStatus FlStatus { get; set; }
        public Gate Gate { get; set; }        
        public int AircraftCapacity { get; set; }
        public Ticket[] Tickets { get; set; }      
    }
    
}

