using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    public class ShapeLine : Shape
    {
        public ShapeLine(Dot firstPoint,int ixRow, int ixColumn) : base(firstPoint,ixRow, ixColumn)
        {
        }

        public override void FillMatrix()
        {
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    SquareMatrix[i, j] = true;
                }
            }
        }
    }
}
