using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    internal class Shape4 : Shape
    {
        public Shape4(Dot firstPoint, int ixRow, int ixColumn) : base(firstPoint, ixRow, ixColumn)
        {

        }

        public override void FillMatrix()
        {
            SquareMatrix[0, 0] = true;
            SquareMatrix[1, 0] = true;
            SquareMatrix[1, 1] = true;
            SquareMatrix[2, 1] = true;
        }
    }
}
