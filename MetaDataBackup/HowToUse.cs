using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Preston.Media
{
    public partial class HowToUse : Form
    {

        private const String RTF_FILENAME = "MetadataBackupHelp.rtf";
        private const String DEFAULT_RTF =
            "The local help file was not found. More information about Metadata Backup is at {0}";

        public HowToUse()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            Icon = Properties.Resources.MainIcon;
        }

        private void HowToUse_Load(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string rtfPath = Path.Combine(appPath, RTF_FILENAME);
            if (File.Exists(rtfPath))
            {
                try
                {
                    FileStream fs = File.Open(rtfPath, FileMode.Open,
                        FileAccess.Read);

                    byte[] bytes = new byte[fs.Length];

                    fs.Read(bytes, 0, bytes.Length);
                    Encoding enc = System.Text.ASCIIEncoding.UTF8;

                    this.richTextBox1.Rtf = enc.GetString(bytes);

                    return;
                }
                catch {}
            }
            this.richTextBox1.Text = String.Format(DEFAULT_RTF,
                Properties.Resources.WebsiteURL);

        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

    }
}