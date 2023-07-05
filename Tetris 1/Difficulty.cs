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
    public partial class Difficulty : Form
    {
        public bool Easy { get; set; }
        public bool Intermediate { get; set; }
        public bool Hard { get; set; }
        public Difficulty(int langInt)
        {
            InitializeComponent();
            changeLanguage(langInt);
            SetBooleans();
            BackColor = Color.Orange;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            btnConfirm.BackColor = Color.Blue;
            btnConfirm.ForeColor = Color.Orange;
        }
        private void SetBooleans()
        {
            Easy = radioButton3.Checked;
            Intermediate = radioButton2.Checked;
            Hard = radioButton1.Checked;
        }
        private void changeLanguage(int langInt)
        {
            if (langInt == 0)
            {
                groupBox1.Text = "Choose Difficulty";
                radioButton3.Text = "Easy";
                radioButton2.Text = "Intermediate";
                radioButton1.Text = "Hard";
                btnConfirm.Text = "Confirm";
            }
            else if (langInt == 1)
            {
                this.Text = "Тежина";
                groupBox1.Text = "Избери Тежина";
                radioButton3.Text = "Лесно";
                radioButton2.Text = "Средно";
                radioButton1.Text = "Тешко";
                btnConfirm.Text = "Потврди";
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SetBooleans();
            DialogResult = DialogResult.OK;
        }

        private void Difficulty_Load(object sender, EventArgs e)
        {

        }
    }
}
