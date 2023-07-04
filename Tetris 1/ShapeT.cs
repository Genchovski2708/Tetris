using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    internal class ShapeT : Shape
    {
        public ShapeT(GridSquare firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight)
        {
        }
        public ShapeT(GridSquare firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight, int stage) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight, stage)
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
                            Matrix[j, i] = true;
                        }
                    }
                    Matrix[2, 1] = true;
                    Width = 3;
                    Height = 3;
                    StartingWidthIndex = 0;
                    StartingHeightIndex = 1;
                    break;
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[i, j] = true;
                        }
                    }
                    Matrix[1, 0] = true;
                    Width = 2;
                    Height = 3;
                    StartingWidthIndex = 0;
                    StartingHeightIndex = 0;
                    break;
                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[j, i] = true;
                        }
                    }
                    Matrix[0, 1] = true;
                    Width = 3;
                    Height = 2;
                    StartingWidthIndex = 0;
                    StartingHeightIndex = 0;
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[i, j] = true;
                        }
                    }
                    Matrix[1, 2] = true;
                    Width = 3;
                    Height = 3;
                    StartingWidthIndex = 1;
                    StartingHeightIndex = 0;
                    break;
            }
        }
        public override object Clone()
        {
            return new ShapeT(FirstPoint, IndexRow, IndexColumn, LimitLeft, LimitRight,Stage);
        }
    }
}
