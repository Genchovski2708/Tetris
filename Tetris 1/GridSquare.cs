using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_1
{
    public class GridSquare
    {
        public Point Center { get; set; }
        public int Size { get; set; } = 5;
        public int SquareSize { get; set; } = 20;
        public Color Color { get; set; } = Color.Red;
        public bool HasSquare { get; set; } = false;
        public bool BottomPreview { get; set; } = false;
        public bool Visible { get; set; } = true;
        public GridSquare() 
        {
            Center = Point.Empty;
        }

        public GridSquare(Point center)
        {
            Center = center;
        }
        public GridSquare(Point center, Color color)
        {
            Center = center;
            Color = color;
        }
        public void Draw(Graphics g)
        {
            if (Visible)
            {
                if (HasSquare)
                {
                    Brush b = new SolidBrush(Color);
                    g.FillRectangle(b, Center.X - SquareSize, Center.Y - SquareSize, 2 * SquareSize, 2 * SquareSize);
                    b.Dispose();
                }
                else
                {
                    Brush b = new SolidBrush(Color.Transparent);
                    g.FillEllipse(b, Center.X - Size, Center.Y - Size, 2 * Size, 2 * Size);
                    b.Dispose();
                }
                if (BottomPreview)
                {
                    Pen p = new Pen(Color.Purple, 5);
                    g.DrawRectangle(p, Center.X - SquareSize, Center.Y - SquareSize, 2 * SquareSize, 2 * SquareSize);
                    p.Dispose();
                }
            }
            
        }
    }
}
