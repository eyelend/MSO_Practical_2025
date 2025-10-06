using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command
{
    internal class Repeat : ICommand
    {
        int count;
        ICommand[] body;

        public void ApplyOnWorld(ref ActualWorld world)
        {
            throw new NotImplementedException();
        }
    }
}
