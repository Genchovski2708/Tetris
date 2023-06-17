using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_1
{
    public partial class HelpStart : Form
    {
        int k;
        public HelpStart(int m)
        {
            InitializeComponent();
            BackColor = Color.Black;
            button1.BackColor = Color.Blue;
            k = m;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void HelpStart_Load(object sender, EventArgs e)
        {
            if (k == 0)
            {
                label1.Text = "WELCOME!\r\nThis is TETRIS game!\r\nYou can choose one game mode.\r\nFor help click the 'HELP' button in game!\r\nGood Luck!\r\n\r\n";
            }
            else if (k == 1)
            {
                label1.Text = "Player Controls:\r\nUse left arrow to move left.\r\nUse right arrow to move right.\r\n" +
                    "Use up arrow to rotate for 90 degrees.\r\nUse down arrow to move down faster.";
            }
            else if (k==2)
            {
                this.Height = 400;
                button1.Location= new Point(185, 290);
                label1.Text = "Player 1 Controls:\r\nUse left arrow to move left.\r\nUse right arrow to move right.\r\n" +
                    "Use up arrow to rotate for 90 degrees.\r\nUse down arrow to move down faster.\r\n"+ "Player 2 Controls:\r\nUse 'a' to move left.\r\nUse 'd' to move right.\r\n" +
                    "Use 'w' to rotate for 90 degrees.\r\nUse 's' to move down faster.";
            }
            else
            {
                button1.Location = new Point(185, 220);
                label1.Text = "Player Controls:\r\nUse left arrow to move left.\r\nUse right arrow to move right.\r\n" +
                    "Use up arrow to rotate for 90 degrees.\r\nUse down arrow to move down faster.\r\nNow you don't have preview of next shape!"+
                    "\r\nCareful! Time passes faster!";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
