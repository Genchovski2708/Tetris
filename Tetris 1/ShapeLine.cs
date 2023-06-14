using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    public class ShapeLine : Shape
    {
        public ShapeLine(Dot firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight)
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
                            SquareMatrix[i, j] = true;
                        }
                    }
                    Width = 4;
                    Height = 2;
                    break;
                case 1:
                case 3:
                    for (int i = 1; i < 2; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            SquareMatrix[j, i] = true;
                        }
                    }
                    Width = 2;
                    Height = 4;
                    break;
            }

        }



    }
}
