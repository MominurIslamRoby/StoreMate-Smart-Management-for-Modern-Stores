using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IMS
{
    public partial class RegistrationForm : Form
    {
        // 🔹 UPDATED: Now uses the central DatabaseConfig class
        private SqlConnection con = DatabaseConfig.GetConnection();

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // --- KEY DOWN LOGIC FOR NAVIGATION ---
        private void txtFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLastName.Focus();
                e.SuppressKeyPress = true; // Prevents the 'ding' sound
            }
        }

        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContact.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUsername.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbRole.Focus();
                cmbRole.DroppedDown = true; // Automatically open the dropdown for the user
                e.SuppressKeyPress = true;
            }
        }

        private void cmbRole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRegister.Focus();
                btnRegister.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Basic Validations
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter First Name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter Username", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter Password", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Role", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus();
                return;
            }

            try
            {
                // 🔹 UPDATED: Using 'using' block for automatic connection management
                using (SqlConnection registrationCon = DatabaseConfig.GetConnection())
                {
                    registrationCon.Open();

                    //  UNIQUE USERNAME LOGIC 
                    string checkQuery = "SELECT COUNT(*) FROM Registration WHERE username = @user";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, registrationCon);
                    checkCmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());

                    int userExists = (int)checkCmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        MessageBox.Show(
                            $"The username '{txtUsername.Text.Trim()}' is already taken.\nPlease choose a different username.",
                            "Username Unavailable",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        txtUsername.Focus();
                        txtUsername.SelectAll();
                        return;
                    }

                    // Proceed with Registration
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Registration " +
                        "(firstname, lastname, username, password, email, contact, Role, IsActive) " +
                        "VALUES (@firstname, @lastname, @username, @password, @email, @contact, @role, @isActive)",
                        registrationCon
                    );

                    cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@lastname", txtLastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", DBNull.Value); // optional
                    cmd.Parameters.AddWithValue("@contact", txtContact.Text.Trim());
                    cmd.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());

                    // Admin active by default, others inactive
                    if (cmbRole.SelectedItem.ToString() == "Admin")
                        cmd.Parameters.AddWithValue("@isActive", 1);
                    else
                        cmd.Parameters.AddWithValue("@isActive", 0);

                    cmd.ExecuteNonQuery();
                } // 🔹 Connection closes automatically here

                // Professional DialogBox with Enter Key focus
                DialogResult result = MessageBox.Show(
                    "Registration successful!\n\nYour account has been created. Please log in to continue.",
                    "Registration Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1 // This makes Enter key trigger 'OK'
                );

                // If user clicks OK or presses Enter on the DialogBox
                if (result == DialogResult.OK)
                {
                    // Open Login Page automatically
                    Login loginForm = new Login();
                    loginForm.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database Error:\n" + ex.Message,
                    "System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtContact.Clear();
            txtUsername.Clear();
            txtPassword.Clear();

            cmbRole.SelectedIndex = -1;

            txtFirstName.Focus();
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }
    }
}