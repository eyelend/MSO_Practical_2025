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
        public Repeat(int count, ICommand[] body)
        {
            this.count = count;
            this.body = body;
        }
        public void ApplyOnWorld(ref ActualWorld world)
        {
            for (int i = 0; i < count; i++)
                foreach (ICommand command in body)
                    command.ApplyOnWorld(ref world);
        }
    }
}
