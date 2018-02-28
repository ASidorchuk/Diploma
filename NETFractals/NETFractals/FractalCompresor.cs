using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu;
using System.IO;

namespace NETFractals
{
    class FractalCompresor
    {

        private Mat img;
        private int rangeSize, domainSize;
        private int domainStep;
        private const int numberOfTranforms = 8;
        int rangeCols;
        int rangeRows;
        int domainCols;
        int domainRows;

        public DomainBlock[,,] domainBlocksArray;
        public RangeBlock[,] rangeBlocksArray;
        private TransformationMatrix[,] transMatrix;


        struct RangeBlock
        {
            Mat currentRangeBlock { get; set; }
            Mat centredRangeBlock;
            double meanValue;
            double inverseSquareStd;

            Mat centredRangeBlockMulCoeffs;
            double inverseSquareStdMulcoeffs;
            int X { get; set; }
            int Y { get; set; }

            
        }

        public struct DomainBlock
        {
            Mat centredDomainBlockTransform;
            int t;
            int x0;
            int y0;
            double meanValue;
            double std;
            double inverseSquareStd;

            Mat centredDomainBlockTransformMulCoeffs;
            double inverseSquareStdMulCoeffs;
        }

        struct TransformationMatrix
        {
            int x;
            int y;
            int t; // number of Transform [0-7]
            float s; // контрастность
            float o; // яркость
        }

        #region constr
        public FractalCompresor(string imgName, int rs, int ds, int dStep)
        {
            Image<Gray, byte> img = new Image<Gray, byte>(imgName);
            this.img = img.Mat;

            this.rangeSize = rs;
            this.domainSize = ds;
            this.domainStep = dStep;
        }

        public FractalCompresor()
        {
            img = null;
            rangeSize = 0;
            domainSize = 0;
            domainStep = 0;
        }
        #endregion

        private void InitializeArrays()
        {
            int imCols = this.img.Cols;
            int imRows = this.img.Rows;
            this.rangeCols = imCols / this.rangeSize;
            this.rangeRows = imRows / this.rangeSize;
            this.domainCols = (imCols - this.domainSize) / this.domainStep + 1;
            this.domainRows = (imRows - this.domainSize) / this.domainStep + 1;

            this.domainBlocksArray = new DomainBlock[domainCols, domainRows, numberOfTranforms];
            this.rangeBlocksArray = new RangeBlock[rangeCols, rangeRows];
            this.transMatrix = new TransformationMatrix[rangeCols, rangeRows];
        }

        private void FillArrays()
        {
            Mat currentRangeBlock;

            for (int k = 0; k < this.rangeCols; k++)
            {
                for (int l = 0; l < this.rangeRows; l++)
                {

                    //currentRangeBlock = Mat(this.img, CvInvoke.InRange(k * this.rangeSize, this.rangeSize * (k + 1)), CvInvoke.InRange(l * this.rangeSize, this.rangeSize * (l + 1)));

                    //rangeBlocksArray[k, l].currentRangeBlock = currentRangeBlock;

                    //rangeBlocksArray[k, l].meanValue = mean(currentRangeBlock);
                    //rangeBlocksArray[k, l].inverseSquareStd = 1.0 / sqrt((rangeBlocksArray[k, l].currentRangeBlock - rangeBlocksArray[k, l].meanValue).dot(rangeBlocksArray[k, l].currentRangeBlock - rangeBlocksArray[k, l].meanValue));
                    //rangeBlocksArray[k, l].centredRangeBlock = rangeBlocksArray[k, l].currentRangeBlock - rangeBlocksArray[k, l].meanValue;
                    this.rangeBlocksArray[k, l].X = k * this.rangeSize;
                    rangeBlocksArray[k, l].Y = l * this.rangeSize;
                }
            }
        }
    }
}
    

