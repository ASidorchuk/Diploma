using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaExperiments
{
    class Structures
    {
        public struct MyUnit
        {
            int x, y;
            double u, v;

            public MyUnit(int x, int y, double u, double v)
            {
                this.x = x;
                this.y = y;
                this.u = u;
                this.v = v;
            }

            public int X { get => x; set => x = value; }
            public int Y { get => y; set => y = value; }
            public double U { get => u; set => u = value; }
            public double V { get => v; set => v = value; }
        }

        public struct RangeBlock
        {
            int x, y;

            public RangeBlock(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int X { get => x; set => x = value; }
            public int Y { get => y; set => y = value; }
        }

        public struct DomainBlock : IEquatable<DomainBlock>
        {
            int x, y;
            double o, s;

         

            public DomainBlock(int x, int y, double o, double s)
            {
                this.x = x;
                this.y = y;
                this.o = o;
                this.s = s;
            }

            public int X { get => x; set => x = value; }
            public int Y { get => y; set => y = value; }
            public double O { get => o; set => o = value; }
            public double S { get => s; set => s = value; }

            public bool Equals(DomainBlock other)
            {
                if (this.X == other.X && this.Y == other.Y && Math.Round(this.O) == Math.Round(other.O) && Math.Round(this.S) == Math.Round(other.S))
                    return true;
                return false;
            }
        }
    }
}
