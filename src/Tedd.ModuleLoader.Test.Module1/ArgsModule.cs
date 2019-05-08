using System;
using System.Collections.Generic;
using System.Text;
using Tedd.ModuleLoader.Test.Common;

namespace Tedd.ModuleLoader.Test.Module1
{
    public class ArgsModule : ICtorArgsModule
    {
        public string Arg1 { get; private set; }
        public int Arg2 { get; private set; }

        public ArgsModule(string arg1, int arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }
    }

}
