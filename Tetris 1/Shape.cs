using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    public abstract class Shape : ICloneable
    {
        private int location;
        public static Random Random = new Random();
        public Dot FirstPoint { get; set; }
        public bool [,] Matrix { get; set; }
        public int IndexColumn { get; set; }
        public int IndexRow { get; set; }
        public bool AtBottom { get; set; } = false;
        public int StartingWidthIndex { get; set; }
        public int StartingHeightIndex { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int LimitLeft { get; set; }
        public int LimitRight { get; set; }
        public int Stage { get; set; } // 0 default, 1 - rotated 90, 2 - rotated 180, 4 - rotated 270
        public Color Color { get; set; }

        protected Shape(Dot firstPoint,int indexRow, int indexColumn, int limitLeft, int limitRight)
        {
            FirstPoint = firstPoint;
            Matrix = new bool[4,4];
            LimitLeft = limitLeft;
            LimitRight = limitRight;
            IndexColumn = indexColumn;
            IndexRow = indexRow;
            Stage = Random.Next(0,4);
            Color = RandomColorPicker();
            FillMatrix();
            FixLimits();
        }
        protected Shape(Dot firstPoint, int indexRow, int indexColumn, int limitLeft, int limitRight, int stage)
        {
            FirstPoint = firstPoint;
            Matrix = new bool[4, 4];
            LimitLeft = limitLeft;
            LimitRight = limitRight;
            IndexColumn = indexColumn;
            IndexRow = indexRow;
            Stage = stage;
            Color = RandomColorPicker();
            FillMatrix();
            FixLimits();
        }

        protected Shape(Dot firstPoint, int location)
        {
            FirstPoint = firstPoint;
            this.location = location;
        }

        public abstract void FillMatrix();
        private Color RandomColorPicker()
        {
            int randomInt = Random.Next(0, 5);
            switch(randomInt)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Green;
                case 2:
                    return Color.Violet;
                case 3:
                    return Color.Yellow;
                case 4:
                    return Color.Salmon;
                default:
                    return Color.Red;
            }
        }
        public void ResetMatrix()
        {
            for(int i=0;i<4;i++)
            {
                for(int j=0;j<4;j++)
                {
                    Matrix[i, j] = false;
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
            int previusStage = Stage;
            Stage = (Stage + 1) % 4;
            ResetMatrix();
            FillMatrix();
            if (!Limits())
            {
                Stage = previusStage;
                ResetMatrix();
                FillMatrix();
            }

        }

        public void FixLimits()
        {
            if (IndexColumn + Width > LimitRight)
            {
                IndexColumn = LimitRight - Width;
            }
            if (IndexColumn + StartingWidthIndex < LimitLeft)
            {
                IndexColumn = LimitLeft - StartingWidthIndex;
            }
        }
        public bool Limits()
        {
            if (IndexColumn + Width > LimitRight || IndexColumn + StartingWidthIndex < LimitLeft)
            {
                return false;
            }
            return true;
        }

        public abstract object Clone();
    }
}
