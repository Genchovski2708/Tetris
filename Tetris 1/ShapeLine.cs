using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    public class ShapeLine : Shape
    {
        public ShapeLine(GridSquare firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight)
        {
          
        }
        public ShapeLine(GridSquare firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight, int stage) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight, stage)
        {

        }
        public override void FillMatrix()
        {
            switch (Stage)
            {
                case 0:
                case 2:
                    for (int i = 1; i < 2; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Matrix[i, j] = true;
                        }
                    }
                    Width = 4;
                    Height = 2;
                    StartingWidthIndex = 0;
                    StartingHeightIndex = 1;
                    break;
                case 1:
                case 3:
                    for (int i = 1; i < 2; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Matrix[j, i] = true;
                        }
                    }
                    Width = 2;
                    Height = 4;
                    StartingWidthIndex = 1;
                    StartingHeightIndex = 0;
                    break;
            }

        }

        public override object Clone()
        {
            return new ShapeLine(FirstPoint, IndexRow, IndexColumn, LimitLeft, LimitRight, Stage);
        }

    }
}
