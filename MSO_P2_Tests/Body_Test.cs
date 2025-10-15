using MSO_P2_Code.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Tests
{
    public class Body_Test
    {
        [Fact]
        public void TestBuild1()
        {
            Body.Builder bb = new Body.Builder();
            ICommand[] expectation = { new Move(3), new Turn(MSO_P2_Code.Dir2.Left) };

            bb.move(3).turn(MSO_P2_Code.Dir2.Left).Build();

            Assert.IsType<Move>(expectation[0]);
            Assert.IsType<Turn>(expectation[1]);
        }
    }
}
