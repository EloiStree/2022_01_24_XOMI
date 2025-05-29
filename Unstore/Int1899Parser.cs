using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOMI.Int1899
{
    public  class Int1899Parser
    {
        public static void GetPlayerId(int value, out byte playerId1To18)
        {
            value = (value / 100000000);
            if (value < 0)
                value = -value;
            playerId1To18 = (byte)(value % 20);
        }
        public static void GetTag99(int value, out byte tag99)
        {
            int tagPart = (value / 1000000) % 100;
            if (tagPart < 0) tagPart = -tagPart;
            tag99 = (byte)tagPart;
        }
        public static void GetValue999999(int value, out int value666666)
        {
            value666666 = value % 1000000;
        }

    }
}
