using MSO_P2_Code;
using MSO_P2_Code.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Tests
{
    public class InnerProgram_Tests
    {
        [Fact]
        public void TestMetrics1()
        {
            Body.Builder bb = new Body.Builder().move(3).turn(Dir2.Right).move(1).turn(Dir2.Right);
            InnerProgram p = new InnerProgram(bb.Build());
            ProgramMetrics expectation = new ProgramMetrics(4, 0, 0);

            Assert.Equal(p.GetMetrics(), expectation);
        }
        [Fact]
        public void TestMetrics2()
        {
            Body.Builder bb = new Body.Builder()
                .move(3)
                .turn(Dir2.Right)
                .repeat(2, new Body.Builder()
                    .move(1))
                .repeat(0, new Body.Builder()
                    .repeat(3, new Body.Builder())
                );
            InnerProgram p = new InnerProgram(bb.Build());
            ProgramMetrics expectation = new ProgramMetrics(6, 2, 3);

            Assert.Equal(p.GetMetrics(), expectation);
        }

        [Fact]
        public void TestRepeat1()
        {
            Body.Builder bb = new Body.Builder()
                .move(3)
                .turn(Dir2.Right)
                .repeat(2, new Body.Builder()
                    .move(1))
                .repeat(0, new Body.Builder()
                    .repeat(3, new Body.Builder()
                        .move(8))
                );
            InnerProgram p = new InnerProgram(bb.Build());
            (int x, int y) expectedEnd = (3, 2);

            (int x, int y) result = p.Execute().playerState.Pos;

            Assert.Equal(result, expectedEnd);
        }
    }
}
