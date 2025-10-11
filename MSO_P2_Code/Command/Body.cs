using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Command
{
    internal class Body : ICommand
    {
        ICommand[] commands;
        private Body(ICommand[] commands)
        {
            this.commands = commands;
        }

        public void ApplyOnWorld(ref ActualWorld world)
        {
            foreach (ICommand c in commands)
                c.ApplyOnWorld(ref world);
        }
        public ProgramMetrics GetMetrics()
        {
            int max(int x1, int x2) => x1 >= x2 ? x1 : x2;
            ProgramMetrics acc = new ProgramMetrics();
            foreach (ICommand c in commands)
            {
                ProgramMetrics cMet = c.GetMetrics();
                acc = new(
                    acc.commandCount + cMet.commandCount,
                    max(acc.maxNestingLevel, cMet.maxNestingLevel),
                    acc.repeatCommandCount + cMet.repeatCommandCount);
            }
            return acc;
        }

        public class Builder
        {
            public Queue<ICommand> commands = new();
            public Builder()
            {

            }
            public Body Build() => new Body(commands.ToArray());

            private Builder AddCommand(ICommand command)
            {
                commands.Enqueue(command);
                return this;
            }
            public Builder turn(Dir2 dir)
                => AddCommand(new Turn(dir));
            public Builder move(int stepCount)
                => AddCommand(new Move(stepCount));
            public Builder repeat(int count, Builder body)
            {
                return AddCommand(new Repeat(count, body.Build()));
            }

            public Builder FromCommands(ICollection<ICommand> commands) // Todo: remove this function for looser coupling
            {
                // Todo: remove this function for looser coupling.
                // Builder is supposed to be used from the outside without needing to input command-instances.
                foreach (ICommand c in commands)
                {
                    AddCommand(c);
                }
                return this;
            }
        }
    }
}
