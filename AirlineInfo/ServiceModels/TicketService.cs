using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineInfo
{
    class TicketService
    {
        // method for add Ticket
        public static Ticket AddPassengerTicket(List<Flights> flyingList, string enterFlNumber)
        {
            Console.WriteLine("Enter Type of Ticket Business=1, Economy=2");
            TicketClass enterClass = (TicketClass)Enum.Parse(typeof(TicketClass), Console.ReadLine());

            Ticket ticPas = new Ticket
            {
                FlNumber = enterFlNumber,
                Type = enterClass,
                Price = Price(flyingList, enterFlNumber, enterClass)
            };
            return ticPas;
        }

        // method for fill ticket array
        public static void FillTicketArray(List<Flights> flyingList)
        {
            for (int i = 0; i < flyingList.Count; i++)
            {
                var flyingListItem = flyingList[i];
                flyingListItem.Tickets = new Ticket[flyingListItem.AircraftCapacity];

                int businessTicketsCount = (int)(flyingListItem.AircraftCapacity * 0.2);
                int economyTicketsCount = flyingListItem.AircraftCapacity - businessTicketsCount;

                FillTicket(flyingListItem, businessTicketsCount, economyTicketsCount);
            }
        }
        
        //combined  version FillTicketEconomy and FillTicketBusiness
        private static void FillTicket(Flights flyingListItem, int businessTicketsCount, int economyTicketsCount)
        {
            for (int j = 0; j < flyingListItem.Tickets.Length; j++)
            {
                if (j <= businessTicketsCount)
                {
                    flyingListItem.Tickets[j] =
                        new Ticket
                        {
                            Type = TicketClass.Business,
                            Price = flyingListItem.AircraftCapacity * 3,
                            Quantity = businessTicketsCount,
                            FlNumber = flyingListItem.FlNumber,
                            Passenger = null
                        };
                }
                else
                {
                    flyingListItem.Tickets[j] = new Ticket
                    {
                        Type = TicketClass.Economy,
                        Price = flyingListItem.AircraftCapacity * 1.5,
                        Quantity = economyTicketsCount,
                        FlNumber = flyingListItem.FlNumber,
                        Passenger = null
                    };
                }
            }
        }

        //add existin passenger from passList in our flight
        public static void AddExistPass(List<Flights> flyingList, List<Passenger> passList)
        {
            for (int i = 0; i < flyingList.Count; i++)
            {
                for (int j = 0; j < flyingList[i].Tickets.Length; j++)
                {
                    for (int k = 0; k < passList.Count; k++)
                    {
                        if (passList[k].Ticket.FlNumber== flyingList[i].Tickets[j].FlNumber&& passList[k].Ticket.Type == flyingList[i].Tickets[j].Type)
                        {
                            flyingList[i].Tickets[j].Passenger = passList[k];
                        }
                    }
                }
            }
        }
        
        public static void AddPassengerInFlight(List<Flights> flyingList)
        {
            Console.WriteLine("Enter flight for add passenger");
            string enterFlNumber = Console.ReadLine().ToUpper();

            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].FlNumber == enterFlNumber)
                {
                    for (int j = 0; i < flyingList[i].Tickets.Length; j++)
                    {
                        if (flyingList[i].Tickets[j].Passenger == null)
                        {
                            flyingList[i].Tickets[j].Passenger = PassengerService.AddPassenger(flyingList, enterFlNumber);
                        }
                    }
                }
            }
        }

        // method for take price
        public static double Price(List<Flights> flyingList, string enterFlNumber, TicketClass enterClass)
        {
            double price = 0;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].FlNumber == enterFlNumber)
                {
                    /*initial version was "not comprehended until the end." 
                    Price is assigned depending on the class + tickets array that would get to the properties 
                    of a particular element has entered the second bust              
                    */
                    for (int j = 0; j < flyingList[i].Tickets.Length; j++)
                    {
                        if (flyingList[i].Tickets[j].Type == enterClass)
                        {
                            price = flyingList[i].Tickets[j].Price;
                            break;
                        }                        
                    }
                }
            }
            return price;
        }
    }
}
