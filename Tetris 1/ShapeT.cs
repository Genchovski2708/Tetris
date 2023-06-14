using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    internal class ShapeT : Shape
    {
        public ShapeT(Dot firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight)
        {
            Width = 3;
        }

        public override void FillMatrix()
        {
            

            switch (Stage)
            {
                case 0:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            SquareMatrix[j, i] = true;
                        }
                    }
                    SquareMatrix[2, 1] = true;
                    break;
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            SquareMatrix[i, j] = true;
                        }
                    }
                    SquareMatrix[1, 0] = true;
                    break;
                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            SquareMatrix[j, i] = true;
                        }
                    }
                    SquareMatrix[0, 1] = true;
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            SquareMatrix[i, j] = true;
                        }
                    }
                    SquareMatrix[1, 2] = true;
                    break;
            }
        }
    }
}
