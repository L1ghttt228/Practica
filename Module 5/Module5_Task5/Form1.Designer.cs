namespace Calculator
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btnDiv = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btnMul = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btnSub = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btnDot = new System.Windows.Forms.Button();
            this.btnEq = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 440F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn8, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn9, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDiv, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnMul, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn3, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSub, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn0, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnDot, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnEq, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn7
            // 
            this.btn7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn7.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn7.Margin = new System.Windows.Forms.Padding(3);
            this.btn7.Name = "btn7";
            this.btn7.TabIndex = 1;
            this.btn7.TabStop = false;
            this.btn7.Text = "7";
            this.btn7.Click += new System.EventHandler(this.Digit_Click);
            // 
            // btn8
            // 
            this.btn8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn8.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn8.Margin = new System.Windows.Forms.Padding(3);
            this.btn8.Name = "btn8";
            this.btn8.TabIndex = 2;
            this.btn8.TabStop = false;
            this.btn8.Text = "8";
            this.btn8.Click += new System.EventHandler(this.Digit_Click);
            // 
            // btn9
            // 
            this.btn9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn9.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn9.Margin = new System.Windows.Forms.Padding(3);
            this.btn9.Name = "btn9";
            this.btn9.TabIndex = 3;
            this.btn9.TabStop = false;
            this.btn9.Text = "9";
            this.btn9.Click += new System.EventHandler(this.Digit_Click);
            // 
            // btnDiv
            // 
            this.btnDiv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDiv.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnDiv.Margin = new System.Windows.Forms.Padding(3);
            this.btnDiv.Name = "btnDiv";
            this.btnDiv.TabIndex = 4;
            this.btnDiv.TabStop = false;
            this.btnDiv.Text = "/";
            this.btnDiv.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btn4
            // 
            this.btn4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn4.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn4.Margin = new System.Windows.Forms.Padding(3);
            this.btn4.Name = "btn4";
            this.btn4.TabIndex = 5;
            this.btn4.TabStop = false;
            this.btn4.Text = "4";
            this.btn4.Click += new System.EventHandler(this.Digit_Click);
            // 
            // btn5
            // 
            this.btn5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn5.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn5.Margin = new System.Windows.Forms.Padding(3);
            this.btn5.Name = "btn5";
            this.btn5.TabIndex = 6;
            this.btn5.TabStop = false;
            this.btn5.Text = "5";
            this.btn5.Click += new System.EventHandler(this.Digit_Click);
            // 
            // btn6
            // 
            this.btn6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn6.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn6.Margin = new System.Windows.Forms.Padding(3);
            this.btn6.Name = "btn6";
            this.btn6.TabIndex = 7;
            this.btn6.TabStop = false;
            this.btn6.Text = "6";
            this.btn6.Click += new System.EventHandler(this.Digit_Click);
            // 
            // btnMul
            // 
            this.btnMul.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMul.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnMul.Margin = new System.Windows.Forms.Padding(3);
            this.btnMul.Name = "btnMul";
            this.btnMul.TabIndex = 8;
            this.btnMul.TabStop = false;
            this.btnMul.Text = "*";
            this.btnMul.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btn1
            // 
            this.btn1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn1.Margin = new System.Windows.Forms.Padding(3);
            this.btn1.Name = "btn1";
            this.btn1.TabIndex = 9;
            this.btn1.TabStop = false;
            this.btn1.Text = "1";
            this.btn1.Click += new System.EventHandler(this.Digit_Click);
            // 
            // btn2
            // 
            this.btn2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn2.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn2.Margin = new System.Windows.Forms.Padding(3);
            this.btn2.Name = "btn2";
            this.btn2.TabIndex = 10;
            this.btn2.TabStop = false;
            this.btn2.Text = "2";
            this.btn2.Click += new System.EventHandler(this.Digit_Click);
            // 
            // btn3
            // 
            this.btn3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn3.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn3.Margin = new System.Windows.Forms.Padding(3);
            this.btn3.Name = "btn3";
            this.btn3.TabIndex = 11;
            this.btn3.TabStop = false;
            this.btn3.Text = "3";
            this.btn3.Click += new System.EventHandler(this.Digit_Click);
            // 
            // btnSub
            // 
            this.btnSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSub.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnSub.Margin = new System.Windows.Forms.Padding(3);
            this.btnSub.Name = "btnSub";
            this.btnSub.TabIndex = 12;
            this.btnSub.TabStop = false;
            this.btnSub.Text = "-";
            this.btnSub.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btn0
            // 
            this.btn0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn0.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn0.Margin = new System.Windows.Forms.Padding(3);
            this.btn0.Name = "btn0";
            this.btn0.TabIndex = 13;
            this.btn0.TabStop = false;
            this.btn0.Text = "0";
            this.btn0.Click += new System.EventHandler(this.Digit_Click);
            // 
            // btnDot
            // 
            this.btnDot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDot.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnDot.Margin = new System.Windows.Forms.Padding(3);
            this.btnDot.Name = "btnDot";
            this.btnDot.TabIndex = 14;
            this.btnDot.TabStop = false;
            this.btnDot.Text = ".";
            this.btnDot.Click += new System.EventHandler(this.Dot_Click);
            // 
            // btnEq
            // 
            this.btnEq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEq.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnEq.Margin = new System.Windows.Forms.Padding(3);
            this.btnEq.Name = "btnEq";
            this.btnEq.TabIndex = 15;
            this.btnEq.TabStop = false;
            this.btnEq.Text = "=";
            this.btnEq.Click += new System.EventHandler(this.Equals_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.TabIndex = 16;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "+";
            this.btnAdd.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3);
            this.btnClear.Name = "btnClear";
            this.btnClear.TabIndex = 17;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "C";
            this.btnClear.Click += new System.EventHandler(this.Clear_Click);
            this.tableLayoutPanel1.SetColumnSpan(this.btnClear, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 460);
            this.Controls.Add(this.tableLayoutPanel2);
            this.MinimumSize = new System.Drawing.Size(370, 500);
            this.Name = "Form1";
            this.Text = "Калькулятор";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btnDiv;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btnMul;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btnDot;
        private System.Windows.Forms.Button btnEq;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClear;
    }
}
