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
        public int Points { get; set; } = 0;
        public DialogResult DialogRes { get; set; } = DialogResult.None;
        public bool GameOver { get; set; } = false;
        public bool FinishedT { get; set; } = false;
        public bool FinishedS { get; set; } = false;
        public bool SecondGround { get; set; }
        public Dot[,] PreviewShapeDots { get; set; }
        Shape PreviewShape;
        public bool TwoPlayers { get; set; } = false;
        public bool Extreme { get; set; } = false;
        public int Level { get; set; } = 1;
        public bool ChangedLevel { get; set; } = false;
        public Playground(Point topLeft, Point topRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            Dots = new List<Dot>();
            Shapes = new List<Shape>();
            Shape MovingShape;
            HorizontalDots = (TopRight.X + DISTANCE - TopLeft.X) / DISTANCE;
            VerticalDots = (TopRight.Y + DISTANCE - TopLeft.Y) / DISTANCE;
            DotsArray = new Dot[VerticalDots + 4 , HorizontalDots];
            PreviewShapeDots = new Dot[4, 4];
            for(int i = 0; i < VerticalDots; i++)
            {
                for(int j = 0; j<HorizontalDots; j++)
                {
                    DotsArray[i,j] = new Dot();
                }
            }
            for(int i=0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    PreviewShapeDots[i, j] = new Dot();
                }
            }
            GenerateDots();
        }
        public void GenerateDots()
        {
            int countRow = 0;
            int countCol = 0;
            for (int j = TopLeft.Y ; j <= TopRight.Y; j += DISTANCE)
            {
                for (int i = TopLeft.X; i <= TopRight.X; i += DISTANCE)
                {
                    Dot temp = new Dot(new Point(i, j));
                    DotsArray[countRow, countCol] = temp;
                    countCol++;
                }
                countCol = 0;
                countRow++;
            }
            for(int i=0; i < 4; i++)
            {
                for(int j = 3; j >= 0; j--)
                {
                    PreviewShapeDots[j, i] = new Dot(new Point(TopRight.X-3*DISTANCE+i*DISTANCE,TopLeft.Y-2*DISTANCE-(3-j)*DISTANCE));
                }
            }
        }
        public void DrawDots(Graphics g)
        {
            Brush p = new SolidBrush(Color.Blue);
            g.FillRectangle(p, TopLeft.X-20, TopLeft.Y-15, TopRight.X - TopLeft.X+40, TopRight.Y - TopLeft.Y+15);
            if (!Extreme)
            {
                g.FillRectangle(p, TopLeft.X + 260, TopLeft.Y - 220, DISTANCE * 4, DISTANCE * 4);
            }
            p.Dispose();
            for (int i = 0; i < VerticalDots; i++)
            {
                for (int j = 0; j < HorizontalDots; j++)
                {
                    DotsArray[i, j].Draw(g);
                }
            }
            if (!Extreme)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        PreviewShapeDots[i, j].Draw(g);
                    }
                }
            }
            Font font = new Font("Arial", 18);
            Brush brush = Brushes.White;

            string text = String.Format("Rows: {0}", ClearedRows.ToString());
            Point point = new Point(TopLeft.X, TopLeft.Y - 4 * DISTANCE);
            g.DrawString(text, font, brush, point);

            text = String.Format("Points: {0}", Points.ToString());
            point = new Point(TopLeft.X, TopLeft.Y - 3 * DISTANCE);
            g.DrawString(text, font, brush, point);

            if (!Extreme)
            {
                text = String.Format("Level: {0}", Level.ToString());
                point = new Point(TopLeft.X, TopLeft.Y - 2 * DISTANCE);
                g.DrawString(text, font, brush, point);
            }

            Pen pen = new Pen(Color.Black, 2);
            for (int i = 0; i < VerticalDots; i++)
            {
                for (int j = 0; j < HorizontalDots; j++)
                {
                    g.DrawRectangle(pen, DotsArray[i, j].Center.X - DISTANCE / 2, DotsArray[i, j].Center.Y - DISTANCE / 2, DISTANCE, DISTANCE);
                }
            }
            if (!Extreme)
            {
                for (int i = TopLeft.X + 260; i < (TopLeft.X + 260 + (4 * DISTANCE)); i += DISTANCE)
                {
                    for (int j = TopLeft.Y - 220; j < (TopLeft.Y - 220) + (4 * DISTANCE); j += DISTANCE)
                    {
                        g.DrawRectangle(pen, i, j, DISTANCE, DISTANCE);
                    }
                }
            }
            Pen pe = new Pen(Color.Yellow, 3);
            if (!Extreme)
            {
                g.DrawRectangle(pe, TopLeft.X + 257, TopLeft.Y - 223, DISTANCE * 4 + 4, DISTANCE * 4 + 4);
            }
            g.DrawRectangle(pe, TopLeft.X - 23, TopLeft.Y - 18, TopRight.X - TopLeft.X + 44, TopRight.Y - TopLeft.Y + 19);
            pen.Dispose();
            pe.Dispose();

        }  
        public void AddShape()
        {
            if (MovingShape == null || MovingShape.AtBottom && GameIsStarted)
            {
                CheckIfGameOver();
                if(MovingShape!=null && !GameIsStarted)
                {
                    GameOver = true;
                    if (!TwoPlayers)
                    {
                        FinishedS = true;    
                    }
                    else
                    {
                        FinishedT= true;
                    }
                }
                else
                {
                    CheckFullRows();
                    int indx = HorizontalDots / 2 -1;
                    if (Extreme)
                    {
                        indx = Random.Next(0, HorizontalDots);
                    }
                    Dot randomDot = DotsArray[0, indx];
                    if(PreviewShape!= null)
                    {
                        MovingShape = PreviewShape;
                    }
                    else
                    {
                        MovingShape = GenerateShape(randomDot, indx);
                    }
                    PreviewShape = GenerateShape(randomDot, indx);
                    ChangePreviewDots();
                    Shapes.Add(MovingShape);

                }

            }

        }
        private void ChangePreviewDots()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j =0; j<4; j++)
                {
                    PreviewShapeDots[i, j].HasSquare = PreviewShape.Matrix[i, j];
                    PreviewShapeDots[i, j].Color = PreviewShape.Color;
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
                        DotsArray[rowIndex, columnIndex].Color = MovingShape.Color;
                        if (rowIndex == VerticalDots - 1)
                        {
                            MovingShape.AtBottom = true;
                        }
                    }
                }
            }

            
        }
        private void BottomPreview()
        {
            if (!MovingShape.AtBottom && !Extreme)
            {
                Shape tempShape = (Shape)MovingShape.Clone();
                for (int i = MovingShape.IndexRow; i < VerticalDots - MovingShape.Height; i++)
                {
                    tempShape.IndexRow = i;
                    if (CheckIfShapeAtBottom(tempShape))
                    {
                        if (!CheckIfOverlapping(tempShape, i))
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                for (int x = 0; x < 4; x++)
                                {
                                    if (tempShape.Matrix[y, x])
                                    {
                                        DotsArray[i + y, tempShape.IndexColumn + x].BottomPreview = true;
                                    }
                                }
                            }
                        }
                        return;
                    }
                }
                if (!CheckIfOverlapping(tempShape, VerticalDots - tempShape.Height))
                {
                    for (int y = tempShape.Height - 1; y >= 0; y--)
                    {
                        for (int x = 0; x < tempShape.Width; x++)
                        {
                            if (tempShape.Matrix[y, x])
                            {
                                DotsArray[VerticalDots - (tempShape.Height - y), tempShape.IndexColumn + x].BottomPreview = true;
                            }
                        }
                    }
                }
            }
            

            


        }

        private bool CheckIfOverlapping(Shape tempShape, int i)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (tempShape.Matrix[y, x] && DotsArray[i + y, tempShape.IndexColumn + x].HasSquare && !IsMovingShapeDot(i + y, tempShape.IndexColumn + x))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsMovingShapeDot(int y, int x)
        {
            for(int i=0;i<MovingShape.Height;i++)
            {
                for(int j = 0; j < MovingShape.Width; j++)
                {
                    if(MovingShape.IndexRow+i == y && MovingShape.IndexColumn+j == x && MovingShape.Matrix[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckIfShapeAtBottom(Shape CheckedShape)
        {
            if (CheckedShape != null && !CheckedShape.AtBottom)
            {
                int rowIndex = CheckedShape.IndexRow;
                int columnIndex = CheckedShape.IndexColumn;
                for (int i = CheckedShape.Height - 1; i >= 0; i--)
                {
                    bool found = false;
                    for (int j = 0; j < CheckedShape.Width; j++)
                    {

                        if (CheckedShape.Matrix[i, j])
                        {
                            rowIndex = CheckedShape.IndexRow + i;
                            columnIndex = CheckedShape.IndexColumn + j;
                            if (rowIndex >= 0 && rowIndex + 1 < VerticalDots && columnIndex >= 0 && columnIndex < HorizontalDots && DotsArray[rowIndex + 1, columnIndex].HasSquare && (i == 3 || (!CheckedShape.Matrix[i + 1, j])))
                            {
                                return true;
                            }
                        }
                    }
                }
                
            }
            return false;
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
                ResetBottomPreview();
                BottomPreview();
            }

        }

        private void ResetBottomPreview()
        {
            for(int i = 0; i < VerticalDots; i++)
            {
                for(int j=0; j< HorizontalDots; j++)
                {
                    DotsArray[i, j].BottomPreview = false;
                }
            }
        }

      

        public Shape GenerateShape(Dot dot, int index)
        {

            int limitLeft = 0;
            int limitRight = HorizontalDots;
            int random = Random.Next(0, 5);
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
            if (!SecondGround)
            {
                if (MovingShape != null)
                {
                    if (GameIsStarted && !MovingShape.AtBottom)
                    {
                        if (keys == Keys.Left || keys == Keys.Right || keys == Keys.Up || keys == Keys.Down)
                        {
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
                        ResetBottomPreview();
                        BottomPreview();
                    }
                }
            }
            else
            {
                if (MovingShape != null)
                {
                    if (GameIsStarted && !MovingShape.AtBottom)
                    {
                        if (keys == Keys.A || keys == Keys.D || keys == Keys.W || keys == Keys.S)
                        {
                            ResetDots();
                        }
                        if (keys == Keys.A)
                        {
                            if (!CheckCollisionsLeft())
                            {
                                MoveLeft();
                            }

                        }
                        else if (keys == Keys.D)
                        {
                            if (!CheckCollisionsRight())
                            {
                                MoveRight();
                            }
                        }
                        else if (keys == Keys.W)
                        {
                            MoveUp();
                        }
                        else if (keys == Keys.S)
                        {

                            MoveDown();
                        }
                        UpdateDots();
                        ResetBottomPreview();
                        BottomPreview();
                    }
                }
            }
        }

        private bool CheckCollisionsLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                int indexRow = MovingShape.IndexRow + i;
                int indexColumn = MovingShape.IndexColumn;
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
            if (MovingShape != null)
            {
                CheckIfMovingShapeAtBottom();
                if (!MovingShape.AtBottom)
                {
                    MovingShape.MoveDown();
                }

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
            int clearedRows = 0;
            for(int i=0; i < VerticalDots; i++)
            {
                if (RowIsFull(i))
                {
                    ClearRow(i);
                    ClearedRows++;
                    if (ClearedRows != 0 && ClearedRows % 10 == 0)
                    {
                        ChangedLevel = true;
                        Level++;
                    }
                    if(Level > 29)
                    {
                        Level = 29;
                    }
                    else if (Level < 1)
                    {
                        Level = 1;
                    }
                    clearedRows++;
                }
            }
            while (clearedRows > 0){
                switch (clearedRows)
                {
                    case 0:
                        break;
                    case 1:
                        Points += Level * 40;
                        clearedRows--;
                        break;
                    case 2:
                        Points += Level * 100;
                        clearedRows -= 2;
                        break;
                    case 3:
                        Points += Level * 300;
                        clearedRows -= 3;
                        break;
                    default:
                        Points += Level * 1200;
                        clearedRows -= 4;
                        break;
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
