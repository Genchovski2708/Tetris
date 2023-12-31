﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    internal class ShapeL : Shape
    {
        public bool IsRegular { get; set; }  // true = _| , false = |_ 
        public ShapeL(GridSquare firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight)
        {
            IsRegular = Random.Next(0, 2) == 0;
            ResetMatrix();
            FillMatrix();
        }
        public ShapeL(GridSquare firstPoint, int ixRow, int ixColumn, int limitLeft, int limitRight, int stage, bool isRegular) : base(firstPoint, ixRow, ixColumn, limitLeft, limitRight, stage)
        {
            IsRegular = isRegular;
            ResetMatrix();
            FillMatrix();
        }
        public override void FillMatrix()
        {
            if (IsRegular)
            {
                FillMatrixRegular();
            }
            else
            {
                FillMatrixIrregular();
            }
        }

        private void FillMatrixRegular()
        {
            switch (Stage)
            {
                case 0:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[i, j] = true;
                        }
                    }
                    Matrix[2, 0] = true;
                    Width = 2;
                    Height = 3;
                    StartingWidthIndex = 0;
                    StartingHeightIndex = 0;
                    break;
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[j, i] = true;
                        }
                    }
                    Matrix[0, 0] = true;
                    Width = 3;
                    Height = 2;
                    StartingWidthIndex = 0;
                    StartingHeightIndex = 0;
                    break;
                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[i, j] = true;
                        }
                    }
                    Matrix[0, 2] = true;
                    Width = 3;
                    Height = 3;
                    StartingWidthIndex = 1;
                    StartingHeightIndex = 0;
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[j, i] = true;
                        }
                    }
                    Matrix[2, 2] = true;
                    Width = 3;
                    Height = 3;
                    StartingWidthIndex = 0;
                    StartingHeightIndex = 1;
                    break;
            }
        }

        private void FillMatrixIrregular()
        {
            switch (Stage)
            {
                case 0:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[i, j] = true;
                        }
                    }
                    Matrix[2, 2] = true;
                    Width = 3;
                    Height = 3;
                    StartingWidthIndex = 1;
                    StartingHeightIndex = 0;
                    break;
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[j, i] = true;
                        }
                    }
                    Matrix[2, 0] = true;
                    Width = 3;
                    Height = 3;
                    StartingWidthIndex = 0;
                    StartingHeightIndex = 1;
                    break;
                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[i, j] = true;
                        }
                    }
                    Matrix[0, 0] = true;
                    Width = 2;
                    Height = 3;
                    StartingWidthIndex = 0;
                    StartingHeightIndex = 0;
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 1; j < 2; j++)
                        {
                            Matrix[j, i] = true;
                        }
                    }
                    Matrix[0, 2] = true;
                    Width = 3;
                    Height = 2;
                    StartingWidthIndex = 0;
                    StartingHeightIndex = 0;
                    break;
            }
        }


        public override object Clone()
        {
            return new ShapeL(FirstPoint, IndexRow, IndexColumn, LimitLeft, LimitRight, Stage, IsRegular);
        }
    }
}
