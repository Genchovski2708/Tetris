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
        public List<Shape> Shapes { get; set; }
        public Shape MovingShape { get; set; }
        public GridSquare [,] GridMatrix { get; set; }
        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }

        public int HorizontalSquares { get; set; }
        public int VerticalSquares { get; set; }
        public bool GameIsStarted { get; set; }
        public int ClearedRows { get; set; } = 0;
        public int Points { get; set; } = 0;
        public DialogResult DialogRes { get; set; } = DialogResult.None;
        public bool GameOver { get; set; } = false;
        public bool FinishedT { get; set; } = false;
        public bool FinishedS { get; set; } = false;
        public bool SecondGround { get; set; }
        public GridSquare[,] PreviewShapeSquares { get; set; }
        Shape PreviewShape;
        public bool TwoPlayers { get; set; } = false;
        public bool Extreme { get; set; } = false;
        public int Level { get; set; } = 1;
        public bool ChangedLevel { get; set; } = false;
        public int  Language { get; set; }//0 - EN 1 - MK
        public bool HasLeft { get; set; }
        public bool HasRight { get; set; }
        public bool Moved { get; set; } = true;
        public Playground(Point topLeft, Point topRight, int lang)
        {
            TopLeft = topLeft;
            BottomRight = topRight;
            Language = lang;
            Shapes = new List<Shape>();
            Shape MovingShape;
            HorizontalSquares = (BottomRight.X + DISTANCE - TopLeft.X) / DISTANCE;
            VerticalSquares = (BottomRight.Y + DISTANCE - TopLeft.Y) / DISTANCE;
            GridMatrix = new GridSquare[VerticalSquares + 4 , HorizontalSquares];
            PreviewShapeSquares = new GridSquare[4, 4];
            InitilizeGridMatrix();
            InitilizePreviewShapeSquares();
            MakeGrid();
        }

        private void InitilizePreviewShapeSquares()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PreviewShapeSquares[i, j] = new GridSquare();
                }
            }
        }

        private void InitilizeGridMatrix()
        {
            for (int i = 0; i < VerticalSquares; i++)
            {
                for (int j = 0; j < HorizontalSquares; j++)
                {
                    GridMatrix[i, j] = new GridSquare();
                }
            }
        }

        public void MakeGrid()
        {
            int countRow = 0;
            int countCol = 0;
            for (int j = TopLeft.Y ; j <= BottomRight.Y; j += DISTANCE)
            {
                for (int i = TopLeft.X; i <= BottomRight.X; i += DISTANCE)
                {
                    GridSquare temp = new GridSquare(new Point(i, j));
                    GridMatrix[countRow, countCol] = temp;
                    countCol++;
                }
                countCol = 0;
                countRow++;
            }
            for(int i=0; i < 4; i++)
            {
                for(int j = 3; j >= 0; j--)
                {
                    PreviewShapeSquares[j, i] = new GridSquare(new Point(BottomRight.X-3*DISTANCE+i*DISTANCE,TopLeft.Y-2*DISTANCE-(3-j)*DISTANCE));
                }
            }
        }
        public void DrawSquares(Graphics g)
        {
            Brush p = new SolidBrush(Color.Blue);
            g.FillRectangle(p, TopLeft.X - 20, TopLeft.Y - 15, BottomRight.X - TopLeft.X + 40, BottomRight.Y - TopLeft.Y + 15);
            if (!Extreme)
            {
                g.FillRectangle(p, TopLeft.X + 260, TopLeft.Y - 220, DISTANCE * 4, DISTANCE * 4);
            }
            p.Dispose();

            DrawGridMatrix(g);

            if (!Extreme)
            {
                DrawPreviewShapeSquares(g);
            }

            DrawText(g, ClearedRows.ToString(), Points.ToString(), TopLeft, DISTANCE, Language);

            if (!Extreme)
            {
                DrawLevel(g);
            }

            DrawGridRectangles(g);

            if (!Extreme)
            {
                DrawPreviewRectangles(g);
            }

            DrawOuterRectangles(g);
        }

        private void DrawText(Graphics g, string rowsText, string pointsText, Point topLeft, int distance, int language)
        {
            Font font = new Font("Arial", 18);
            Brush brush = Brushes.White;
            string text;
            Point point;

            if (language == 0)
            {
                text = String.Format("Rows: {0}", rowsText);
                point = new Point(topLeft.X, topLeft.Y - 4 * distance);
                g.DrawString(text, font, brush, point);

                text = String.Format("Points: {0}", pointsText);
                point = new Point(topLeft.X, topLeft.Y - 3 * distance);
                g.DrawString(text, font, brush, point);
            }
            else
            {
                text = String.Format("Редици: {0}", rowsText);
                point = new Point(topLeft.X, topLeft.Y - 4 * distance);
                g.DrawString(text, font, brush, point);

                text = String.Format("Поени: {0}", pointsText);
                point = new Point(topLeft.X, topLeft.Y - 3 * distance);
                g.DrawString(text, font, brush, point);
            }
        }

        private void DrawGridMatrix(Graphics g)
        {
            for (int i = 0; i < VerticalSquares; i++)
            {
                for (int j = 0; j < HorizontalSquares; j++)
                {
                    GridMatrix[i, j].Draw(g);
                }
            }
        }

        private void DrawPreviewShapeSquares(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PreviewShapeSquares[i, j].Draw(g);
                }
            }
        }

        private void DrawLevel(Graphics g)
        {
            Font font = new Font("Arial", 18);
            Brush brush = Brushes.White;
            string text;
            Point point;
            if (Language == 0)
            {
                text = String.Format("Level: {0}", Level.ToString());
                point = new Point(TopLeft.X, TopLeft.Y - 2 * DISTANCE);
                g.DrawString(text, font, brush, point);
            }
            else
            {
                text = String.Format("Ниво: {0}", Level.ToString());
                point = new Point(TopLeft.X, TopLeft.Y - 2 * DISTANCE);
                g.DrawString(text, font, brush, point);
            }
        }

        private void DrawGridRectangles(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            for (int i = 0; i < VerticalSquares; i++)
            {
                for (int j = 0; j < HorizontalSquares; j++)
                {
                    g.DrawRectangle(pen, GridMatrix[i, j].Center.X - DISTANCE / 2, GridMatrix[i, j].Center.Y - DISTANCE / 2, DISTANCE, DISTANCE);
                }
            }
            pen.Dispose();
        }

        private void DrawPreviewRectangles(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            for (int i = TopLeft.X + 260; i < (TopLeft.X + 260 + (4 * DISTANCE)); i += DISTANCE)
            {
                for (int j = TopLeft.Y - 220; j < (TopLeft.Y - 220) + (4 * DISTANCE); j += DISTANCE)
                {
                    g.DrawRectangle(pen, i, j, DISTANCE, DISTANCE);
                }
            }
            pen.Dispose();
        }

        private void DrawOuterRectangles(Graphics g)
        {
            Pen pe = new Pen(Color.Yellow, 3);
            if (!Extreme)
            {
                g.DrawRectangle(pe, TopLeft.X + 257, TopLeft.Y - 223, DISTANCE * 4 + 4, DISTANCE * 4 + 4);
            }
            g.DrawRectangle(pe, TopLeft.X - 23, TopLeft.Y - 18, BottomRight.X - TopLeft.X + 44, BottomRight.Y - TopLeft.Y + 19);
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
                    int indx = HorizontalSquares / 2 -1;
                    GridSquare randomDot = GridMatrix[0, indx];
                    if(PreviewShape!= null)
                    {
                        MovingShape = PreviewShape;
                    }
                    else
                    {
                        MovingShape = GenerateShape(randomDot, indx);
                    }
                    PreviewShape = GenerateShape(randomDot, indx);
                    ChangePreviewSquares();
                    Shapes.Add(MovingShape);

                }

            }

        }
        private void ChangePreviewSquares()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j =0; j<4; j++)
                {
                    PreviewShapeSquares[i, j].HasSquare = PreviewShape.Matrix[i, j];
                    PreviewShapeSquares[i, j].Color = PreviewShape.Color;
                }
            }
        }

        public void UpdateSquares()
        {

            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int rowIndex = MovingShape.IndexRow + i;
                    int columnIndex = MovingShape.IndexColumn + j;

                    if (MovingShape.Matrix[i, j] && rowIndex >= 0 && rowIndex < VerticalSquares && columnIndex >= 0 && columnIndex < HorizontalSquares)
                    {
                        GridMatrix[rowIndex, columnIndex].HasSquare = true;
                        GridMatrix[rowIndex, columnIndex].Color = MovingShape.Color;

                    }
                }
            }

            
        }
        private void BottomPreview()
        {
            if (!MovingShape.AtBottom && !Extreme)
            {
                Shape tempShape = (Shape)MovingShape.Clone();
                for (int i = MovingShape.IndexRow; i < VerticalSquares - MovingShape.Height; i++)
                {
                    tempShape.IndexRow = i;
                    if (CheckIfShapeAtBottom(tempShape))
                    {
                        if (!CheckIfOverlappingByRow(tempShape, i))
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                for (int x = 0; x < 4; x++)
                                {
                                    if (tempShape.Matrix[y, x])
                                    {
                                        GridMatrix[i + y, tempShape.IndexColumn + x].BottomPreview = true;
                                    }
                                }
                            }
                        }
                        return;
                    }
                }
                if (!CheckIfOverlappingByRow(tempShape, VerticalSquares - tempShape.Height))
                {
                    for (int y = tempShape.Height - 1; y >= 0; y--)
                    {
                        for (int x = 0; x < tempShape.Width; x++)
                        {
                            if (tempShape.Matrix[y, x])
                            {
                                GridMatrix[VerticalSquares - (tempShape.Height - y), tempShape.IndexColumn + x].BottomPreview = true;
                            }
                        }
                    }
                }
            }
            

            


        }

        private bool CheckIfOverlappingByRow(Shape tempShape, int i)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (tempShape.Matrix[y, x] && GridMatrix[i + y, tempShape.IndexColumn + x].HasSquare && !IsMovingShapeSquare(i + y, tempShape.IndexColumn + x))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsMovingShapeSquare(int y, int x)
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
                            if (rowIndex >= 0 && rowIndex + 1 < VerticalSquares && columnIndex >= 0 && columnIndex < HorizontalSquares && GridMatrix[rowIndex + 1, columnIndex].HasSquare && (i == 3 || (!CheckedShape.Matrix[i + 1, j])))
                            {
                                return true;
                            }
                        }
                    }
                }
                
            }
            return false;
        }

        private void ResetSquares()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if (MovingShape.Matrix[i, j])
                    {
                        GridMatrix[i + MovingShape.IndexRow, j + MovingShape.IndexColumn].HasSquare = false;
                    }
                }
            }
        }

        public void Tick()
        {

            if(GameIsStarted)
            {
                CheckIfMovingShapeAtBottom();
                if (MovingShape != null && MovingShape.IndexRow < VerticalSquares && !MovingShape.AtBottom)
                {
                        MovingShape.FixLimits();
                        ResetSquares();
                        MovingShape.IndexRow++;
                        UpdateSquares();
                        Moved = true;
                    
                }
                ResetBottomPreview();
                BottomPreview();
            }

        }

        private void ResetBottomPreview()
        {
            for(int i = 0; i < VerticalSquares; i++)
            {
                for(int j=0; j< HorizontalSquares; j++)
                {
                    GridMatrix[i, j].BottomPreview = false;
                }
            }
        }

      

        public Shape GenerateShape(GridSquare dot, int index)
        {

            int limitLeft = 0;
            int limitRight = HorizontalSquares;
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
            if (SecondGround)
            {
                MoveSecondGround(keys);
            }
            else
            {
                MoveFirstGround(keys);
            }
        }

        private void MoveFirstGround(Keys keys)
        {
            if (MovingShape == null || !GameIsStarted || MovingShape.AtBottom)
                return;

            CheckIfMoved();
            ResetSquares();

            switch (keys)
            {
                case Keys.Left:
                    if (!HasLeft)
                    {
                        MoveLeft();
                        Moved = true;
                    }
                    else
                    {
                        Moved = false;
                    }
                    break;
                case Keys.Right:
                    if (!HasRight)
                    {
                        MoveRight();
                        Moved = true;
                    }
                    else
                    {
                        Moved = false;
                    }
                    break;
                case Keys.Up:
                    Moved = Rotate();
                    break;
                case Keys.Down:
                    MoveDown();
                    Moved = true;
                    break;
                default:
                    Moved = false;
                    break;
            }

            UpdateSquares();
            ResetBottomPreview();
            BottomPreview();
        }

        private void MoveSecondGround(Keys keys)
        {
            if (MovingShape == null || !GameIsStarted || MovingShape.AtBottom)
                return;

            CheckIfMoved();
            ResetSquares();

            switch (keys)
            {
                case Keys.A:
                    if (!HasLeft)
                    {
                        MoveLeft();
                        Moved = true;
                    }
                    else
                    {
                        Moved = false;
                    }
                    break;
                case Keys.D:
                    if (!HasRight)
                    {
                        MoveRight();
                        Moved = true;
                    }
                    else
                    {
                        Moved = false;
                    }
                    break;
                case Keys.W:
                    Moved = Rotate();
                    break;
                case Keys.S:
                    MoveDown();
                    Moved = true;
                    break;
                default:
                    Moved = false;
                    break;
            }

            UpdateSquares();
            ResetBottomPreview();
            BottomPreview();
        }

        private void CheckIfMoved()
        {
            if (Moved)
            {
                HasRight = CheckCollisionsRight();
                HasLeft = CheckCollisionsLeft();
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
                        if (GridMatrix[indexRow, indexColumn - 1].HasSquare && (j == 0 || !MovingShape.Matrix[i, j - 1]))
                        {
                            return true;
                        }
                    }
                }
            }
            if (MovingShape.IndexColumn + MovingShape.StartingWidthIndex < MovingShape.LimitLeft)
            {
                return true;
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
                        if (indexColumn + 1 >= HorizontalSquares)
                        {
                            return true;
                        }
                        if (GridMatrix[indexRow, indexColumn + 1].HasSquare && (j == 3 || !MovingShape.Matrix[i, j + 1]))
                        {
                            return true;
                        }
                    }
                }
            }
            if (MovingShape.IndexColumn + MovingShape.Width > MovingShape.LimitRight )
            {
                return true;
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
        public bool Rotate()
        {
            Shape tempShape = (Shape)MovingShape.Clone();
            tempShape.Rotate();
            if (MovingShape != null && !MovingShape.AtBottom && !CheckIfOverlappingByRow(tempShape, tempShape.IndexRow))
            {
                return MovingShape.Rotate();
            }
            return false;
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
                            if (rowIndex == VerticalSquares - 1 || rowIndex >= 0 && rowIndex + 1 < VerticalSquares && columnIndex>=0 && columnIndex < HorizontalSquares && GridMatrix[rowIndex + 1, columnIndex].HasSquare && (i == 3 || (!MovingShape.Matrix[i + 1, j])))
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
                        break;
                    }
                }
            
            }
        }

        private void CheckFullRows()
        {
            int clearedRows = 0;
            for(int i=0; i < VerticalSquares; i++)
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
                for(int y = 0; y < HorizontalSquares; y++)
                {
                    GridMatrix[x, y].HasSquare = GridMatrix[x-1,y].HasSquare;
                }
            }
        }

        private bool RowIsFull(int i)
        {
            
            for(int j=0;j < HorizontalSquares; j++)
            {
                if (!GridMatrix[i, j].HasSquare)
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
                for (int i = 0; i < HorizontalSquares; i++)
                {
                    if (GridMatrix[x,i].HasSquare)
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
