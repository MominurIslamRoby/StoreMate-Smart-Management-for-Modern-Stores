using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS
{
    public partial class FormSplash : Form
    {
        public FormSplash()
        {
            InitializeComponent();

            // --- SILENT SETTINGS ---
            this.ControlBox = false;      // Disables the system menu that causes beeps
            this.Text = string.Empty;     // Empty text prevents sound on window initialization
            this.ShowInTaskbar = false;   // Optional: keeps it cleaner

            // Start the timer
            timerSplash.Start();
        }

        // ✅ This prevents the "Ding" sound if the user accidentally presses keys during loading
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // If any key is pressed (Enter, Escape, etc.), return true to handle it silently
            return true;
        }

        // ✅ This adds a professional drop shadow to a borderless form
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            // Increment the progress bar
            if (pbLoading.Value < 100)
            {
                pbLoading.Value += 2; // Adjust this number to change loading speed

                // ✅ Professional Dynamic Status Text
                if (pbLoading.Value < 30)
                {
                    lblStatus.Text = "Initializing StoreMate system...";
                }
                else if (pbLoading.Value < 60)
                {
                    lblStatus.Text = "Connecting to StoreMateDB...";
                }
                else if (pbLoading.Value < 90)
                {
                    lblStatus.Text = "Loading user dashboard...";
                }
                else
                {
                    lblStatus.Text = "Starting session...";
                }
            }
            else
            {
                // Stop timer and close splash once loading hits 100%
                timerSplash.Stop();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {
            // Ensure the label is readable on top of background images
            lblStatus.BackColor = Color.Transparent;
        }
    }
}