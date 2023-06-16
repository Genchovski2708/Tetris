using Microsoft.VisualBasic.ApplicationServices;

namespace Tetris_1
{
    public partial class Form1 : Form
    {
        public Playground Playground { get; set; }
        int tick = 0;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            timer1.Interval = 1000;
            UpdateStartBackground();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (Playground != null && Playground.GameIsStarted)
            {
                Playground.DrawDots(e.Graphics);
            }

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Playground == null || !Playground.GameIsStarted)
            {
                Playground = new Playground(new Point(100, 100), new Point(500, 700));
            }
            timer1.Start();
            Playground.GameIsStarted = true;
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Playground.AddShape();
            Playground.Tick();
            UpdateLabels();
            if (Playground.DialogRes == DialogResult.Yes)
            {
                UpdateStartBackground();
            }
            else if (Playground.DialogRes == DialogResult.No)
            {
                this.Close();
            }
            Invalidate();
        }
        private void UpdateStartBackground()
        {
            BackgroundImage = Image.FromFile("Images\\TETRIS 1.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            button1.Visible = true;
            button1.Enabled = true;
            labelRows.Visible = false;
        }
        private void UpdateGameBackground()
        {
            BackgroundImage = Image.FromFile("Images\\pozadina.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            button1.Visible = false;
            button1.Enabled = false;
            labelRows.Visible = true;
        }
        private void UpdateLabels()
        {
            labelRows.Text = String.Format("Rows: {0}", Playground.ClearedRows);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Playground.Move(e.KeyCode);
            Invalidate();
        }

        private void singlePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Playground == null || !Playground.GameIsStarted)
            {
                Playground = new Playground(new Point(100, 100), new Point(500, 600));
            }
            timer1.Start();
            Playground.GameIsStarted = true;
            Invalidate();
        }

        private void twoPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Treba da se porabote na voa
            if (Playground == null || !Playground.GameIsStarted)
            {
                Playground = new Playground(new Point(100, 100), new Point(500, 600));
            }
            timer1.Start();
            Playground.GameIsStarted = true;
            Invalidate();
        }
        private void RemoveBackground()
        {
            this.BackgroundImage = null;
            this.BackColor = Color.White;
            button1.Visible = false;
            button1.Enabled = false;
            labelRows.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Playground == null || !Playground.GameIsStarted)
            {
                Playground = new Playground(new Point(100, 100), new Point(500, 600));
                RemoveBackground();
                UpdateGameBackground();
            }
            timer1.Start();
            Playground.GameIsStarted = true;
            Invalidate();
        }
    }
}