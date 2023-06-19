using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XOMI.Static;
using XOMI.Unstore.Core;

namespace XOMI.UI
{
    public class ConsoleUI
    {
        public static void DisplayWelcomeMessage()
        {
            Console.WriteLine("XOMI");
            Console.WriteLine("");
            Console.WriteLine("Required to be installed:\n https://github.com/ViGEm/ViGEmBus/releases/tag/v1.21.442.0");

            Console.WriteLine("");
            Console.WriteLine("XOMI Doc & code here:\n https://github.com/EloiStree/2022_01_24_XOMI");
            Console.WriteLine("Support my work in general:\n https://eloi.page.link/support");

         
            Console.WriteLine("");
            DrawLine();
        }

        public static void DisplayipAndPortTargets()
        {
            IpAccess.GetAllLocalIPv4(NetworkInterfaceType.Ethernet, out string[] ips);

            DisplayIPAndPortToTarget(ips);
        }

        public static void DrawLine()
        {
            Console.WriteLine("--------------------");
        }

        public static void DisplayIPAndPortToTarget(string[] ips)
        {
            Console.WriteLine("IP: " + string.Join(", ", ips));
            Console.WriteLine("Port:  " + StaticUserPreference.m_port);
        }
    }
}
