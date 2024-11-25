using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abc.iot
{
    public partial class UserControl2 : UserControl
    {
        private PictureBox pictureBox;
        private int _mode;
        public UserControl2()
        {
            InitializeComponent();
            LoadImage();
            this.Size = new Size(229, 212);
            this.AutoSize = false; // Disable automatic resizing

            // Prevent resizing by setting Anchor to None
            this.Anchor = AnchorStyles.None;
        }
        public int mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                UpdateLabel(); // Update all labels when humidity changes
                Invalidate(); // Redraw the UI whenever the humidity changes
            }
        }
        
        private void UpdateLabel()
        {

            if (modeData != null)
            {
                Color circleColor;
                if (_mode ==0)
                {
                    circleColor = Color.Black;
                    modeData.Text = $"mode: \n  OFF ";
                }
                else if (_mode==1)
                {
                    circleColor = Color.Red;
                    modeData.Text = $"mode: \n  ON ";
                }
                else
                {
                    circleColor = Color.Red;
                    modeData.Text = $"mode: \n  n/a ";
                }
               
                modeData.ForeColor = circleColor; // You can customize the color as needed
            }
        }

        private Label modeData;
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
                Image originalImage = Image.FromFile("C:\\Users\\hritu\\source\\repos\\abc\\abc\\iot\\power-supply-ups-icon-design-template-simple-and-clean-vector-removebg-preview.png");
                Image resizedImage = ResizeImage(originalImage, 800, 600);
                pictureBox.Image = resizedImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Create and add the labels if they are not already created
            if (modeData == null)
            {
                modeData = new Label
                {
                    Text = $"Power \n {_mode}",
                    Font = new Font("Arial", 7, FontStyle.Bold),
                    ForeColor = Color.Black,
                    BackColor = Color.Transparent,
                    AutoSize = true
                };
                pictureBox.Controls.Add(modeData);
            }
            modeData.Location = new Point(pictureBox.Width / 2 -20, pictureBox.Height / 2 -50);
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

    }
}
