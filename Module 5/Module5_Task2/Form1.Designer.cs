namespace TextEditor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnOpen = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            SuspendLayout();
            // 
            // btnOpen
            // 
            btnOpen.Location = new System.Drawing.Point(12, 12);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new System.Drawing.Size(90, 28);
            btnOpen.TabIndex = 2;
            btnOpen.Text = "Открыть";
            btnOpen.Click += btnOpen_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(108, 12);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(98, 28);
            btnSave.TabIndex = 1;
            btnSave.Text = "Сохранить";
            btnSave.Click += btnSave_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox1.Location = new System.Drawing.Point(12, 48);
            textBox1.MaxLength = int.MaxValue;
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBox1.Size = new System.Drawing.Size(560, 301);
            textBox1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            // 
            // Form1
            // 
            ClientSize = new System.Drawing.Size(584, 361);
            Controls.Add(textBox1);
            Controls.Add(btnSave);
            Controls.Add(btnOpen);
            Name = "Form1";
            Text = "Текстовый редактор";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
