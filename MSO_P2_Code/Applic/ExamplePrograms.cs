using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Applic
{
    internal class ExamplePrograms
    {
        public static ExamplePrograms Instance { get; private set; } = new();
        private ExamplePrograms()
        {

        }
    }
}
