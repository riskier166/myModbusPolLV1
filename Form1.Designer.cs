namespace myModbusPolLV1
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button4 = new Button();
            button5 = new Button();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            richTextBox1 = new RichTextBox();
            label7 = new Label();
            label8 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(67, 18);
            button1.Name = "button1";
            button1.Size = new Size(8, 8);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(26, 269);
            button2.Name = "button2";
            button2.Size = new Size(94, 60);
            button2.TabIndex = 1;
            button2.Text = "Connect Flow sensor";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(310, 269);
            button3.Name = "button3";
            button3.Size = new Size(131, 60);
            button3.TabIndex = 2;
            button3.Text = "Connect Pressure Sensor";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(-1, 351);
            label1.Name = "label1";
            label1.Size = new Size(212, 20);
            label1.TabIndex = 4;
            label1.Text = "Waiting for Instant Flow data....";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(279, 351);
            label2.Name = "label2";
            label2.Size = new Size(196, 20);
            label2.TabIndex = 5;
            label2.Text = "Waiting for preassure data....";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(584, 351);
            label3.Name = "label3";
            label3.Size = new Size(173, 20);
            label3.TabIndex = 6;
            label3.Text = "Waiting for Power data....";
            // 
            // button4
            // 
            button4.Location = new Point(608, 269);
            button4.Name = "button4";
            button4.Size = new Size(125, 60);
            button4.TabIndex = 7;
            button4.Text = "Connect Power Sensor";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(878, 269);
            button5.Name = "button5";
            button5.Size = new Size(94, 60);
            button5.TabIndex = 8;
            button5.Text = "Connect to Pump";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(161, 115);
            label4.Name = "label4";
            label4.Size = new Size(167, 20);
            label4.TabIndex = 9;
            label4.Text = "Communication Status...";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(161, 162);
            label5.Name = "label5";
            label5.Size = new Size(103, 20);
            label5.TabIndex = 10;
            label5.Text = "Data Status......";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(161, 213);
            label6.Name = "label6";
            label6.Size = new Size(64, 20);
            label6.TabIndex = 11;
            label6.Text = "Alarm.....";
            // 
            // panel1
            // 
            panel1.Location = new Point(802, 105);
            panel1.Name = "panel1";
            panel1.Size = new Size(37, 30);
            panel1.TabIndex = 12;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Location = new Point(802, 152);
            panel2.Name = "panel2";
            panel2.Size = new Size(37, 30);
            panel2.TabIndex = 13;
            panel2.Paint += panel2_Paint;
            // 
            // panel3
            // 
            panel3.Location = new Point(802, 203);
            panel3.Name = "panel3";
            panel3.Size = new Size(37, 30);
            panel3.TabIndex = 13;
            panel3.Paint += panel3_Paint;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 18);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(991, 74);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(-1, 396);
            label7.Name = "label7";
            label7.Size = new Size(188, 20);
            label7.TabIndex = 14;
            label7.Text = "Waiting for totalizer data....";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(584, 396);
            label8.Name = "label8";
            label8.Size = new Size(178, 20);
            label8.TabIndex = 15;
            label8.Text = "Waiting for Energy data....";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(1029, 479);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Panel Control ";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button4;
        private Button button5;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private RichTextBox richTextBox1;
        private Label label7;
        private Label label8;
    }
}
