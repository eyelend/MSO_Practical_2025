using MSO_P2_Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code
{
    /*public struct Dir2
    {
        private bool right; //whether we turn left or right
        private Dir2(bool right) { this.right = right; }
        public static Dir2 Left => new Dir2(false);
        public static Dir2 Right => new Dir2(true);

        public bool IsRight => right;
        public bool IsLeft => !IsRight;
    }*/
    enum Dir2 { Left, Right }
}
