﻿using System;

namespace XOMI.Beans
{
    public class TimeInMilliseconds
    {
        private int m_milliseconds;

        public TimeInMilliseconds(int milliseconds)
        {
            m_milliseconds = milliseconds;
        }

        public int GetValue()
        {
            return m_milliseconds;
        }
    }
}