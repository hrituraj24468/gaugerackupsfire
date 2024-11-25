using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace abc
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;
        private dashboard dashboard;
        private bool connectionStatus = false;

        public MDIParent1()
        {
            InitializeComponent();
            TestDatabaseConnection(); // Test connection on application load
        }

        private void TestDatabaseConnection()
        {
            string connectionString = "Server=127.0.0.1;Database=data;User Id=root;Password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connectionStatus = true; // Connection successful
                    UpdateStatusBar("Database connection successful");
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connectionStatus = false; // Connection failed
                    UpdateStatusBar($"Database connection failed: {ex.Message}");
                }
            }
        }

        private void UpdateStatusBar(string message)
        {
            if (statusStrip != null)
            {
                if (statusStrip.Items.Count == 0)
                {
                    statusStrip.Items.Add(new ToolStripStatusLabel());
                }

                statusStrip.Items[0].Text = message;
            }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (dashboard == null)
            {
                dashboard = new dashboard();
                dashboard.MdiParent = this;
                dashboard.Show();
            }
            else
            {
                dashboard.Show();
            }
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
        }
    }
}
