using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsServiceStopper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    var directory = Path.Combine(fbd.SelectedPath, "WinServiceStoper");

                    //Copy the fiels

                    try
                    {
                        Directory.CreateDirectory(directory);
                        var name = "ServicesToStop.txt";
                        File.Copy(Path.GetFullPath(name), Path.Combine(directory, name), true);

                        name = "Topshelf.dll";
                        File.Copy(Path.GetFullPath(name), Path.Combine(directory, name));

                        name = "Topshelf.xml";
                        File.Copy(Path.GetFullPath(name), Path.Combine(directory, name), true);

                        name = "WinServiceController.exe";
                        File.Copy(Path.GetFullPath(name), Path.Combine(directory, name), true);

                        name = "WinServiceController.exe.config";
                        File.Copy(Path.GetFullPath(name), Path.Combine(directory, name), true);

                        name = "WinServiceController.pdb";
                        File.Copy(Path.GetFullPath(name), Path.Combine(directory, name), true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    var servicePath = Path.Combine(directory, "WinServiceController.exe");
                    Process process = new Process();
                    var processInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        WorkingDirectory = @"C:\Windows\System32",
                        FileName = @"C:\Windows\System32\cmd.exe", 
                        Verb = "runas",
                        Arguments = $@"/c ""{servicePath}"" install start",
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    process.StartInfo = processInfo;
                    process.Start();
                    MessageBox.Show("Installed");
                    Close();
                }
            }
        }
    }
}
