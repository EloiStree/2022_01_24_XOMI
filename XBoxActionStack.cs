using System;
using System.Collections.Generic;

namespace Test
{
    public class XBoxActionStack
    {
       public  List<TimedXBoxAction> actions = new List<TimedXBoxAction>();

        public List<TimedXBoxAction> GetRefToActionStack()
        {
            return actions;
        }
    }
}