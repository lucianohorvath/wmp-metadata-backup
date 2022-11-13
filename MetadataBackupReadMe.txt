Metadata Backup
http://sourceforge.net/projects/metadatabackup

License

Tim De Baets grants the user the right to use Metadata Backup on as many computers as he or she likes for the purpose of copying and viewing the metadata of their music files. To use Metadata Backup, you must agree that they are using it solely at their own risk and will not hold Tim De Baets or his heirs or assigns responsible for any damage or loss of data resulting from its use. You must also agree that Tim De Baets is not responsible for providing any support for Metadata Backup.

While I believe Metadata Backup to be safe to use, there are many things that can cause the loss of data including user error or unexpected behaviour from badly malformed data.  You should only use Metadata backup on files that have been properly backed up. That just makes good sense - always backup files before modifying them in any way.

Version History

April 18, 2010 – Version 1.2

* Fixed: "directory could not be located" error when a root folder (such as C:\) is selected.
* Fixed: "invalid character" errors during backup. 
* Fixed: System.InvalidOperationException error during backup.
* Metadata Backup now skips subfolders that can't be accessed (like System Volume Information), instead of failing the whole backup operation.
* The Restore and Cancel buttons now require confirmation first.
* Created an installer for Metadata Backup.
* Various small changes.

May 28, 2007 - Version 1.1

UTF-8 XML data does not like null characters (bytes containing the value of zero). Some ID3 tag editing programs insert a null character to divide lists such as composers or genres. Version 1.1 of Metadata Backup fixes an issue that occurred when encountering those null characters by replacing the null character with the string "<NULL>" when backing up and replacing the string "<NULL>" with a null character when restoring.

Also, there was added some detailed logging, turned off by default, that can be used to troubleshoot further data compatibility issues.

March 18, 2007 - Initial release.


For more help on using Metadata Backup, click the Help menu in the program (after it's installed), and select How to use Metadata Backup.