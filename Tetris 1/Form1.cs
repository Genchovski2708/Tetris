namespace Tetris_1
{
    public partial class Form1 : Form
    {
        public Playground Playground { get; set; }
        int tick = 0;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered= true;
            timer1.Interval = 1000;
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(Playground!= null && Playground.GameIsStarted)
            {
                Playground.DrawDots(e.Graphics);
            }

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Playground == null || !Playground.GameIsStarted)
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
            Invalidate();
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
    }
}