using System;
using System.Drawing;
using System.Windows.Forms;

namespace iotracks.iotrack
{
    public partial class UserControl1 : UserControl
    {
        private PictureBox pictureBox;
        private int _Temperature;
        private int _Humidity;
        private System.Windows.Forms.Timer blinkTimer; // Timer to make the circle blink
        private bool isCircleVisible = true; // State for the circle's visibility

        public UserControl1()
        {
            InitializeComponent();
            LoadImage();
            InitializeBlinkTimer();
        }

        private bool _AcStatus;
        // Backing field for AC status

        public bool AcStatus
        {
            get { return _AcStatus; }
            set
            {
                _AcStatus = value;
                UpdateLabel();
                Invalidate(); // Redraw the UI whenever the AC status changes
            }
        }

        public int Humidity
        {
            get { return _Humidity; }
            set
            {
                _Humidity = value;
                UpdateLabel(); // Update all labels when humidity changes
                Invalidate(); // Redraw the UI whenever the humidity changes
            }
        }

        public int Temperature
        {
            get { return _Temperature; }
            set
            {
                _Temperature = value;
                UpdateLabel(); // Update both labels when the temperature changes
                Invalidate(); // Redraw the UI whenever the temperature changes
            }
        }

        private void UpdateLabel()
        {
            if (acstatusdata != null)
            {
                // Update the text of the label to reflect the current AC status
                acstatusdata.Text = $"AC Status = {_AcStatus}";
                acstatusdata.ForeColor = _AcStatus ? Color.Green : Color.Red;
            }

            if (temperatureData != null)
            {
                // Update the temperature label text and color
                temperatureData.Text = $"T: {_Temperature}°C";
                // Set color based on temperature range
                if (_Temperature < 30)
                {
                    temperatureData.ForeColor = Color.Green;
                }
                else if (_Temperature >= 30 && _Temperature < 70)
                {
                    temperatureData.ForeColor = Color.Yellow;
                }
                else
                {
                    temperatureData.ForeColor = Color.Red;
                }
            }

            if (humidityData != null)
            {
                Color circleColor;
                if (_Humidity < 30)
                {
                    circleColor = Color.Cyan;
                }
                else if (_Humidity >= 30 && _Humidity < 70)
                {
                    circleColor = Color.Yellow;
                }
                else
                {
                    circleColor = Color.Red;
                }
                humidityData.Text = $"H: {_Humidity}%";
                humidityData.ForeColor = circleColor; // You can customize the color as needed
            }
        }

        // Property to get the color based on AC status
        public Color AcStatusColor
        {
            get { return _AcStatus ? Color.Green : Color.Red; }
        }


        private Image ResizeImage(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(img, 0, 0, width, height);
            }
            return bmp;
        }

        private void LoadImage()
        {
            // Initialize the PictureBox
            pictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Dock = DockStyle.Fill
            };

            // Attach the Paint event for drawing the overlay directly on the PictureBox
            pictureBox.Paint += PictureBox_Paint;
            this.Controls.Add(pictureBox);

            try
            {
                // Load and resize the image
                Image originalImage = Image.FromFile("C:\\Users\\hritu\\source\\repos\\iotracks\\iotracks\\iotrack\\26170443.jpg");
                Image resizedImage = ResizeImage(originalImage, 800, 600);
                pictureBox.Image = resizedImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Label acstatusdata;
        private Label temperatureData;
        private Label humidityData;

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Create and add the labels if they are not already created
            if (acstatusdata == null)
            {
                acstatusdata = new Label
                {
                    Text = $"AC Status = {_AcStatus}",
                    Font = new Font("Arial", 6, FontStyle.Bold),
                    ForeColor = Color.Green,
                    BackColor = Color.Transparent,
                    AutoSize = true
                };

                // Add the label as a child of the PictureBox
                pictureBox.Controls.Add(acstatusdata);
            }
            if (temperatureData == null)
            {
                temperatureData = new Label
                {
                    Text = $"T: {_Temperature}°C",
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    ForeColor = Color.Red,
                    BackColor = Color.Black,
                    AutoSize = true
                };
                pictureBox.Controls.Add(temperatureData);
            }
            if (humidityData == null)
            {
                humidityData = new Label
                {
                    Text = $"H: {_Humidity}%",
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    ForeColor = Color.Cyan,
                    BackColor = Color.Black,
                    AutoSize = true
                };
                pictureBox.Controls.Add(humidityData);
            }

            // Position the labels on the PictureBox
            acstatusdata.Location = new Point(pictureBox.Width / 2 - acstatusdata.Width / 2, pictureBox.Height - 245);
            temperatureData.Location = new Point(pictureBox.Width / 2 - temperatureData.Width / 2 - 20, pictureBox.Height - 358);
            humidityData.Location = new Point(pictureBox.Width / 2 - humidityData.Width / 2 + 50, pictureBox.Height - 358);

            // Draw the Temperature Rectangle Gauge
            DrawTemperatureGauge(e.Graphics);
            DrawHumidityGauge(e.Graphics);

            // Draw the Blinking Circle (Ellipse)
            DrawBlinkingCircle(e.Graphics);
            DrawBlinkingCirclefire(e.Graphics);
            DrawBlinkingCirclewld(e.Graphics);
        }

        private void DrawTemperatureGauge(Graphics g)
        {
            int gaugeWidth = 4;
            int gaugeHeight = 15;
            int gaugeX = pictureBox.Width - 177;
            int gaugeY = pictureBox.Height - 357;

            int fillHeight = Math.Min(gaugeHeight, Math.Max(0, (_Temperature * gaugeHeight) / 100));
            Color gaugeColor;
            if (_Temperature < 30)
            {
                gaugeColor = Color.Green;
            }
            else if (_Temperature >= 30 && _Temperature < 70)
            {
                gaugeColor = Color.Yellow;
            }
            else
            {
                gaugeColor = Color.Red;
            }

            using (Brush fillBrush = new SolidBrush(gaugeColor))
            {
                g.FillRectangle(fillBrush, gaugeX, gaugeY + (gaugeHeight - fillHeight), gaugeWidth, fillHeight);
            }
        }

        private void DrawHumidityGauge(Graphics g)
        {
            int gaugeWidth = 4;
            int gaugeHeight = 15;
            int gaugeX = pictureBox.Width - 190;
            int gaugeY = pictureBox.Height - 358;

            int fillHeight = Math.Min(gaugeHeight, Math.Max(0, (_Humidity * gaugeHeight) / 100));

            using (Brush fillBrush = new SolidBrush(Color.Cyan))
            {
                g.FillRectangle(fillBrush, gaugeX, gaugeY + (gaugeHeight - fillHeight), gaugeWidth, fillHeight);
            }
        }

        private void InitializeBlinkTimer()
        {
            blinkTimer = new System.Windows.Forms.Timer();
            blinkTimer.Interval = 1000;
            blinkTimer.Tick += BlinkTimer_Tick;
            blinkTimer.Start();
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            isCircleVisible = !isCircleVisible;
            pictureBox.Invalidate();
        }

        private void DrawBlinkingCircle(Graphics g)
        {
            if (isCircleVisible)
            {
                int circleDiameter = 8;
                int circleX = pictureBox.Width / 2 - circleDiameter / 2 + 5;
                int circleY = pictureBox.Height / 2 - circleDiameter / 2 - 5;

                using (Brush circleBrush = new SolidBrush(AcStatusColor))
                {
                    g.FillEllipse(circleBrush, circleX, circleY, circleDiameter, circleDiameter);
                }
            }
        }

        private void DrawBlinkingCirclefire(Graphics g)
        {
            if (isCircleVisible)
            {
                int circleDiameter = 8;
                int circleX = pictureBox.Width / 2 - circleDiameter / 2 - 12;
                int circleY = pictureBox.Height / 2 - circleDiameter / 2 - 5;


                Color circleColor;
                if (_Temperature < 30)
                {
                    circleColor = Color.Green;
                }
                else if (_Temperature >= 30 && _Temperature < 70)
                {
                    circleColor = Color.Yellow;
                }
                else
                {
                    circleColor = Color.Red;
                }

                using (Brush circleBrush = new SolidBrush(circleColor))
                {
                    g.FillEllipse(circleBrush, circleX, circleY, circleDiameter, circleDiameter);
                }
            }
        }

        private void DrawBlinkingCirclewld(Graphics g)
        {
            if (isCircleVisible)
            {
                int circleDiameter = 8;
                int circleX = pictureBox.Width / 2 - circleDiameter / 2 + 25;
                int circleY = pictureBox.Height / 2 - circleDiameter / 2 - 5;
                Color circleColor;
                if (_Humidity < 30)
                {
                    circleColor = Color.Blue;
                }
                else if (_Humidity >= 30 && _Humidity < 70)
                {
                    circleColor = Color.Yellow;
                }
                else
                {
                    circleColor = Color.Red;
                }

                using (Brush circleBrush = new SolidBrush(circleColor))
                {
                    g.FillEllipse(circleBrush, circleX, circleY, circleDiameter, circleDiameter);
                }
            }
        }
    }
}
