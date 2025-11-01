using MSO_P2_Code.Command.Condition;
using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSO_P2_Code.Command.ICommand;

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
        public T Fold<T,C>(IAlgebra<T,C> algebra)
        {
            return algebra.FoldBody(map(commands, (ICommand c) => c.Fold(algebra)));
            T2[] map<T1, T2>(T1[] inpArray, Func<T1, T2> f)
            {
                // applies f to every element of inpArray
                T2[] result = new T2[inpArray.Length];
                for (int i = 0; i < inpArray.Length; i++)
                    result[i] = f(inpArray[i]);
                return result;
            }
        }

        public class Builder
        {
            Queue<ICommand> commands = new();
            public Builder()
            {

            }
            public Body Build() => new Body(commands.ToArray());

            private Builder AddCommand(ICommand command)
            {
                commands.Enqueue(command);
                return this;
            }
            #region commands
            public Builder turn(Dir2 dir)
                => AddCommand(new Turn(dir));
            public Builder move(int stepCount)
                => AddCommand(new Move(stepCount));
            public Builder repeat(int count, Builder body)
            {
                return AddCommand(new Repeat(count, body.Build()));
            }
            public Builder repeatUntil(ICondition condition, Builder body)
            {
                return AddCommand(new RepeatUntil(condition, body.Build()));
            }
            public Builder _if(ICondition condition, Builder body)
            {
                return AddCommand(new If(condition, body.Build()));
            }
            public Builder body(Builder addedBody)
            {
                foreach (ICommand c in addedBody.commands)
                    this.commands.Enqueue(c);
                return this;
            }
            public ICondition facingBlock() => new FacingBlock();
            public ICondition facingGridEdge() => new FacingGridEdge();
            public ICondition not(ICondition condition) => new Not(condition);
            #endregion commands
        }

    }
}
