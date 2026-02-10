using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace IMS
{
    public partial class User_Management : Form
    {
        // 🔹 STORE LOGGED-IN ADMIN USERNAME
        private string loggedAdmin;

        // 🔹 STORE USERS FOR SEARCH / FILTER / EXPORT
        private DataTable usersTable;

        // 🔹 ROLE CHANGE SUPPORT
        private string previousRole = "";
        private bool suppressChange = false;

        // 🔹 DEFAULT CONSTRUCTOR
        public User_Management()
        {
            InitializeComponent();

            // Event subscriptions preserved exactly as original
            dgvUsers.CellValueChanged += dgvUsers_CellValueChanged;
            dgvUsers.CurrentCellDirtyStateChanged += dgvUsers_CurrentCellDirtyStateChanged;
            dgvUsers.CellBeginEdit += dgvUsers_CellBeginEdit;
            dgvUsers.DataError += dgvUsers_DataError;
        }

        // 🔹 CONSTRUCTOR CALLED FROM DASHBOARD
        public User_Management(string adminUsername) : this()
        {
            loggedAdmin = adminUsername;
        }

        // 🔹 FORM LOAD
        private void User_Management_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        // 🔹 LOAD USERS FROM DATABASE
        private void LoadUsers()
        {
            try
            {
                // 🔹 UPDATED: Using 'using' block with central DatabaseConfig
                using (SqlConnection con = DatabaseConfig.GetConnection())
                {
                    string query = "SELECT username, Role, contact, CAST(IsActive AS BIT) AS IsActive FROM Registration";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);

                    usersTable = new DataTable();
                    da.Fill(usersTable);

                    dgvUsers.AutoGenerateColumns = false;
                    dgvUsers.DataSource = usersTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load users.\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // 🔹 CAPTURE OLD ROLE BEFORE EDIT
        private void dgvUsers_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvUsers.Columns[e.ColumnIndex].Name == "colRole")
            {
                previousRole = dgvUsers.Rows[e.RowIndex].Cells["colRole"].Value?.ToString() ?? "";
            }
        }

        // 🔹 SEARCH USERS (Logic preserved)
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (usersTable == null) return;

            string keyword = txtSearch.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(keyword))
                usersTable.DefaultView.RowFilter = "";
            else
                usersTable.DefaultView.RowFilter =
                    $"username LIKE '%{keyword}%' OR Role LIKE '%{keyword}%'";
        }

        // 🔹 HANDLE GRID CHANGES
        private void dgvUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || suppressChange) return;

            string username = dgvUsers.Rows[e.RowIndex].Cells["colUsername"].Value.ToString();

            // ================= ROLE CHANGE =================
            if (dgvUsers.Columns[e.ColumnIndex].Name == "colRole")
            {
                string newRole = dgvUsers.Rows[e.RowIndex].Cells["colRole"].Value.ToString();

                // ❌ Prevent admin changing his own role
                if (username == loggedAdmin && newRole != "Admin")
                {
                    MessageBox.Show(
                        "You cannot change your own admin role.",
                        "Action Blocked",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    ResetCellValue(e.RowIndex, "colRole", previousRole);
                    return;
                }

                // ⚠ CONFIRM ROLE CHANGE
                DialogResult confirm = MessageBox.Show(
                    $"Are you sure you want to change role of '{username}' to '{newRole}'?",
                    "Confirm Role Change",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.No)
                {
                    ResetCellValue(e.RowIndex, "colRole", previousRole);
                    return;
                }

                UpdateUserRole(username, newRole);
                return;
            }

            // ================= STATUS CHANGE =================
            if (dgvUsers.Columns[e.ColumnIndex].Name == "colStatus")
            {
                bool isActive = Convert.ToBoolean(dgvUsers.Rows[e.RowIndex].Cells["colStatus"].Value);

                // ❌ Prevent admin from deactivating himself
                if (username == loggedAdmin && isActive == false)
                {
                    MessageBox.Show(
                        "You cannot deactivate your own admin account.",
                        "Action Blocked",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    ResetCellValue(e.RowIndex, "colStatus", true);
                    return;
                }

                // ⚠ CONFIRM DEACTIVATION
                if (isActive == false)
                {
                    DialogResult confirm = MessageBox.Show(
                        $"Are you sure you want to deactivate '{username}'?",
                        "Confirm Deactivation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirm == DialogResult.No)
                    {
                        ResetCellValue(e.RowIndex, "colStatus", true);
                        return;
                    }
                }

                UpdateUserStatus(username, isActive);
            }
        }

        // 🔹 HELPER TO RESET CELL WITHOUT TRIGGERING EVENT LOOP
        private void ResetCellValue(int rowIndex, string colName, object value)
        {
            suppressChange = true;
            dgvUsers.Rows[rowIndex].Cells[colName].Value = value;
            suppressChange = false;
        }

        // 🔹 UPDATE ROLE
        private void UpdateUserRole(string username, string role)
        {
            try
            {
                using (SqlConnection con = DatabaseConfig.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Registration SET Role=@role WHERE username=@username",
                        con
                    );

                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update role\n" + ex.Message);
            }
        }

        // 🔹 UPDATE STATUS
        private void UpdateUserStatus(string username, bool isActive)
        {
            try
            {
                using (SqlConnection con = DatabaseConfig.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Registration SET IsActive=@isActive WHERE username=@username",
                        con
                    );

                    cmd.Parameters.AddWithValue("@isActive", isActive);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update status\n" + ex.Message);
            }
        }

        // 🔹 COMMIT CHECKBOX IMMEDIATELY
        private void dgvUsers_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvUsers.IsCurrentCellDirty)
                dgvUsers.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        // 🔹 EXPORT TO EXCEL (CSV)
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (usersTable == null || usersTable.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.csv)|*.csv",
                FileName = "Users.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        sw.WriteLine("Username,Role,Contact,IsActive");

                        foreach (DataRowView row in usersTable.DefaultView)
                        {
                            sw.WriteLine(
                                $"{row["username"]},{row["Role"]},{row["contact"]},{row["IsActive"]}"
                            );
                        }
                    }
                    MessageBox.Show("Users exported successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting data: " + ex.Message);
                }
            }
        }

        // Standard event handlers preserved for designer stability
        private void dgvUsers_DataError(object sender, DataGridViewDataErrorEventArgs e) { e.ThrowException = false; }
        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}