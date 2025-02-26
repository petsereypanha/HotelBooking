namespace HotelBookingSystem
{
    partial class HomeForm
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
            panel1 = new Panel();
            label1 = new Label();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            userControl11 = new UserControl1();
            userControlDashboard1 = new UserControlDashboard();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Silver;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(255, 682);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Vanna-English Kbach Khmer", 11F, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.DarkRed;
            label1.Location = new Point(3, 32);
            label1.Name = "label1";
            label1.Size = new Size(245, 17);
            label1.TabIndex = 4;
            label1.Text = "Hotel Booking System";
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 15F);
            button4.Location = new Point(3, 299);
            button4.Name = "button4";
            button4.Size = new Size(249, 62);
            button4.TabIndex = 2;
            button4.Text = "Customer";
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 15F);
            button3.Location = new Point(3, 95);
            button3.Name = "button3";
            button3.Size = new Size(249, 62);
            button3.TabIndex = 3;
            button3.Text = "Dashboard";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 15F);
            button2.Location = new Point(3, 231);
            button2.Name = "button2";
            button2.Size = new Size(249, 62);
            button2.TabIndex = 2;
            button2.Text = "Booking History";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 15F);
            button1.Location = new Point(3, 163);
            button1.Name = "button1";
            button1.Size = new Size(249, 62);
            button1.TabIndex = 1;
            button1.Text = "Room Booking";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // userControl11
            // 
            userControl11.Location = new Point(254, 3);
            userControl11.Name = "userControl11";
            userControl11.Size = new Size(1197, 682);
            userControl11.TabIndex = 1;
            // 
            // userControlDashboard1
            // 
            userControlDashboard1.Location = new Point(254, 0);
            userControlDashboard1.Name = "userControlDashboard1";
            userControlDashboard1.Size = new Size(1197, 682);
            userControlDashboard1.TabIndex = 2;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1452, 682);
            Controls.Add(userControlDashboard1);
            Controls.Add(userControl11);
            Controls.Add(panel1);
            Name = "HomeForm";
            Text = "HomeForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private Label label1;
        private Button button4;
        private Button button3;
        private Button button2;
        private UserControl1 userControl11;
        private UserControlDashboard userControlDashboard1;
    }
}