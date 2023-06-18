using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections.Generic;
using System.Globalization;
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

    class Program
    {
        //➤ ☗ | ↓ ← → ↑ _ ‾ ∨ ∧ ¬ ⊗ ≡ ≤ ≥ ⌃ ⌄ ⊓⇅ ⊔⇵ ⊏ ⊐ ↱↳ ∑ -no unity ⤒ ⤓ ⌈ ⌊ 🀲 🀸 ⌛ ⏰ ▸ ▹ 🐁 🖱 💾



        public static XBoxActionStack m_waitingActions = new XBoxActionStack();
        public static Queue<string> m_udpPackageReceived = new Queue<string>();
        public static UDPListenerThread m_udpThread = new UDPListenerThread();
        public static XboxSingleControllerExecuter m_xboxExecuter;
        static void Main(string[] args)
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
                        if (StaticVariable.m_debugDevMessage)
                            Console.WriteLine("Received UDP Message:" + s);
                        parser.TryToAppendParseToWaitingActions(s);
                    }
                    CheckForActionToExecute(actionInWaitingToBeExecuted, readyToBeExecutedAndRemoved);

                    bool requestFlush = false;
                    for (int i = 0; i < readyToBeExecutedAndRemoved.Count; i++)
                    {
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
            m_udpThread.Launch(ref m_udpPackageReceived, StaticUserPreference.m_port);
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
