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
                Price = Price(flyingList, enterFlNumber)
            };
            return ticPas;
        }

        // method for fill ticket array
        public static void FillTicketArray(List<Flights> flyingList)
        {
            for (int i = 0; i < flyingList.Count; i++)
            {
                flyingList[i].Tickets = new Ticket[flyingList[i].AircraftCapacity];

                int business = (int)(flyingList[i].AircraftCapacity * 0.2);
                int economy = flyingList[i].AircraftCapacity - business;

                FillTicketBusiness(flyingList, i, business);
                FillTicketEconomy(flyingList, i, business, economy);
            }
        }

        // method for take price
        public static double Price(List<Flights> flyingList, string enterFlNumber)
        {
            double price = 0;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].FlNumber == enterFlNumber)
                {
                    for (int j = 0; j < flyingList[i].Tickets.Length; j++)
                    {
                        price = flyingList[i].Tickets[j].Price;
                    }

                }
            }
            return price;
        }

        // method for fill economy ticket array
        private static void FillTicketEconomy(List<Flights> flyingList, int i, int business, int economy)
        {
            for (int j = business; j < flyingList[i].Tickets.Length; j++)
            {
                flyingList[i].Tickets[j] = new Ticket
                {
                    Type = TicketClass.Economy,
                    Price = flyingList[i].AircraftCapacity * 1.5,
                    Quantity = economy,
                    FlNumber = flyingList[i].FlNumber
                };
                IsTicketsFree(flyingList, i, j);
            }
        }

        // method for fill business ticket array
        private static void FillTicketBusiness(List<Flights> flyingList, int i, int business)
        {
            for (int j = 0; j < business; j++)
            {
                flyingList[i].Tickets[j] = new Ticket
                {
                    Type = TicketClass.Business,
                    Price = flyingList[i].AircraftCapacity * 3,
                    Quantity = business,
                    FlNumber = flyingList[i].FlNumber
                };
                IsTicketsFree(flyingList, i, j);
            }
        }

        // method for fill ticket array of free or occupied tickets
        private static void IsTicketsFree(List<Flights> flyingList, int i, int j)
        {
            if (flyingList[i].Passengers == null)
            {
                flyingList[i].Tickets[j].IsFree = true;
            }
            else
            {
                for (int k = 0; k < flyingList[i].Passengers.Length; k++)
                {
                    flyingList[i].Tickets[j].IsFree = false;
                }

                for (int k = flyingList[i].Passengers.Length; k < flyingList[i].Tickets.Length; k++)
                {
                    flyingList[i].Tickets[j].IsFree = true;
                }
            }
        }
    }
}
