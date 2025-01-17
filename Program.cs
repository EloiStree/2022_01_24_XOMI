using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading;
using XOMI.InfoHolder;
using XOMI.Parse;
using XOMI.Static;
using XOMI.TimedAction;
using XOMI.UDP;
using XOMI.UI;
using XOMI.Unstore.Xbox;

namespace XOMI
{



    public enum PressType { Press, Release }
    public enum XBoxInputType
    {
        ArrowLeft, ArrowRight, ArrowDown, ArrowUp,
        JoystickLeft_Left,
        JoystickLeft_Right,
        JoystickLeft_Down,
        JoystickLeft_Up,
        JoystickRight_Left,
        JoystickRight_Right,
        JoystickRight_Down,
        JoystickRight_Up,
        ButtonUp, ButtonDown, ButtonRight, ButtonLeft,
        SideButtonLeft, SideButtonRight,
        TriggerLeft, TriggerRight,
        MenuLeft, MenuRight,
        XboxButton,
        JoystickLeftButton,
        JoystickRightButton,
        Undefined
    }

    public enum XBoxAxisInputType
    {
        JoystickLeft_Left2Right,
        JoystickLeft_Down2Up,
        JoystickRight_Left2Right,
        JoystickRight_Down2Up,
        TriggerLeft, TriggerRight,
        Undefined
    }
    public enum XBoxJoystickInputType
    {
        JoystickLeft, JoystickRight,
        Undefined
    }

    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World :) !");
            ProgramV2_FourControllerIntegerVersion.Run(args);
            // ProgramV1_TextVersion.Run(args);

        }
    }

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
                        if (FlushTimedActionIf1256(integer, ref queueBytes))
                            continue;
                        Console.WriteLine($"I{integer}");
                        for (int i = 0; i < 4; i++)
                        {
                            if (IsNotBan(integer)) { 
                                if (m_integerToActions[i]!=null)
                                m_integerToActions[i].FetchAndApply(integer);
                            }
                        }
                    }
                    else if (bytes.Length == 8) { 
                    
                        int index = BitConverter.ToInt32(bytes, 0);
                        int value = BitConverter.ToInt32(bytes, 4);

                        if (FlushTimedActionIf1256(value, ref queueBytes))
                            continue;
                        Console.WriteLine($"II{index} {value}");
                        if (IsNotBan(value)) { 
                            if (index >= 1 && index < 4)
                            {
                                if (m_integerToActions[index - 1] != null)
                                    m_integerToActions[index - 1].FetchAndApply(value);
                            }
                            else { 
                        
                                for (int i = 0; i < 4; i++)
                                {
                                    if (m_integerToActions[i] != null)
                                        m_integerToActions[i].FetchAndApply(value);
                                }
                            }
                        }
                    }
                    else if (bytes.Length == 16)
                    {
                        int index = BitConverter.ToInt32(bytes, 0);
                        int value = BitConverter.ToInt32(bytes, 4);

                        if (FlushTimedActionIf1256(value, ref queueBytes))
                            continue;
                        Console.WriteLine($"IID{index} {value}");
                        if (IsNotBan(value)) { 
                            DateTime date = DateTime.FromBinary(BitConverter.ToInt64(bytes, 8));
                            if (index >= 1 && index < 4)
                            {
                                if (m_integerToActions[index - 1] != null)
                                    m_integerToActions[index - 1].FetchAndApply(value);
                            }
                            else
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (m_integerToActions[i] != null)
                                        m_integerToActions[i].FetchAndApply(value);
                                }
                            }
                        }
                    }
                    else if (bytes.Length == 12)
                    {
                        int value = BitConverter.ToInt32(bytes, 0);

                        if (FlushTimedActionIf1256(value, ref queueBytes))
                            continue;
                        Console.WriteLine($"ID {value}");
                        if (IsNotBan(value)) { 
                            DateTime date = DateTime.FromBinary(BitConverter.ToInt64(bytes, 4));
                            for (int i = 0; i < 4; i++)
                            {
                                if (m_integerToActions[i] != null)
                                    m_integerToActions[i].FetchAndApply(value);
                            }
                        }
                    }
                }
                
                Thread.Sleep(1);
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

        private static bool IsNotBan(int integer)
        {
            return !m_banInput.Contains(integer);
        }

    }

    public class IndexIntegerDateQueue { 
    
        public List<IndexIntegerDate> m_queue = new List<IndexIntegerDate>();
        public struct IndexIntegerDate { 
            public int m_index;
            public int m_integer;
            public DateTime m_date;
            public IndexIntegerDate(int index, int integer, DateTime date) {
                m_index = index;
                m_integer = integer;
                m_date = date;
            }
        }

        public void Enqueue(int index, int value, DateTime time) { 
        
            m_queue.Add(new IndexIntegerDate(index, value, time));
        }

        public void DequeueActionReady() {

            // Remote from top of the stack to start if date less that now
            for(int i= m_queue.Count-1; i>=0; i--) {
                if (m_queue[i].m_date <= DateTime.Now) {
                    
                    PushToAction( m_queue[i]);
                    m_queue.RemoveAt(i);
                }
            }
        }

        private void PushToAction( IndexIntegerDate indexIntegerDate)
        {
            Console.WriteLine("PushToAction:" + indexIntegerDate.m_index + " " + indexIntegerDate.m_integer);
        }
    }

    class ProgramV1_TextVersion
    {
        //➤ ☗ | ↓ ← → ↑ _ ‾ ∨ ∧ ¬ ⊗ ≡ ≤ ≥ ⌃ ⌄ ⊓⇅ ⊔⇵ ⊏ ⊐ ↱↳ ∑ -no unity ⤒ ⤓ ⌈ ⌊ 🀲 🀸 ⌛ ⏰ ▸ ▹ 🐁 🖱 💾



        public static XBoxActionStack m_waitingActions = new XBoxActionStack();
        public static Queue<string> m_udpPackageReceived = new Queue<string>();
        public static UDPTextListenerThread m_udpThread = new UDPTextListenerThread();
        public static XboxSingleControllerExecuter m_xboxExecuter;
        public static void Run(string[] args)
        {

            // To Add later
            //↓ ← → ↑ as array click
            //jl↓ jl← jl→ jl↑ as array click
            //jr↓ jr← jr→ jr↑  jr↖ jr↗ jr↘ jr↙ as array click


            // ←↑→=↓↔=↕↖↗↘↙ 	◰◱◲◳


            for(int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-p" && i + 1 < args.Length)
                {
                    int.TryParse(args[i + 1], out StaticUserPreference.m_port);
                }
                if (args[i] == "-d")
                {
                    StaticUserPreference.m_wantDebugInfo = true;
                }
            }
            StaticVariable.m_debugUserMessage = StaticUserPreference.m_wantDebugInfo;

            ///////////SET THE APP
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;


            //SAY HELLO
            ConsoleUI.DisplayWelcomeMessage();
            LaunchTheUdpThreadListener();
            ConsoleUI.DisplayipAndPortTargets();

            try
            {
                m_xboxExecuter = new XboxSingleControllerExecuter();

            }
            catch (Exception e) {

                ConsoleUI.DrawLine();
                Console.WriteLine("Impossible to create the controller. Error happened:" + e.StackTrace);
                ConsoleUI.DrawLine();
                Console.WriteLine("Make sure you installed ViGEm.");
                Console.WriteLine("Contact me on GitHub or Discord for Help");
            }

            try
            {
              
                TextParseToActions parser = new TextParseToActions(m_waitingActions);
                List<TimedXBoxAction> actionInWaitingToBeExecuted = m_waitingActions.GetRefToActionStack();
                List<TimedXBoxAction> readyToBeExecutedAndRemoved = new List<TimedXBoxAction>();


                while (true)
                {
                    m_udpThread.UpdateTheAutodestructionOfThreadTimer();
                    if (m_udpPackageReceived.Count > 0)
                    {
                        string s = m_udpPackageReceived.Dequeue();
                        //if (StaticVariable.m_debugDevMessage)
                            Console.WriteLine("Received UDP Message:" + s);
                        parser.TryToAppendParseToWaitingActions(s);
                    }
                    CheckForActionToExecute(actionInWaitingToBeExecuted, readyToBeExecutedAndRemoved);

                    bool requestFlush = false;
                    for (int i = 0; i < readyToBeExecutedAndRemoved.Count; i++)
                    {
                      //  Console.WriteLine("E#" + readyToBeExecutedAndRemoved.GetType());
                        if (readyToBeExecutedAndRemoved[i] is TimedXBoxAction_ApplyChange)
                        {
                            TimedXBoxAction_ApplyChange toApply = (TimedXBoxAction_ApplyChange)readyToBeExecutedAndRemoved[i];
                            m_xboxExecuter.Execute(toApply);
                        }
                        if (readyToBeExecutedAndRemoved[i] is TimedXBoxAction_AxisChange)
                        {
                            TimedXBoxAction_AxisChange toApply = (TimedXBoxAction_AxisChange)readyToBeExecutedAndRemoved[i];
                            m_xboxExecuter.Execute(toApply);
                        }
                        if (readyToBeExecutedAndRemoved[i] is TimedXBoxAction_JoysticksChange)
                        {
                            TimedXBoxAction_JoysticksChange toApply = (TimedXBoxAction_JoysticksChange)readyToBeExecutedAndRemoved[i];
                            m_xboxExecuter.Execute(toApply);
                        }
                        if (readyToBeExecutedAndRemoved[i] is TimedXBoxAction_FlushAllCommands)
                        {
                            requestFlush = true;

                        }
                        if (readyToBeExecutedAndRemoved[i] is TimedXBoxAction_Disconnect)
                        {
                            TimedXBoxAction_Disconnect toApply = (TimedXBoxAction_Disconnect)readyToBeExecutedAndRemoved[i];
                            m_xboxExecuter.Execute(toApply);
                        }
                        if (readyToBeExecutedAndRemoved[i] is TimedXBoxAction_Connect)
                        {
                            TimedXBoxAction_Connect toApply = (TimedXBoxAction_Connect)readyToBeExecutedAndRemoved[i];
                            m_xboxExecuter.Execute(toApply);
                        }

                        if (readyToBeExecutedAndRemoved[i] is TimedXBoxAction_ReleaseAll)
                        {

                            TimedXBoxAction_ReleaseAll toApply = (TimedXBoxAction_ReleaseAll)readyToBeExecutedAndRemoved[i];
                            m_xboxExecuter.Execute(toApply);
                        }
                    }
                    if (requestFlush)
                    {
                        Console.WriteLine("Flush Requested, Deleted:" + actionInWaitingToBeExecuted.Count);
                        actionInWaitingToBeExecuted.Clear();
                        requestFlush = false;
                    }



                    Thread.Sleep(1);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception happen:" + e.StackTrace);
                Console.WriteLine("Contact me if you need help: https://eloistree.page.link/discord");
                Console.WriteLine("Did you install ViGemBus?\n https://github.com/ViGEm/ViGEmBus/releases/tag/v1.21.442.0");
            }
        }

        private static void CheckForActionToExecute(List<TimedXBoxAction> actionInWaitingToBeExecuted, List<TimedXBoxAction> readyToBeExecutedAndRemoved)
        {
            readyToBeExecutedAndRemoved.Clear();
            DateTime now = DateTime.Now;
            for (int i = 0; i < actionInWaitingToBeExecuted.Count; i++)
            {
                if (actionInWaitingToBeExecuted[i].GetWhenToExecute() <= now)
                {
                    readyToBeExecutedAndRemoved.Add(actionInWaitingToBeExecuted[i]);
                }
            }
            for (int i = 0; i < readyToBeExecutedAndRemoved.Count; i++)
            {
                actionInWaitingToBeExecuted.Remove(readyToBeExecutedAndRemoved[i]);

            }
        }

        private static void LaunchTheUdpThreadListener()
        {
            int i = 0;
            int port = StaticUserPreference.m_port;
            bool succeedToCreatePort=false;
            while (!succeedToCreatePort && i<20) {

                Console.WriteLine("Attempt udp connection to " + (port+i));
                if (IsPortOpen(port + i))
                {
                    Thread.Sleep(10);
                    m_udpThread.Launch(ref m_udpPackageReceived, port + i);
                    succeedToCreatePort = true;
                }
                else { 
                    i++;
                }
            }
            StaticUserPreference.m_port = port + i;
        }

        static bool IsPortOpen(int port)
        {
            try
            {
                using (var client = new UdpClient(port))
                {


                    client.Close();
                    client.Dispose();
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        private static void  Multi(ref StringBuilder sb,  string text, int count)
        {
            for (int i = 0; i < count; i++)
            {
                sb.Append(text);
            }
        }
    }
}




public class IntegerToActions {
    public int m_integerIndex= 0;
    public bool m_useDebugConsole = true;
    public void FetchAndApply(int value) { 
    
        for (int i = 0;i < m_actions.Count ;i++) {
            if (m_actions[i].IsPressing(value)) { 
                m_actions[i].Press();
                if(m_useDebugConsole)
                    System.Console.WriteLine($"Pressed ({value}):{m_actions[i].GetDescription()}");
                return;
            }
            if (m_actions[i].IsReleasing(value)) { 
                m_actions[i].Release();
                if (m_useDebugConsole)
                    System.Console.WriteLine($"Release ({value}):{m_actions[i].GetDescription()}");
                return;
            }
        }
        if (m_useDebugConsole)
            System.Console.WriteLine($"No action for ({value})");
    }

    public class PressReleaseIntegerAction {
        public string m_name;
        public int m_pressInteger;
        public int m_releaseInteger;
        public Action m_pressAction;
        public Action m_releaseAction;
        public PressReleaseIntegerAction(string name, int press, int release, Action pressAction, Action releaseAction) {
            m_name = name;
            m_pressInteger = press;
            m_releaseInteger = release;
            m_pressAction = pressAction;
            m_releaseAction = releaseAction;
        }

        public bool IsPressing(int value) {
            return m_pressInteger ==value;
        }
        public bool IsReleasing(int value)
        {
            return m_releaseInteger == value;
        }

        public void Press() {
            if(m_pressAction!=null)
                m_pressAction();
        }
        public void Release()
        {
            if (m_releaseAction != null)
                m_releaseAction();
        }   
        public string GetDescription() {
            return m_name;
        }
    }

    public List<PressReleaseIntegerAction> m_actions = new List<PressReleaseIntegerAction>();


    public void Add(string name, int press, Action pressAction, Action releaseAction)
    {
        m_actions.Add(new PressReleaseIntegerAction(name, press, press + 1000, pressAction, releaseAction));
    }
    public void Add(string name, int press, int release, Action pressAction, Action releaseAction)
    {
        m_actions.Add(new PressReleaseIntegerAction(name, press, release, pressAction, releaseAction));
    }
    public int m_index = 0;
    public XboxSingleControllerExecuter m_executer = null;

    public DateTime Now() { return DateTime.Now; }

    public float RandomFloat11() { return (float)(new Random().NextDouble() * 2 - 1); }

    public IntegerToActions(int index, XboxSingleControllerExecuter executor)
    {
        m_index = index;
        m_executer = executor;

        Add("Random input for all gamepads, no menu", 1399, 2399
            , () => {


                m_executer.Randomize_AllButMenu();
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_ReleaseAll(Now()));
            }
        );
        Add("Enable hardware joystick ON/OFF", 1390, 2390
            , () => { }
            , () => { }
        );

        Add("Release All Button", 1390, 2390,
            () =>
            {
                m_executer.Execute(new TimedXBoxAction_ReleaseAll(Now()));
            },
            () =>
            {
                m_executer.Execute(new TimedXBoxAction_ReleaseAll(Now()));
            }
         );
        Add("Release All Button But Menu", 1391, 2391,
            () =>
            {
                m_executer.Execute(new TimedXBoxAction_ReleaseAllButMenu(Now()));
            },
            () =>
            {
                m_executer.Execute(new TimedXBoxAction_ReleaseAllButMenu(Now()));
            }
         );
        Add("Press A button", 1300, 2300
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ButtonDown)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ButtonDown)); }
        );
        Add("Press X button", 1301, 2301
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ButtonLeft)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ButtonLeft)); }
        );
        Add("Press B button", 1302, 2302
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ButtonRight)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ButtonRight)); }

        );
        Add("Press Y button", 1303, 2303
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ButtonUp)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ButtonUp)); }

        );
        Add("Press left side button", 1304, 2304
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.SideButtonLeft)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.SideButtonLeft)); }
);
        Add("Press right side button", 1305, 2305
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.SideButtonRight)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.SideButtonRight)); }

        );
        Add("Press left stick", 1306, 2306
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.JoystickLeftButton)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.JoystickLeftButton)); }

        );
        Add("Press right stick", 1307, 2307
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.JoystickRightButton)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.JoystickRightButton)); }

        );
        Add("Press menu right", 1308, 2308
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.MenuLeft)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.MenuLeft)); }

        );
        Add("Press menu left", 1309, 2309
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.MenuRight)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.MenuRight)); }

        );
        Add("Release D-pad", 1310, 2310
            , () => { 
            
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowLeft));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowRight));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowDown));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowUp));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowLeft));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowRight));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowDown));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowUp));
            }
        );
        Add("Press arrow north", 1311, 2311
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowUp)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowUp)); }

        );
        Add("Press arrow northeast", 1312, 2312
            , () => {
            
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowRight));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowUp));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowRight));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowUp));
            }
        );
        Add("Press arrow east", 1313, 2313
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowRight)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowRight)); }

        );
        Add("Press arrow southeast", 1314, 2314
            , () => { 
            
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowRight));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowDown));
            }
            , () => {

                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowRight));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowDown));
            }
        );
        Add("Press arrow south", 1315, 2315
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowDown)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowDown)); }

        );
        Add("Press arrow southwest", 1316, 2316
            , () => {
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowLeft));
                    m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowDown));

            }
            , () => {

                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowLeft));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowDown));
            }
        );
        Add("Press arrow west", 1317, 2317
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowLeft)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowLeft)); }

        );
        Add("Press arrow northwest", 1318, 2318
            , () => {
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowLeft));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.ArrowUp));

            }
            , () => {

                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowLeft));
                m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.ArrowUp));
            }
        );
        Add("Press Xbox home button", 1319, 2319
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Press, XOMI.XBoxInputType.XboxButton)); }
            , () => { m_executer.Execute(new TimedXBoxAction_ApplyChange(Now(), XOMI.PressType.Release, XOMI.XBoxInputType.XboxButton)); }
        );
        Add("Random axis", 1320, 2320
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, RandomFloat11(), RandomFloat11()));
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, RandomFloat11(), RandomFloat11()));

            }
            , () => {

                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0,0));
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0,0));
            }
        );
        Add("Start recording", 1321, 2321
            , () => {  }
            , () => {  }
        );
        Add("Set left stick to neutral(clockwise)  ", 1330, 2330

            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0, 0));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0, 0));
            }
        );
        Add("Move left stick up", 1331, 2331
            , () => { 
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft,0, 1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0, 0));
            }
        );
        Add("Move left stick up-right", 1332, 2332
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft,1,1 ));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft,0,0 ));
            }
        );
        Add("Move left stick right", 1333, 2333
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 1, 0));

            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0, 0));
            }
        );
        Add("Move left stick down-right", 1334, 2334
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 1, -1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0, 0));
            }
        );
        Add("Move left stick down", 1335, 2335
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0, -1));

            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0,0));

            }
        );
        Add("Move left stick down-left", 1336, 2336
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, -1,-1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0, 0));
            }
        );
        Add("Move left stick left", 1337, 2337
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, -1, 0));

            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0, 0));
            }
        );
        Add("Move left stick up-left", 1338, 2338
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, -1, 1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickLeft, 0, 0));
            }
        );
        Add("Set right stick to neutral(clockwise)",1340, 2340
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, 0));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, 0));
            }
        );
        Add("Move right stick up", 1341, 2341
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, 1));

            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, 0));
            }
        );
        Add("Move right stick up-right", 1342, 2342
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 1, 1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, 0));
            }
        );
        Add("Move right stick right", 1343, 2343
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 1, 0));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0,0));
            }
        );
        Add("Move right stick down-right", 1344, 2344
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 1, -1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, 0));
            }
        );
        Add("Move right stick down", 1345, 2345
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, -1));

            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, 0));

            }
        );
        Add("Move right stick down-left", 1346, 2346
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, -1, -1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, 0));
            }
        );
        Add("Move right stick left", 1347, 2347
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, -1, 0));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, 0));
            }
        );
        Add("Move right stick up-left", 1348, 2348
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, -1, 1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_JoysticksChange(Now(), XOMI.XBoxJoystickInputType.JoystickRight, 0, 0));
            }
        );
        Add("Set left stick horizontal to 1.0", 1350, 2350
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0));
            }
        );
        Add("Set left stick horizontal to -1.0", 1351, 2351
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, -1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0));
            }
        );
        Add("Set left stick vertical to 1.0", 1352, 2352
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0));
            }
        );
        Add("Set left stick vertical to -1.0", 1353, 2353
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, -1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0));
            }
        );
        Add("Set right stick horizontal to 1.0", 1354, 2354
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0));
            }
        );
        Add("Set right stick horizontal to -1.0", 1355, 2355
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, -1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0));
            }
        );
        Add("Set right stick vertical to 1.0", 1356, 2356
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0));
            }
        );
        Add("Set right stick vertical to -1.0", 1357, 2357
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, -1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0));
            }
        );
        Add("Set left trigger to 100%", 1358, 2358
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerLeft, 1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerLeft, 0));
            }
        );
        Add("Set right trigger to 100%", 1359, 2359
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerRight, 1));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerRight, 0));
            }
        );
        Add("Set left stick horizontal to 0.75", 1360, 2360
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0.75f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0));
            }
        );
        Add("Set left stick horizontal to -0.75", 1361, 2361
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, -0.75f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0));
            }
        );
        Add("Set left stick vertical to 0.75", 1362, 2362
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0.75f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0));
            }
        );
        Add("Set left stick vertical to -0.75", 1363, 2363
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, -0.75f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0));
            }
        );
        Add("Set right stick horizontal to 0.75", 1364, 2364
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0.75f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0));
            }
        );
        Add("Set right stick horizontal to -0.75", 1365, 2365
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, -0.75f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0));
            }
        );
        Add("Set right stick vertical to 0.75", 1366, 2366
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0.75f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0));
            }
        );
        Add("Set right stick vertical to -0.75", 1367, 2367
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, -0.75f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0));
            }
        );
        Add("Set left trigger to 75%", 1368, 2368
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerLeft, 0.75f));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerLeft, 0));
            }
        );
        Add("Set right trigger to 75%", 1369, 2369
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerRight, 0.75f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerRight, 0));
            }
        );
        Add("Set left stick horizontal to 0.5", 1370, 2370
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0.5f));
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0));
                
            }
        );
        Add("Set left stick horizontal to -0.5", 1371, 2371
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, -0.5f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0));
            }
        );
        Add("Set left stick vertical to 0.5", 1372, 2372
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0.5f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0));
            }
        );
        Add("Set left stick vertical to -0.5", 1373, 2373
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, -0.5f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0));
            }
        );
        Add("Set right stick horizontal to 0.5", 1374, 2374
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0.5f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0));
            }
        );
        Add("Set right stick horizontal to -0.5", 1375, 2375
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, -0.5f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0));
            }
        );
        Add("Set right stick vertical to 0.5", 1376, 2376
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0.5f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0));
            }
        );
        Add("Set right stick vertical to -0.5", 1377, 2377
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, -0.5f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0));
            }
        );
        Add("Set left trigger to 50%", 1378, 2378
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerLeft, 0.5f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerLeft, 0));
            }
        );
        Add("Set right trigger to 50%", 1379, 2379
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerRight, 0.5f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerRight, 0));
            }
        );
        Add("Set left stick horizontal to 0.25", 1380, 2380
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0.25f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0));
            }
        );
        Add("Set left stick horizontal to -0.25", 1381, 2381
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, -0.25f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Left2Right, 0));
            }
        );
        Add("Set left stick vertical to 0.25", 1382, 2382
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0.25f));
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0));
            }
        );
        Add("Set left stick vertical to -0.25", 1383, 2383
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, -0.25f));
                
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickLeft_Down2Up, 0));
                
            }
        );
        Add("Set right stick horizontal to 0.25", 1384, 2384
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0.25f));
                
            }
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0));
            }
        );
        Add("Set right stick horizontal to -0.25", 1385, 2385
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, -0.25f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Left2Right, 0));
            }
        );
        Add("Set right stick vertical to 0.25", 1386, 2386
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0.25f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0));
            }
        );
        Add("Set right stick vertical to -0.25", 1387, 2387
            , () => {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, -0.25f));
                
            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.JoystickRight_Down2Up, 0));
            }
        );
        Add("Set left trigger to 25%", 1388, 2388
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerLeft, 0.25f));

            }
            , () => {
                
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerLeft, 0));
            }
        );
        Add("Set right trigger to 25%", 1389, 2389
            , () =>
            {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerRight, 0.25f));
                
            }
            , () =>
            {
                m_executer.Execute(new TimedXBoxAction_AxisChange(Now(), XOMI.XBoxAxisInputType.TriggerRight, 0));
                
            });
        }
    }
