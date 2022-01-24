using System;

namespace Test
{
    internal class TimeOfDayToExecute
    {
        private DateTime m_now;

        public TimeOfDayToExecute(DateTime now, TimeInMilliseconds milliseconds)
        {
            this.m_now = now;
            this.m_now = this.m_now.AddMilliseconds(milliseconds.GetValue());
        }
        public TimeOfDayToExecute(DateTime now): this(now, new TimeInMilliseconds(0))
        {
            this.m_now = now;
        }

        internal void GetTime(out DateTime whenToExecute)
        {
            whenToExecute = m_now;
        }
    }
}