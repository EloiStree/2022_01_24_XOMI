using System;

namespace XOMI.ParseItemClass
{
    public class ParseItem_DelayNextItems : ParseItem
    {
        private int m_timeInMilliseconds;

        public ParseItem_DelayNextItems(int timeInMilliseconds)
        {
            m_timeInMilliseconds = timeInMilliseconds;
        }

        public double GetValueInMilliseconds()
        {
            return m_timeInMilliseconds;
        }
    }
}