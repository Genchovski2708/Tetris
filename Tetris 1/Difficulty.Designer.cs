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
            this.rbEasy = new System.Windows.Forms.RadioButton();
            this.gbChooseDifficulty = new System.Windows.Forms.GroupBox();
            this.rbHard = new System.Windows.Forms.RadioButton();
            this.rbIntermediate = new System.Windows.Forms.RadioButton();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.gbChooseDifficulty.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbEasy
            // 
            this.rbEasy.AutoSize = true;
            this.rbEasy.Checked = true;
            this.rbEasy.Location = new System.Drawing.Point(6, 26);
            this.rbEasy.Name = "rbEasy";
            this.rbEasy.Size = new System.Drawing.Size(59, 24);
            this.rbEasy.TabIndex = 0;
            this.rbEasy.TabStop = true;
            this.rbEasy.Text = "Easy";
            this.rbEasy.UseVisualStyleBackColor = true;
            // 
            // gbChooseDifficulty
            // 
            this.gbChooseDifficulty.Controls.Add(this.rbHard);
            this.gbChooseDifficulty.Controls.Add(this.rbIntermediate);
            this.gbChooseDifficulty.Controls.Add(this.rbEasy);
            this.gbChooseDifficulty.Location = new System.Drawing.Point(36, 46);
            this.gbChooseDifficulty.Name = "gbChooseDifficulty";
            this.gbChooseDifficulty.Size = new System.Drawing.Size(170, 125);
            this.gbChooseDifficulty.TabIndex = 1;
            this.gbChooseDifficulty.TabStop = false;
            this.gbChooseDifficulty.Text = "Choose Difficulty";
            // 
            // rbHard
            // 
            this.rbHard.AutoSize = true;
            this.rbHard.Location = new System.Drawing.Point(6, 86);
            this.rbHard.Name = "rbHard";
            this.rbHard.Size = new System.Drawing.Size(63, 24);
            this.rbHard.TabIndex = 2;
            this.rbHard.Text = "Hard";
            this.rbHard.UseVisualStyleBackColor = true;
            // 
            // rbIntermediate
            // 
            this.rbIntermediate.AutoSize = true;
            this.rbIntermediate.Location = new System.Drawing.Point(6, 56);
            this.rbIntermediate.Name = "rbIntermediate";
            this.rbIntermediate.Size = new System.Drawing.Size(115, 24);
            this.rbIntermediate.TabIndex = 1;
            this.rbIntermediate.Text = "Intermediate";
            this.rbIntermediate.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(63, 177);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(94, 29);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // Difficulty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 221);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.gbChooseDifficulty);
            this.Name = "Difficulty";
            this.Text = "Difficulty";
            this.gbChooseDifficulty.ResumeLayout(false);
            this.gbChooseDifficulty.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RadioButton rbEasy;
        private GroupBox gbChooseDifficulty;
        private RadioButton rbHard;
        private RadioButton rbIntermediate;
        private Button btnConfirm;
    }
}