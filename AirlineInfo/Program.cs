using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            //create  flying list
            List<Flights> flyingList = new List<Flights>
            {
                new Flights {Time=DateTime.Now.AddMinutes(40), FlNumber= "PO6H4",Type=FlightsType.Arrival,
                    Town ="Kharkiv",Company="AF",Terminal=Terminal.TerminalF,FlStatus=FlStatus.Unknown, Gate=Gate.Gate5, AircraftCapacity =380,
                    Passengers =new Passenger []
                    {
                        new Passenger { FirstName ="Alex", SecondName="SomePassenger", Nationality = Nationality.Americans, Passport="GH12577", Sex =Sex.Male,
                            Birthday = new DateTime(1986, 5, 14), Ticket= new Ticket{ FlNumber= "PO6H4", Type= TicketClass.Business} },
                        new Passenger { FirstName ="Edvard",SecondName="Philips", Nationality = Nationality.Afghans, Passport="GH12577", Sex =Sex.Female,
                            Birthday = new DateTime(1995, 11, 14), Ticket= new Ticket{ FlNumber= "PO6H4", Type= TicketClass.Business} }
                    },
                    Tickets =new Ticket[380] },

                new Flights {Time=DateTime.Now.AddHours(2), FlNumber= "S4563",Type=FlightsType.Departures,
                    Town ="Kiev",Company="S7",Terminal=Terminal.TerminalA,FlStatus=FlStatus.Arrived,Gate=Gate.Gate1, AircraftCapacity =160,
                    Passengers =new Passenger[]
                    {
                         new Passenger { FirstName ="Bill",SecondName="SomeGuy", Nationality = Nationality.British, Passport="GH12577", Sex =Sex.Female,
                             Birthday = new DateTime(1973, 11, 22), Ticket= new Ticket{ FlNumber= "S4563", Type= TicketClass.Economy} },
                    },
                    Tickets =new Ticket[160] },

                new Flights {Time=DateTime.Now.AddHours(1), FlNumber= "L4789",Type=FlightsType.Departures,
                    Town ="Moskow",Company="SA",Terminal=Terminal.TerminalB,FlStatus=FlStatus.Canceled,Gate=Gate.Gate2, AircraftCapacity =200,
                    Passengers =new Passenger[]
                    {
                        new Passenger { FirstName ="Max",SecondName="Oggi", Nationality = Nationality.TurkishCypriots, Passport="GH12577", Sex =Sex.Male,
                            Birthday = new DateTime(1969, 5, 20), Ticket= new Ticket{ FlNumber= "L4789", Type= TicketClass.Economy} },
                    },
                    Tickets =new Ticket[200] },
            };


            TicketService.FillTicketArray(flyingList);
            FlightsService.StartFlights(flyingList);

            Console.ReadLine();
        }
    }
}
