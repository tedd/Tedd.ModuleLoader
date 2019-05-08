using System;
using Tedd.ModuleLoader.Test.Common;

namespace Tedd.ModuleLoader.Test.Module1
{
    public class CalcModule : ICalcModule
    {
        public int Add(int a, int b) => a + b;
    }
}
