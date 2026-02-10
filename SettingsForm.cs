using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IMS
{
    public partial class SettingsForm : Form
    {
        private string loggedUsername;

        // 🔹 UPDATED: Connection now handled via central DatabaseConfig class
        // The old private SqlConnection con line has been removed.

        public SettingsForm(string username)
        {
            InitializeComponent();
            loggedUsername = username;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        // ===================== LOAD DATA LOGIC =====================
        private void LoadUserData()
        {
            try
            {
                // 🔹 UPDATED: Using DatabaseConfig with a 'using' block for safety
                using (SqlConnection con = DatabaseConfig.GetConnection())
                {
                    con.Open();
                    string query = "SELECT firstname, lastname, contact, UserImage FROM Registration WHERE username = @uname";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@uname", loggedUsername);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtFirstName.Text = dr["firstname"].ToString();
                                txtLastName.Text = dr["lastname"].ToString();
                                txtContact.Text = dr["contact"].ToString();

                                // ✅ PRESERVED FIX: Load Image using a cloned stream to prevent HRESULT E_FAIL
                                if (dr["UserImage"] != DBNull.Value)
                                {
                                    byte[] img = (byte[])dr["UserImage"];
                                    using (MemoryStream ms = new MemoryStream(img))
                                    {
                                        // Cloning the image prevents the stream-disposal error
                                        pbSettingsProfile.Image = new Bitmap(Image.FromStream(ms));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading settings: " + ex.Message); }
        }

        // ===================== PHOTO BUTTON LOGIC =====================
        private void btnChangePhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    // ✅ PRESERVED FIX: Use a stream to load the file so the file isn't locked on disk
                    using (FileStream fs = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))
                    {
                        pbSettingsProfile.Image = new Bitmap(Image.FromStream(fs));
                    }
                }
            }
        }

        // ===================== SAVE BUTTON LOGIC (WITH VALIDATION) =====================
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = DatabaseConfig.GetConnection())
                {
                    con.Open();

                    // STEP 1: Verify Old Password first (Logic Preserved)
                    string checkQuery = "SELECT password FROM Registration WHERE username = @uname";
                    string currentDbPassword = "";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@uname", loggedUsername);
                        var result = checkCmd.ExecuteScalar();
                        currentDbPassword = result != null ? result.ToString() : "";
                    }

                    if (txtOldPassword.Text != currentDbPassword)
                    {
                        MessageBox.Show("The 'Old Password' you entered is incorrect. Changes cannot be saved.", "Security Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // STEP 2: Handle New Password (if empty, keep the old one)
                    string passwordToSave = string.IsNullOrWhiteSpace(txtNewPassword.Text) ? currentDbPassword : txtNewPassword.Text;

                    // STEP 3: Convert Image to Bytes (Logic Preserved)
                    byte[] imgBytes = null;
                    if (pbSettingsProfile.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Use a clone to save to prevent GDI+ generic errors
                            using (Bitmap bmp = new Bitmap(pbSettingsProfile.Image))
                            {
                                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            }
                            imgBytes = ms.ToArray();
                        }
                    }

                    // STEP 4: Update Database
                    string updateQuery = @"UPDATE Registration 
                                         SET firstname=@fn, lastname=@ln, contact=@con, password=@pass, UserImage=@img 
                                         WHERE username=@uname";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@fn", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@ln", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@con", txtContact.Text);
                        cmd.Parameters.AddWithValue("@pass", passwordToSave);
                        cmd.Parameters.AddWithValue("@uname", loggedUsername);

                        // Handle image parameter properly
                        if (imgBytes != null)
                            cmd.Parameters.Add("@img", SqlDbType.VarBinary).Value = imgBytes;
                        else
                            cmd.Parameters.Add("@img", SqlDbType.VarBinary).Value = DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear password fields for security
                txtOldPassword.Clear();
                txtNewPassword.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Update Error: " + ex.Message); }
        }
    }
}