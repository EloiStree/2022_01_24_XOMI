using System;

namespace Test
{
    public class TimeInMilliseconds
    {
        private int m_milliseconds;

        public TimeInMilliseconds(int milliseconds)
        {
            this.m_milliseconds = milliseconds;
        }

        public int GetValue()
        {
            return m_milliseconds;
        }
    }
}