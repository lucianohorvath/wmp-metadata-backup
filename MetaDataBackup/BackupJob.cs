using System.Collections.Generic;
using System.Xml;

namespace Preston.Media
{
    internal class BackupJob : AbstractJob
    {
        private IList<string> files;
        private XmlWriter xmlWriter;

        public BackupJob(string startingFolder, string backupFileName, IList<string> files) 
            : base(startingFolder, backupFileName)
        {
            this.files = files;
        }

        public IList<string> Files
        {
            get { return files; }
        }

        public XmlWriter XmlWriter
        {
            get { return xmlWriter; }
            set { xmlWriter = value; }
        }

        public override int FileCount
        {
            get { return files.Count; }
        }

    }
}
