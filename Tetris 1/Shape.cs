﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    public abstract class Shape
    {
        private int location;
        public static Random Random = new Random();
        public Dot FirstPoint { get; set; }
        public bool [,] SquareMatrix { get; set; }
        public int IndexColumn { get; set; }
        public int IndexRow { get; set; }
        public bool AtBottom { get; set; } = false;
        public int Width { get; set; }
        public int Height { get; set; }
        public int LimitLeft { get; set; }
        public int LimitRight { get; set; }
        public int Stage { get; set; } // 0 default, 1 - rotated 90, 2 - rotated 180, 4 - rotated 270

        protected Shape(Dot firstPoint,int indexRow, int indexColumn, int limitLeft, int limitRight)
        {
            FirstPoint = firstPoint;
            SquareMatrix = new bool[4,4];
            LimitLeft = limitLeft;
            LimitRight = limitRight;
            IndexColumn = indexColumn;
            IndexRow = indexRow;
            Stage = Random.Next(0,4);
            FixLimits();
            FillMatrix();
        }

        protected Shape(Dot firstPoint, int location)
        {
            FirstPoint = firstPoint;
            this.location = location;
        }

        public abstract void FillMatrix();
        public void ResetMatrix()
        {
            for(int i=0;i<4;i++)
            {
                for(int j=0;j<4;j++)
                {
                    SquareMatrix[i, j] = false;
                }
            }
        }
        public void MoveDown()
        {
            IndexRow++;
        }

        public void MoveLeft()
        {
            IndexColumn--;
            FixLimits();
        }

        public void MoveRight()
        {
            IndexColumn++;
            FixLimits();
        }
        public void MoveUp()
        {
            Stage = (Stage + 1) % 4;
            ResetMatrix();
            FillMatrix();
            if (!Limits())
            {
                Stage = (Stage - 1) % 4;
                FillMatrix();
            }

        }

        public void FixLimits()
        {
            while (!Limits())
            {

                if (IndexColumn + Width > LimitRight+1)
                {
                    IndexColumn--;
                }
                if (IndexColumn < LimitLeft)
                {
                    IndexColumn++;
                }
            }
        }
        public bool Limits()
        {
            if (IndexColumn + Width > LimitRight+1 || IndexColumn < LimitLeft)
            {
                return false;
            }
            return true;
        }

    }
}
