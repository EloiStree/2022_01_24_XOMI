using System;

namespace Test
{
    public  class ParseItem_DelayNextItems : ParseItem
    {
        private int m_timeInMilliseconds;

        public ParseItem_DelayNextItems(int timeInMilliseconds)
        {
            this.m_timeInMilliseconds = timeInMilliseconds;
        }

        public double GetValueInMilliseconds()
        {
            return m_timeInMilliseconds;
        }
    }
}