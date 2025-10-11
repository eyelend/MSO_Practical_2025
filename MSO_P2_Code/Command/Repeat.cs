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
        //ICommand[] body;
        Body body;
        public Repeat(int count, Body body)
        {
            this.count = count;
            this.body = body;
        }
        public Repeat(int count, ICommand[] body)
        {
            this.count = count;
            this.body = new Body.Builder().FromCommands(body).Build();
        }
        public void ApplyOnWorld(ref ActualWorld world)
        {
            for (int i = 0; i < count; i++)
                body.ApplyOnWorld(ref world);
        }
        public ProgramMetrics GetMetrics()
        {
            ProgramMetrics bodyMet = body.GetMetrics();
            return new ProgramMetrics(1 + bodyMet.commandCount, 1 + bodyMet.maxNestingLevel, 1 + bodyMet.repeatCommandCount);
        }
    }
}
