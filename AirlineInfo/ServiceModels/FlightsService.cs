using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineInfo
{
    class FlightsService
    {
        //method for choose what we do
        public static void StartFlights(List<Flights> flyingList)
        {
            while (true)
            {
                Console.WriteLine("What you need?");
                Console.WriteLine("Arrival list-enter - 1; departures list-enter - 2; exit fron console - 0");

                int enter = int.Parse(Console.ReadLine());

                if (enter == 0)
                {
                    Environment.Exit(0);
                }

                //We put swich-case in the block try-catch that would catch possible exceptions
                try
                {
                    FlightsType enterType = (FlightsType)Enum.Parse(typeof(FlightsType), enter.ToString());

                    ShowFlight(flyingList, enterType);
                }
                catch (FormatException)
                {
                    Emergency.EmergencySituation();
                }

                StartChoose(flyingList);
            }
        }

        //method for print in console one element of the list
        public static void ShowOneFlight(List<Flights> flyingList, int count)
        {
            Console.WriteLine(flyingList[count].Type + flyingList[count].Time.ToString() + "\t" + flyingList[count].FlNumber + "\t" + flyingList[count].Town
                                + "\t" + flyingList[count].Company + "\t" + flyingList[count].Terminal + "\t" + flyingList[count].FlStatus
                                + "\t" + flyingList[count].Gate + "\t" + flyingList[count].AircraftCapacity);
        }

        //Select the desired action
        private static void StartChoose(List<Flights> flyingList)
        {
            Dictionary<int, Action<List<Flights>>> choiseOperations = new Dictionary<int, Action<List<Flights>>>
            {
                { 1, AddFlight },
                { 2, DeleteFlight },
                { 3, FindFlight },
                { 4, FindNearestFlight },
                { 5, ChangeFlight },
                { 6, AddPassengerInFlight },
                { 7, PassengerService.PassengerStart }
            };

            Console.WriteLine("Do you whant add new flight - 1 or delete flight - 2? Find a flight on the specified parameters - 3, Find the nearest flight - 4" +
                "change somthin in list - 5? add new passenger in list - 6, find somthin in passenger list - 7");
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

        //method for print in console all element of the list
        private static void ShowFlight(List<Flights> flyingList, FlightsType enterType)
        {
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].Type == enterType)
                {
                    ShowOneFlight(flyingList, i);
                }
            }
        }

        //method for add new element in the list
        private static void AddFlight(List<Flights> flyingList)
        {
            Console.WriteLine("Do you whant add new arrival - 1 or departures - 2");
            FlightsType enterType = (FlightsType)Enum.Parse(typeof(FlightsType), Console.ReadLine());

            Console.WriteLine("Enter Time in format yyyy-MM-dd HH:mm");
            DateTime timeChoice = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter Flight Number");
            string flNumberChoice = Console.ReadLine().ToUpper();

            Console.WriteLine("Enter Company");
            string companyChoice = Console.ReadLine().ToUpper();

            Console.WriteLine("Enter AircraftCapacity");
            int enterAircraftCapacity = int.Parse(Console.ReadLine());

            Terminal terminalChoise = ChooseTerminal();

            FlStatus flStatusChoise;

            Gate gateChoise = ChooseGate();

            string townChoice = "Kharkiv";

            if (FlightsType.Departures == enterType)
            {
                Console.WriteLine("Enter Town");
                townChoice = Console.ReadLine().ToUpper();
                flStatusChoise = ChooseFlStatusArrival();
            }

            else
            {
                flStatusChoise = ChooseFlStatusDepartures();
            }

            flyingList.Add(new Flights
            {
                Type = enterType,
                Time = timeChoice,
                FlNumber = flNumberChoice,
                Company = companyChoice,
                Terminal = terminalChoise,
                FlStatus = flStatusChoise,
                Town = townChoice,
                Gate = gateChoise,
                AircraftCapacity = enterAircraftCapacity,
                Passengers = new Passenger[enterAircraftCapacity],
                Tickets = new Ticket[enterAircraftCapacity]
            });

            TicketService.FillTicketArray(flyingList);

            ShowFlight(flyingList, enterType);
        }

        //method to add passengers in flight
        private static void AddPassengerInFlight(List<Flights> flyingList)
        {
            Console.WriteLine("Enter flight for add passenger");
            string enterFlNumber = Console.ReadLine().ToUpper();

            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].FlNumber == enterFlNumber)
                {
                    for (int j = 0; i < flyingList[i].Passengers.Length; i++)
                    {
                        if (flyingList[i].Passengers[j] == null)
                        {
                            flyingList[i].Passengers[j] = PassengerService.AddPassenger(flyingList, enterFlNumber);
                        }
                    }
                }
            }
        }

        //method for choose a new Terminal
        private static Terminal ChooseTerminal()
        {
            Console.WriteLine("Enter Terminal TerminalA - 1, TerminalB - 2, TerminalC - 3, TerminalD - 4, TerminalF - 5, unknown - default- 0");
            Terminal terminalChoise = Terminal.Unknown;

            try
            {
                terminalChoise = (Terminal)Enum.Parse(typeof(Terminal), Console.ReadLine());
            }
            catch (FormatException)
            {
                Emergency.EmergencySituation();
            }
            return terminalChoise;
        }

        //method for choose a new FlStatus for arrival List
        private static FlStatus ChooseFlStatusArrival()
        {
            Dictionary<int, FlStatus> choiseOperations = new Dictionary<int, FlStatus>
            {
                { 1, FlStatus.CheckIn },
                { 2, FlStatus.Arrived },
                { 3, FlStatus.Canceled },
                { 4, FlStatus.ExpectedAt },
                { 5, FlStatus.Delayed },
                { 6, FlStatus.InFlight }
            };

            Console.WriteLine("Enter FlyStatus check-in - 1, arrived - 2, canceled - 3, expected at - 4, delayed - 5, in flight - 6, unknown - default");
            FlStatus flStatusChoise = FlStatus.Unknown;

            try
            {
                int enterChoise = int.Parse(Console.ReadLine());

                if (choiseOperations.ContainsKey(enterChoise))
                {
                    flStatusChoise = choiseOperations[enterChoise];
                }
            }
            catch (FormatException)
            {
                Emergency.EmergencySituation();
            }
            return flStatusChoise;
        }

        //method for choose a new FlStatus for departures List
        private static FlStatus ChooseFlStatusDepartures()
        {
            Dictionary<int, FlStatus> choiseOperations = new Dictionary<int, FlStatus>
            {
                { 1, FlStatus.CheckIn },
                { 2, FlStatus.GateClosed },
                { 3, FlStatus.DepartedAt },
                { 4, FlStatus.Canceled },
                { 5, FlStatus.Delayed }
            };

            Console.WriteLine("Enter FlyStatus check-in - 1, gate closed - 2, departed at - 3, canceled - 4, delayed - 5, unknown - default");
            FlStatus flStatusChoise = FlStatus.Unknown;

            try
            {
                int enterChoise = int.Parse(Console.ReadLine());

                if (choiseOperations.ContainsKey(enterChoise))
                {
                    flStatusChoise = choiseOperations[enterChoise];
                }
            }
            catch (FormatException)
            {
                Emergency.EmergencySituation();
            }

            return flStatusChoise;
        }

        //method for choose a new Gate
        private static Gate ChooseGate()
        {
            Console.WriteLine("Enter gate Gate1 - 1, Gate2 - 2, Gate3 - 3, Gate4 - 4, Gate5 - 5, Gate6 - 6, Gate7 - 7 , Gate8 - 8 , Gate9 - 9, unknown - default - 0");
            Gate gateChoise = Gate.Unknown;

            try
            {
                gateChoise = (Gate)Enum.Parse(typeof(Gate), Console.ReadLine());
            }
            catch (FormatException)
            {
                Emergency.EmergencySituation();
            }
            return gateChoise;
        }

        //method for delete element of the list
        private static void DeleteFlight(List<Flights> flyingList)
        {
            Console.WriteLine("Do you whant delete  arrival - 1 or departures - 2");
            FlightsType enterType = (FlightsType)Enum.Parse(typeof(FlightsType), Console.ReadLine());

            Console.WriteLine("Enter Flight Number");
            string delFlNumber = Console.ReadLine().ToUpper();

            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].FlNumber == delFlNumber)
                {
                    flyingList.RemoveAt(i);
                    break;
                }
            }
            ShowFlight(flyingList, enterType);
        }

        // method for searching elements of the List
        private static void FindFlight(List<Flights> flyingList)
        {
            Dictionary<int, Action<List<Flights>>> choiseOperations = new Dictionary<int, Action<List<Flights>>>
            {
                { 1, FindFlightTime },
                { 2, FindFlightFlNumber },
                { 3, FindFlightTown },
                { 4, FindFlightCompany },
                { 5, FindFlightTerminal },
                { 6, FindFlightFlStatus },
                { 7, FindFlightGate }
            };
            Console.WriteLine("Enter what you want to find Time - 1, FlNumber - 2, Town - 3, Company - 4, Terminal - 5, FlStatus - 6, Gate - 7");

            try
            {
                int findNumber = int.Parse(Console.ReadLine());

                if (choiseOperations.ContainsKey(findNumber))
                {
                    choiseOperations[findNumber](flyingList);
                }
            }
            catch (Exception)
            {
                Emergency.EmergencySituation();
            }
        }

        // method for searching Gate in element of the List
        private static void FindFlightGate(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Gate ");
            Gate enterGate = (Gate)Enum.Parse(typeof(Gate), Console.ReadLine());

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].Gate == enterGate)
                {
                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        // method for searching flStatus in element of the List
        private static void FindFlightFlStatus(List<Flights> flyingList)
        {
            Console.WriteLine("Enter flStatus ");
            FlStatus enterFlStatus = (FlStatus)Enum.Parse(typeof(FlStatus), Console.ReadLine());

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].FlStatus == enterFlStatus)
                {
                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        // method for searching Terminal in element of the List
        private static void FindFlightTerminal(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Terminal ");
            Terminal enterTerminal = (Terminal)Enum.Parse(typeof(Terminal), Console.ReadLine());

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].Terminal == enterTerminal)
                {
                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        // method for searching Company in element of the List
        private static void FindFlightCompany(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Company ");
            string enterCompany = Console.ReadLine().ToUpper();

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].Company == enterCompany)
                {
                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        // method for searching Town in element of the List
        private static void FindFlightTown(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Town ");
            string enterTown = Console.ReadLine().ToUpper();

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].Town == enterTown)
                {
                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        // method for searching FlNumber in element of the List
        private static void FindFlightFlNumber(List<Flights> flyingList)
        {
            Console.WriteLine("Enter FlNumber ");
            string enterFlNumber = Console.ReadLine().ToUpper();

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].FlNumber == enterFlNumber)
                {
                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        // method for searching Time in element of the List
        private static void FindFlightTime(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Time in format yyyy-MM-dd HH:mm");
            DateTime enterTime = DateTime.Parse(Console.ReadLine());

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {

                if (flyingList[i].Time == enterTime)
                {
                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }

                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        //  method for changing elements of the List
        private static void ChangeFlight(List<Flights> flyingList)
        {
            Dictionary<int, Action<List<Flights>>> choiseOperations = new Dictionary<int, Action<List<Flights>>>
            {
                { 1, ChangeFlightTime },
                { 2, ChangeFlightFlNumber },
                { 3, ChangeFlightTown },
                { 4, ChangeFlightCompany },
                { 5, ChangeFlightTerminal },
                { 6, ChangeFlightFlStatus },
                { 7, ChangeFlightGate }
            };

            Console.WriteLine("Enter what you want to change Time - 1, FlNumber - 2, Town - 3, Company - 4, Terminal - 5, FlStatus - 6, Gate - 7");

            try
            {
                int findNumber = int.Parse(Console.ReadLine());

                if (choiseOperations.ContainsKey(findNumber))
                {
                    choiseOperations[findNumber](flyingList);
                }
            }
            catch (Exception)
            {
                Emergency.EmergencySituation();
            }
        }

        //  method for changing Gate in element of the List
        private static void ChangeFlightGate(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Gate ");
            Gate enterGate = (Gate)Enum.Parse(typeof(Gate), Console.ReadLine());

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].Gate == enterGate)
                {
                    Gate newGate = ChooseGate();

                    var temp = flyingList.ToArray()[i];
                    temp.Gate = newGate;

                    flyingList.RemoveAt(i);
                    flyingList.Insert(i, temp);

                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        //  method for changing FlStatus in element of the List
        private static void ChangeFlightFlStatus(List<Flights> flyingList)
        {
            Console.WriteLine("Enter FlStatus ");
            FlStatus enterFlStatus = (FlStatus)Enum.Parse(typeof(FlStatus), Console.ReadLine());

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].FlStatus == enterFlStatus)
                {
                    FlStatus newFlStatus = ChooseFlStatusArrival();

                    var temp = flyingList.ToArray()[i];
                    temp.FlStatus = newFlStatus;

                    flyingList.RemoveAt(i);
                    flyingList.Insert(i, temp);

                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        //  method for changing Terminal in element of the List
        private static void ChangeFlightTerminal(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Terminal ");
            Terminal enterTerminal = (Terminal)Enum.Parse(typeof(Terminal), Console.ReadLine());

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].Terminal == enterTerminal)
                {
                    Terminal newTerminal = ChooseTerminal();

                    var temp = flyingList.ToArray()[i];
                    temp.Terminal = newTerminal;

                    flyingList.RemoveAt(i);
                    flyingList.Insert(i, temp);

                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        //  method for changing Company in element of the List
        private static void ChangeFlightCompany(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Company ");
            string enterCompany = Console.ReadLine().ToUpper();

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].Company == enterCompany)
                {
                    Console.WriteLine("Enter new Company ");
                    string enterNewCompany = Console.ReadLine().ToUpper();

                    var temp = flyingList.ToArray()[i];
                    temp.Town = enterNewCompany;

                    flyingList.RemoveAt(i);
                    flyingList.Insert(i, temp);

                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        //  method for changing Town in element of the List
        private static void ChangeFlightTown(List<Flights> flyingList)
        {
            Console.WriteLine("Enter Town ");
            string enterTown = Console.ReadLine().ToUpper();

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].Town == enterTown)
                {
                    Console.WriteLine("Enter new Town ");
                    string enterNewTown = Console.ReadLine().ToUpper();

                    var temp = flyingList.ToArray()[i];
                    temp.Town = enterNewTown;

                    flyingList.RemoveAt(i);
                    flyingList.Insert(i, temp);

                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        //  method for changing FlNumber in element of the List
        private static void ChangeFlightFlNumber(List<Flights> flyingList)
        {
            Console.WriteLine("Enter a editable FlNumber ");
            string enterFlNumber = Console.ReadLine().ToUpper();

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {

                if (flyingList[i].FlNumber == enterFlNumber)
                {
                    Console.WriteLine("Enter new FlNumber ");
                    string enterNewFlNumber = Console.ReadLine().ToUpper();

                    var temp = flyingList.ToArray()[i];
                    temp.FlNumber = enterNewFlNumber;

                    flyingList.RemoveAt(i);
                    flyingList.Insert(i, temp);

                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }
                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        //  method for changing Time in element of the List
        private static void ChangeFlightTime(List<Flights> flyingList)
        {
            Console.WriteLine("Enter a editable time in format yyyy-MM-dd HH:mm");
            DateTime enterTime = DateTime.Parse(Console.ReadLine());

            bool notFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {

                if (flyingList[i].Time == enterTime)
                {
                    Console.WriteLine("Enter new time in format yyyy-MM-dd HH:mm");
                    DateTime enterTimeNew = DateTime.Parse(Console.ReadLine());

                    var temp = flyingList.ToArray()[i];
                    temp.Time = enterTime;

                    flyingList.RemoveAt(i);
                    flyingList.Insert(i, temp);

                    ShowOneFlight(flyingList, i);
                    notFind = false;
                }

                if (notFind)
                {
                    Console.WriteLine("Flights meet these criteria are not found");
                }
            }
        }

        //method for find nearest flights
        private static void FindNearestFlight(List<Flights> flyingList)
        {
            Console.WriteLine("Find a nearest flight arrival - 1 or departures - 2");
            FlightsType enterType = (FlightsType)Enum.Parse(typeof(FlightsType), Console.ReadLine());

            bool noFind = true;
            for (int i = 0; i < flyingList.Count; i++)
            {
                if (flyingList[i].Type == enterType)
                {
                    if (flyingList[i].Time >= DateTime.Now.Subtract(new TimeSpan(0, 1, 0, 0)) && flyingList[i].Time <= DateTime.Now.AddHours(1))
                    {
                        ShowOneFlight(flyingList, i);
                        noFind = false;
                    }
                }
            }

            if (noFind)
            {
                Console.WriteLine("No closest flights");
            }
        }
    }
}

