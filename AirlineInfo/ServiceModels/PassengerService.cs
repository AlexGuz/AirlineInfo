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
        public static void PassengerStart(List<Flights> flyingList)
        {
            Dictionary<int, Action<List<Flights>>> choiseOperations = new Dictionary<int, Action<List<Flights>>>
            {
                { 1, FindFlightsWithoutPass},
                { 2, FindPassFlNumber},
                { 3, FindPass}
            };

            Console.WriteLine("Find flights without passengers = 1, or flight’s passengers = 2, find passengers by criteria = 3, change somthin in passenger=4");

            int enterChoise = int.Parse(Console.ReadLine());

            if (choiseOperations.ContainsKey(enterChoise))
            {
                choiseOperations[enterChoise](flyingList);
            }
        }

        //method for print in console one Passenger
        public static void ShowOnePassOnFlight(List<Flights> flyingList, int count, int passCount)
        {
            Console.WriteLine(flyingList[count].FlNumber + "\t" + flyingList[count].AircraftCapacity + "\t" + flyingList[count].Passengers[passCount].Birthday + "\t"
                + flyingList[count].Passengers[passCount].FirstName + "\t" + flyingList[count].Passengers[passCount].SecondName + "\t"
                + flyingList[count].Passengers[passCount].Nationality + "\t" + flyingList[count].Passengers[passCount].Passport + "\t"
                + flyingList[count].Passengers[passCount].Sex + "\t" + flyingList[count].Passengers[passCount].Ticket.Price + "\t"
                + flyingList[count].Passengers[passCount].Ticket.Type);
        }

        // method for add passengers
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

        //method for change somthin in passenger
        private static void ChangePassengers(List<Flights> flyingList)
        {
            Dictionary<int, Action<List<Flights>>> choiseOperations = new Dictionary<int, Action<List<Flights>>>
            {
                { 1, ChangeName},
                { 2, ChangeNationality},
                { 3, ChangePassport},
                { 4, ChangeBirthday},
                { 5, ChangeSex},
                { 6, ChangeTicket}
            };

            Console.WriteLine("Enter What you change FirstName or SecondName - 1, Nationality - 2, Passport - 3, Birthday - 4, Sex - 5, Ticket - 6");

            int enterChoise = int.Parse(Console.ReadLine());

            if (choiseOperations.ContainsKey(enterChoise))
            {
                choiseOperations[enterChoise](flyingList);
            }

            ChangeName(flyingList);
        }

        //method for change Ticket
        private static void ChangeTicket(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Ticket FlNumber to change");
            string enterTicket = Console.ReadLine();

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Passengers.Length; j++)
                {
                    if (flyingList[i].Passengers[j].Ticket.FlNumber == enterTicket)
                    {
                        Console.WriteLine("Enter new Ticket FlNumber");
                        string enterNewTicket = Console.ReadLine().ToUpper();
                        if (flyingList[i].FlNumber == enterNewTicket)
                        {
                            var temp = flyingList.ToArray()[i];
                            temp.Passengers[j].Ticket = TicketService.AddPassengerTicket(flyingList, enterTicket);

                            flyingList.RemoveAt(i);
                            flyingList.Insert(i, temp);

                            ShowOnePassOnFlight(flyingList, i, j);
                        }
                    }
                }
            }
        }

        //method for change Sex
        private static void ChangeSex(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Sex to change");
            Sex enterSex = (Sex)Enum.Parse(typeof(Sex), Console.ReadLine());

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Passengers.Length; j++)
                {
                    if (flyingList[i].Passengers[j].Sex == enterSex)
                    {
                        Console.WriteLine("Enter new Sex");
                        string enterNewName = Console.ReadLine().ToUpper();

                        var temp = flyingList.ToArray()[i];
                        temp.Passengers[j].Sex = enterSex;

                        flyingList.RemoveAt(i);
                        flyingList.Insert(i, temp);

                        ShowOnePassOnFlight(flyingList, i, j);
                    }
                }
            }
        }

        //method for change Birthday
        private static void ChangeBirthday(List<Flights> flyingList)
        {
            Console.WriteLine("Enter a editable Birthday");
            DateTime enterBirthday = DateTime.Parse(Console.ReadLine());

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Passengers.Length; j++)
                {
                    if (flyingList[i].Passengers[j].Birthday == enterBirthday)
                    {
                        Console.WriteLine("Enter new Birthday");
                        string enterNewName = Console.ReadLine().ToUpper();

                        var temp = flyingList.ToArray()[i];
                        temp.Passengers[j].Birthday = enterBirthday;

                        flyingList.RemoveAt(i);
                        flyingList.Insert(i, temp);

                        ShowOnePassOnFlight(flyingList, i, j);
                    }
                }
            }
        }

        //method for change Passport
        private static void ChangePassport(List<Flights> flyingList)
        {
            Console.WriteLine("Enter a editable Passport");
            string enterPassport = Console.ReadLine();

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Passengers.Length; j++)
                {
                    if (flyingList[i].Passengers[j].Passport == enterPassport)
                    {
                        Console.WriteLine("Enter new Passport");
                        string enterNewName = Console.ReadLine().ToUpper();

                        var temp = flyingList.ToArray()[i];
                        temp.Passengers[j].Passport = enterPassport;

                        flyingList.RemoveAt(i);
                        flyingList.Insert(i, temp);

                        ShowOnePassOnFlight(flyingList, i, j);
                    }
                }
            }
        }

        //method for change Nationality
        private static void ChangeNationality(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Nationality to change");
            Nationality enterNationality = (Nationality)Enum.Parse(typeof(Nationality), Console.ReadLine());

            try
            {
                for (int i = 0; i < flyingList.Capacity; i++)
                {
                    for (int j = 0; j < flyingList[i].Passengers.Length; j++)
                    {
                        if (flyingList[i].Passengers[j].Nationality == enterNationality)
                        {
                            Console.WriteLine("Enter new Nationality");
                            string enterNewName = Console.ReadLine().ToUpper();

                            var temp = flyingList.ToArray()[i];
                            temp.Passengers[j].Nationality = enterNationality;

                            flyingList.RemoveAt(i);
                            flyingList.Insert(i, temp);

                            ShowOnePassOnFlight(flyingList, i, j);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Emergency.EmergencySituation();
            }
        }

        //method for choose what we will change FirstName or SecondName
        private static void ChangeName(List<Flights> flyingList)
        {
            Console.WriteLine("Enter FirstName or SecondName to change");
            string enterName = Console.ReadLine();

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Passengers.Length; j++)
                {
                    if (flyingList[i].Passengers[j].FirstName == enterName)
                    {
                        ChangeFirstName(flyingList, i, j);
                    }

                    else if (flyingList[i].Passengers[j].SecondName == enterName)
                    {
                        ChangeSecondName(flyingList, i, j);
                    }
                }
            }
        }

        //method for change FirstName 
        private static void ChangeFirstName(List<Flights> flyingList, int count, int passCount)
        {
            Console.WriteLine("Enter new FirstName");
            string enterNewName = Console.ReadLine().ToUpper();

            var temp = flyingList.ToArray()[count];
            temp.Passengers[passCount].FirstName = enterNewName;

            flyingList.RemoveAt(count);
            flyingList.Insert(count, temp);

            ShowOnePassOnFlight(flyingList, count, passCount);
        }

        //method for change SecondName
        private static void ChangeSecondName(List<Flights> flyingList, int count, int passCount)
        {
            Console.WriteLine("Enter new SecondName");
            string enterNewName = Console.ReadLine().ToUpper();

            var temp = flyingList.ToArray()[count];
            temp.Passengers[passCount].SecondName = enterNewName;

            flyingList.RemoveAt(count);
            flyingList.Insert(count, temp);

            ShowOnePassOnFlight(flyingList, count, passCount);
        }

        //method for find flights without passengers
        private static void FindFlightsWithoutPass(List<Flights> flyingList)
        {
            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Passengers.Length; j++)
                {
                    if (flyingList[i].Passengers == null)
                    {
                        ShowOnePassOnFlight(flyingList, i, j);
                    }
                }
            }
        }

        //method for find passengers on flight 
        private static void FindPassFlNumber(List<Flights> flyingList)
        {
            Console.WriteLine("Enter FlNumber");
            string enterFlNumber = Console.ReadLine().ToUpper();

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Passengers.Length; j++)
                {
                    if (flyingList[i].FlNumber == enterFlNumber && flyingList[i].Passengers == null)
                    {
                        ShowOnePassOnFlight(flyingList, i, j);
                    }
                }
            }
        }

        // method for find passengers by criteria
        private static void FindPass(List<Flights> flyingList)
        {
            Dictionary<int, Action<List<Flights>>> choiseOperations = new Dictionary<int, Action<List<Flights>>>
            {
                { 1, FindPassName},
                { 2, FindPassFlNumber},
                { 3, FindPassport}
            };

            Console.WriteLine("Enter criteria for search First name or last name - 1,  Flight number - 2, Passport number - 3");

            int enterChoise = int.Parse(Console.ReadLine());

            if (choiseOperations.ContainsKey(enterChoise))
            {
                choiseOperations[enterChoise](flyingList);
            }
        }

        // method for find passengers by first name or last name
        private static void FindPassName(List<Flights> flyingList)
        {
            Console.WriteLine("Enter First name or last name");
            string enterName = Console.ReadLine();

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Passengers.Length; j++)
                {
                    if (flyingList[i].Passengers[j].FirstName == enterName || flyingList[i].Passengers[j].SecondName == enterName)
                    {
                        ShowOnePassOnFlight(flyingList, i, j);
                    }
                }
            }
        }

        // method for find passengers by Passport
        private static void FindPassport(List<Flights> flyingList)
        {
            Console.WriteLine("Enter number of Passport");
            string enterPassport = Console.ReadLine();

            for (int i = 0; i < flyingList.Capacity; i++)
            {
                for (int j = 0; j < flyingList[i].Passengers.Length; j++)
                {
                    if (flyingList[i].Passengers[j].Passport == enterPassport)
                    {
                        ShowOnePassOnFlight(flyingList, i, j);
                    }
                }
            }
        }
    }
}
