namespace HotelBookingSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomeForm f2 = new HomeForm();
            f2.Show();
        }
    }
}
