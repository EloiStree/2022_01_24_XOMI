using System;
using XOMI.ParseItemClass;

namespace XOMI
{
    internal class ParseItem_PressInput : ParseItem
    {
        public PressType press;
        public XBoxInputType inputType;

        public ParseItem_PressInput(PressType press, XBoxInputType inputType)
        {
            this.press = press;
            this.inputType = inputType;
        }
    }
    public class ParseItem_OneAxisPercent : ParseItem
    {
        public XBoxAxisInputType m_inputType;
        public float m_pressionPercent;

        public ParseItem_OneAxisPercent(XBoxAxisInputType inputType, float percent)
        {
            this.m_pressionPercent = percent;
            this.m_inputType = inputType;
        }
    }
    public class ParseItem_JoystickPercent : ParseItem
    {
        public XBoxJoystickInputType m_inputType;
        public float m_percentHorizontal;
        public float m_percentVertical;

        public ParseItem_JoystickPercent(XBoxJoystickInputType joystick, float percentHorizontal, float percentVertical)
        {
            m_inputType = joystick;
            this.m_percentHorizontal = percentHorizontal;
            this.m_percentVertical = percentVertical;
        }
    }
    public class ParseItem_FlushAllCommands : ParseItem
    {
        public ParseItem_FlushAllCommands() 
        {
        }
    }
    public class ParseItem_ReleaseAll : ParseItem
    {
        public ParseItem_ReleaseAll()
        {
        }
    }
    public class ParseItem_Connect : ParseItem
    {
        public ParseItem_Connect()
        {
        }
    }
    public class ParseItem_Disconnect : ParseItem
    {
        public ParseItem_Disconnect()
        {
        }
    }
}