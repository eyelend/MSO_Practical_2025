using MSO_P2_Code.Command;
using MSO_P2_Code.World;

namespace MSO_P2_Code
{
    internal class InnerProgram
    {
        //ICommand[] commands;
        Body commands;
        ActualWorld startWorld;

        public InnerProgram(Body commands, ActualWorld startWorld)
        {
            this.commands = commands;
            this.startWorld = startWorld;
        }
        public InnerProgram(Body commands) : this(commands, new ActualWorld()) { }

        public WorldState Execute()
        {
            ActualWorld world = startWorld.CopyState();
            commands.ApplyOnWorld(ref world);
            return world.state;
        }
        public ProgramMetrics GetMetrics()
            => commands.GetMetrics();
        public T FoldCommands<T,C>(ICommand.IAlgebra<T,C> algebra)
            => commands.Fold(algebra);
    }
}
