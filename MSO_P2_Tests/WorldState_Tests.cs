using MSO_P2_Code.Geometry2D;
using MSO_P2_Code.World;
using MSO_P2_Code.World.EventTrace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Tests
{
    public class WorldState_Tests
    {
        [Fact]
        public void TestMoveWest1()
        {
            WorldState state = new WorldState();
            (int x, int y) expectation = (5, 0);

            state.MoveForward(3);
            state.MoveForward(2);

            Assert.Equal(state.playerState.Pos, expectation);
        }
        [Fact]
        public void TestMoveNorth1()
        {
            WorldState state = new WorldState();
            (int x, int y) expectation = (0, -8);

            state.TurnLeft();
            state.MoveForward(3);
            state.MoveForward(4);
            state.MoveForward(1);

            Assert.Equal(state.playerState.Pos, expectation);
        }
        [Fact]
        public void TestMoveNorthWest1()
        {
            WorldState state = new WorldState();
            (int x, int y) expectation = (-3, -5);

            state.TurnLeft();
            state.TurnLeft();
            state.MoveForward(3);
            state.TurnRight();
            state.MoveForward(4);
            state.MoveForward(1);

            Assert.Equal(state.playerState.Pos, expectation);
        }

        [Fact]
        public void TestTurn1()
        {
            WorldState state = new WorldState();
            Dir4 expectation = Dir4.North;

            state.TurnLeft();
            state.TurnLeft();
            state.MoveForward(3);
            state.TurnRight();
            state.MoveForward(4);
            state.MoveForward(1);

            Assert.Same(state.playerState.Dir, expectation);
        }

        [Fact]
        public void TestTrace1()
        {
            WorldState state = new WorldState();
            string expectation = "";

            state.TurnLeft();
            state.TurnLeft();
            state.MoveForward(3);
            state.TurnRight();
            state.MoveForward(4);
            state.MoveForward(1);

            Assert.IsType<TurnTrace>(state.Trace[3]);
        }

    }
}
