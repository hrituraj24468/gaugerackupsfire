using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iotracks.iotrack;
using abc.iot;
using System.Windows.Controls;

namespace abc
{
    public partial class dashboard : Form
    {
        // Update the connection string with the correct parameters


        public dashboard()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }



        private void dashboard_Load(object sender, EventArgs e)
        {
            // Test the connection status on load
            flowLayoutPanel1.AutoScroll = true;  // AutoScroll enables scrolling automatically when controls exceed the panel size

            // Adjust the height dynamically if necessary
            flowLayoutPanel1.Height = this.ClientSize.Height - 150;

            CreateDynamicUserControls();
        }
        string connectionString = "Server=127.0.0.1;Database=data;User Id=root;Password=;";


        private List<UserControl1> userControlsList = new List<UserControl1>();
        private List<UserControl2> userControlsList2 = new List<UserControl2>();
        private List<UserControl3> userControlsList3 = new List<UserControl3>();
        // List to track UserControl1 instances

        private void CreateDynamicUserControls()
        {
            try
            {
                // Create a Timer that will call this method every 10 seconds to refresh the data
                Timer timer = new Timer();
                timer.Interval = 500; // 10 seconds (adjust as needed)
                timer.Tick += (sender, e) => UpdateControlValues();
                timer.Start();

                // Call the initial method to populate the controls
                InitialPopulateData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up timer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitialPopulateData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM datas ORDER BY serverindex"; // Modify query if needed

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a new instance of UserControl1 for each data row
                            UserControl1 userControl = new UserControl1();
                            UserControl2 userControl2 = new UserControl2();
                            UserControl3 userControl3 = new UserControl3();


                            // Add labels to the panel for each piece of data


                            // Populate UserControl1 with the data values from the database
                            if (int.TryParse(reader["temperature"].ToString(), out int temperature))
                            {
                                userControl.Temperature = temperature;
                            }

                            if (int.TryParse(reader["humidity"].ToString(), out int humidity))
                            {
                                userControl.Humidity = humidity;
                            }

                            if (int.TryParse(reader["AC"].ToString(), out int acstatus))
                            {
                                userControl.AcStatus = acstatus == 1;
                            }
                            if (int.TryParse(reader["fir"].ToString(), out int fires))
                            {
                                userControl.fire = fires;
                            }
                            if (int.TryParse(reader["ups"].ToString(), out int ups))
                            {
                                userControl2.mode = ups;
                            }
                            if (int.TryParse(reader["fir"].ToString(), out int fire))
                            {
                                userControl3.fire = fire;
                            }

                            // Add the new UserControl1 and Panel to the FlowLayoutPanel
                            flowLayoutPanel1.Controls.Add(userControl);
                            flowLayoutPanel1.Controls.Add(userControl2);
                            flowLayoutPanel1.Controls.Add(userControl3);


                            // Add the UserControl1 instance to the tracking list
                            userControlsList.Add(userControl); userControlsList2.Add(userControl2); userControlsList3.Add(userControl3);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateControlValues()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM datas ORDER BY serverindex"; // Modify query if needed

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int index = 0;
                        while (reader.Read())
                        {
                            if (index < userControlsList.Count)
                            {
                                UserControl1 userControl = userControlsList[index];

                                // Update the values of existing UserControl1 without recreating the controls
                                if (int.TryParse(reader["temperature"].ToString(), out int temperature))
                                {
                                    userControl.Temperature = temperature;
                                }

                                if (int.TryParse(reader["humidity"].ToString(), out int humidity))
                                {
                                    userControl.Humidity = humidity;
                                }

                                if (int.TryParse(reader["AC"].ToString(), out int acstatus))
                                {
                                    userControl.AcStatus = acstatus == 1;
                                }
                                if (int.TryParse(reader["fir"].ToString(), out int fires))
                                {
                                    userControl.fire = fires;
                                }

                            }
                            if (index < userControlsList2.Count)
                            {
                                UserControl2 userControl2 = userControlsList2[index];

                                // Update the values of existing UserControl2 without recreating the controls


                                if (int.TryParse(reader["ups"].ToString(), out int ups))
                                {
                                    userControl2.mode = ups;
                                }
                            }
                            if (index < userControlsList3.Count)
                            {
                                UserControl3 userControl3 = userControlsList3[index];

                                // Update the values of existing UserControl2 without recreating the controls


                                if (int.TryParse(reader["fir"].ToString(), out int fire))
                                {
                                    userControl3.fire = fire;
                                }

                            }

                            index++;



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating values: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
      

        private void userControl31_Load(object sender, EventArgs e)
        {

        }
    }
}
