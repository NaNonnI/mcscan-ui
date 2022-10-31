using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCScanUI
{
    public partial class Main : Form
    {

        private static string rootPath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
        private static string tempPath = Path.GetTempPath();
        private static string tempPathExe = Path.Combine(Path.GetTempPath(), "mcscan");
        private static bool is64bit = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
        
        private static string masscanPath = Path.Combine(rootPath, "masscan");
        private string masscanexePath = Path.Combine(masscanPath, "masscan.exe");

        private static string nodejsPath = Path.Combine(rootPath, @"Program Files\nodejs");
        private static string npmexePath = Path.Combine(nodejsPath, "npm.cmd");

        private string missingReq = "Nothing";

        private bool hasFile = false;
        private string pathFile = "???";

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            checkAll();

            refreshFile();
        }

        private void refreshFile()
        {
            if (!hasFile)
            {
                scanFileB.Text = "Scan?";
                scanFileB.ForeColor = Color.Red;
            }
            else
            {
                scanFileB.Text = "Scan!";
                scanFileB.ForeColor = Color.Green;
            }

            scanFileTB.Text = pathFile;
        }

        private void checkAll()
        {
            //Checking requirement

            if (is64bit)
            {
                masscanexePath = Path.Combine(masscanPath, "masscan64.exe");
            }
            else
            {
                masscanexePath = Path.Combine(masscanPath, "masscan32.exe");
            }

            missingReq = hasRequired();

            reqlabel.Text = "Checking Requirement..";
            reqlabel.Text = "Requirement missing: " + missingReq;
            if (missingReq != "None") { installReq(missingReq); checkAll(); }
        }

        //What requirement is missing?
        private void installReq(string missing)
        {

            if(!Directory.Exists(tempPathExe)) { Directory.CreateDirectory(tempPathExe); }

            switch (missing) {
                case "NPM":
                    installNPM();
                    break;

                case "Masscan":
                    installMassScan();
                    break;
                case "Nothing":

                    break;
            }
        }

        //Download & install MassScan
        private void installMassScan()
        {
            bool isDownloaded = false;
            string downloadPath = masscanexePath;

            if(!Directory.Exists(masscanPath)) { Directory.CreateDirectory(masscanPath); }

            if (is64bit)
            {
                isDownloaded = Download("https://github.com/Arryboom/MasscanForWindows/blob/master/masscan64.exe", downloadPath);
            }
            else
            {
                isDownloaded = Download("https://github.com/Arryboom/MasscanForWindows/blob/master/masscan32.exe", downloadPath);
            }

            if (isDownloaded)
            {
                string command = "/C set PATH=%PATH%;" + masscanPath;
                Process.Start("cmd.exe", command);
                installMassScanReq();
            }
            else
            {
                errorMsg(
                    "Form is closing, open an issue on Github. \nThe link has been copied into the clipboard;",
                    "An error as occured. | DOWNLOADING MASSCAN",
                    "DOWNLOADING MASSCAN"
                );
            }
        }

        //Download & install MassScan Requirement
        private void installMassScanReq()
        {
            bool isDownloaded = false;
            string downloadPath = Path.Combine(tempPathExe, "winpcap.exe");

            isDownloaded = Download("https://www.winpcap.org/install/bin/WinPcap_4_1_3.exe", downloadPath);

            if (isDownloaded)
            {
                string command = "/C start " + downloadPath;
                Process.Start("cmd.exe", command);
            }
            else
            {
                errorMsg(
                    "Form is closing, open an issue on Github. \nThe link has been copied into the clipboard;",
                    "An error as occured. | DOWNLOADING WINPCAP",
                    "DOWNLOADING WINPCAP"
                );
            }
        }

        //Download & install NPM
        private void installNPM()
        {
            bool isDownloaded = false;
            string downloadPath = Path.Combine(tempPathExe, "nodejs.msi");

            if (is64bit)
            {
                isDownloaded = Download("https://nodejs.org/dist/v18.12.0/node-v18.12.0-x64.msi", downloadPath);
            } 
            else
            {
                isDownloaded = Download("https://nodejs.org/dist/v18.12.0/node-v18.12.0-x86.msi", downloadPath);
            }

            if(isDownloaded)
            {
                string command = "/C msiexec /i " + downloadPath;
                Process.Start("cmd.exe", command);
            } 
            else
            {
                errorMsg(
                    "Form is closing, open an issue on Github. \nThe link has been copied into the clipboard;",
                    "An error as occured. | DOWNLOADING NPM",
                    "DOWNLOADING NPM"
                );
            }
        }


        //Download function
        private bool Download(string URL, string Filename)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(URL, Filename);
            }
            return true;
        }

        //Checking requirement
        //MMMMMMMMHHHH NESTING but do I have a choice?
        private string hasRequired()
        {
            bool hasmasscan = false;
            bool hasnpm = false;

            if(File.Exists(masscanexePath))
            {
                hasmasscan = true;
            }

            if (File.Exists(npmexePath))
            {
                hasnpm = true;
            }

            if (hasmasscan && hasnpm)
            {
                return "None";
            } 
            else
            {
                if(!hasmasscan && !hasnpm)
                {
                    return "All";
                } 
                else
                {
                    if(!hasnpm)
                    {
                        return "NPM";
                    }
                    if(!hasmasscan)
                    {
                        return "Masscan";
                    }
                }
            }

            errorMsg(
                    "Form is closing, open an issue on Github. \nThe link has been copied into the clipboard;",
                    "An error as occured. | CHECKING REQUIREMENT",
                    "CHECKING REQUIREMENT"
            );
            return "Errored";
        }

        private void errorMsg(string message, string caption, string error)
        {
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK);
            if (result == DialogResult.OK)
            {
                Clipboard.SetText("https://github.com/NaNonnI/mcscan-ui/issues/new?title=[" + error.Replace(" ", "%20") + "]&labels=crash");
                this.Dispose();
                this.Close();
            }
        }

        private void scanFileB_Click(object sender, EventArgs e)
        {
            if (!hasFile)
            {
                openFileDialog.InitialDirectory = rootPath;
                openFileDialog.Filter = "MasscanFile (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Open masscan.txt file from MassScan output";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    pathFile = openFileDialog.FileName;
                    hasFile = true;
                    refreshFile();
                }
            } 
            else
            {
                if (!File.Exists(pathFile))
                {
                    hasFile = false;
                    refreshFile();
                    return;
                }


            }
        }
    }
}
