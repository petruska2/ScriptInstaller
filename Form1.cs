using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;

namespace Script_Installer_
{
    public partial class MainWindoww : Form
    {

        public MainWindoww()
        {
            InitializeComponent();
        }

public static bool IsAdministrator()
    {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
        {
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
    private void button1_Click(object sender, EventArgs e)
        {
            int size = -1;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    FilePath.Text = file;
                }
                catch (System.IO.IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Install_Click(object sender, EventArgs e)
        {
            const string LOCATION = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp";
            string Path = FilePath.Text;
            string FileName = Path.Split('\\').Last();
            string NewPath = LOCATION + FileName;
            if (!(IsAdministrator()))
            {
                Result.Text = "Please run as Administrator";
            }
            else if (Path == "")
            {
                Result.Text = "Please select a file";
            } 
            else if (!(File.Exists(Path)))
            {
                Result.Text = "Invalid Selection";
            }
            else
            {
                if (File.Exists(NewPath))
                {
                    try
                    {
                        File.Delete(NewPath);
                    }
                    catch (Exception)
                    {
                        Result.Text = "Unexpected Error deleting original script";
                    }
                }
                try
                {
                    File.Move(Path, NewPath);
                    Result.Text = "Success";
                }
                catch (Exception) 
                {
                    Result.Text = "Unexpected Error moving " + Path + " to " + NewPath;
                }
            }
        }

        private void Result_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
