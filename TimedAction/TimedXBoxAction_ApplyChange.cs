using System;

namespace XOMI.TimedAction
{
    public class TimedXBoxAction_ApplyChange : TimedXBoxAction
    {
        private PressType m_press;
        private XBoxInputType m_inputType;

        public TimedXBoxAction_ApplyChange(DateTime whenToExecute, PressType press, XBoxInputType inputType) : base(whenToExecute)
        {
            m_press = press;
            m_inputType = inputType;
        }

        public XBoxInputType GetInputType()
        {
            return m_inputType;
        }

        public PressType GetPressionType()
        {
            return m_press;
        }
    }

    public class TimedXBoxAction_AxisChange : TimedXBoxAction
    {
        private XBoxAxisInputType m_inputType;
        private float m_percentValue;
        public TimedXBoxAction_AxisChange(DateTime timeCount, XBoxAxisInputType inputType, float percent) : base(timeCount)
        {
            m_percentValue = percent;
            m_inputType = inputType;
        }

        public XBoxAxisInputType GetInputType()
        {
            return m_inputType;
        }

        public float GetPercentToApply()
        {
            return m_percentValue;
        }
    }

    public class TimedXBoxAction_JoysticksChange : TimedXBoxAction
    {
        private XBoxJoystickInputType m_inputType;
        private float m_percentXLeft2Right;
        private float m_percentYBot2Top;

        public TimedXBoxAction_JoysticksChange(DateTime timeCount, XBoxJoystickInputType inputType, float percentXLeft2Right, float percentYBot2Top) : base(timeCount)
        {
            m_inputType = inputType;
            m_percentXLeft2Right = percentXLeft2Right;
            m_percentYBot2Top = percentYBot2Top;
        }

        public XBoxJoystickInputType GetInputType()
        {
            return m_inputType;
        }

        public float GetPercentHorizontalLeftRightToApply()
        {
            return m_percentXLeft2Right;
        }
        public float GetPercentVerticalBotTopToApply()
        {
            return m_percentYBot2Top;
        }
    }
    public class TimedXBoxAction_FlushAllCommands : TimedXBoxAction
    {
        public TimedXBoxAction_FlushAllCommands(DateTime whenToExecute) : base(whenToExecute)
        {
        }
    }
    public class TimedXBoxAction_ReleaseAll : TimedXBoxAction
    {
        public TimedXBoxAction_ReleaseAll(DateTime whenToExecute) : base(whenToExecute)
        {
        }
    }
    public class TimedXBoxAction_ReleaseAllButMenu : TimedXBoxAction
    {
        public TimedXBoxAction_ReleaseAllButMenu(DateTime whenToExecute) : base(whenToExecute)
        {
        }
    }
    public class TimedXBoxAction_Disconnect : TimedXBoxAction
    {
        public TimedXBoxAction_Disconnect(DateTime whenToExecute) : base(whenToExecute)
        {
        }
    }
    public class TimedXBoxAction_Connect : TimedXBoxAction
    {
        public TimedXBoxAction_Connect(DateTime whenToExecute) : base(whenToExecute)
        {
        }
    }
    public class TimedXBoxAction_Exit : TimedXBoxAction
    {
        public TimedXBoxAction_Exit(DateTime whenToExecute) : base(whenToExecute)
        {
        }
    }
    public class TimedXBoxAction_Restart : TimedXBoxAction
    {
        public TimedXBoxAction_Restart(DateTime whenToExecute) : base(whenToExecute)
        {
        }
    }
}
    