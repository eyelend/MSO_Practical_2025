using MSO_P2_Code.Command;
using MSO_P2_Code.World;

namespace MSO_P2_Code
{
    internal class InnerProgram
    {
        ICommand[] commands;
        ActualWorld startWorld;

        public WorldState Execute()
        {
            ActualWorld world = startWorld.CopyState();
            foreach (ICommand c in commands)
                c.ApplyOnWorld(ref world);
            return world.state;
        }
        public ProgramMetrics GetMetrics()
        {
            throw new NotImplementedException();
        }
    }
}
