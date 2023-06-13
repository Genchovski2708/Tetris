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
            Playground = new Playground(new Point(100,100),new Point(500,400));
            timer1.Interval = 400;
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Playground.DrawDots(e.Graphics);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start();
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tick == 0)
            {
                Playground.AddShape();
                tick++;
            }
            else
            {
                tick = (tick + 1) % 4;
            }
            Playground.Tick();
            Invalidate();
        }
    }
}