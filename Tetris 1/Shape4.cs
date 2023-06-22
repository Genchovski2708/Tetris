using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    internal class Shape4 : Shape
    {
        public bool IsRegular { get; set; }
        public Shape4(Dot firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight)
        {
            IsRegular = Random.Next(0, 2) == 0;
            ResetMatrix();
            FillMatrix();
        }
        public Shape4(Dot firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight, int stage, bool isRegular) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight, stage)
        {
            IsRegular = isRegular;
            ResetMatrix();
            FillMatrix();
        }
        
        public override void FillMatrix()
        {
            if (IsRegular)
            {
                switch (Stage)
                {
                    case 0:
                    case 2:
                        Matrix[0, 0] = true;
                        Matrix[1, 0] = true;
                        Matrix[1, 1] = true;
                        Matrix[2, 1] = true;
                        Width = 2;
                        Height = 3;
                        break;
                    case 1:
                    case 3:
                        Matrix[0, 1] = true;
                        Matrix[0, 2] = true;
                        Matrix[1, 0] = true;
                        Matrix[1, 1] = true;
                        Width = 3;
                        Height = 2;
                        break;
                }
                StartingWidthIndex = 0;
                StartingHeightIndex = 0;
            }
            else
            {
                switch (Stage)
                {
                    case 0:
                    case 2:
                        Matrix[0, 1] = true;
                        Matrix[1, 0] = true;
                        Matrix[1, 1] = true;
                        Matrix[2, 0] = true;
                        Width = 2;
                        Height = 3;
                        break;
                    case 1:
                    case 3:
                        Matrix[0, 0] = true;
                        Matrix[0, 1] = true;
                        Matrix[1, 1] = true;
                        Matrix[1, 2] = true;
                        Width = 3;
                        Height = 2;
                        break;
                }
                StartingWidthIndex = 0;
                StartingHeightIndex = 0;
            }
            
        }
        public override object Clone()
        {
            return new Shape4(FirstPoint, IndexRow, IndexColumn, LimitLeft, LimitRight, Stage, IsRegular);
        }
    }
}
