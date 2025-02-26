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
            btnCustomer = new Button();
            btnDashboard = new Button();
            btnHistory = new Button();
            btnRoomBook = new Button();
            userControlDashboard1 = new UserControlDashboard();
            userControl11 = new UserControl1();
            userControlHistory1 = new UserControlHistory();
            userControlCustomer1 = new UserControlCustomer();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Silver;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnCustomer);
            panel1.Controls.Add(btnDashboard);
            panel1.Controls.Add(btnHistory);
            panel1.Controls.Add(btnRoomBook);
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
            // btnCustomer
            // 
            btnCustomer.Font = new Font("Segoe UI", 15F);
            btnCustomer.Location = new Point(3, 299);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Size = new Size(249, 62);
            btnCustomer.TabIndex = 2;
            btnCustomer.Text = "Customer";
            btnCustomer.UseVisualStyleBackColor = true;
            btnCustomer.Click += btnCustomer_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.Font = new Font("Segoe UI", 15F);
            btnDashboard.Location = new Point(3, 95);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(249, 62);
            btnDashboard.TabIndex = 3;
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // btnHistory
            // 
            btnHistory.Font = new Font("Segoe UI", 15F);
            btnHistory.Location = new Point(3, 231);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(249, 62);
            btnHistory.TabIndex = 2;
            btnHistory.Text = "Booking History";
            btnHistory.UseVisualStyleBackColor = true;
            btnHistory.Click += btnHistory_Click;
            // 
            // btnRoomBook
            // 
            btnRoomBook.Font = new Font("Segoe UI", 15F);
            btnRoomBook.Location = new Point(3, 163);
            btnRoomBook.Name = "btnRoomBook";
            btnRoomBook.Size = new Size(249, 62);
            btnRoomBook.TabIndex = 1;
            btnRoomBook.Text = "Room Booking";
            btnRoomBook.UseVisualStyleBackColor = true;
            btnRoomBook.Click += btnRoomBook_Click;
            // 
            // userControlDashboard1
            // 
            userControlDashboard1.Location = new Point(254, 0);
            userControlDashboard1.Name = "userControlDashboard1";
            userControlDashboard1.Size = new Size(1197, 682);
            userControlDashboard1.TabIndex = 1;
            // 
            // userControl11
            // 
            userControl11.Location = new Point(254, 0);
            userControl11.Name = "userControl11";
            userControl11.Size = new Size(1197, 682);
            userControl11.TabIndex = 2;
            // 
            // userControlHistory1
            // 
            userControlHistory1.Location = new Point(254, 0);
            userControlHistory1.Name = "userControlHistory1";
            userControlHistory1.Size = new Size(1197, 682);
            userControlHistory1.TabIndex = 3;
            // 
            // userControlCustomer1
            // 
            userControlCustomer1.Location = new Point(254, 0);
            userControlCustomer1.Name = "userControlCustomer1";
            userControlCustomer1.Size = new Size(1197, 682);
            userControlCustomer1.TabIndex = 4;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1452, 682);
            Controls.Add(userControlCustomer1);
            Controls.Add(userControlHistory1);
            Controls.Add(userControl11);
            Controls.Add(userControlDashboard1);
            Controls.Add(panel1);
            Name = "HomeForm";
            Text = "HomeForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnRoomBook;
        private Label label1;
        private Button btnCustomer;
        private Button btnDashboard;
        private Button btnHistory;
        private UserControlDashboard userControlDashboard1;
        private UserControl1 userControl11;
        private UserControlHistory userControlHistory1;
        private UserControlCustomer userControlCustomer1;
    }
}