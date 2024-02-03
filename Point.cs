using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze2
{
    struct Point
    {
        private int i;
        private int j;

        public Point(int i, int j)
        {
            this.i = i;
            this.j = j;
        }

        public void SetIJ (int i, int j)
        {
            this.i = i;
            this.j = j;
        }

        public int GetI ()
        {
            return i;
        }

        public int GetJ ()
        {
            return j;
        }
    }
}
