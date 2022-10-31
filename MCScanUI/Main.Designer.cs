namespace MCScanUI
{
    partial class Main
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
            this.reqlabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.scanFileTB = new System.Windows.Forms.TextBox();
            this.scanFileB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // reqlabel
            // 
            this.reqlabel.AutoSize = true;
            this.reqlabel.Location = new System.Drawing.Point(12, 35);
            this.reqlabel.Name = "reqlabel";
            this.reqlabel.Size = new System.Drawing.Size(44, 13);
            this.reqlabel.TabIndex = 0;
            this.reqlabel.Text = "reqlabel";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // scanFileTB
            // 
            this.scanFileTB.Location = new System.Drawing.Point(12, 12);
            this.scanFileTB.Name = "scanFileTB";
            this.scanFileTB.Size = new System.Drawing.Size(695, 20);
            this.scanFileTB.TabIndex = 1;
            // 
            // scanFileB
            // 
            this.scanFileB.Location = new System.Drawing.Point(713, 12);
            this.scanFileB.Name = "scanFileB";
            this.scanFileB.Size = new System.Drawing.Size(75, 20);
            this.scanFileB.TabIndex = 2;
            this.scanFileB.Text = "scanFileB";
            this.scanFileB.UseVisualStyleBackColor = true;
            this.scanFileB.Click += new System.EventHandler(this.scanFileB_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scanFileB);
            this.Controls.Add(this.scanFileTB);
            this.Controls.Add(this.reqlabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label reqlabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox scanFileTB;
        private System.Windows.Forms.Button scanFileB;
    }
}