using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingSystem
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
            userControlDashboard1.BringToFront();
        }

       

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            userControlCustomer1.BringToFront();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            userControlDashboard1.BringToFront();
        }

        private void btnRoomBook_Click(object sender, EventArgs e)
        {
            userControl11.BringToFront();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            userControlHistory1.BringToFront();
        }
    }
}
