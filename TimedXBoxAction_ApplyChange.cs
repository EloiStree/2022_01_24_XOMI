using System;

namespace Test
{
    public class TimedXBoxAction_ApplyChange : TimedXBoxAction
    {
        private DateTime m_timeCount;
        private PressType m_press;
        private XBoxInputType m_inputType;

        public TimedXBoxAction_ApplyChange(DateTime timeCount, PressType press, XBoxInputType inputType) : base(timeCount)
        {
            this.m_press = press;
            this.m_inputType = inputType;
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
}