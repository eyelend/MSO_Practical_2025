using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI
{
    internal interface IOutputLanguage
    {
        string ExecutionResult(WorldState endState);
        string ShowMetrics(InnerProgram program);
    }
}
