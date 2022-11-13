using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace Preston.Media
{
    public partial class MetaDataBackupForm : Form
    {

        private const string MESSAGEBOX_TITLE = "Metadata Backup";

        private string backupFileName;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private int filesWorked = 0;
        private AbstractJob currentJob = null;
        private MediaAttributeCollection attributeList;

        // application mutex, checked by installer
        private Mutex appMutex = new Mutex(false, "PrestonMediaMetadataBackupMutex");

        public MetaDataBackupForm()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            InitializeBackgoundWorker();
            attributeList = Properties.Settings.Default.LastAttributeList;
            Icon = Properties.Resources.MainIcon;
        }

        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeBackgoundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;

            backgroundWorker.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            backgroundWorker.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);
        }

        private void InitializeWork()
        {
            Properties.Settings.Default.BackupFilePath = openFileDialog.FileName;
            Properties.Settings.Default.IncludeSubfolders = chkRecursive.Checked;
            Properties.Settings.Default.LibrarySourcePath = folderBrowserDialog.SelectedPath;
            Properties.Settings.Default.Save();
            filesWorked = 0;
            lbResults.Items.Clear();
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            string lastFolder = Properties.Settings.Default.LibrarySourcePath;

            if (!System.IO.Directory.Exists(lastFolder))
            {
                lastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                if (!System.IO.Directory.Exists(lastFolder))
                {
                    lastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                if (!System.IO.Directory.Exists(lastFolder))
                {
                    lastFolder = Application.ExecutablePath;
                }
            }

            folderBrowserDialog.SelectedPath = lastFolder;

            DialogResult result = this.folderBrowserDialog.ShowDialog();
            if (result != DialogResult.OK || (!Directory.Exists(folderBrowserDialog.SelectedPath)))
            {
                return;
            }

            this.lblSourceFolder.Text = folderBrowserDialog.SelectedPath;
            ValidateBackupJob();
        }


        private bool ValidateSourceDirectory(string command)
        {
            if (!Directory.Exists(lblSourceFolder.Text))
            {
                MessageBox.Show(
                    String.Format("{0} could not be located. " +
                        "Click Browse to select a valid directory to {1}.", 
                        lblSourceFolder.Text, command),
                    MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            InitializeWork();

            if (!ValidateSourceDirectory("backup")
                || !ValidateBackupFile("backup")
                || !ValidateBackupPath())
            {
                ValidateBackupJob();
                return;
            }

            lbResults.Items.Add("Creating file list...");

            IList<string> files = new List<string>();
            // don't use Directory.GetFiles with a *.* pattern because this completely fails when one dir/file can't be accessed
            try
            {
                RetrieveFilesInTree(lblSourceFolder.Text, this.chkRecursive.Checked, files);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(String.Format(
                    "{0} could not be accessed. Check if you have the required read permissions on that folder.",
                        lblSourceFolder.Text),
                    MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentJob = new BackupJob(lblSourceFolder.Text, backupFileName, files);

            startAsync(currentJob);
        }

        private bool ValidateBackupPath()
        {
            string path = Path.GetDirectoryName(lblDatabaseFileLocation.Text);
            if (!Directory.Exists(path))
            {
                MessageBox.Show(String.Format("{0} could not be located. " +
                    "Click Browse to select a valid folder to save the backup file in.",
                        path),
                    MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ValidateBackupFile(string command)
        {
            if (File.Exists(lblDatabaseFileLocation.Text))
            {
                if (command == "backup")
                {
                    DialogResult fileCheckResult = MessageBox.Show(String.Format(
                        "File {0} already exists. Do you want to overwrite it?",
                            lblDatabaseFileLocation.Text),
                        MESSAGEBOX_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (fileCheckResult != DialogResult.Yes)
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (command == "restore")
                {
                    MessageBox.Show(String.Format("The selected backup file, {0} " + 
                        "could not be found. " +
                        "Click Browse to select a valid backup file.",
                            lblDatabaseFileLocation.Text),
                        MESSAGEBOX_TITLE,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ValidateBackupJob();
                    return false;
                }
            }

            return true;
        }

        #region BackgroundWorker Thread

        private void startAsync(AbstractJob job)
        {
            // Reset the text in the result label.
            resultLabel.Text = String.Empty;

            // Disable the Browse Source button until 
            // the asynchronous operation is done.
            this.btnBrowseSource.Enabled = false;
            this.btnDatabaseBrowse.Enabled = false;
            this.btnClose.Enabled = false;

            // Disable the Start button until 
            // the asynchronous operation is done.
            this.btnBackup.Enabled = false;
            this.btnRestore.Enabled = false;

            // Enable the Cancel button while 
            // the asynchronous operation runs.
            this.btnCancel.Enabled = true;

            // Start the asynchronous operation.
            backgroundWorker.RunWorkerAsync(job);
        }

        // This event handler updates the progress bar.
        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            filesWorked++;

            this.progressBar.Value = e.ProgressPercentage;
            this.lbResults.Items.Add((string)e.UserState);

            this.resultLabel.Text = 
                String.Format("{0} media items out of {1} files {2}.",
                filesWorked, currentJob.FileCount,
                (currentJob is BackupJob) ? "backed up" : "restored");
        }

        private void btnCancel_Click(System.Object sender,
            System.EventArgs e)
        {
            DialogResult confirmResult =
                MessageBox.Show(String.Format(
                "Are you sure you want to cancel the current {0} operation?",
                    (currentJob is BackupJob) ? "backup" : "restore"),
                MESSAGEBOX_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                // Cancel the asynchronous operation.
                this.backgroundWorker.CancelAsync();
                // Disable the Cancel button.
                btnCancel.Enabled = false;
            }
        }

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            AbstractJob job = (AbstractJob)e.Argument;
            BackgroundWorker worker = sender as BackgroundWorker;

            int filesWorked = 0;

            job.MediaPlayer = new MediaPlayer();

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.

            if (job is BackupJob)
            {
                BackupJob backupJob = (BackupJob)job;

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "     ";
                settings.CloseOutput = true;

                backupJob.XmlWriter = XmlWriter.Create(job.BackupFileName, 
                    settings);
                backupJob.XmlWriter.WriteStartDocument();
                backupJob.XmlWriter.WriteStartElement("MediaLibraryMetadataBackup");
                try
                {
                    backupJob.XmlWriter.WriteAttributeString("SourcePathRoot", 
                        job.StartingFolder);

                    BackupMetadata(backupJob, worker, e, ref filesWorked);
                }
                finally
                {
                    backupJob.XmlWriter.WriteEndElement();
                    backupJob.XmlWriter.WriteEndDocument();
                    backupJob.XmlWriter.Flush();
                    backupJob.XmlWriter.Close();
                }
            }
            else
            {
                RestoreMetadata((RestoreJob)job, worker, e, ref filesWorked);
            }
        }

        private void RestoreMetadata(RestoreJob job, BackgroundWorker worker, 
            DoWorkEventArgs e, ref int filesWorked)
        {
            // TODO: move to MediaPlayer.cs?
            WMPLib.WindowsMediaPlayer player = job.MediaPlayer.Player;
            WMPLib.IWMPPlaylist playlist;
            WMPLib.IWMPMedia currentMedia;

            string nullString = "\0";

            // TODO: merge with MediaPlayer.loggingSwitch
            System.Diagnostics.TraceSwitch loggingSwitch =
                new System.Diagnostics.TraceSwitch("LoggingSwitch", "Logging Switch");

            for (int xCount = 0; xCount < job.Iterator.Count; xCount++)
            {

                string attributeName;
                string attributeValue;
                string readOnly;
                bool isReadOnly = false;

                job.Iterator.MoveNext();

                job.Iterator.Current.MoveToAttribute("SourceUrl", "");
                string sourceUrl = MergeOriginalAndNewPath(job.StartingFolder, 
                    job.OriginalStartingFolder, job.Iterator.Current.Value);

                if (loggingSwitch.TraceVerbose)
                    Logger.WriteLine(sourceUrl);

                playlist = 
                    job.MediaPlayer.Player.mediaCollection.getByAttribute("SourceURL",
                        sourceUrl);

                if (playlist.count == 1)
                {
                    currentMedia = playlist.get_Item(0);
                }
                else
                {
                    if (loggingSwitch.TraceVerbose)
                        Logger.WriteLine("Not found in library!");
                    // TODO: still increase the progressbar here
                    // otherwise, it won't get to 100% if there are files that don't exist in library
                    continue;
                }

                job.Iterator.Current.MoveToParent();
                bool isChild = job.Iterator.Current.MoveToFirstChild();
                while (isChild)
                {
                    attributeName = job.Iterator.Current.Name;
                    if (attributeName.StartsWith("WM_"))
                    {
                        attributeName = "WM/" + attributeName.Substring(3);
                    }

                    attributeValue = job.Iterator.Current.Value
                        .Replace("<NULL>", nullString)
                        .Replace("<null>", nullString)
                        .Replace("<Null>", nullString);

                    job.Iterator.Current.MoveToAttribute("IsReadOnly", "");
                    readOnly = job.Iterator.Current.Value;

                    if (bool.TryParse(readOnly, out isReadOnly) && !isReadOnly)
                    {
                        currentMedia.setItemInfo(attributeName, attributeValue);
                    }

                    job.Iterator.Current.MoveToParent();
                    isChild = job.Iterator.Current.MoveToNext(XPathNodeType.Element);
                }

                filesWorked++;
                int percentComplete = CalculatePercentComplete(job.FileCount, filesWorked);
                worker.ReportProgress(percentComplete, currentMedia.sourceURL);

                // Abort the operation if the user has canceled.
                // Note that a call to CancelAsync may have set 
                // CancellationPending to true just after the
                // last invocation of this method exits, so this 
                // code will not have the opportunity to set the 
                // DoWorkEventArgs.Cancel flag to true. This means
                // that RunWorkerCompletedEventArgs.Cancelled will
                // not be set to true in your RunWorkerCompleted
                // event handler. This is a race condition.
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                // preserve the original stack trace - see http://csharptest.net/?p=350
                ThreadStart savestack = 
                    Delegate.CreateDelegate(typeof(ThreadStart), e.Error, 
                    "InternalPreserveStackTrace", false, false) as ThreadStart;
                if (savestack != null) savestack();
                throw e.Error;
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                resultLabel.Text = "Canceled";
                progressBar.Value = 0;
            }

            // Enable the Browse Source button.
            btnDatabaseBrowse.Enabled = btnBrowseSource.Enabled = true;

            // Enable the Start button.
            btnRestore.Enabled = btnBackup.Enabled = btnClose.Enabled = true;

            // Disable the Cancel button.
            btnCancel.Enabled = false;
        }

        private void BackupMetadata(BackupJob job, BackgroundWorker worker, 
            DoWorkEventArgs e, ref int filesWorked)
        {
            foreach (string file in job.Files)
            {
                job.MediaPlayer.BackupLibraryData(file, job.XmlWriter);

                filesWorked++;
                int percentComplete = CalculatePercentComplete(job.Files.Count, filesWorked);
                worker.ReportProgress(percentComplete, file);

                // Abort the operation if the user has canceled.
                // Note that a call to CancelAsync may have set 
                // CancellationPending to true just after the
                // last invocation of this method exits, so this 
                // code will not have the opportunity to set the 
                // DoWorkEventArgs.Cancel flag to true. This means
                // that RunWorkerCompletedEventArgs.Cancelled will
                // not be set to true in your RunWorkerCompleted
                // event handler. This is a race condition.
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }

            e.Result = filesWorked;
        }

        private int CalculatePercentComplete(int fileCount, int filesWorked)
        {
            double dFileCount = (double)fileCount;
            double dFilesWorked = (double)filesWorked;
            double dPercent = (dFilesWorked / dFileCount) * 100d;

            return (int)dPercent;
        }

        #endregion BackgroundWorker Thread


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            InitializeWork();

            if (!ValidateSourceDirectory("restore")
                || !ValidateBackupFile("restore"))
            {
                return;
            }

            DialogResult confirmResult =
                MessageBox.Show(String.Format(
                    "Are you sure that you want to restore {0}?",
                        lblDatabaseFileLocation.Text),
                    MESSAGEBOX_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult != DialogResult.Yes)
                return;

            RestoreJob restoreJob = new RestoreJob(lblSourceFolder.Text, backupFileName);
            currentJob = restoreJob;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(backupFileName);
            XmlElement el = xDoc.DocumentElement;
            restoreJob.OriginalStartingFolder = el.GetAttribute("SourcePathRoot");
            XPathNavigator xNav = xDoc.CreateNavigator();

            restoreJob.Iterator = xNav.Select("//MediaItem");

            startAsync(currentJob);
            return;
        }

        private string MergeOriginalAndNewPath(string newSourcePathRoot, 
            string originalSourcePathRoot, string pathToMerge)
        {
            return pathToMerge.Replace(originalSourcePathRoot, newSourcePathRoot);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void btnDatabaseBrowse_Click(object sender, EventArgs e)
        {
            string lastFolder = Properties.Settings.Default.BackupFilePath;

            if (!System.IO.Directory.Exists(lastFolder))
            {
                lastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                if (!System.IO.Directory.Exists(lastFolder))
                {
                    lastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                if (!System.IO.Directory.Exists(lastFolder))
                {
                    lastFolder = Application.ExecutablePath;
                }
            }

            openFileDialog.InitialDirectory = lastFolder;

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                backupFileName = lblDatabaseFileLocation.Text = openFileDialog.FileName;
                ValidateBackupJob();
            }
        }

        private void ValidateBackupJob()
        {
            bool pathsAreValid = 
                (Directory.Exists(System.IO.Path.GetDirectoryName(lblDatabaseFileLocation.Text))
                && Directory.Exists(lblSourceFolder.Text));

            this.btnBackup.Enabled = this.btnRestore.Enabled = pathsAreValid;
            if (pathsAreValid)
            {
                backupFileName = lblDatabaseFileLocation.Text;
            }
        }

        private void MetaDataBackupForm_Load(object sender, EventArgs e)
        {
            // migrate settings if an older version was installed
            if (Properties.Settings.Default.CallUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.CallUpgrade = false;
                Properties.Settings.Default.Save();
            }

            this.lblSourceFolder.Text = folderBrowserDialog.SelectedPath = 
                Properties.Settings.Default.LibrarySourcePath;
            this.lblDatabaseFileLocation.Text =  
                Properties.Settings.Default.BackupFilePath;
            string defaultFilePath = 
                (string)Properties.Settings.Default.Properties["BackupFilePath"].DefaultValue;
            if (!lblDatabaseFileLocation.Text.Equals(defaultFilePath))
                this.openFileDialog.FileName = Properties.Settings.Default.BackupFilePath;
            this.chkRecursive.Checked = Properties.Settings.Default.IncludeSubfolders;
            this.openFileDialog.InitialDirectory = 
                Path.GetDirectoryName(lblDatabaseFileLocation.Text);

            ValidateBackupJob();
        }

        private void howToUseMetadataBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HowToUse howToUse = new HowToUse();
            howToUse.Show();
        }

        
        private void tsmiChooseAttributesToBackup_Click(object sender, EventArgs e)
        {
            /*
            if (attributeList == null)
            {
                attributeList = MediaAttributeCollectionFactory.CreateDefaultCollection();
            }
            //MediaAttributeCollection mac = new MediaAttributeCollection();
            AttributeListSelector ec = new AttributeListSelector(attributeList);
            ec.ShowDialog();
            */
        }

        // based on StackBasedIteration source at http://msdn.microsoft.com/en-us/library/bb513869.aspx
        private void RetrieveFilesInTree(string root, bool recursive, IList<string> fileList)
        {
            // Data structure to hold names of subfolders to be
            // examined for files.
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            bool isRoot = true;

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;

                if (recursive)
                {
                    try
                    {
                        subDirs = System.IO.Directory.GetDirectories(currentDir);
                    }
                    // An UnauthorizedAccessException exception will be thrown if we do not have
                    // discovery permission on a folder or file. It may or may not be acceptable 
                    // to ignore the exception and continue enumerating the remaining files and 
                    // folders. It is also possible (but unlikely) that a DirectoryNotFound exception 
                    // will be raised. This will happen if currentDir has been deleted by
                    // another application or thread after our call to Directory.Exists. The 
                    // choice of which exceptions to catch depends entirely on the specific task 
                    // you are intending to perform and also on how much you know with certainty 
                    // about the systems on which this code will run.
                    catch (UnauthorizedAccessException e)
                    {
                        if (isRoot)
                            throw e;
                        else
                        {
                            this.lbResults.Items.Add("Warning: " + e.Message);
                            continue;
                        }
                    }
                    catch (System.IO.DirectoryNotFoundException)
                    {
                        continue;
                    }
                }
                else
                    subDirs = new string[0];

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    if (isRoot)
                        throw e;
                    else
                    {
                        this.lbResults.Items.Add("Warning: " + e.Message);
                        continue;
                    }
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    continue;
                }
                // Perform the required action on each file here.
                foreach (string file in files)
                    fileList.Add(file);

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs)
                    dirs.Push(str);

                isRoot = false;
            }
        }

    }
}