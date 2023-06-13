using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    internal class ShapeSquare : Shape
    {
        public ShapeSquare(Dot firstPoint, int ixRow, int ixColumn) : base(firstPoint, ixRow, ixColumn)
        {
        }

        public override void FillMatrix()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    SquareMatrix[i, j] = true;
                }
            }
        }
    }
}
