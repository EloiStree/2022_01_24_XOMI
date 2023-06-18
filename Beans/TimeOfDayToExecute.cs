using System;

namespace XOMI.Beans
{
    internal class TimeOfDayToExecute
    {
        private DateTime m_now;

        public TimeOfDayToExecute(DateTime now, TimeInMilliseconds milliseconds)
        {
            m_now = now;
            m_now = m_now.AddMilliseconds(milliseconds.GetValue());
        }
        public TimeOfDayToExecute(DateTime now) : this(now, new TimeInMilliseconds(0))
        {
            m_now = now;
        }

        internal void GetTime(out DateTime whenToExecute)
        {
            whenToExecute = m_now;
        }
    }
}