using System;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace IMS
{
    public partial class Login : Form
    {
        // 🔹 PASSWORD VISIBILITY FLAG
        private bool isPasswordVisible = false;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // 🔒 Default password state
            txtPassword.UseSystemPasswordChar = true;

            // Note: Ensure these resources exist in your project
            try { picTogglePassword.Image = Properties.Resources.eyeHidden; } catch { }
        }

        private void btnOpenRegister_Click(object sender, EventArgs e)
        {
            RegistrationForm rf = new RegistrationForm();
            rf.Show();
            this.Hide();
        }

        // ===================== LOGIN LOGIC WITH SPLASH =====================
        private void btnUserLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Please enter Username and Password",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // 🔹 UPDATED: Using DatabaseConfig.GetConnection() inside a using block
                using (SqlConnection loginCon = DatabaseConfig.GetConnection())
                {
                    // Selects username, Role, and IsActive to handle permissions and approval status
                    string query = "SELECT username, Role, IsActive FROM Registration WHERE username=@u AND password=@p";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, loginCon))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                        da.SelectCommand.Parameters.AddWithValue("@p", txtPassword.Text.Trim());

                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            string username = dt.Rows[0]["username"].ToString();
                            string role = dt.Rows[0]["Role"].ToString();
                            bool isActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);

                            if (!isActive)
                            {
                                MessageBox.Show(
                                    "Your account is not active yet.\nPlease wait for admin approval.\n\nMail: mominur.roby@gmail.com",
                                    "Access Denied",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );
                                return;
                            }

                            // ✅ SUCCESSFUL LOGIN: Show Professional Splash Screen
                            this.Hide();

                            using (FormSplash splash = new FormSplash())
                            {
                                // ShowDialog stops code execution here until splash finishes (100%)
                                if (splash.ShowDialog() == DialogResult.OK)
                                {
                                    // After splash is done, open the dashboard with permissions
                                    dashboard d = new dashboard(username, role);
                                    d.Show();
                                }
                                else
                                {
                                    this.Show();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(
                                "Invalid username or password",
                                "Login Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                } // 🔹 Connection is automatically closed and disposed here
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database Connection Error:\n" + ex.Message,
                    "System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ===================== KEYBOARD SUPPORT =====================
        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Stops the 'ding' sound
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnUserLogin.PerformClick();
            }
        }

        // ===================== SHOW / HIDE PASSWORD =====================
        private void picTogglePassword_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                txtPassword.UseSystemPasswordChar = true;
                try { picTogglePassword.Image = Properties.Resources.eyeHidden; } catch { }
                isPasswordVisible = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
                try { picTogglePassword.Image = Properties.Resources.eyeShow; } catch { }
                isPasswordVisible = true;
            }
        }

        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "Mail to our Boss Roby from your official mail only.\n\nEmail: mominur.roby@gmail.com",
                "Forgot Password",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // Unused events - cleaning up if necessary
        private void btnLogin_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
    }
}