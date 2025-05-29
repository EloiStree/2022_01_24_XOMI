using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using XOMI.InfoHolder;
using XOMI.Int1899;
using XOMI.TimedAction;
using XOMI.UDP;
using XOMI.UI;
using XOMI.Unstore.Xbox;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XOMI.Unstore
{
    public class ProgramV2_FourControllerIntegerVersion
    {
        public static XBoxActionStack m_waitingActions = new XBoxActionStack();
        public static UDPTextListenerThread m_udpThread = new UDPTextListenerThread();
        public static XboxSingleControllerExecuter m_xboxExecuter;


        public static List<int> m_banInput = new List<int>();



        //public static IndexIntegerDateQueue m_iidQueue = new IndexIntegerDateQueue();
        public static IntegerToActions [] m_integerToActions = new IntegerToActions[4];
        public static int m_controllerNumber = 4;

        public static void Run(string[] args)
        {

            // Use joy.cpl to check if it works.

            // IF b1355  ( remove the integer 1355 )
            for (int i = 0; i < args.Length; i++) { 
                if (args[i].Length>0 && args[i][0] == 'b' ){
                    if (int.TryParse( args[i].Replace("b", ""), out int integer)) 
                    {
                        m_banInput.Add(integer);
                        Console.WriteLine("Banned integer: " + integer);
                    }
                }
            }

            string relativeBanPath = "IntBan.txt";
            if (!File.Exists(relativeBanPath)) {
                File.WriteAllText(relativeBanPath,$"{EnumScratchToWarcraftGamepad.PressMenuLeft+0} {EnumScratchToWarcraftGamepad.PressMenuLeft + 1000} {EnumScratchToWarcraftGamepad.PressMenuLeft + 2000}");
            }

            string banIntegersText = File.ReadAllText(relativeBanPath);
            string [] banIntegersToken = banIntegersText.Split(new char[] { ' ', '\n' });
            foreach (string integer in banIntegersToken) {
                if (int.TryParse(integer.Trim(), out int i))
                    m_banInput.Add(i);
            }

            // Remove the Menu Left as it allows to leave path of exil and most game.
            //m_banInput.AddRange(new int[] { 1309 });


            Queue<byte[]> queueBytes = new Queue<byte[]>();
            Queue<string> queueText = new Queue<string>();


            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            //SAY HELLO
            ConsoleUI.DisplayWelcomeMessage();
            UDPIIDListenerThread udpIid = new UDPIIDListenerThread();
            udpIid.Launch(ref queueBytes,3615);
            // I removed the text version for the moment while I am coding the integer version.
            // Text is greate, but I am going in a more row version with an integer index and the logic outside of it.
            // Using text was good to push atomic command, but it is the reason of the D in IID (having a precised NTP action
            // Code to add later.
            // If you add text interpretor in this code you have to do it in all the remote control code.
            // When the interpretor is on the client side, you just need some interpretor on creat.io pip, Nuget for all the RC possible...
            //UDPTextListenerThread udpText = new UDPTextListenerThread();

            //udpText.Launch(ref queueText,3614);
            ConsoleUI.DisplayipAndPortTargets();



            Console.WriteLine("Port Integer: " + 3615);
            Console.WriteLine("Index Mapping: https://github.com/EloiStree/2024_08_29_ScratchToWarcraft.git");




            Console.WriteLine("Did you install ViGemBus?\n https://github.com/ViGEm/ViGEmBus/releases/tag/v1.21.442.0");


            m_integerToActions = new IntegerToActions[4];
            if (m_controllerNumber > 4)
                m_controllerNumber = 4;
            for (int i = 0; i < m_controllerNumber; i++){ 
                m_integerToActions[i]= new IntegerToActions(i + 1, new XboxSingleControllerExecuter());
                Thread.Sleep(100);
            }



            //bool useAllFeatureTest = false;
            //if (useAllFeatureTest) {
            //    foreach (var action in m_integerToActions[0].m_actions)
            //    {
            //        Console.WriteLine("Test:" + action.m_name);
            //        Thread.Sleep(2000);
            //        for (int i = 0; i < 4; i++)
            //        {
            //            m_integerToActions[i].FetchAndApply(action.m_pressInteger);
            //            Thread.Sleep(500);
            //        }
            //        Thread.Sleep(2000);
            //        for (int i = 0; i < 4; i++)
            //        {
            //            m_integerToActions[i].FetchAndApply(action.m_releaseInteger);
            //            Thread.Sleep(500);

            //        }

            //        Thread.Sleep(1000);
            //        for (int i = 0; i < 4; i++)
            //        {
            //            m_integerToActions[i].m_executer.Execute(new TimedXBoxAction_ReleaseAll(DateTime.Now));

            //        }

            //    }
            //}


            foreach (int b in banIntegersText)
            {
                Console.WriteLine("Ban Input:"+b);
            }

            Console.WriteLine("Ready to loop for udp action.");
            while (true)
            {
               // udpText.UpdateTheAutodestructionOfThreadTimer();
                udpIid.UpdateTheAutodestructionOfThreadTimer();
                while (queueBytes.Count > 0) { 
                
                    byte[] bytes = queueBytes.Dequeue();
                    Console.WriteLine($"{bytes.Length}: {bytes}");
                    if (bytes.Length == 4)
                    {
                        int integer = BitConverter.ToInt32(bytes, 0);
                        ProcessIndexIntegerReceviedWithoutDate(integer);
                    }
                    else if (bytes.Length == 8) { 
                    
                        int index = BitConverter.ToInt32(bytes, 0);
                        int value = BitConverter.ToInt32(bytes, 4);
                        ProcessIndexIntegerReceviedWithoutDate(index, value);
                    }
                    else if (bytes.Length == 16)
                    {

                        int index = BitConverter.ToInt32(bytes, 0);
                        int value = BitConverter.ToInt32(bytes, 4);
                        DateTime date = DateTime.FromBinary(BitConverter.ToInt64(bytes, 8));
                        ProcessIndexIntegerReceviedWithoutDate(index, value);
                    }
                    else if (bytes.Length == 12)
                    {
                        int value = BitConverter.ToInt32(bytes, 0);
                        ProcessIndexIntegerReceviedWithoutDate(value);
                    }
                }
                
                Thread.Sleep(1);
            }

        }

        public static void ProcessIndexIntegerReceviedWithoutDate(int index, int value)
        {

            if (value < 1000000)
            {
                
                ProcessValueAsScratchToWarcraftButton(index, value);

            }
            else if (value > 1700000000)
            {
                // That a XOMI 1899999999 action or 1700000000
                CheckForGamepadAction(index, value);

            }
            else {

                Int1899Parser.GetValue999999(value, out int value999999);
                Int1899Parser.GetPlayerId(value, out byte playerId);
                Int1899Parser.GetTag99(value, out byte tag99);
                if (tag99 == 19)
                {

                    ProcessValueAsScratchToWarcraftButton(playerId, value999999);
                }
                else {


                    CheckForGamepadAction( index, value);
                }

            }

          

        }

        private static void CheckForGamepadAction(int index, int value)
        {
            CheckForGamepadAction(m_integerToActions, index, value, DateTime.UtcNow);
        }

        private static void ProcessValueAsScratchToWarcraftButton(int playerId, int value999999)
        {
            if (IsBanGamepadButton(value999999))
                return;
            int indexGamepad = playerId;
            if (indexGamepad > 4)
                indexGamepad = 0;

            for (int selectGamepad = 0; selectGamepad < 4; selectGamepad++) {
                int gamepadNumber = selectGamepad + 1;
                IntegerToActions focusGamepad = m_integerToActions[selectGamepad];
                if (indexGamepad == 0 || indexGamepad == gamepadNumber) {

                    focusGamepad.FetchAndApply(value999999);
                }
            }

        }


        public static void ProcessIndexIntegerReceviedWithoutDate(int value)
        {
            ProcessIndexIntegerReceviedWithoutDate(0, value);

        }


        public static void CheckForGamepadAction(IntegerToActions[] integerToActions, int index, int value, DateTime utcNow)
        {
            if (value >= 1700000000)
            {

                int tag = value / 100000000;

                if (tag == 18)
                {



                    int integer = value % 100000000;
                    int value99000000LX = integer / 1000000 % 100;
                    int value00990000LY = integer / 10000 % 100;
                    int value00009900RX = integer / 100 % 100;
                    int value00000099RY = integer % 100;
                    Convert01To99ToPercent(value99000000LX, out float percent99000000LX);
                    Convert01To99ToPercent(value00990000LY, out float percent00990000LY);
                    Convert01To99ToPercent(value00009900RX, out float percent00009900RX);
                    Convert01To99ToPercent(value00000099RY, out float percent00000099RY);



                    TimedXBoxAction_DoubleJoysticksChange doubleJoystick =
                                            new TimedXBoxAction_DoubleJoysticksChange(utcNow,
                                            percent99000000LX,
                                            percent00990000LY,
                                            percent00009900RX,
                                            percent00000099RY);
                    int indexGamepad = index;
                    if (indexGamepad > 4)
                        indexGamepad = 0;
                    for (int selectGamepad = 0; selectGamepad < 4; selectGamepad++)
                    {
                        int gamepadNumber = selectGamepad + 1;
                        if (indexGamepad == 0 || indexGamepad == gamepadNumber)
                        {
                            IntegerToActions focusGamepad = m_integerToActions[selectGamepad];
                            integerToActions[selectGamepad].m_executer.Execute(doubleJoystick);
                            // Print a debug of the doubleJoystick

                            //Console.WriteLine($"Debug Double Joystick: {gamepadNumber} {percent99000000LX} {percent00990000LY} {percent00009900RX} {percent00000099RY}");


                        }
                    }
                }



            }
            else { 
            
                ff
            }
        }

        private static void Convert01To99ToPercent(int intValue99, out float percent)
        {
            if (intValue99 == 0)
            {
                percent = 0.0f;
            }
            else {
                percent = ((intValue99 - 1) / 98f-0.5f)*2f;
            }
        }

        public static bool FlushTimedActionIf1256(int integer, ref Queue<byte[]> queue)
        {
            if (integer == 1256 || integer == 2256) { 
                queue.Clear();
                return true;
            }
            return false;
        }

        public static bool IsNotBanGamepadButton(int integer)
        {
            return !m_banInput.Contains(integer);
        }
        public static bool IsBanGamepadButton(int integer)
        {
            return m_banInput.Contains(integer);
        }

    }
}
