using System.Xml.XPath;

namespace Preston.Media
{
    internal class RestoreJob : AbstractJob
    {
        private XPathNodeIterator iterator;
        private string originalStartingFolder;

        public RestoreJob(string startingFolder, string backupFileName)
            : base(startingFolder, backupFileName)
        { }

        public XPathNodeIterator Iterator
        {
            get { return iterator; }
            set { iterator = value; }
        }

        public string OriginalStartingFolder
        {
            get { return originalStartingFolder; }
            set { originalStartingFolder = value; }
        }

        public override int FileCount
        {
            get { return iterator.Count; }
        }

    }
}