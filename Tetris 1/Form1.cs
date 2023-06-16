using Microsoft.VisualBasic.ApplicationServices;
using System.Media;
using System.Windows.Forms;

namespace Tetris_1
{
    public partial class Form1 : Form
    {
        public Playground Playground { get; set; }
        public Playground Playground2 { get; set; }
        public bool TwoPlayers { get; set; }
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
            if (TwoPlayers)
            {
                if (Playground2 != null && Playground2.GameIsStarted)
                {
                    Playground2.DrawDots(e.Graphics);
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Playground.AddShape();
            Playground.Tick();
            if (TwoPlayers)
            {
                Playground2.AddShape();
                Playground2.Tick();
            }
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
            button2.Enabled = true;
            button2.Visible = true;
            //  labelRows.Visible = false;
        }
        private void UpdateGameBackground()
        {
            BackgroundImage = Image.FromFile("Images\\pozadina.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            button1.Visible = false;
            button1.Enabled = false;
            button2.Visible = false;
            button2.Enabled = false;
            //  labelRows.Visible = true;
        }
        private void UpdateLabels()
        {
            //labelRows.Text = String.Format("Rows: {0}", Playground.ClearedRows);
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
            button1.Visible = false;
            button1.Enabled = false;
            button2.Visible = false;
            button2.Enabled = false;
            // labelRows.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SinglePlayerStart();
            TwoPlayers = false;
            Invalidate();
        }
        public void SinglePlayerStart()
        {
            if (Playground == null || !Playground.GameIsStarted)
            {
                Playground = new Playground(new Point(100, 250), new Point(500, 750));
                RemoveBackground();
                UpdateGameBackground();
            }
            timer1.Start();
            Playground.GameIsStarted = true;
        }
        public void TwoPlayersStart()
        {
            if ((Playground == null || !Playground.GameIsStarted) && (Playground2 == null || !Playground2.GameIsStarted))
            {
                Playground = new Playground(new Point(50, 250), new Point(450, 750));
                Playground2 = new Playground(new Point(550, 250), new Point(950, 750));
                Playground2.SecondGround = true;
                RemoveBackground();
                UpdateGameBackground();
            }
            timer1.Start();
            Playground.GameIsStarted = true;
            Playground2.GameIsStarted = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            TwoPlayersStart();
            TwoPlayers = true;
            Invalidate();
        }
    }
}