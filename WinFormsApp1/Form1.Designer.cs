namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Enter = new Button();
            openFileDialog1 = new OpenFileDialog();
            tbPathFile = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // Enter
            // 
            Enter.Location = new Point(342, 123);
            Enter.Name = "Enter";
            Enter.Size = new Size(90, 37);
            Enter.TabIndex = 0;
            Enter.Text = "Enter";
            Enter.UseVisualStyleBackColor = true;
            Enter.Click += button1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "xml";
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.FileOk += openFileDialog1_FileOk;
            // 
            // tbPathFile
            // 
            tbPathFile.Location = new Point(24, 64);
            tbPathFile.Name = "tbPathFile";
            tbPathFile.Size = new Size(264, 27);
            tbPathFile.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 27);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 2;
            label1.Text = "BuildData";
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(444, 172);
            Controls.Add(label1);
            Controls.Add(tbPathFile);
            Controls.Add(Enter);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Enter;
        private OpenFileDialog openFileDialog1;
        private TextBox tbPathFile;
        private Label label1;
    }
}