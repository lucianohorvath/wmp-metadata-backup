namespace Preston.Media
{
    internal abstract class AbstractJob
    {
        private string startingFolder;
        private string backupFileName;

        private MediaPlayer mediaPlayer;

        public AbstractJob(string startingFolder, string backupFileName)
        {
            this.startingFolder = startingFolder;
            this.backupFileName = backupFileName;
        }

        public string StartingFolder
        {
            get { return startingFolder; }
        }

        public string BackupFileName
        {
            get { return backupFileName; }
        }

        public MediaPlayer MediaPlayer
        {
            get { return mediaPlayer; }
            set { mediaPlayer = value; }
        }

        public abstract int FileCount
        {
            get;
        }

    }
}