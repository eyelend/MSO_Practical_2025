using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.World.EventTrace
{
    internal class MoveTrace : IEventTrace
    {
        int stepCount;

        public MoveTrace(int stepCount)
        {
            this.stepCount = stepCount;
        }

        public string TextualTrace()
        {
            throw new NotImplementedException();
        }
    }
}
