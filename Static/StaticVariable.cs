using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOMI.Static
{
    public class StaticVariable
    {
        public static bool m_debugUserMessage = true;
        public static bool m_debugDevMessage = true;
    }

    public class StaticUserPreference
    {

        public static int m_port = 2504;
        public static bool m_wantDebugInfo;
    }
}
