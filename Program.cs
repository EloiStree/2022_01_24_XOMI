using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using XOMI.Unstore;

namespace XOMI
{

    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World :) !");
            ProgramV2_FourControllerIntegerVersion.Run(args);
            // ProgramV1_TextVersion.Run(args);
        }
    }
}
