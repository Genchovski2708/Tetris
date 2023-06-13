using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    public abstract class Shape
    {
        private int location;

        public Dot FirstPoint { get; set; }
        public bool [,] SquareMatrix { get; set; }
        public int IndexColumn { get; set; }
        public int IndexRow { get; set; }
        public bool AtBottom { get; set; } = false;

        protected Shape(Dot firstPoint,int indexRow, int indexColumn)
        {
            FirstPoint = firstPoint;
            SquareMatrix = new bool[4,4];
            IndexColumn = indexColumn;
            IndexRow = indexRow;
            FillMatrix();
        }

        protected Shape(Dot firstPoint, int location)
        {
            FirstPoint = firstPoint;
            this.location = location;
        }

        public abstract void FillMatrix();
        
    }
}
