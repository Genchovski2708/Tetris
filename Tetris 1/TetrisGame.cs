using Microsoft.VisualBasic.ApplicationServices;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Tetris_1
{
    public partial class TetrisGame : Form
    {
        public Playground Playground { get; set; }
        public Playground Playground2 { get; set; }
        public bool TwoPlayers { get; set; } = false;
        public bool SinglePlayer { get; set; } = false;
        public bool ExtremeModeOn { get; set; } = false;
        public bool Started { get; set; } = false;
        public bool Easy { get; set; } = false;
        public bool Intermediate { get; set; } = false;
        public bool Hard { get; set; } = false;

        int speed = 10;

        int tick = 0;
        int tick2 = 0;
        int SelectedLang = 0;
        public TetrisGame()
        {
            InitializeComponent();
            DoubleBuffered = true;
            timer1.Interval = 100;
            UpdateStartBackground();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            this.Icon = new Icon("Images\\TETRIS4.ico");
            langPack.SelectedIndex = 0;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.Orange;
            langPack.BackColor = Color.Orange;
            langPack.ForeColor = Color.Blue;
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
                Playground.DrawSquares(e.Graphics);
                string text;
                if (langPack.SelectedIndex == 0)
                {
                    text = "Player 1";
                }
                else
                {
                    text = "Играч 1";
                }
                Point point = new Point(Playground.TopLeft.X, Playground.TopLeft.Y - 5 * Playground.DISTANCE);
                e.Graphics.DrawString(text, font, brush, point);
            }
            if (TwoPlayers)
            {
                if (Playground2 != null && Playground2.GameIsStarted)
                {
                    Playground2.DrawSquares(e.Graphics);
                    string text;
                    if (langPack.SelectedIndex == 0)
                    {
                        text = "Player 2";
                    }
                    else
                    {
                        text = "Играч 2";
                    }
                    Point point = new Point(Playground2.TopLeft.X, Playground2.TopLeft.Y - 5 * Playground.DISTANCE);
                    e.Graphics.DrawString(text, font, brush, point);
                }
            }
        }

        private bool SetDifficulty()
        {
            Difficulty difficulty = new Difficulty(SelectedLang);
            difficulty.StartPosition = FormStartPosition.CenterScreen;
            DialogResult result = difficulty.ShowDialog();
            SetDifficultyBooleans(difficulty);
            return result == DialogResult.OK; // Return true if the form was closed using DialogResult.OK


        }
        private void SetDifficultyBooleans(Difficulty difficulty)
        {
            Easy = difficulty.Easy;
            Intermediate = difficulty.Intermediate;
            Hard = difficulty.Hard;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            tick++;
            if (TwoPlayers)
            {
                tick2++;
            }
            if (!ExtremeModeOn)
            {
                timer1.Interval = 1;
                int tickNow1 = SetTickNow(Playground);

                if (tick % tickNow1 == 0 || Playground.MovingShape == null || Playground.MovingShape.AtBottom)
                {
                    PlaygroundAddShapeTick(Playground);
                    HandleGameOver();
                    HandleSinglePlayerGameFinish();
                    HandleTwoPlayersGameFinish();
                    tick = 0;
                }
                int tickNow2 = 1;
                if (TwoPlayers)
                {
                    tickNow2 = SetTickNow(Playground2);
                }
                if (TwoPlayers && (tick2 % tickNow2 == 0 || Playground2.MovingShape == null || Playground2.MovingShape.AtBottom))
                {

                    if (TwoPlayers)
                    {
                        PlaygroundAddShapeTick(Playground2);
                    }
                    HandleGameOver();
                    HandleSinglePlayerGameFinish();
                    HandleTwoPlayersGameFinish();
                    tick2 = 0;
                }

            }
            else
            {
                HandleExtremeMode();
            }
            Invalidate();
        }

        private void PlaygroundAddShapeTick(Playground Playground)
        {
            Playground.AddShape();
            Playground.Tick();
        }

        private int SetTickNow(Playground Playground)
        {
            if (Easy)
            {
                return 100 / (int)(0.5 * Playground.Level + 3);
            }
            if (Intermediate)
            {
                return 100 / (Playground.Level + 3);
            }
            if (Hard)
            {
                return 100 / (int)(2 * Playground.Level + 3);
            }
            return 1;
        }
        private void HandleGameOver()
        {
            if (TwoPlayers)
            {
                if (Playground.GameOver && (TwoPlayers && Playground2.GameOver))
                {
                    timer1.Stop();
                }
            }
            else
            {
                if (Playground.GameOver)
                {
                    timer1.Stop();
                }
            }
        }
        private void HandleSinglePlayerGameFinish()
        {
            if (Playground.FinishedS)
            {
                DialogResult dg;
                if (langPack.SelectedIndex == 0)
                {
                    dg = MessageBox.Show($"Your points: {Playground.Points}, New Game?", "Game Over", MessageBoxButtons.YesNo);
                }
                else
                {
                    dg = MessageBox.Show($"Вашите поени: {Playground.Points}, Нова игра?", "Играта заврши", MessageBoxButtons.YesNo);
                }
                if (dg == DialogResult.Yes)
                {
                    Playground = null;
                    SinglePlayerStart();
                }
                else
                {
                    UpdateStartBackground();
                }
            }
        }
        private void HandleTwoPlayersGameFinish()
        {
            if ((Playground != null && Playground.FinishedT) && (Playground2 != null && Playground2.FinishedT))
            {
                string winner = GetWinner();
                if (string.IsNullOrEmpty(winner))
                {
                    ShowDrawMessageBox();
                }
                else
                {
                    ShowWinnerMessageBox(winner);
                }
                DialogResult dg;
                if (langPack.SelectedIndex == 0)
                {
                    dg = MessageBox.Show("New game?", "Game Over", MessageBoxButtons.YesNo);
                }
                else
                {
                    dg = MessageBox.Show("Нова игра?", "Играта заврши", MessageBoxButtons.YesNo);
                }
                if (dg == DialogResult.Yes)
                {
                    Playground = null;
                    Playground2 = null;
                    TwoPlayersStart();
                    textBox2.Visible = false;
                }
                else
                {
                    UpdateStartBackground();
                }
            }
            else if (Playground != null && Playground.FinishedT)
            {
                textBox2.Visible = true; textBox1.Enabled = false;
                textBox2.Location = new Point(200, 530);
                if (langPack.SelectedIndex == 0)
                {
                    textBox2.Text = $"Points: {Playground.Points.ToString()}";
                }
                else
                {
                    textBox2.Text = $"Поени: {Playground.Points.ToString()}";
                }
                textBox2.Enabled = false;
            }
            else if (TwoPlayers && Playground2.GameOver)
            {
                textBox2.Visible = true; textBox1.Enabled = false;
                textBox2.Location = new Point(700, 530);
                if (langPack.SelectedIndex == 0)
                {
                    textBox2.Text = $"Points: {Playground.Points.ToString()}";
                }
                else
                {
                    textBox2.Text = $"Поени: {Playground.Points.ToString()}";
                }
                textBox2.Enabled = false;
            }
        }

        private string GetWinner()
        {
            string winner = "DRAW";
            if (langPack.SelectedIndex != 0)
            {
                winner = "НЕРЕШЕНО";
            }
            if (Playground.Points > Playground2.Points)
            {
                if (langPack.SelectedIndex == 0)
                {
                    winner = "Player 1!";
                }
                else
                {
                    winner = "Играч 1";
                }
            }
            else if (Playground.Points < Playground2.Points)
            {
                if (langPack.SelectedIndex == 0)
                {
                    winner = "Player 2";
                }
                else
                {
                    winner = "Играч 2";
                }
            }
            return winner;
        }

        private void ShowDrawMessageBox()
        {
            if (langPack.SelectedIndex == 0)
            {
                MessageBox.Show("DRAW!", "Game Ended", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("НЕРЕШЕНО!", "Играта заврши", MessageBoxButtons.OK);
            }
        }

        private void ShowWinnerMessageBox(string winner)
        {
            if (langPack.SelectedIndex == 0)
            {
                if (!winner.Equals("DRAW"))
                {
                    MessageBox.Show($"The winner is: {winner}!", "Game Ended", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"DRAW!", "Game Ended", MessageBoxButtons.OK);
                }
            }
            else
            {
                if (!winner.Equals("НЕРЕШЕНО"))
                {
                    MessageBox.Show($"Победникот е: {winner}", "Играта заврши", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"НЕРЕШЕНО!", "Играта заврши", MessageBoxButtons.OK);
                }
            }
        }
        private void HandleExtremeMode()
        {
            if (tick % speed == 0 || Playground.MovingShape == null || Playground.MovingShape.AtBottom)
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
                DialogResult dg;
                if (langPack.SelectedIndex == 0)
                {
                    dg = MessageBox.Show($"Your points: {Playground.Points}, New Game?", "Game Over", MessageBoxButtons.YesNo);
                }
                else
                {
                    dg = MessageBox.Show($"Вашите поени: {Playground.Points}, Нова игра?", "Играта заврши", MessageBoxButtons.YesNo);
                }
                if (dg == DialogResult.Yes)
                {
                    Playground = null;
                    ExtremeModeStart();
                }
                else
                {
                    UpdateStartBackground();
                }
            }
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
            langPack.Enabled = true;
            langPack.Visible = true;
            label1.Visible = true;
            label1.Enabled = true;
            textBox1.Visible = false;
            textBox2.Visible = false;
            Started = false;
            TwoPlayers = false;
            SinglePlayer = false;
            ExtremeModeOn = false;
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
            langPack.Visible = false;
            langPack.Enabled = false;
            label1.Visible = false;
            label1.Enabled = false;
        }
        private void langChange(int i)
        {
            if (i == 0)
            {
                button1.Text = "Single Player";
                button2.Text = "Two Players";
                button3.Text = "HELP";
                button4.Text = "Exit";
                button5.Text = "Extreme Mode";
                label1.Text = "Choose language:";
                langPack.SelectedIndex = 0;
            }
            else if (i == 1)
            {
                button1.Text = "Еден играч";
                button2.Text = "Двајца играчи";
                button3.Text = "ПОМОШ";
                button4.Text = "Излез";
                button5.Text = "Екстремен левел";
                label1.Text = "Изберете јазик:";
                langPack.SelectedIndex = 1;
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Playground.Move(e.KeyCode);
            if (TwoPlayers)
            {
                Playground2.Move(e.KeyCode);
            }
        }

        private void RemoveBackground()
        {
            this.BackgroundImage = null;
            this.BackColor = Color.White;
            SetButtonsOff();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SinglePlayerStart();


        }
        private void UpdateLevelMode()
        {
            textBox1.Visible = true;
            textBox1.Enabled = false;
            textBox1.BackColor = Color.Orange;
            if (langPack.SelectedIndex == 0)
            {
                if (Easy)
                {
                    textBox1.Text = "Easy";
                }
                else if (Intermediate)
                {
                    textBox1.Text = "Intermediate";
                }
                else if (Hard)
                {
                    textBox1.Text = "Hard";
                }
                else if (ExtremeModeOn)
                {
                    textBox1.Text = "Extreme";
                }
            }
            else
            {
                if (Easy)
                {
                    textBox1.Text = "Лесно";
                }
                else if (Intermediate)
                {
                    textBox1.Text = "Средно";
                }
                else if (Hard)
                {
                    textBox1.Text = "Тешко";
                }
                else if (ExtremeModeOn)
                {
                    textBox1.Text = "Екстремно";
                }
            }
        }
        public void SinglePlayerStart()
        {
            timer1.Interval = 1;
            if (Playground == null || !Playground.GameIsStarted)
            {
                Playground = new Playground(new Point(335, 250), new Point(735, 910), langPack.SelectedIndex);
                RemoveBackground();
                UpdateGameBackground();
            }
            Playground.GameIsStarted = true;
            Playground.TwoPlayers = false;
            SinglePlayer = true;
            TwoPlayers = false;
            if (SetDifficulty())
            {
                HelpStart hp;
                if (langPack.SelectedIndex == 0)
                {
                    hp = new HelpStart(1);
                }
                else
                {
                    hp = new HelpStart(5);
                }
                UpdateLevelMode();
                hp.StartPosition = FormStartPosition.CenterScreen;
                hp.ShowDialog();
                timer1.Start();
                Started = true;
                Invalidate();
            }
            else
            {
                Playground = null;
                UpdateStartBackground();
            }
        }
        public void TwoPlayersStart()
        {
            timer1.Interval = 1;
            if ((Playground == null || !Playground.GameIsStarted) && (Playground2 == null || !Playground2.GameIsStarted))
            {
                Playground = new Playground(new Point(70, 250), new Point(470, 910), langPack.SelectedIndex);
                Playground2 = new Playground(new Point(580, 250), new Point(980, 910), langPack.SelectedIndex);
                Playground2.SecondGround = true;
                RemoveBackground();
                UpdateGameBackground();
            }
            Playground.GameIsStarted = true;
            Playground2.GameIsStarted = true;
            Playground.TwoPlayers = true;
            Playground2.TwoPlayers = true;
            TwoPlayers = true;
            SinglePlayer = false;
            textBox2.Visible=false;
            if (SetDifficulty())
            {
                HelpStart hp;
                if (langPack.SelectedIndex == 0)
                {
                    hp = new HelpStart(2);
                }
                else
                {
                    hp = new HelpStart(6);
                }
                UpdateLevelMode();
                hp.StartPosition = FormStartPosition.CenterScreen;
                hp.ShowDialog();
                timer1.Start();
                Started = true;
                Invalidate();
            }
            else
            {
                Playground = null;
                UpdateStartBackground();
            }
        }
        public void ExtremeModeStart()
        {
            timer1.Interval = 100;
            if (Playground == null || !Playground.GameIsStarted)
            {
                Playground = new Playground(new Point(335, 250), new Point(735, 910), langPack.SelectedIndex);
                RemoveBackground();
                UpdateGameBackground();
            }
            Playground.GameIsStarted = true;
            Playground.TwoPlayers = false;
            ExtremeModeOn = true;
            Playground.Extreme = true;
            Playground.GameIsStarted = true;
            HelpStart hp;
            if (langPack.SelectedIndex == 0)
            {
                hp = new HelpStart(3);
            }
            else
            {
                hp = new HelpStart(7);
            }
            UpdateLevelMode();
            hp.StartPosition = FormStartPosition.CenterScreen;
            hp.ShowDialog();
            timer1.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            TwoPlayersStart();
            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            HelpStart hp;
            if (langPack.SelectedIndex == 0)
            {
                hp = new HelpStart(0);
            }
            else
            {
                hp = new HelpStart(4);
            }
            hp.StartPosition = FormStartPosition.CenterScreen;
            hp.ShowDialog();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (langPack.SelectedIndex == 0)
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to quit game?", "Quit game", MessageBoxButtons.YesNo))
                {
                    this.Close();
                }
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("Дали сте сигурни дека сакате да излезете од играта?", "Излез", MessageBoxButtons.YesNo))
                {
                    this.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExtremeModeStart();
            
            Invalidate();
        }

        private void langPack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (langPack.SelectedIndex == 0)
            {
                SelectedLang = 0;
                langChange(0);
            }
            else if (langPack.SelectedIndex == 1)
            {
                SelectedLang = 1;
                langChange(1);
            }
            Invalidate();
        }

        private void langPack_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            //  Cursor.Current = Cursors.Cross;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            // Cursor.Current = Cursors.Default;
        }
    }
}