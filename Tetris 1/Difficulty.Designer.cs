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
            gbChooseDifficulty.SuspendLayout();
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
            gbChooseDifficulty.Location = new Point(36, 46);
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
            btnConfirm.Location = new Point(63, 177);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(94, 29);
            btnConfirm.TabIndex = 2;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // Difficulty
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(252, 221);
            Controls.Add(btnConfirm);
            Controls.Add(gbChooseDifficulty);
            Name = "Difficulty";
            Text = "Difficulty";
            Load += Difficulty_Load;
            gbChooseDifficulty.ResumeLayout(false);
            gbChooseDifficulty.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RadioButton rbEasy;
        private GroupBox gbChooseDifficulty;
        private RadioButton rbHard;
        private RadioButton rbIntermediate;
        private Button btnConfirm;
    }
}