using Microsoft.VisualBasic.ApplicationServices;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Tetris_1
{
    public partial class Form1 : Form
    {
        public Playground Playground { get; set; }
        public Playground Playground2 { get; set; }
        public bool TwoPlayers { get; set; } = false;
        public bool SinglePlayer { get; set; } = false;
        public bool HardModeOn { get; set; } = false;
        public bool Started { get; set; } = false;
        int speed = 10;

        int tick = 0;
        int tick2 = 0;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            timer1.Interval = 100;
            UpdateStartBackground();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            this.Icon = new Icon("Images\\TETRIS4.ico");
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Font font = new Font("Arial", 18);
            Brush brush = Brushes.White;
            if (Playground != null && Playground.GameIsStarted)
            {
                Playground.DrawDots(e.Graphics);

                string text = "Player 1";
                Point point = new Point(Playground.TopLeft.X, Playground.TopLeft.Y - 5 * Playground.DISTANCE);
                e.Graphics.DrawString(text, font, brush, point);
            }
            if (TwoPlayers)
            {
                if (Playground2 != null && Playground2.GameIsStarted)
                {
                    Playground2.DrawDots(e.Graphics);
                    string text = "Player 2";
                    Point point = new Point(Playground2.TopLeft.X, Playground2.TopLeft.Y - 5 * Playground.DISTANCE);
                    e.Graphics.DrawString(text, font, brush, point);
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            tick++;
            if (TwoPlayers)
            {
                tick2++;
            }
            if (!HardModeOn)
            {
                timer1.Interval = 33;
                if (tick % (30 - Playground.Level) == 0)
                {

                    Playground.AddShape();
                    Playground.Tick();
                    if (Playground.GameOver)
                    {
                        timer1.Stop();
                    }
                    if (TwoPlayers && Playground2.GameOver)
                    {
                        timer1.Stop();
                    }
                    if (Playground.FinishedS)
                    {
                        DialogResult dg = MessageBox.Show($"Your points: {Playground.Points}, New Game?", "Game Over", MessageBoxButtons.YesNo);
                        if (dg == DialogResult.Yes)
                        {
                            Playground = null;
                            UpdateStartBackground();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else if ((Playground.FinishedT) || (Playground2 != null && Playground2.FinishedT))
                    {
                        string w = "DRAW"; //0 - D 1 - P1 2 - P2
                        if (Playground.Points > Playground2.Points)
                        {
                            w = "Player 1!";
                        }
                        else if (Playground.Points < Playground2.Points)
                        {
                            w = "Player 2";
                        }
                        if (w.Equals("DRAW"))
                        {
                            MessageBox.Show($"DRAW!", "Game Ended", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show($"The winner is: {w}", "Game Ended", MessageBoxButtons.OK);
                        }
                        DialogResult dg = MessageBox.Show("New game?", "Game Over", MessageBoxButtons.YesNo);
                        if (dg == DialogResult.Yes)
                        {
                            Playground = null;
                            Playground2 = null;
                            UpdateStartBackground();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }

                if (SinglePlayer && tick == 30 - Playground.Level)
                {
                    tick = 0;
                }
                if (TwoPlayers && tick2 % (30 - Playground2.Level) == 0)
                {

                    if (TwoPlayers)
                    {
                        Playground2.AddShape();
                        Playground2.Tick();
                    }
                    if (Playground.GameOver)
                    {
                        timer1.Stop();
                    }
                    if (TwoPlayers && Playground2.GameOver)
                    {
                        timer1.Stop();
                    }
                    if (Playground.FinishedS)
                    {
                        DialogResult dg = MessageBox.Show($"Your points: {Playground.Points}, New Game?", "Game Over", MessageBoxButtons.YesNo);
                        if (dg == DialogResult.Yes)
                        {
                            Playground = null;
                            UpdateStartBackground();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else if ((Playground.FinishedT) || (Playground2 != null && Playground2.FinishedT))
                    {
                        string w = "DRAW"; //0 - D 1 - P1 2 - P2
                        if (Playground.Points > Playground2.Points)
                        {
                            w = "Player 1!";
                        }
                        else if (Playground.Points < Playground2.Points)
                        {
                            w = "Player 2";
                        }
                        if (w.Equals("DRAW"))
                        {
                            MessageBox.Show($"DRAW!", "Game Ended", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show($"The winner is: {w}", "Game Ended", MessageBoxButtons.OK);
                        }
                        DialogResult dg = MessageBox.Show("New game?", "Game Over", MessageBoxButtons.YesNo);
                        if (dg == DialogResult.Yes)
                        {
                            Playground = null;
                            Playground2 = null;
                            UpdateStartBackground();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }

                if (TwoPlayers && tick2 == 30 - Playground2.Level)
                {
                    tick2 = 0;
                }
            }
            else
            {
                if (tick % speed == 0)
                {
                    Playground.AddShape();
                    Playground.Tick();
                }
                if (tick == 100)
                {
                    tick = 0;
                    if (speed >= 3)
                    {
                        speed--;
                    }
                }
                if (Playground.FinishedS)
                {
                    timer1.Stop();
                    DialogResult dg = MessageBox.Show($"Your points: {Playground.Points}, New Game?", "Game Over", MessageBoxButtons.YesNo);
                    if (dg == DialogResult.Yes)
                    {
                        Playground = null;
                        UpdateStartBackground();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            Invalidate();
        }
        private void UpdateStartBackground()
        {
            BackgroundImage = Image.FromFile("Images\\TETRIS 4.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            button1.Visible = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button2.Visible = true;
            button3.Enabled = true;
            button3.Visible = true;
            button4.Visible = true;
            button4.Enabled = true;
            button5.Visible = true;
            button5.Enabled = true;
            Started = false;
            TwoPlayers = false;
            SinglePlayer = false;
            HardModeOn = false;
        }
        private void UpdateGameBackground()
        {
            BackgroundImage = Image.FromFile("Images\\pozadina.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            SetButtonsOff();
        }
        private void SetButtonsOff()
        {
            button1.Visible = false;
            button1.Enabled = false;
            button2.Visible = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button3.Visible = false;
            button4.Visible = false;
            button4.Enabled = false;
            button5.Visible = false;
            button5.Enabled = false;

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Playground.Move(e.KeyCode);
            if (TwoPlayers)
            {
                Playground2.Move(e.KeyCode);
            }
            Invalidate();
        }
        private void RemoveBackground()
        {
            this.BackgroundImage = null;
            this.BackColor = Color.White;
            SetButtonsOff() ;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SinglePlayerStart();
            SinglePlayer = true;
            TwoPlayers = false;
            HelpStart hp = new HelpStart(1);
            hp.StartPosition = FormStartPosition.CenterScreen;
            hp.ShowDialog();
            timer1.Start();
            Started = true;
            Invalidate();
        }
        public void SinglePlayerStart()
        {
            timer1.Interval = 100;
            if (Playground == null || !Playground.GameIsStarted)
            {
                Playground = new Playground(new Point(335, 250), new Point(735, 750));
                RemoveBackground();
                UpdateGameBackground();
            }
            Playground.GameIsStarted = true;
            Playground.TwoPlayers = false;
        }
        public void TwoPlayersStart()
        {
            timer1.Interval = 100;
            if ((Playground == null || !Playground.GameIsStarted) && (Playground2 == null || !Playground2.GameIsStarted))
            {
                Playground = new Playground(new Point(70, 250), new Point(470, 750));
                Playground2 = new Playground(new Point(580, 250), new Point(980, 750));
                Playground2.SecondGround = true;
                RemoveBackground();
                UpdateGameBackground();
            }
            Playground.GameIsStarted = true;
            Playground2.GameIsStarted = true;
            Playground.TwoPlayers = true;
            Playground2.TwoPlayers = true;
        }
        public void HardMode()
        {
            timer1.Interval = 100;
            if (Playground == null || !Playground.GameIsStarted)
            {
                Playground = new Playground(new Point(335, 250), new Point(735, 750));
                RemoveBackground();
                UpdateGameBackground();
            }
            Playground.GameIsStarted = true;
            Playground.TwoPlayers = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            TwoPlayersStart();
            TwoPlayers = true;
            SinglePlayer = false;
            HelpStart hp = new HelpStart(2);
            hp.StartPosition = FormStartPosition.CenterScreen;
            hp.ShowDialog();
            timer1.Start();
            Started = true;
            Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HelpStart hp = new HelpStart(0);
            hp.StartPosition = FormStartPosition.CenterScreen;
            hp.ShowDialog();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Quit game?", "Quit game", MessageBoxButtons.YesNo))
            {
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HardMode();
            HardModeOn = true;
            Playground.Hard = true;
            Playground.GameIsStarted = true;
            HelpStart hp = new HelpStart(3);
            hp.StartPosition = FormStartPosition.CenterScreen;
            hp.ShowDialog();
            timer1.Start();
            Invalidate();
        }
    }
}