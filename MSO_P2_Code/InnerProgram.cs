using MSO_P2_Code.Command;
using MSO_P2_Code.World;

namespace MSO_P2_Code
{
    internal class InnerProgram
    {
        Body commands;
        ActualWorld startWorld;

        public InnerProgram(Body commands, ActualWorld startWorld)
        {
            this.commands = commands;
            this.startWorld = startWorld;
        }
        public InnerProgram(Body commands) : this(commands, new ActualWorld()) { }

        public WorldState Execute()
            => Execute(out _);
        public WorldState Execute(out bool reachedDest)
        {
            ActualWorld world = startWorld.CopyState();
            commands.ApplyOnWorld(ref world);
            reachedDest = world.AtDestination();
            return world.state;
        }
        public ProgramMetrics GetMetrics()
            => ProgramMetrics.FromBody(commands);
        public T FoldCommands<T,C>(ICommand.IAlgebra<T,C> algebra)
            => commands.Fold(algebra);
    }
}
