using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace IMS
{
    public partial class dashboard : Form
    {
        private string loggedUsername;
        private string loggedRole;

        // Timer for Fade effect
        private Timer fadeTimer = new Timer();
        private double opacityValue = 0;

        public dashboard(string username, string role)
        {
            InitializeComponent();
            loggedUsername = username;
            loggedRole = role;

            // Setup Fade Timer
            fadeTimer.Interval = 30;
            fadeTimer.Tick += FadeTimer_Tick;
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            lblLoggedUser.Text = loggedUsername;
            timerClock.Start();

            ApplyRolePermissions();
            MakePictureBoxRound();
            RetrieveUserProfilePicture();

            // Default view
            ResetSidebarButtons();
            ActivateButton(btnDashboard);
            LoadForm(new DashboardHomeForm());
        }

        // ===================== MODERN UI EFFECTS (FADE & HOVER) =====================

        private void LoadForm(Form frm)
        {
            panelMain.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Start Fade-In Logic
            opacityValue = 0;
            panelMain.Visible = false;

            panelMain.Controls.Add(frm);
            frm.Show();

            panelMain.Visible = true;
            fadeTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (opacityValue < 1)
            {
                opacityValue += 0.1;
            }
            else
            {
                fadeTimer.Stop();
            }
        }

        private void SidebarButton_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.BackColor != Color.White)
            {
                btn.BackColor = Color.FromArgb(50, 50, 150);
            }
        }

        private void SidebarButton_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.BackColor != Color.White)
            {
                btn.BackColor = Color.Navy;
            }
        }

        // ===================== ROLE & SECURITY LOGIC =====================

        private void ApplyRolePermissions()
        {
            // Reset visibility first to ensure no leaks
            btnUserManagement.Visible = true;
            btnProducts.Visible = true;
            btnReports.Visible = true;
            btnSales.Visible = true;
            btnCustomers.Visible = true;
            btnDashboard.Visible = true;
            btnSettings.Visible = true;

            if (loggedRole == "Admin")
            {
                // Admin has access to everything
                btnUserManagement.Visible = true;
                btnProducts.Visible = true;
                btnReports.Visible = true;
            }
            else if (loggedRole == "Manager")
            {
                btnUserManagement.Visible = false;
                btnProducts.Visible = true;
                btnReports.Visible = true;
            }
            else if (loggedRole == "Staff")
            {
                // STAFF ACCESS: Only Dashboard, Sales, Customers, Settings
                btnUserManagement.Visible = false;
                btnProducts.Visible = false;
                btnReports.Visible = false;
            }
        }

        // ===================== PROFILE PICTURE LOGIC =====================

        private void RetrieveUserProfilePicture()
        {
            try
            {
                // 🔹 UPDATED: Using DatabaseConfig for safe SQL Express connection
                using (SqlConnection con = DatabaseConfig.GetConnection())
                {
                    con.Open();
                    string query = "SELECT UserImage FROM Registration WHERE username = @uname";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@uname", loggedUsername);
                        object data = cmd.ExecuteScalar();

                        if (data != DBNull.Value && data != null)
                        {
                            byte[] imgBytes = (byte[])data;
                            using (MemoryStream ms = new MemoryStream(imgBytes))
                            {
                                pbUserProfile.Image = Image.FromStream(ms);
                                pbUserProfile.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                        }
                    }
                }
            }
            catch { /* Silent load if no image exists */ }
        }

        private void pbUserProfile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    Image selectedImg = new Bitmap(open.FileName);
                    pbUserProfile.Image = selectedImg;
                    byte[] imgBytes;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        selectedImg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        imgBytes = ms.ToArray();
                    }
                    SaveImageToDatabase(imgBytes);
                }
            }
        }

        private void SaveImageToDatabase(byte[] imgData)
        {
            try
            {
                // 🔹 UPDATED: Using DatabaseConfig
                using (SqlConnection con = DatabaseConfig.GetConnection())
                {
                    con.Open();
                    string query = "UPDATE Registration SET UserImage = @img WHERE username = @uname";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@img", imgData);
                        cmd.Parameters.AddWithValue("@uname", loggedUsername);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Profile updated!", "StoreMate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // ===================== UI NAVIGATION =====================

        private void MakePictureBoxRound()
        {
            if (pbUserProfile != null)
            {
                GraphicsPath gp = new GraphicsPath();
                gp.AddEllipse(0, 0, pbUserProfile.Width, pbUserProfile.Height);
                pbUserProfile.Region = new Region(gp);
            }
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy | hh:mm:ss tt");
        }

        private void ResetSidebarButtons()
        {
            foreach (Control c in panelSidebar.Controls)
            {
                if (c is Button) { c.BackColor = Color.Navy; c.ForeColor = Color.White; }
            }
        }

        private void ActivateButton(object sender)
        {
            if (sender is Button btn) { btn.BackColor = Color.White; btn.ForeColor = Color.Black; }
        }

        private void btnDashboard_Click(object sender, EventArgs e) { ResetSidebarButtons(); ActivateButton(sender); LoadForm(new DashboardHomeForm()); }
        private void btnProducts_Click(object sender, EventArgs e) { ResetSidebarButtons(); ActivateButton(sender); LoadForm(new ProductsForm()); }
        private void btnSales_Click(object sender, EventArgs e) { ResetSidebarButtons(); ActivateButton(sender); LoadForm(new SalesForm()); }
        private void btnCustomers_Click(object sender, EventArgs e) { ResetSidebarButtons(); ActivateButton(sender); LoadForm(new CustomersForm()); }
        private void btnReports_Click(object sender, EventArgs e) { ResetSidebarButtons(); ActivateButton(sender); LoadForm(new ReportsForm()); }
        private void btnSettings_Click(object sender, EventArgs e) { ResetSidebarButtons(); ActivateButton(sender); LoadForm(new SettingsForm(loggedUsername)); }
        private void btnUserManagement_Click(object sender, EventArgs e) { ResetSidebarButtons(); ActivateButton(sender); LoadForm(new User_Management(loggedUsername)); }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to sign out?", "StoreMate | Sign Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                new Login().Show();
            }
        }
    }
}