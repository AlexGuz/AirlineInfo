using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineInfo
{
    class PassengerService
    {
        //Select the desired action
        public static void ActionWithPassenger(List<Flights> flyingList)
        {
            Dictionary<int, Action<List<Flights>>> choiseOperations = new Dictionary<int, Action<List<Flights>>>
            {
                { 1, FindFlightsWithoutPass},
                { 2, FindPassTicketFlNumber},
                { 3, FindPass},
                { 4, ChangePassengers }
            };

            Console.WriteLine("Find flights without passengers = 1, or flight’s passengers = 2, find passengers by criteria = 3, change somthin in passenger=4");

            WorkWithDictionary(flyingList, choiseOperations);
        }

        //One method is to work with dictionaries
        private static void WorkWithDictionary(List<Flights> flyingList, Dictionary<int, Action<List<Flights>>> choiseOperations)
        {
            try
            {
                int enterChoise = int.Parse(Console.ReadLine());

                if (choiseOperations.ContainsKey(enterChoise))
                {
                    choiseOperations[enterChoise](flyingList);
                }
            }
            catch (FormatException)
            {
                Emergency.EmergencySituation();
            }
        }

        //method for print in console one Passenger
        public static void ShowOnePassOnFlight(List<Flights> flyingList, int count, int passCount)
        {
            Console.WriteLine(flyingList[count].FlNumber + "\t" + flyingList[count].AircraftCapacity + "\t" + flyingList[count].Tickets[passCount].Passenger.Birthday + "\t"
                + flyingList[count].Tickets[passCount].Passenger.FirstName + "\t" + flyingList[count].Tickets[passCount].Passenger.SecondName + "\t"
                + flyingList[count].Tickets[passCount].Passenger.Nationality + "\t" + flyingList[count].Tickets[passCount].Passenger.Passport + "\t"
                + flyingList[count].Tickets[passCount].Passenger.Sex + "\t" + flyingList[count].Tickets[passCount].Passenger.Ticket.Price + "\t"
                + flyingList[count].Tickets[passCount].Passenger.Ticket.Type);
        }

        public static Passenger AddPassenger(List<Flights> flyingList, string enterFlNumber)
        {
            Console.WriteLine("Enter FirstName of Passenger");
            string enterFirstName = Console.ReadLine();

            Console.WriteLine("Enter SecondName of Passenger");
            string enterSecondName = Console.ReadLine();

            Console.WriteLine("Enter Nationality of Passenger Afghans=1, Albanians=2,Algerians=3,Americans=4,Andorrans=5,Angolans=6");
            Nationality enterNationality = (Nationality)Enum.Parse(typeof(Nationality), Console.ReadLine());

            Console.WriteLine("Enter Passport number of Passenger");
            string enterPassport = Console.ReadLine();

            Console.WriteLine("Enter Birthday of Passenger");
            DateTime enterBirthday = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter Sex of Passenger Male=1,Female=2");
            Sex enterSex = (Sex)Enum.Parse(typeof(Sex), Console.ReadLine());

            Ticket enterTicket = TicketService.AddPassengerTicket(flyingList, enterFlNumber);

            Passenger pas = new Passenger
            {
                Birthday = enterBirthday,
                FirstName = enterFirstName,
                Nationality = enterNationality,
                Passport = enterPassport,
                SecondName = enterSecondName,
                Sex = enterSex,
                Ticket = enterTicket
            };
            return pas;
        }

        //method for choose what we will change in passenger
        private static void ChangePassengers(List<Flights> flyingList)
        {
            Dictionary<int, Action<List<Flights>>> choiseOperations = new Dictionary<int, Action<List<Flights>>>
            {
                { 1, FindPasName},
                { 2, FindPasNationality},
                { 3, FindPasPassport},
                { 4, FindPasSex},
                { 5, FindPassTicketFlNumber}
            };

            Console.WriteLine("Enter What you change FirstName or SecondName - 1, Nationality - 2, Passport - 3, Sex - 4, Ticket - 5");

            WorkWithDictionary(flyingList, choiseOperations);
        }

        //methods for find Passenger by criteria
        private static void FindPass(List<Flights> flyingList)
        {
            Dictionary<int, Action<List<Flights>>> choiseOperations = new Dictionary<int, Action<List<Flights>>>
            {
                { 1, FindPasName},
                { 2, FindPassTicketFlNumber},
                { 3, FindPasPassport}
            };

            Console.WriteLine("Enter criteria for search First name or last name - 1,  Flight number - 2, Passport number - 3");

            WorkWithDictionary(flyingList, choiseOperations);
        }

        private static void FindPassTicketFlNumber(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Ticket FlNumber");
            string enterTicket = Console.ReadLine();

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Tickets.Length; j++)
                {
                    Console.WriteLine("Do you whant to change FlNumber Passenger - yes 1; no 2");
                    int enterChoise = int.Parse(Console.ReadLine());
                    if (enterChoise == 1)
                    {
                        if (flyingList[i].Tickets[j].FlNumber == enterTicket)
                        {
                            ChangePassTicketFlNumber(flyingList, i, j);
                        }
                    }
                    else
                    {
                        ShowOnePassOnFlight(flyingList, i, j);
                    }
                }
            }
        }

        private static void FindPasSex(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Sex to change");
            Sex enterSex = (Sex)Enum.Parse(typeof(Sex), Console.ReadLine());

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Tickets.Length; j++)
                {
                    if (flyingList[i].Tickets[j].Passenger.Sex == enterSex)
                    {
                        ChangePasSex(flyingList, enterSex, i, j);
                    }
                }
            }
        }

        private static void FindPasBirthday(List<Flights> flyingList)
        {
            Console.WriteLine("Do you whant to change Birthday Passenger - yes 1; no 2");
            int enterChoise = int.Parse(Console.ReadLine());
            if (enterChoise == 1)
            {
                for (int i = 0; i < flyingList.Capacity; i++)
                {
                    for (int j = 0; j < flyingList[i].Tickets.Length; j++)
                    {
                        ChangePasBirthday(flyingList, i, j);
                    }
                }
            }
        }

        private static void FindPasPassport(List<Flights> flyingList)
        {
            Console.WriteLine("Enter a Passport number");
            string enterPassport = Console.ReadLine();

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Tickets.Length; j++)
                {
                    if (flyingList[i].Tickets[j].Passenger.Passport == enterPassport)
                    {
                        Console.WriteLine("Do you whant to change Passport number - yes 1; no 2");
                        int enterChoise = int.Parse(Console.ReadLine());
                        if (enterChoise == 1)
                        {
                            ChangePasPassport(flyingList, enterPassport, i, j);
                            break;
                        }
                        else
                        {
                            ShowOnePassOnFlight(flyingList, i, j);
                        }
                    }
                }
            }
        }

        private static void FindPasNationality(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Nationality to change");
            Nationality enterNationality = (Nationality)Enum.Parse(typeof(Nationality), Console.ReadLine());

            try
            {
                for (int i = 0; i < flyingList.Capacity; i++)
                {
                    for (int j = 0; j < flyingList[i].Tickets.Length; j++)
                    {
                        if (flyingList[i].Tickets[j].Passenger.Nationality == enterNationality)
                        {
                            ChangePasNationality(flyingList, enterNationality, i, j);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Emergency.EmergencySituation();
            }
        }

        private static void FindPasName(List<Flights> flyingList)
        {
            Console.WriteLine("Enter FirstName or SecondName");
            string enterName = Console.ReadLine();

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Tickets.Length; j++)
                {
                    Console.WriteLine("Do you whant to change FirstName or SecondName or Birthday Passenger - yes 1; no 2");
                    int enterChoise = int.Parse(Console.ReadLine());
                    if (enterChoise == 1)
                    {
                        if (flyingList[i].Tickets[j].Passenger.FirstName == enterName)
                        {
                            ChangePasFirstName(flyingList, i, j);
                            FindPasBirthday(flyingList);
                        }

                        else if (flyingList[i].Tickets[j].Passenger.SecondName == enterName)
                        {
                            ChangePasSecondName(flyingList, i, j);
                            FindPasBirthday(flyingList);
                        }
                    }
                    else
                    {
                        ShowOnePassOnFlight(flyingList, i, j);
                    }
                }
            }
        }

        //methods for change Passenger by selected criteria
        private static void ChangePassTicketFlNumber(List<Flights> flyingList, int i, int j)
        {
            Console.WriteLine("Enter new Ticket FlNumber");
            string enterNewTicketFlNumber = Console.ReadLine().ToUpper();
            if (flyingList[i].FlNumber == enterNewTicketFlNumber)
            {
                var temp = flyingList.ToArray()[i];
                temp.Tickets[j].FlNumber = enterNewTicketFlNumber;

                flyingList.RemoveAt(i);
                flyingList.Insert(i, temp);

                ShowOnePassOnFlight(flyingList, i, j);
            }
        }

        private static void ChangePasSex(List<Flights> flyingList, Sex enterSex, int i, int j)
        {
            Console.WriteLine("Enter new Sex");
            string enterNewName = Console.ReadLine().ToUpper();

            var temp = flyingList.ToArray()[i];
            temp.Tickets[j].Passenger.Sex = enterSex;

            flyingList.RemoveAt(i);
            flyingList.Insert(i, temp);

            ShowOnePassOnFlight(flyingList, i, j);
        }

        private static void ChangePasBirthday(List<Flights> flyingList, int i, int j)
        {
            Console.WriteLine("Enter new Birthday");
            DateTime enterBirthday = DateTime.Parse(Console.ReadLine());

            var temp = flyingList.ToArray()[i];
            temp.Tickets[j].Passenger.Birthday = enterBirthday;

            flyingList.RemoveAt(i);
            flyingList.Insert(i, temp);

            ShowOnePassOnFlight(flyingList, i, j);
        }

        private static void ChangePasPassport(List<Flights> flyingList, string enterPassport, int i, int j)
        {
            Console.WriteLine("Enter new Passport");
            string enterNewName = Console.ReadLine().ToUpper();

            var temp = flyingList.ToArray()[i];
            temp.Tickets[j].Passenger.Passport = enterPassport;

            flyingList.RemoveAt(i);
            flyingList.Insert(i, temp);

            ShowOnePassOnFlight(flyingList, i, j);
        }

        private static void ChangePasNationality(List<Flights> flyingList, Nationality enterNationality, int i, int j)
        {
            Console.WriteLine("Enter new Nationality");
            string enterNewName = Console.ReadLine().ToUpper();

            var temp = flyingList.ToArray()[i];
            temp.Tickets[j].Passenger.Nationality = enterNationality;

            flyingList.RemoveAt(i);
            flyingList.Insert(i, temp);

            ShowOnePassOnFlight(flyingList, i, j);
        }

        private static void ChangePasFirstName(List<Flights> flyingList, int count, int ticketCount)
        {
            Console.WriteLine("Enter new FirstName");
            string enterNewName = Console.ReadLine().ToUpper();

            var temp = flyingList.ToArray()[count];
            temp.Tickets[ticketCount].Passenger.FirstName = enterNewName;

            flyingList.RemoveAt(count);
            flyingList.Insert(count, temp);

            ShowOnePassOnFlight(flyingList, count, ticketCount);
        }

        private static void ChangePasSecondName(List<Flights> flyingList, int count, int ticketCount)
        {
            Console.WriteLine("Enter new SecondName");
            string enterNewName = Console.ReadLine().ToUpper();

            var temp = flyingList.ToArray()[count];
            temp.Tickets[ticketCount].Passenger.SecondName = enterNewName;

            flyingList.RemoveAt(count);
            flyingList.Insert(count, temp);

            ShowOnePassOnFlight(flyingList, count, ticketCount);
        }

        //method for find flights without passengers
        private static void FindFlightsWithoutPass(List<Flights> flyingList)
        {
            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Tickets.Length; j++)
                {
                    if (flyingList[i].Tickets[j].Passenger == null)
                    {
                        ShowOnePassOnFlight(flyingList, i, j);
                    }
                }
            }
        }
    }
}