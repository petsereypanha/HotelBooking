using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingSystem
{
    public partial class Form1 : Form
    {
        private HotelManager _hotelManager;
        private int _selectedRoomId = -1;
        private decimal _basePrice = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
            _hotelManager = new HotelManager();
        }

        private void InitializeDatabase()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DatabaseManager.Instance.GetConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database initialization error: {ex.Message}\n\nPlease ensure your Docker SQL Server container is running.",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DesignForm();
            InitializeControls();
        }

        private void DesignForm()
        {
            // Set form properties
            this.Text = "Hotel Booking System";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(800, 500);
            // Initialize the controls
            listView1 = new ListView();
            button1 = new Button { Text = "Confirm Booking" };
            button2 = new Button { Text = "Reset" };
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            checkBox1 = new CheckBox { Text = "Breakfast" };
            checkBox2 = new CheckBox { Text = "Bar Access" };
            checkBox3 = new CheckBox { Text = "Gym and Spa" };
            label7 = new Label { Text = "$0.00" };
            label9 = new Label();
            pictureBox1 = new PictureBox();

            // Create main layout
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            this.Controls.Add(mainLayout);

            // Left panel - Available rooms
            GroupBox roomsGroup = new GroupBox
            {
                Text = "Available Rooms",
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            mainLayout.Controls.Add(roomsGroup, 0, 0);

            // Search panel - Top of left panel
            Panel searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120
            };
            roomsGroup.Controls.Add(searchPanel);

            // Add search controls (date pickers, comboboxes, labels)
            // [Detailed control placement code would go here]

            // Room list
            listView1.Dock = DockStyle.Fill;
            roomsGroup.Controls.Add(listView1);

            // Right panel - Booking details
            GroupBox bookingGroup = new GroupBox
            {
                Text = "Booking Details",
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            mainLayout.Controls.Add(bookingGroup, 1, 0);

            // Guest info section
            Panel guestPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100
            };
            bookingGroup.Controls.Add(guestPanel);
            // [Guest control placement code would go here]

            // Room details section
            Panel roomDetailsPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                Top = guestPanel.Bottom
            };
            bookingGroup.Controls.Add(roomDetailsPanel);
            // [Room details control placement code would go here]

            // Amenities section
            GroupBox amenitiesGroup = new GroupBox
            {
                Text = "Additional Services",
                Dock = DockStyle.Top,
                Height = 120,
                Top = roomDetailsPanel.Bottom
            };
            bookingGroup.Controls.Add(amenitiesGroup);
            // [Amenities control placement code would go here]

            // Price section
            Panel pricePanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                Top = amenitiesGroup.Bottom
            };
            bookingGroup.Controls.Add(pricePanel);
            // [Price control placement code would go here]

            // Button panel at bottom
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };
            bookingGroup.Controls.Add(buttonPanel);

            // Apply consistent styling to controls
            ApplyControlStyling();
        }

        private void ApplyControlStyling()
        {
            // Apply consistent fonts, colors, and sizes to controls
            foreach (Control c in this.Controls)
            {
                StyleControlRecursive(c);
            }

            // Style specific controls
            button1.BackColor = Color.FromArgb(0, 120, 212);
            button1.ForeColor = Color.White;
            button1.Font = new Font(button1.Font, FontStyle.Bold);
        }

        private void StyleControlRecursive(Control control)
        {
            // Apply consistent styling to all controls
            control.Font = new Font("Segoe UI", 9F);

            if (control is TextBox txt)
            {
                txt.BorderStyle = BorderStyle.FixedSingle;
            }

            // Process child controls
            foreach (Control child in control.Controls)
            {
                StyleControlRecursive(child);
            }
        }

        private void InitializeControls()
        {
            // Configure the ListView
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("Hotel", 150);
            listView1.Columns.Add("Room", 80);
            listView1.Columns.Add("Type", 100);
            listView1.Columns.Add("Beds", 60);
            listView1.Columns.Add("Price", 80);

            // Set default dates
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today.AddDays(1);

            // Set up room type combobox
            comboBox1.Items.AddRange(new object[] { "Standard", "Deluxe", "Suite", "Executive" });
            comboBox1.SelectedIndex = 0;

            // Set up bed count combobox
            comboBox2.Items.AddRange(new object[] { "1 Bed", "2 Beds", "3+ Beds" });
            comboBox2.SelectedIndex = 0;

            // Load initial data
            LoadAvailableRooms();
        }

        private DataTable ConvertToDataTable(List<Room> rooms)
        {
            DataTable table = new DataTable();
            table.Columns.Add("HotelName", typeof(string));
            table.Columns.Add("RoomNumber", typeof(string));
            table.Columns.Add("TypeName", typeof(string));
            table.Columns.Add("BedCount", typeof(int));
            table.Columns.Add("BasePrice", typeof(decimal));
            table.Columns.Add("RoomId", typeof(int));

            foreach (var room in rooms)
            {
                table.Rows.Add(room.Hotel.HotelName, room.RoomNumber, room.RoomType.TypeName, room.BedCount, room.RoomType.BasePrice, room.RoomId);
            }

            return table;
        }

        private void LoadAvailableRooms()
        {
            try
            {
                if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
                    return;

                string roomType = comboBox1.SelectedItem.ToString();
                int bedCount = int.Parse(comboBox2.SelectedItem.ToString().Split(' ')[0]);
                DateTime checkIn = dateTimePicker1.Value;
                DateTime checkOut = dateTimePicker2.Value;

                // Show loading indicator
                Cursor = Cursors.WaitCursor;

                List<Room> rooms = _hotelManager.GetAvailableRooms(roomType, bedCount, checkIn, checkOut);
                DataTable roomsTable = ConvertToDataTable(rooms);

                listView1.Items.Clear();

                foreach (DataRow row in roomsTable.Rows)
                {
                    ListViewItem item = new ListViewItem(row["HotelName"].ToString());
                    item.SubItems.Add(row["RoomNumber"].ToString());
                    item.SubItems.Add(row["TypeName"].ToString());
                    item.SubItems.Add(row["BedCount"].ToString());
                    item.SubItems.Add("$" + Convert.ToDecimal(row["BasePrice"]).ToString("F2"));
                    item.Tag = row["RoomId"];

                    listView1.Items.Add(item);
                }

                // Update UI with results count
                label9.Text = $"Found {roomsTable.Rows.Count} rooms matching your criteria";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading rooms: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Reset cursor
                Cursor = Cursors.Default;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Confirm Booking button
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter guest name", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (_selectedRoomId == -1)
            {
                MessageBox.Show("Please select a room from the list", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                // Calculate total price
                decimal totalPrice = _basePrice;
                if (checkBox1.Checked) totalPrice += 15; // Breakfast
                if (checkBox2.Checked) totalPrice += 25; // Bar Access
                if (checkBox3.Checked) totalPrice += 20; // Gym and Spa

                // Add guest
                int guestId = _hotelManager.AddGuest(textBox1.Text, textBox2.Text);

                // Add booking
                int bookingId = _hotelManager.AddBooking(
                    guestId,
                    _selectedRoomId,
                    dateTimePicker1.Value,
                    dateTimePicker2.Value,
                    totalPrice,
                    checkBox1.Checked,
                    checkBox3.Checked,
                    checkBox2.Checked
                );

                MessageBox.Show($"Booking confirmed!\nBooking ID: {bookingId}\nTotal Price: ${totalPrice}",
                                "Booking Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error making booking: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            _selectedRoomId = -1;
            _basePrice = 0;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            label7.Text = "$0.00";
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today.AddDays(1);
            LoadAvailableRooms();
            pictureBox1.Image = null;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Get the selected item
                ListViewItem selected = listView1.SelectedItems[0];
                
                // Update the selected room ID
                _selectedRoomId = Convert.ToInt32(selected.Tag);
                
                // Get the price from the "Price" column (index 4)
                string priceText = selected.SubItems[4].Text.Trim('$');
                _basePrice = decimal.Parse(priceText);
                
                // Update room info display
                string roomInfo = $"{selected.SubItems[1].Text} - {selected.SubItems[2].Text} ({selected.SubItems[0].Text})";
                textBox2.Text = roomInfo;
                
                // Update total price
                UpdateTotalPrice();
            }
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotalPrice();
        }
        
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotalPrice();
        }
        
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotalPrice();
        }
        
        private void UpdateTotalPrice()
        {
            decimal totalPrice = _basePrice;
            if (checkBox1.Checked) totalPrice += 15; // Breakfast
            if (checkBox2.Checked) totalPrice += 25; // Bar Access
            if (checkBox3.Checked) totalPrice += 20; // Gym and Spa
            
            label7.Text = $"${totalPrice:F2}";
        }
    }
}