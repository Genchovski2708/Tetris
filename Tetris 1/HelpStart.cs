using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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
            this.Icon = new Icon("Images\\TETRIS4.ico");
            label1.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void HelpStart_Load(object sender, EventArgs e)
        {
            switch (k)
            {
                case 0:
                    label1.Location=new Point(this.Width/9, this.Height/6);
                    label1.Text = "WELCOME!\r\nThis is TETRIS game!\r\nYou can choose one game mode.\r\nYou will get instructions at the beginning.\r\nGood Luck!\r\n\r\n";
                    this.Text = "Help"; break;
                case 1:
                    label1.Location = new Point(this.Width / 7, this.Height / 6);
                    label1.Text = "Player Controls:\r\nUse left arrow to move left.\r\nUse right arrow to move right.\r\n" +
                    "Use up arrow to rotate for 90 degrees.\r\nUse down arrow to move down faster."; this.Text = "Help"; break;
                case 2:
                    this.Height = 400;
                    button1.Location = new Point(185, 290);
                    label1.Location = new Point(this.Width / 7, this.Height / 9);
                    label1.Text = "Player 1 Controls:\r\nUse left arrow to move left.\r\nUse right arrow to move right.\r\n" +
                        "Use up arrow to rotate for 90 degrees.\r\nUse down arrow to move down faster.\r\n" + "Player 2 Controls:\r\nUse 'a' to move left.\r\nUse 'd' to move right.\r\n" +
                        "Use 'w' to rotate for 90 degrees.\r\nUse 's' to move down faster."; this.Text = "Help"; break;
                case 3:
                    button1.Location = new Point(185, 220);
                    label1.Location = new Point(this.Width / 10, this.Height / 8);
                    label1.Text = "Player Controls:\r\nUse left arrow to move left.\r\nUse right arrow to move right.\r\n" +
                        "Use up arrow to rotate for 90 degrees.\r\nUse down arrow to move down faster.\r\nNow you don't have preview of next shape!" +
                        "\r\nCareful! Time passes faster!"; this.Text = "Help Start"; break;
                case 4:
                    label1.Location = new Point(this.Width /5 - 10, this.Height / 6);
                    label1.Text = "ДОБРЕДОЈДОВТЕ!\r\nОва е игра тетрис!\r\nМожеш да одбереш еден вид на игра.\r\nИнструкции ќе добиеш на почетокот.\r\nСреќно!\r\n\r\n";
                    this.Text = "Помош"; break;
                case 5:
                    label1.Location = new Point(this.Width / 12, this.Height / 6);
                    label1.Text = "Контроли за играчот:\r\nСо левата стрелка се придвижува налево.\r\nСо десната стрелка се придвижува надесно.\r\n" +
                    "Со горната стрелка се ротира за 90 степени.\r\nСо долната стрелка се придвижвува побрзо надолу."; this.Text = "Помош"; break;
                case 6:
                    this.Height = 400;
                    label1.Location = new Point(this.Width / 12, this.Height / 9);
                    button1.Location = new Point(185, 290);
                    label1.Text = "Контроли за Играч 1:\r\nСо левата стрелка се придвижува налево.\r\nСо десната стрелка се придвижува надесно.\r\n" +
                        "Со горната стрелка се ротира за 90 степени.\r\nСо долната стрелка се придвижвува побрзо надолу.\r\n" + "Контроли за Играч 2:\r\nНа буквата 'a' се придвижува налево.\r\nНа буквата 'd' се придвижува надесно.\r\n" +
                        "На буквата 'w' се ротира за 90 степени.\r\nНа буквата 's' се придвижвува побрзо надолу."; this.Text = "Помош"; break;
                case 7:
                    button1.Location = new Point(185, 220);
                    label1.Location = new Point(this.Width / 12, this.Height / 8);
                    label1.Text = "Контроли за играчот:\r\nСо левата стрелка се придвижува налево.\r\nСо десната стрелка се придвижува надесно.\r\n" +
                        "Со горната стрелка се ротира за 90 степени.\r\nСо долната стрелка се придвижвува побрзо надолу." + "\r\nСега немаш преглед на следната форма!" +
                        "\r\nВнимателно! Времето тече побрзо!"; this.Text = "Помош"; break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
