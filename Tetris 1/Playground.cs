using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_1
{
    public class Playground
    {
        public static int DISTANCE { get; set; } = 30;
        public static Random Random { get; set; } = new Random();
        public List<Dot> Dots { get; set; }
        public List<Shape> Shapes { get; set; }
        public Dot [,] DotsArray { get; set; }
        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }

        public int HorizontalDots { get; set; }
        public int VerticalDots { get; set; }

        public Playground(Point topLeft, Point topRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            Dots = new List<Dot>();
            Shapes = new List<Shape>();
            HorizontalDots = (TopRight.X + DISTANCE - TopLeft.X) / DISTANCE;
            VerticalDots = (TopRight.Y + DISTANCE - TopLeft.Y) / DISTANCE;
            DotsArray = new Dot[VerticalDots , HorizontalDots];
            for(int i = 0; i < VerticalDots; i++)
            {
                for(int j = 0; j<HorizontalDots; j++)
                {
                    DotsArray[i,j] = new Dot();
                }
            }
            GenerateDots();
        }
        public void GenerateDots()
        {
            int countRow = 0;
            int countCol = 0;
            for (int j = TopLeft.Y; j <= TopRight.Y; j += DISTANCE)
            {
                for (int i = TopLeft.X; i <= TopRight.X; i += DISTANCE)
                {
                    Dot temp = new Dot(new Point(i, j));
                    Dots.Add(temp);
                    DotsArray[countRow, countCol] = temp;
                    countCol++;
                }
                countCol = 0;
                countRow++;
            }
        }
        public void DrawDots(Graphics g)
        {
            for (int i = 0; i < VerticalDots; i++)
            {
                for (int j = 0; j < HorizontalDots; j++)
                {
                    DotsArray[i, j].Draw(g);
                }
            }
        }
        public void AddShape()
        {
            int randomInx = Random.Next(0, HorizontalDots - 4);
            Dot randomDot = DotsArray[0, randomInx];
            Shapes.Add(GenerateShape(randomDot, randomInx));
        }
        public void UpdateDots()
        {
            ResetDots();
            foreach(Shape shape in Shapes)
            {
                for(int i = 0; i < 4; i++)
                {
                    for(int j=0;j<4; j++)
                    {
                        int rowIndex = shape.IndexRow + i;
                        int columnIndex = shape.IndexColumn + j;

                        if (shape.SquareMatrix[i, j] && rowIndex >= 0 && rowIndex < VerticalDots && columnIndex >= 0 && columnIndex < HorizontalDots)
                        {
                            DotsArray[rowIndex, columnIndex].HasSquare = true;
                            if(rowIndex == VerticalDots - 1)
                            {
                                shape.AtBottom= true;
                            }
                        }
                    }
                }
            }
        }

        private void ResetDots()
        {
            for (int i = 0; i < VerticalDots; i++)
            {
                for (int j = 0; j < HorizontalDots; j++)
                {
                    DotsArray[i, j].HasSquare = false;
                }
            }
        }

        public void Tick()
        {
            foreach(Shape shape in Shapes)
            {
                if (shape.IndexRow < VerticalDots)
                {
                    if (!shape.AtBottom)
                    {
                        shape.IndexRow++;
                    }
                }
            }
            UpdateDots();
            CheckIfAtBottom();
        }

        private void CheckIfAtBottom()
        {
            foreach(Shape shape in Shapes)
            {
                int rowIndex = shape.IndexRow;
                int columnIndex = shape.IndexColumn;
                for (int i = 3; i >= 0; i--) 
                {
                    bool found = false;
                    for(int j = 0; j < 4; j++)
                    {
                        
                        if (shape.SquareMatrix[i, j])
                        {
                            rowIndex = shape.IndexRow + i;
                            columnIndex = shape.IndexColumn + j;
                            if (rowIndex + 1 < VerticalDots && DotsArray[rowIndex + 1, columnIndex].HasSquare && (i==3 || !shape.SquareMatrix[i+1,j]))
                            {
                                shape.AtBottom = true;
                                found = true;
                                break;
                            }
                        }
                    }
                    if(found)
                    {
                        break;
                    }
                }


            }
        }

        public Shape GenerateShape(Dot dot, int index)
        {
            int random = Random.Next(0, 5);
            switch (random)
            {
                case 0: return new ShapeLine(dot,0,index);
                case 1: return new ShapeSquare(dot,0,index); 
                case 2: return new ShapeL(dot,0,index);
                case 3: return new Shape4(dot,0,index);
                case 4: return new ShapeT(dot,0,index);
                default: return null;
            }
            
        }
    }
}
