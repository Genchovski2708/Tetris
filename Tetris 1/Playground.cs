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
        public static int DISTANCE { get; set; } = 40;
        public static Random Random { get; set; } = new Random();
        public List<Dot> Dots { get; set; }
        public List<Shape> Shapes { get; set; }
        public Shape MovingShape { get; set; }
        public Dot [,] DotsArray { get; set; }
        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }

        public int HorizontalDots { get; set; }
        public int VerticalDots { get; set; }
        public bool GameIsStarted { get; set; }
        public int ClearedRows { get; set; } = 0;
        public DialogResult DialogRes { get; set; } = DialogResult.None;
        public bool GameOver { get; set; } = false;

        public Playground(Point topLeft, Point topRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            Dots = new List<Dot>();
            Shapes = new List<Shape>();
            Shape MovingShape;
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
            if (MovingShape == null || MovingShape.AtBottom && GameIsStarted)
            {
                CheckIfGameOver();
                if(MovingShape!=null && !GameIsStarted)
                {
                    GameOver = true;
                    DialogResult dg=MessageBox.Show("Game Over, New Game?","Game Over",MessageBoxButtons.YesNo);
                    DialogRes = dg;
                }
                else
                {
                    CheckFullRows();
                    int randomInx = Random.Next(0, HorizontalDots);
                    Dot randomDot = DotsArray[0, randomInx];
                    MovingShape = GenerateShape(randomDot, randomInx);
                    Shapes.Add(MovingShape);
                }

            }

        }
        public void UpdateDots()
        {

            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int rowIndex = MovingShape.IndexRow + i;
                    int columnIndex = MovingShape.IndexColumn + j;

                    if (MovingShape.Matrix[i, j] && rowIndex >= 0 && rowIndex < VerticalDots && columnIndex >= 0 && columnIndex < HorizontalDots)
                    {
                        DotsArray[rowIndex, columnIndex].HasSquare = true;
                        if (rowIndex == VerticalDots - 1)
                        {
                            MovingShape.AtBottom = true;
                        }
                    }
                }
            }

            
        }

        private void ResetDots()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if (MovingShape.Matrix[i, j])
                    {
                        DotsArray[i + MovingShape.IndexRow, j + MovingShape.IndexColumn].HasSquare = false;
                    }
                }
            }
            //for (int i = 0; i < VerticalDots; i++)
            //{
            //    for (int j = 0; j < HorizontalDots; j++)
            //    {
            //        DotsArray[i, j].HasSquare = false;
            //    }
            //}
        }

        public void Tick()
        {

            if(GameIsStarted)
            {
                CheckIfMovingShapeAtBottom();
                if (MovingShape != null && MovingShape.IndexRow < VerticalDots)
                {
                    if (!MovingShape.AtBottom)
                    {
                        ResetDots();
                        MovingShape.IndexRow++;
                        UpdateDots();
                    }
                }
                CheckIfMovingShapeAtBottom();
                //CheckIfAtBottom();
            }

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
                        
                        if (shape.Matrix[i, j])
                        {
                            rowIndex = shape.IndexRow + i;
                            columnIndex = shape.IndexColumn + j;
                            if (rowIndex + 1 < VerticalDots && DotsArray[rowIndex + 1, columnIndex].HasSquare && (i==3 || !shape.Matrix[i+1,j]))
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
            int limitLeft = 0;
            int limitRight = HorizontalDots;
            switch (random)
            {
                case 0: return new ShapeLine(dot,0,index,limitLeft,limitRight);
                case 1: return new ShapeSquare(dot,0,index, limitLeft, limitRight); 
                case 2: return new ShapeL(dot, 0, index, limitLeft, limitRight);
                case 3: return new Shape4(dot, 0, index, limitLeft, limitRight);
                case 4: return new ShapeT(dot, 0, index, limitLeft, limitRight);
                default: return null;
            }
            
        }
        public void Move(Keys keys) 
        {
            if (GameIsStarted && !MovingShape.AtBottom)
            {
                if(keys == Keys.Left || keys == Keys.Right || keys == Keys.Up || keys == Keys.Down){
                    ResetDots();
                }
                if (keys == Keys.Left)
                {
                    if (!CheckCollisionsLeft())
                    {
                        MoveLeft();
                    }
        
                }
                else if (keys == Keys.Right)
                {
                    if (!CheckCollisionsRight())
                    {
                        MoveRight();
                    }
                }
                else if (keys == Keys.Up)
                {
                    MoveUp();
                }
                else if (keys == Keys.Down)
                {
   
                    MoveDown();
                }
                UpdateDots();
            }
        }

        private bool CheckCollisionsLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                int indexRow = MovingShape.IndexRow + i;
                int indexColumn = MovingShape.IndexColumn; // Move this line outside the inner loop
                for (int j = 0; j < 4; j++)
                {
                    if (MovingShape.Matrix[i, j])
                    {
                        indexColumn = MovingShape.IndexColumn + j;
                        if (indexColumn - 1 < 0)
                        {
                            return true;
                        }
                        if (DotsArray[indexRow, indexColumn - 1].HasSquare && (j == 0 || !MovingShape.Matrix[i, j - 1]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool CheckCollisionsRight()
        {
            for (int i = 0; i < 4; i++)
            {
                int indexRow = MovingShape.IndexRow + i;
                int indexColumn = MovingShape.IndexColumn;
                for (int j = 0; j < 4; j++)
                {
                    indexColumn = MovingShape.IndexColumn + j;
                    if (MovingShape.Matrix[i, j])
                    {
                        if (indexColumn + 1 >= HorizontalDots)
                        {
                            return true;
                        }
                        if (DotsArray[indexRow, indexColumn + 1].HasSquare && (j == 3 || !MovingShape.Matrix[i, j + 1]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void MoveLeft()
        {
            if (MovingShape != null && !MovingShape.AtBottom)
            {
                MovingShape.MoveLeft();
            }


        }
        public void MoveRight()
        {
            if (MovingShape != null && !MovingShape.AtBottom)
            {
                MovingShape.MoveRight();
            }
        }
        public void MoveUp()
        {
            if (MovingShape != null && !MovingShape.AtBottom)
            {
                MovingShape.MoveUp();
            }
        }
        public void MoveDown()
        {
            if (MovingShape != null && !MovingShape.AtBottom)
            {
                MovingShape.MoveDown();
                CheckIfMovingShapeAtBottom();
            }
        }

        public void CheckIfMovingShapeAtBottom()
        {
            if (MovingShape!=null && !MovingShape.AtBottom)
            {
                int rowIndex = MovingShape.IndexRow;
                int columnIndex = MovingShape.IndexColumn;
                for (int i = MovingShape.Height-1; i >= 0; i--)
                {
                    bool found = false;
                    for (int j = 0; j < MovingShape.Width; j++)
                    {

                        if (MovingShape.Matrix[i, j])
                        {
                            rowIndex = MovingShape.IndexRow + i;
                            columnIndex = MovingShape.IndexColumn + j;
                            if (rowIndex >= 0 && rowIndex + 1 < VerticalDots && columnIndex>=0 && columnIndex < HorizontalDots && DotsArray[rowIndex + 1, columnIndex].HasSquare && (i == 3 || (!MovingShape.Matrix[i + 1, j])))
                            {
                                MovingShape.AtBottom = true;
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found)
                    {
                        CheckFullRows();
                        // Treba da se ima metod UpdateMovingShape(), a toa UpdateDots() nema potreba da ga ima
                        // Ama treba i drugi rabote da se smenat
                        break;
                    }
                }
            
            }
        }

        private void CheckFullRows()
        {
            for(int i=0; i < VerticalDots; i++)
            {
                if (RowIsFull(i))
                {
                    ClearRow(i);
                    ClearedRows++;
                }
            }
        }

        private void ClearRow(int i)
        {
            for(int x = i; x > 0; x--)
            {
                for(int y = 0; y < HorizontalDots; y++)
                {
                    DotsArray[x, y].HasSquare = DotsArray[x-1,y].HasSquare;
                }
            }
        }

        private bool RowIsFull(int i)
        {
            
            for(int j=0;j < HorizontalDots; j++)
            {
                if (!DotsArray[i, j].HasSquare)
                {
                    return false;
                }
            }
            return true;
        }

        private void CheckIfGameOver()
        {
            for(int x=0; x<4; x++)
            {
                for (int i = 0; i < HorizontalDots; i++)
                {
                    if (DotsArray[x,i].HasSquare)
                    {
                        GameIsStarted = false;
                        break;
                    }
                }
                if (!GameIsStarted)
                {
                    break;
                }
            }

        }
    }
}
