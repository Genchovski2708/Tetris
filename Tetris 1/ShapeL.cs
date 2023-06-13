using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    internal class ShapeL : Shape
    {
        public ShapeL(Dot firstPoint, int ixRow, int ixColumn) : base(firstPoint, ixRow, ixColumn)
        {
        }

        public override void FillMatrix()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    SquareMatrix[i, j] = true;
                }
            }
            SquareMatrix[2,1] = true;
        }
    }
}
