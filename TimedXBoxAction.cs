﻿using System;

namespace Test
{
    public class TimedXBoxAction
    {
        public DateTime m_whenToExecute;

        public TimedXBoxAction(DateTime whenToExecute)
        {
            m_whenToExecute = whenToExecute;
        }

        public DateTime GetWhenToExecute()
        {
            return m_whenToExecute;
        }
    }
}