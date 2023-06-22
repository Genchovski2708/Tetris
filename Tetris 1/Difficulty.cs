﻿using System;
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

        }
        private void SetBooleans()
        {
            Easy = rbEasy.Checked;
            Intermediate = rbIntermediate.Checked;
            Hard = rbHard.Checked;
        }
        private void changeLanguage(int langInt)
        {
            if(langInt == 0)
            {
                gbChooseDifficulty.Text = "Choose Difficulty";
                rbEasy.Text = "Easy";
                rbIntermediate.Text = "Intermediate";
                rbHard.Text = "Hard";
            }
            else if( langInt == 1)
            {
                gbChooseDifficulty.Text = "Избери Тежина";
                rbEasy.Text = "Лесно";
                rbIntermediate.Text = "Средно";
                rbHard.Text = "Тешко";
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SetBooleans();
            DialogResult = DialogResult.OK;
        }
    }
}
