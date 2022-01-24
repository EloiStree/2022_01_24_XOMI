using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace Test
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

    class Program
    {
        //➤ ☗ | ↓ ← → ↑ _ ‾ ∨ ∧ ¬ ⊗ ≡ ≤ ≥ ⌃ ⌄ ⊓⇅ ⊔⇵ ⊏ ⊐ ↱↳ ∑ -no unity ⤒ ⤓ ⌈ ⌊ 🀲 🀸 ⌛ ⏰ ▸ ▹ 🐁 🖱 💾


        public static XBoxActionStack m_waitingActions = new XBoxActionStack();
        public static Queue<string> m_udpPackageReceived = new Queue<string>();
        public static UDPListenerThread m_udpThread = new UDPListenerThread();
        static void Main(string[] args)
        {

            Console.WriteLine("XOMI");
            Thread.Sleep(1000);

            Console.WriteLine("Launch UDP Listener...");
            m_udpThread.Launch(ref m_udpPackageReceived, 2504);
            Console.WriteLine("Launched.");
            Console.WriteLine("");
            IpAccess.GetAllLocalIPv4(NetworkInterfaceType.Ethernet, out string[] ips);

            Console.WriteLine("IP: " + string.Join(", ", ips));
            Console.WriteLine("Port: 2504 ");
            Console.WriteLine("");



            ViGEmClient client = new ViGEmClient();
            IXbox360Controller controller = client.CreateXbox360Controller();
            controller.Connect();

            TextParseToActions parser = new TextParseToActions(m_waitingActions);

            List<TimedXBoxAction> actions = m_waitingActions.GetRefToActionStack();
            List<TimedXBoxAction> toRemove = new List<TimedXBoxAction>();


            while (true)
            {

                m_udpThread.StayAlivePing();
                if (m_udpPackageReceived.Count > 0)
                {
                    string s = m_udpPackageReceived.Dequeue();
                    Console.WriteLine("Received:"+s);
                    parser.TryToAppendParseToWaitingActions(s);

                }
                toRemove.Clear();
                DateTime now = DateTime.Now;
                for (int i = 0; i < actions.Count; i++)
                {
                    if (actions[i].GetWhenToExecute() <= now)
                    {
                        toRemove.Add(actions[i]);
                    }
                }
                for (int i = 0; i < toRemove.Count; i++)
                {
                    actions.Remove(toRemove[i]);

                }

                for (int i = 0; i < toRemove.Count; i++)
                {
                    if (toRemove[i] is TimedXBoxAction_ApplyChange)
                    {
                        TimedXBoxAction_ApplyChange toApply = (TimedXBoxAction_ApplyChange)toRemove[i];
                        Console.WriteLine(string.Format("{0}| {1}{3} {2}", toApply.GetWhenToExecute().ToString("yyyy-dd-HH-mm-ss-fff"), toApply.GetPressionType(), toApply.GetInputType()
                            , toApply.GetPressionType()==PressType.Press? "↓": "↑"));
                        bool pression = toApply.GetPressionType() == PressType.Press;

                        switch (toApply.GetInputType())
                        {
                            case XBoxInputType.ArrowLeft:
                                controller.SetButtonState(Xbox360Button.Left, pression);
                                break;
                            case XBoxInputType.ArrowRight:
                                controller.SetButtonState(Xbox360Button.Right, pression);
                                break;
                            case XBoxInputType.ArrowDown:
                                controller.SetButtonState(Xbox360Button.Down, pression);
                                break;
                            case XBoxInputType.ArrowUp:
                                controller.SetButtonState(Xbox360Button.Up, pression);
                                break;


                            case XBoxInputType.JoystickLeft_Left:
                                controller.SetAxisValue(Xbox360Axis.LeftThumbX, -32768);
                                break;
                            case XBoxInputType.JoystickLeft_Right:
                                controller.SetAxisValue(Xbox360Axis.LeftThumbX, 32767);
                                break;
                            case XBoxInputType.JoystickLeft_Down:
                                controller.SetAxisValue(Xbox360Axis.LeftThumbY, -32768);
                                break;
                            case XBoxInputType.JoystickLeft_Up:
                                controller.SetAxisValue(Xbox360Axis.LeftThumbY, 32767);
                                break;

                            case XBoxInputType.JoystickRight_Left:
                                controller.SetAxisValue(Xbox360Axis.RightThumbX, -32768);
                                break;
                            case XBoxInputType.JoystickRight_Right:
                                controller.SetAxisValue(Xbox360Axis.RightThumbX, 32767);
                                break;
                            case XBoxInputType.JoystickRight_Down:
                                controller.SetAxisValue(Xbox360Axis.RightThumbY, -32768);
                                break;
                            case XBoxInputType.JoystickRight_Up:
                                controller.SetAxisValue(Xbox360Axis.RightThumbY, 32767);
                                break;


                            case XBoxInputType.ButtonUp:
                                controller.SetButtonState(Xbox360Button.Y, pression);
                                break;
                            case XBoxInputType.ButtonDown:
                                controller.SetButtonState(Xbox360Button.A, pression);
                                break;
                            case XBoxInputType.ButtonRight:
                                controller.SetButtonState(Xbox360Button.B, pression);
                                break;
                            case XBoxInputType.ButtonLeft:
                                controller.SetButtonState(Xbox360Button.X, pression);
                                break;
                            case XBoxInputType.SideButtonLeft:
                                controller.SetButtonState(Xbox360Button.LeftShoulder, pression);
                                break;
                            case XBoxInputType.SideButtonRight:
                                controller.SetButtonState(Xbox360Button.RightShoulder, pression);
                                break;
                            case XBoxInputType.TriggerLeft:
                                controller.SetSliderValue(Xbox360Slider.LeftTrigger, pression ? (byte)255 : (byte)0);
                                break;
                            case XBoxInputType.TriggerRight:
                                controller.SetSliderValue(Xbox360Slider.RightTrigger, pression ? (byte)255 : (byte)0);
                                break;
                            case XBoxInputType.MenuLeft:
                                controller.SetButtonState(Xbox360Button.Back, pression);
                                break;
                            case XBoxInputType.MenuRight:
                                controller.SetButtonState(Xbox360Button.Start, pression);
                                break;
                            case XBoxInputType.XboxButton:
                                //Check if it is correct.
                                controller.SetButtonState(Xbox360Button.Guide, pression);
                                break;
                            case XBoxInputType.JoystickLeftButton:
                                controller.SetButtonState(Xbox360Button.LeftThumb, pression);
                                break;
                            case XBoxInputType.JoystickRightButton:
                                controller.SetButtonState(Xbox360Button.RightThumb, pression);
                                break;
                            case XBoxInputType.Undefined:
                                break;
                            default:
                                break;
                        }




                    }
                }


                Thread.Sleep(1);

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
