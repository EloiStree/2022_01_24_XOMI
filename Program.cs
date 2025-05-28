using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using XOMI.Unstore;

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
}
