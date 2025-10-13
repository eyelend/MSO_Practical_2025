using MSO_P2_Code.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.Applic
{
    internal class ExamplePrograms
    {
        public static ExamplePrograms Instance { get; private set; } = new();

        public readonly InnerProgram
            basic1, basic2,
            advanced1, advanced2,
            expert1;
        private ExamplePrograms()
        {
            Body.Builder square1 = new Body.Builder()
                .repeat(4, new Body.Builder()
                    .move(2)
                    .turn(Dir2.Left)
                    )
                .move(2);

            Body.Builder bigAngle = new Body.Builder()
                .move(6)
                .turn(Dir2.Right)
                .move(8);

            basic1 = new InnerProgram(bigAngle.Build());
            advanced1 = new InnerProgram(new Body.Builder()
                .repeat(3, bigAngle)
                .Build());
            expert1 = new InnerProgram(new Body.Builder()
                .repeat(3, new Body.Builder()
                    .body(bigAngle)
                    .body(square1)
                    )
                .Build());

            basic2 = new InnerProgram(new Body.Builder().turn(Dir2.Left).Build());
            advanced2 = new InnerProgram(new Body.Builder().repeat(2, new Body.Builder().turn(Dir2.Left)).Build());
        }
    }
}
