using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    internal class ShapeSquare : Shape
    {

        public ShapeSquare(Dot firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight)
        {
        }
        public ShapeSquare(Dot firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight, int stage) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight, stage)
        {

        }
        public override object Clone()
        {
            return new ShapeSquare(FirstPoint,IndexRow,IndexColumn,LimitLeft,LimitRight, Stage);
        }

        public override void FillMatrix()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Matrix[i, j] = true;
                }
            }
            Width = 2;
            Height = 2;
        }
    }

    
}
