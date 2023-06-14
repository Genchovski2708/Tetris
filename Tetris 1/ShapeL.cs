using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    internal class ShapeL : Shape
    {
        public ShapeL(Dot firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight)
        {
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
                            SquareMatrix[i, j] = true;
                        }
                    }
                    SquareMatrix[2, 0] = true;
                    Width = 2;
                    Height = 3;
                    break;
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            SquareMatrix[j, i] = true;
                        }
                    }
                    SquareMatrix[0, 0] = true;
                    Width = 3;
                    Height = 2;
                    break;
                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            SquareMatrix[i, j] = true;
                        }
                    }
                    SquareMatrix[0, 2] = true;
                    Width = 3;
                    Height = 3;
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            SquareMatrix[j, i] = true;
                        }
                    }
                    SquareMatrix[2, 2] = true;
                    Width = 3;
                    Height = 3;
                    break;
            }

        }
    }
}
