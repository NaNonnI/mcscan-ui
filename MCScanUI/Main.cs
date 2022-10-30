using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private static string masscanexePath = Path.Combine(masscanPath, "masscan64.exe");

        private static string nodejsPath = Path.Combine(rootPath, @"Program Files\nodejs");
        private static string npmexePath = Path.Combine(nodejsPath, "npm.cmd");

        private string missingReq = "Nothing";

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

            errorMsg(
                    "Form is closing, open an issue on Github. \nThe link has been copied into the clipboard;",
                    "An error as occured. | CHECKING REQUIREMENT",
                    "CHECKING REQUIREMENT"
            );

            //Checking requirement

            missingReq = hasRequired();

            reqlabel.Text = "Checking Requirement..";
            reqlabel.Text = "Requirement missing: " + missingReq;
            if (missingReq != "None") { installReq(missingReq); }
        }

        //What requirement is missing?
        private void installReq(string missing)
        {

            if(!Directory.Exists(tempPathExe)) { Directory.CreateDirectory(tempPathExe); }

            switch (missing) {
                case "NPM":

                    break;

                case "Masscan":

                    break;
                case "Nothing":

                    break;
            }
        }

        //Download & install NPM
        private void installNPM()
        {
            bool isDownloaded = false;

            if (is64bit)
            {
                Download("https://nodejs.org/dist/v18.12.0/node-v18.12.0-x64.msi", Path.Combine(tempPathExe, "nodejs.msi"));
            } 
            else
            {
                Download("https://nodejs.org/dist/v18.12.0/node-v18.12.0-x86.msi", Path.Combine(tempPathExe, "nodejs.msi"));
            }

            if(isDownloaded)
            {

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

            return "Errored";
            errorMsg(
                    "Form is closing, open an issue on Github. \nThe link has been copied into the clipboard;",
                    "An error as occured. | CHECKING REQUIREMENT",
                    "CHECKING REQUIREMENT"
            );
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
    }
}
