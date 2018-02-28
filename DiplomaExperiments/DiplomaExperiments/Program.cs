using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiplomaExperiments.Structures;

namespace DiplomaExperiments
{
    class Program
    {
        /*
         * todo: 
         * 1) add image loader
         * 2) separate image to range blocks
         * 3) add domain transforms
         * 4) scale S, O
         * 5) write info into file
         * 
         * IN MATLAB
         * 1) quantization
         * 2) arithmetic coding
         * 
         * Backward step:
         * 1) What is chenged in loop?
         * 2) What params we need in input? -- we can write d_size and r_size in header
         * */
        static void Main(string[] args)
        {
            Random rand = new Random();

            int rangeSize = 8;
            int domainSize = rangeSize;

            int m_domain = 16;
            int n_domain = 16;

            ForwardStep(rand, rangeSize, domainSize, m_domain, n_domain, out DomainBlock[,] domainBlocks, out List<MyUnit> resultList);

            //Revers operations
            /*
             * input : myUnit
             * todo: get domain blocks params
             */
            bool isOK = BackwardStep(rangeSize, domainBlocks, resultList);

            Console.WriteLine(isOK);

            Console.ReadLine();
        }

        private static bool BackwardStep(int rangeSize, DomainBlock[,] domainBlocks, List<MyUnit> resultList)
        {
            List<Structures.MyUnit> inp = new List<Structures.MyUnit>(resultList);

            int totalRangeCount = inp.Count;
            int m = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(totalRangeCount)));
            int n = m;

            MyUnit[,] restoreUnits = new MyUnit[m, n];
            DomainBlock[,] restoreDomains = new DomainBlock[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var currentUnit = inp[i * n + j];
                    restoreUnits[i, j] = new MyUnit(currentUnit.X, currentUnit.Y, currentUnit.U, currentUnit.V);
                    int restoreDx = currentUnit.X + i * rangeSize / 2;
                    int restoreDy = currentUnit.Y + j * rangeSize / 2;
                    double restoreO = (currentUnit.U + currentUnit.V) / 2;
                    double restoreS = (currentUnit.U - currentUnit.V) / 2;
                    restoreDomains[i, j] = new DomainBlock(restoreDx, restoreDy, restoreO, restoreS);
                }
            }

            bool isOK = true;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (!(domainBlocks[i, j].Equals(restoreDomains[i, j])))
                    {
                        isOK = false;
                        break;
                    }
                }
            }

            return isOK;
        }

        private static void ForwardStep(Random rand, int rangeSize, int domainSize, int m_domain, int n_domain, out DomainBlock[,] domainBlocks, out List<MyUnit> resultList)
        {
            RangeBlock[,] rangeBlocks = new Structures.RangeBlock[4, 4];
            domainBlocks = new Structures.DomainBlock[4, 4];
            // их размер соответствует ранговым, но они получены из кртинки, уменьшенной в 2 раза


            Console.WriteLine("Range blocks");
            Console.WriteLine($"Max domain coord {m_domain - domainSize}");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    rangeBlocks[i, j] = new Structures.RangeBlock(i * rangeSize, j * rangeSize);//координаты ранговых блоков фиксированы
                    domainBlocks[i, j] = new Structures.DomainBlock(rand.Next(m_domain - domainSize + 1), rand.Next(n_domain - domainSize + 1), rand.NextDouble(), rand.NextDouble() * 255);
                    Console.Write($"({rangeBlocks[i, j].X}, {rangeBlocks[i, j].Y}) ");//| ({domainBlocks[i,j].X},{domainBlocks[i,j].Y}) ");
                }
                Console.WriteLine("\n");
            }


            Console.WriteLine("Domain blocks");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    Console.Write($"({domainBlocks[i, j].X},{domainBlocks[i, j].Y}), {domainBlocks[i, j].O}, {domainBlocks[i, j].S}");
                Console.WriteLine("\n");
            }

            //calculate myUnit
            Console.WriteLine("MyUnit");
            resultList = new List<Structures.MyUnit>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int newX, newY;
                    double u, v;
                    newX = domainBlocks[i, j].X - rangeBlocks[i, j].X / 2;
                    newY = domainBlocks[i, j].Y - rangeBlocks[i, j].Y / 2;
                    u = domainBlocks[i, j].O + domainBlocks[i, j].S;
                    v = domainBlocks[i, j].O - domainBlocks[i, j].S;
                    resultList.Add(new Structures.MyUnit(newX, newY, u, v));
                }
            }
        }

    }
}

