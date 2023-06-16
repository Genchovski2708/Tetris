using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    public class Dot
    {
        public Point Center { get; set; }
        public int Size { get; set; } = 5;
        public int SquareSize { get; set; } = 20;
        public Color Color { get; set; } = Color.Red;
        public bool HasSquare { get; set; } = false;

        public Dot() 
        {
            Center = Point.Empty;
        }

        public Dot(Point center)
        {
            Center = center;
        }
        public void Draw(Graphics g)
        {
            if (HasSquare)
            {
                Brush b = new SolidBrush(Color);
                g.FillRectangle(b, Center.X - SquareSize, Center.Y - SquareSize, 2 * SquareSize, 2 * SquareSize);
                b.Dispose();
            }
            else
            {
                Brush b = new SolidBrush(Color.White);
                g.FillEllipse(b, Center.X - Size, Center.Y - Size, 2 * Size, 2 * Size);
                b.Dispose();
            }


        }
    }
}
