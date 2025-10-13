using MSO_P2_Code;
using MSO_P2_Code.Geometry2D;

namespace MSO_P2_Tests
{
    public class Dir4_Tests
    {
        [Fact]
        public void Test1()
        {
            Dir4 dir = Dir4.South;
            Dir4.Rotate(ref dir, Dir2.Left);
            Assert.Same(dir, Dir4.East);
        }
         

        [Theory]
        [InlineData(3, 20, 30, 27)]
        [InlineData(3, -4, 2, -1)]
        public void MovePointNorthTest(int dist, int x, int y, int yEnd)
        {
            Dir4 dir = Dir4.North;
            (int x, int y) expectedEnd = (x, yEnd);

            (int x, int y) actualEnd = dir.MovePoint((x, y), dist);

            Assert.Equal(actualEnd, expectedEnd);
        }
    }
}