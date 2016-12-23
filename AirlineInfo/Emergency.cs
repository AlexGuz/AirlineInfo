using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineInfo
{
    class Emergency
    {
        //emergency situation
        public static void EmergencySituation()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("Declared an emergency situation. Proceed to the nearest exit");

            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
