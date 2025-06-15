namespace Foundation.Main
{
    partial class MainForm
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
            this.TestFunctionButton = new System.Windows.Forms.Button();
            this.StartHeartbeatButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestFunctionButton
            // 
            this.TestFunctionButton.Location = new System.Drawing.Point(12, 12);
            this.TestFunctionButton.Name = "TestFunctionButton";
            this.TestFunctionButton.Size = new System.Drawing.Size(95, 23);
            this.TestFunctionButton.TabIndex = 0;
            this.TestFunctionButton.Text = "Test Function";
            this.TestFunctionButton.UseVisualStyleBackColor = true;
            this.TestFunctionButton.Click += new System.EventHandler(this.TestFunctionButton_Click);
            // 
            // StartHeartbeatButton
            // 
            this.StartHeartbeatButton.Location = new System.Drawing.Point(12, 54);
            this.StartHeartbeatButton.Name = "StartHeartbeatButton";
            this.StartHeartbeatButton.Size = new System.Drawing.Size(95, 23);
            this.StartHeartbeatButton.TabIndex = 1;
            this.StartHeartbeatButton.Text = "Start heartbeat";
            this.StartHeartbeatButton.UseVisualStyleBackColor = true;
            this.StartHeartbeatButton.Click += new System.EventHandler(this.StartHeartbeatButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StartHeartbeatButton);
            this.Controls.Add(this.TestFunctionButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestFunctionButton;
        private System.Windows.Forms.Button StartHeartbeatButton;
    }
}

