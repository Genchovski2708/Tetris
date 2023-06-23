namespace Tetris_1
{
    partial class Difficulty
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rbEasy = new RadioButton();
            gbChooseDifficulty = new GroupBox();
            rbHard = new RadioButton();
            rbIntermediate = new RadioButton();
            btnConfirm = new Button();
            groupBox1 = new GroupBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            gbChooseDifficulty.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // rbEasy
            // 
            rbEasy.AutoSize = true;
            rbEasy.Checked = true;
            rbEasy.Location = new Point(6, 26);
            rbEasy.Name = "rbEasy";
            rbEasy.Size = new Size(59, 24);
            rbEasy.TabIndex = 0;
            rbEasy.TabStop = true;
            rbEasy.Text = "Easy";
            rbEasy.UseVisualStyleBackColor = true;
            // 
            // gbChooseDifficulty
            // 
            gbChooseDifficulty.Controls.Add(rbHard);
            gbChooseDifficulty.Controls.Add(rbIntermediate);
            gbChooseDifficulty.Controls.Add(rbEasy);
            gbChooseDifficulty.Location = new Point(65, 40);
            gbChooseDifficulty.Name = "gbChooseDifficulty";
            gbChooseDifficulty.Size = new Size(170, 125);
            gbChooseDifficulty.TabIndex = 1;
            gbChooseDifficulty.TabStop = false;
            gbChooseDifficulty.Text = "Choose Difficulty";
            // 
            // rbHard
            // 
            rbHard.AutoSize = true;
            rbHard.Location = new Point(6, 86);
            rbHard.Name = "rbHard";
            rbHard.Size = new Size(63, 24);
            rbHard.TabIndex = 2;
            rbHard.Text = "Hard";
            rbHard.UseVisualStyleBackColor = true;
            // 
            // rbIntermediate
            // 
            rbIntermediate.AutoSize = true;
            rbIntermediate.Location = new Point(6, 56);
            rbIntermediate.Name = "rbIntermediate";
            rbIntermediate.Size = new Size(115, 24);
            rbIntermediate.TabIndex = 1;
            rbIntermediate.Text = "Intermediate";
            rbIntermediate.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(103, 180);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(94, 29);
            btnConfirm.TabIndex = 2;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Location = new Point(65, 40);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(170, 125);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Choose Difficulty";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(6, 86);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(63, 24);
            radioButton1.TabIndex = 2;
            radioButton1.Text = "Hard";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(6, 56);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(115, 24);
            radioButton2.TabIndex = 1;
            radioButton2.Text = "Intermediate";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Checked = true;
            radioButton3.Location = new Point(6, 26);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(59, 24);
            radioButton3.TabIndex = 0;
            radioButton3.TabStop = true;
            radioButton3.Text = "Easy";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // Difficulty
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(305, 221);
            Controls.Add(groupBox1);
            Controls.Add(btnConfirm);
            Controls.Add(gbChooseDifficulty);
            Name = "Difficulty";
            Text = "Difficulty";
            Load += Difficulty_Load;
            gbChooseDifficulty.ResumeLayout(false);
            gbChooseDifficulty.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RadioButton rbEasy;
        private GroupBox gbChooseDifficulty;
        private RadioButton rbHard;
        private RadioButton rbIntermediate;
        private Button btnConfirm;
        private GroupBox groupBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
    }
}