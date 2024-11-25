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
    public partial class UserControl3 : UserControl
    {
        private PictureBox pictureBox;
        private int _fire;
        public UserControl3()
        {
            InitializeComponent();
            LoadImage();
            this.Size = new Size(680, 168);
            this.AutoSize = false; // Disable automatic resizing

            // Prevent resizing by setting Anchor to None
            this.Anchor = AnchorStyles.None;
        }
        public int fire
        {
            get { return _fire; }
            set
            {
                _fire = value;
                UpdateLabel(); // Update all labels when humidity changes
                Invalidate(); // Redraw the UI whenever the humidity changes
            }
        }

        private void UpdateLabel()
        {

            if (fireData != null)
            {
                Color circleColor;
                if (_fire == 0)
                {
                    circleColor = Color.Black;
                    fireData.Text = $"Fire: NORMAL ";
                }
                else if (_fire == 1)
                {
                    circleColor = Color.Red;
                    fireData.Text = $"Fire:   FIRE ";
                }
                else
                {
                    circleColor = Color.Red;
                    fireData.Text = $"Fire:   n/a ";
                }

                fireData.ForeColor = circleColor; // You can customize the color as needed
            }
        }

        private Label fireData;
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
                Image originalImage = Image.FromFile("C:\\Users\\hritu\\source\\repos\\abc\\abc\\iot\\rg-1-removebg-preview-1.png");
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
            if (fireData == null)
            {
                fireData = new Label
                {
                    Text = $"Fire: N/A",
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    ForeColor = Color.Black,
                    BackColor = Color.Transparent,
                    AutoSize = true
                };
                pictureBox.Controls.Add(fireData);
            }
            fireData.Location = new Point(pictureBox.Width / 2 - 200, pictureBox.Height / 2 -10);
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
