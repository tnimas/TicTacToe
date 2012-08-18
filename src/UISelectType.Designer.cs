namespace TicTacToe
{
    partial class UISelectType
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
            this.InfLabel = new System.Windows.Forms.Label();
            this.DaggerGameButton = new System.Windows.Forms.Button();
            this.ZeroGameButton = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InfLabel
            // 
            this.InfLabel.AutoSize = true;
            this.InfLabel.Location = new System.Drawing.Point(12, 9);
            this.InfLabel.Name = "InfLabel";
            this.InfLabel.Size = new System.Drawing.Size(246, 17);
            this.InfLabel.TabIndex = 0;
            this.InfLabel.Text = "Предложение игры бла бла бла бла";
            // 
            // DaggerGameButton
            // 
            this.DaggerGameButton.Location = new System.Drawing.Point(15, 63);
            this.DaggerGameButton.Name = "DaggerGameButton";
            this.DaggerGameButton.Size = new System.Drawing.Size(126, 46);
            this.DaggerGameButton.TabIndex = 1;
            this.DaggerGameButton.Text = "играть крестиками";
            this.DaggerGameButton.UseVisualStyleBackColor = true;
            this.DaggerGameButton.Click += new System.EventHandler(this.DaggerGameButtonClick);
            // 
            // ZeroGameButton
            // 
            this.ZeroGameButton.Location = new System.Drawing.Point(147, 62);
            this.ZeroGameButton.Name = "ZeroGameButton";
            this.ZeroGameButton.Size = new System.Drawing.Size(123, 46);
            this.ZeroGameButton.TabIndex = 2;
            this.ZeroGameButton.Text = "играть ноликами";
            this.ZeroGameButton.UseVisualStyleBackColor = true;
            this.ZeroGameButton.Click += new System.EventHandler(this.ZeroGameButtonClick);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Location = new System.Drawing.Point(276, 62);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(112, 46);
            this.Cancel_Button.TabIndex = 3;
            this.Cancel_Button.Text = "отказаться";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // UISelectType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 121);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.ZeroGameButton);
            this.Controls.Add(this.DaggerGameButton);
            this.Controls.Add(this.InfLabel);
            this.Name = "UISelectType";
            this.Text = "Новая игра";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label InfLabel;
        private System.Windows.Forms.Button DaggerGameButton;
        private System.Windows.Forms.Button ZeroGameButton;
        private System.Windows.Forms.Button Cancel_Button;
    }
}