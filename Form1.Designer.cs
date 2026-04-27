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
            button2 = new Button();
            button3 = new Button();
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
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            panel9 = new Panel();
            panel10 = new Panel();
            panel11 = new Panel();
            panel12 = new Panel();
            label18 = new Label();
            label1 = new Label();
            panel13 = new Panel();
            panel14 = new Panel();
            label20 = new Label();
            label21 = new Label();
            label22 = new Label();
            button1 = new Button();
            label19 = new Label();
            panel15 = new Panel();
            label23 = new Label();
            panel16 = new Panel();
            label24 = new Label();
            panel17 = new Panel();
            label27 = new Label();
            label26 = new Label();
            label25 = new Label();
            panel13.SuspendLayout();
            panel14.SuspendLayout();
            panel15.SuspendLayout();
            panel16.SuspendLayout();
            panel17.SuspendLayout();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(105, 248);
            button2.Name = "button2";
            button2.Size = new Size(94, 60);
            button2.TabIndex = 1;
            button2.Text = "Connect Flow sensor";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.Info;
            button3.Location = new Point(88, 248);
            button3.Name = "button3";
            button3.Size = new Size(131, 60);
            button3.TabIndex = 2;
            button3.Text = "Connect Pressure Sensor";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 358);
            label2.Name = "label2";
            label2.Size = new Size(196, 20);
            label2.TabIndex = 5;
            label2.Text = "Waiting for preassure data....";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 330);
            label3.Name = "label3";
            label3.Size = new Size(173, 20);
            label3.TabIndex = 6;
            label3.Text = "Waiting for Power data....";
            // 
            // button4
            // 
            button4.BackColor = SystemColors.Info;
            button4.Location = new Point(87, 246);
            button4.Name = "button4";
            button4.Size = new Size(125, 60);
            button4.TabIndex = 7;
            button4.Text = "Connect Power Sensor";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.Info;
            button5.Location = new Point(1196, 407);
            button5.Name = "button5";
            button5.Size = new Size(94, 60);
            button5.TabIndex = 8;
            button5.Text = "Connect to Pump";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.HighlightText;
            label4.Location = new Point(28, 253);
            label4.Name = "label4";
            label4.Size = new Size(167, 20);
            label4.TabIndex = 9;
            label4.Text = "Communication Status...";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.HighlightText;
            label5.Location = new Point(28, 300);
            label5.Name = "label5";
            label5.Size = new Size(103, 20);
            label5.TabIndex = 10;
            label5.Text = "Data Status......";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.HighlightText;
            label6.Location = new Point(28, 351);
            label6.Name = "label6";
            label6.Size = new Size(64, 20);
            label6.TabIndex = 11;
            label6.Text = "Alarm.....";
            // 
            // panel1
            // 
            panel1.Location = new Point(249, 82);
            panel1.Name = "panel1";
            panel1.Size = new Size(37, 30);
            panel1.TabIndex = 12;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Location = new Point(249, 129);
            panel2.Name = "panel2";
            panel2.Size = new Size(37, 30);
            panel2.TabIndex = 13;
            panel2.Paint += panel2_Paint;
            // 
            // panel3
            // 
            panel3.Location = new Point(249, 180);
            panel3.Name = "panel3";
            panel3.Size = new Size(37, 30);
            panel3.TabIndex = 13;
            panel3.Paint += panel3_Paint;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(22, 611);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(1449, 74);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 375);
            label7.Name = "label7";
            label7.Size = new Size(188, 20);
            label7.TabIndex = 14;
            label7.Text = "Waiting for totalizer data....";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(15, 375);
            label8.Name = "label8";
            label8.Size = new Size(178, 20);
            label8.TabIndex = 15;
            label8.Text = "Waiting for Energy data....";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.HighlightText;
            label9.Location = new Point(15, 92);
            label9.Name = "label9";
            label9.Size = new Size(167, 20);
            label9.TabIndex = 16;
            label9.Text = "Communication Status...";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = SystemColors.HighlightText;
            label10.Location = new Point(15, 139);
            label10.Name = "label10";
            label10.Size = new Size(97, 20);
            label10.TabIndex = 17;
            label10.Text = "Data Status....";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = SystemColors.HighlightText;
            label11.Location = new Point(15, 190);
            label11.Name = "label11";
            label11.Size = new Size(64, 20);
            label11.TabIndex = 18;
            label11.Text = "Alarm.....";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = SystemColors.HighlightText;
            label12.Location = new Point(16, 92);
            label12.Name = "label12";
            label12.Size = new Size(58, 20);
            label12.TabIndex = 19;
            label12.Text = "label12";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = SystemColors.HighlightText;
            label13.Location = new Point(16, 139);
            label13.Name = "label13";
            label13.Size = new Size(58, 20);
            label13.TabIndex = 20;
            label13.Text = "label13";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = SystemColors.HighlightText;
            label14.Location = new Point(16, 190);
            label14.Name = "label14";
            label14.Size = new Size(58, 20);
            label14.TabIndex = 21;
            label14.Text = "label14";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = SystemColors.HighlightText;
            label15.Location = new Point(1196, 253);
            label15.Name = "label15";
            label15.Size = new Size(58, 20);
            label15.TabIndex = 22;
            label15.Text = "label15";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.BackColor = SystemColors.HighlightText;
            label16.Location = new Point(1196, 300);
            label16.Name = "label16";
            label16.Size = new Size(58, 20);
            label16.TabIndex = 23;
            label16.Text = "label16";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.BackColor = SystemColors.HighlightText;
            label17.Location = new Point(1200, 351);
            label17.Name = "label17";
            label17.Size = new Size(58, 20);
            label17.TabIndex = 24;
            label17.Text = "label17";
            // 
            // panel4
            // 
            panel4.Location = new Point(250, 82);
            panel4.Name = "panel4";
            panel4.Size = new Size(37, 30);
            panel4.TabIndex = 13;
            // 
            // panel5
            // 
            panel5.Location = new Point(250, 129);
            panel5.Name = "panel5";
            panel5.Size = new Size(37, 30);
            panel5.TabIndex = 13;
            // 
            // panel6
            // 
            panel6.Location = new Point(250, 180);
            panel6.Name = "panel6";
            panel6.Size = new Size(37, 30);
            panel6.TabIndex = 13;
            // 
            // panel7
            // 
            panel7.Location = new Point(249, 82);
            panel7.Name = "panel7";
            panel7.Size = new Size(37, 30);
            panel7.TabIndex = 25;
            // 
            // panel8
            // 
            panel8.Location = new Point(249, 129);
            panel8.Name = "panel8";
            panel8.Size = new Size(37, 30);
            panel8.TabIndex = 26;
            // 
            // panel9
            // 
            panel9.Location = new Point(249, 180);
            panel9.Name = "panel9";
            panel9.Size = new Size(37, 30);
            panel9.TabIndex = 26;
            // 
            // panel10
            // 
            panel10.Location = new Point(1284, 243);
            panel10.Name = "panel10";
            panel10.Size = new Size(37, 30);
            panel10.TabIndex = 26;
            // 
            // panel11
            // 
            panel11.Location = new Point(1284, 290);
            panel11.Name = "panel11";
            panel11.Size = new Size(37, 30);
            panel11.TabIndex = 26;
            // 
            // panel12
            // 
            panel12.Location = new Point(1284, 341);
            panel12.Name = "panel12";
            panel12.Size = new Size(37, 30);
            panel12.TabIndex = 26;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.ForeColor = SystemColors.ControlDark;
            label18.Location = new Point(28, 587);
            label18.Name = "label18";
            label18.Size = new Size(186, 20);
            label18.TabIndex = 27;
            label18.Text = "Communication messages ";
            label18.Click += label18_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 330);
            label1.Name = "label1";
            label1.Size = new Size(212, 20);
            label1.TabIndex = 4;
            label1.Text = "Waiting for Instant Flow data....";
            label1.Click += label1_Click;
            // 
            // panel13
            // 
            panel13.BorderStyle = BorderStyle.Fixed3D;
            panel13.Controls.Add(panel14);
            panel13.Controls.Add(label19);
            panel13.Controls.Add(label7);
            panel13.Controls.Add(label1);
            panel13.Controls.Add(button2);
            panel13.Location = new Point(22, 159);
            panel13.Name = "panel13";
            panel13.Size = new Size(309, 418);
            panel13.TabIndex = 28;
            // 
            // panel14
            // 
            panel14.BackColor = SystemColors.HighlightText;
            panel14.BorderStyle = BorderStyle.Fixed3D;
            panel14.Controls.Add(label20);
            panel14.Controls.Add(label21);
            panel14.Controls.Add(label22);
            panel14.Controls.Add(button1);
            panel14.Controls.Add(panel2);
            panel14.Controls.Add(panel1);
            panel14.Controls.Add(panel3);
            panel14.Location = new Point(-2, -2);
            panel14.Name = "panel14";
            panel14.Size = new Size(309, 418);
            panel14.TabIndex = 29;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.BackColor = SystemColors.ButtonHighlight;
            label20.Font = new Font("Segoe UI", 15F);
            label20.ForeColor = SystemColors.HotTrack;
            label20.Location = new Point(81, 23);
            label20.Name = "label20";
            label20.Size = new Size(132, 35);
            label20.TabIndex = 15;
            label20.Text = "Flow Panel";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(11, 375);
            label21.Name = "label21";
            label21.Size = new Size(188, 20);
            label21.TabIndex = 14;
            label21.Text = "Waiting for totalizer data....";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(11, 330);
            label22.Name = "label22";
            label22.Size = new Size(212, 20);
            label22.TabIndex = 4;
            label22.Text = "Waiting for Instant Flow data....";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Info;
            button1.Location = new Point(105, 248);
            button1.Name = "button1";
            button1.Size = new Size(94, 60);
            button1.TabIndex = 1;
            button1.Text = "Connect Flow sensor";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.BackColor = SystemColors.ControlText;
            label19.Font = new Font("Segoe UI", 15F);
            label19.ForeColor = SystemColors.ActiveCaption;
            label19.Location = new Point(81, 23);
            label19.Name = "label19";
            label19.Size = new Size(132, 35);
            label19.TabIndex = 15;
            label19.Text = "Flow Panel";
            label19.Click += label19_Click;
            // 
            // panel15
            // 
            panel15.BackColor = SystemColors.ControlLightLight;
            panel15.BorderStyle = BorderStyle.Fixed3D;
            panel15.Controls.Add(label23);
            panel15.Controls.Add(label2);
            panel15.Controls.Add(button3);
            panel15.Controls.Add(label12);
            panel15.Controls.Add(panel9);
            panel15.Controls.Add(label13);
            panel15.Controls.Add(panel8);
            panel15.Controls.Add(panel7);
            panel15.Controls.Add(label14);
            panel15.Location = new Point(735, 159);
            panel15.Name = "panel15";
            panel15.Size = new Size(309, 418);
            panel15.TabIndex = 30;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.BackColor = SystemColors.ButtonHighlight;
            label23.Font = new Font("Segoe UI", 15F);
            label23.ForeColor = Color.ForestGreen;
            label23.Location = new Point(73, 23);
            label23.Name = "label23";
            label23.Size = new Size(175, 35);
            label23.TabIndex = 16;
            label23.Text = "Pressure Panel";
            // 
            // panel16
            // 
            panel16.BackColor = SystemColors.ControlLightLight;
            panel16.BorderStyle = BorderStyle.Fixed3D;
            panel16.Controls.Add(label24);
            panel16.Controls.Add(label9);
            panel16.Controls.Add(label10);
            panel16.Controls.Add(label11);
            panel16.Controls.Add(button4);
            panel16.Controls.Add(panel5);
            panel16.Controls.Add(panel4);
            panel16.Controls.Add(panel6);
            panel16.Controls.Add(label3);
            panel16.Controls.Add(label8);
            panel16.Location = new Point(375, 159);
            panel16.Name = "panel16";
            panel16.Size = new Size(309, 418);
            panel16.TabIndex = 31;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.BackColor = SystemColors.ButtonHighlight;
            label24.Font = new Font("Segoe UI", 15F);
            label24.ForeColor = Color.Orange;
            label24.Location = new Point(78, 23);
            label24.Name = "label24";
            label24.Size = new Size(149, 35);
            label24.TabIndex = 16;
            label24.Text = "Power Panel";
            // 
            // panel17
            // 
            panel17.BackColor = Color.White;
            panel17.Controls.Add(label27);
            panel17.Controls.Add(label26);
            panel17.Controls.Add(label25);
            panel17.Location = new Point(22, 29);
            panel17.Name = "panel17";
            panel17.Size = new Size(1022, 108);
            panel17.TabIndex = 32;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Segoe UI", 15F);
            label27.Location = new Point(478, 36);
            label27.Name = "label27";
            label27.Size = new Size(216, 35);
            label27.TabIndex = 2;
            label27.Text = "Pump efficiency.....";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(120, 16);
            label26.Name = "label26";
            label26.Size = new Size(72, 20);
            label26.TabIndex = 1;
            label26.Text = "Real time";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI", 25F);
            label25.Location = new Point(13, 36);
            label25.Name = "label25";
            label25.Size = new Size(326, 57);
            label25.TabIndex = 0;
            label25.Text = "Pump Efficiency:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.OldLace;
            ClientSize = new Size(1483, 707);
            Controls.Add(panel17);
            Controls.Add(label18);
            Controls.Add(panel12);
            Controls.Add(panel11);
            Controls.Add(panel10);
            Controls.Add(label17);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(button5);
            Controls.Add(richTextBox1);
            Controls.Add(panel13);
            Controls.Add(panel15);
            Controls.Add(panel16);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Panel Control ";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            panel15.ResumeLayout(false);
            panel15.PerformLayout();
            panel16.ResumeLayout(false);
            panel16.PerformLayout();
            panel17.ResumeLayout(false);
            panel17.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button2;
        private Button button3;
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
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private Panel panel9;
        private Panel panel10;
        private Panel panel11;
        private Panel panel12;
        private Label label18;
        private Label label1;
        private Panel panel13;
        private Label label19;
        private Panel panel14;
        private Label label20;
        private Label label21;
        private Label label22;
        private Button button1;
        private Panel panel15;
        private Label label23;
        private Panel panel16;
        private Label label24;
        private Panel panel17;
        private Label label26;
        private Label label25;
        private Label label27;
    }
}
