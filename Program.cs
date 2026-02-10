using System;
using System.Windows.Forms;

namespace IMS
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Added a simple try-catch to catch unhandled hardware exceptions
            try
            {
                Application.Run(new Login());
            }
            catch (Exception ex)
            {
                MessageBox.Show("A critical error occurred: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}