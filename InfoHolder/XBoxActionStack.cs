using System;
using System.Collections.Generic;
using XOMI.TimedAction;

namespace XOMI.InfoHolder
{
    public class XBoxActionStack
    {
        public List<TimedXBoxAction> actions = new List<TimedXBoxAction>();

        public List<TimedXBoxAction> GetRefToActionStack()
        {
            return actions;
        }
    }
}