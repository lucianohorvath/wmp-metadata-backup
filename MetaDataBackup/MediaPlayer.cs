using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WMPLib;

namespace Preston.Media
{
    public class MediaPlayer
    {
        public static System.Diagnostics.TraceSwitch loggingSwitch =
            new System.Diagnostics.TraceSwitch("LoggingSwitch", "Logging Switch");

        WindowsMediaPlayer player = new WindowsMediaPlayer();

        public MediaPlayer()
        {
            player.MediaError +=
                new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
        }

        private void Player_MediaError(object pMediaObject)
        {
            throw new Exception(pMediaObject.ToString());
        }

        public WMPLib.WindowsMediaPlayer Player
        {
            get { return player; }
        }

        internal void BackupLibraryData(IWMPMedia media, XmlWriter writer)
        {
            writer.WriteStartElement("MediaItem");

            writer.WriteAttributeString("SourceUrl", media.sourceURL);

            WriteAttributeValue(writer, media, "AcquisitionTime", true);
            WriteAttributeValue(writer, media, "AlbumID", true);
            WriteAttributeValue(writer, media, "AlbumIDAlbumArtist", true);
            WriteAttributeValue(writer, media, "Author", false);
            WriteAttributeValue(writer, media, "AverageLevel", false);
            WriteAttributeValue(writer, media, "Bitrate", true);
            WriteAttributeValue(writer, media, "BuyNow", true);
            WriteAttributeValue(writer, media, "BuyTickets", true);
            WriteAttributeValue(writer, media, "Copyright", true);
            WriteAttributeValue(writer, media, "Duration", true);
            WriteAttributeValue(writer, media, "FileSize", true);
            WriteAttributeValue(writer, media, "FileType", true);
            WriteAttributeValue(writer, media, "Is_Protected", true);
            WriteAttributeValue(writer, media, "MediaType", true);
            WriteAttributeValue(writer, media, "MoreInfo", true);
            WriteAttributeValue(writer, media, "PeakValue", false);
            WriteAttributeValue(writer, media, "ProviderLogoURL", true);
            WriteAttributeValue(writer, media, "ProviderURL", true);
            WriteAttributeValue(writer, media, "RecordingTime", true);
            WriteAttributeValue(writer, media, "ReleaseDate", false);
            WriteAttributeValue(writer, media, "RequestState", false);
            WriteAttributeValue(writer, media, "SourceURL", true);
            WriteAttributeValue(writer, media, "SyncState", true);
            WriteAttributeValue(writer, media, "Title", false);
            WriteAttributeValue(writer, media, "TrackingID", true);
            WriteAttributeValue(writer, media, "UserCustom1", false);
            WriteAttributeValue(writer, media, "UserCustom2", false);
            WriteAttributeValue(writer, media, "UserEffectiveRating", true);
            WriteAttributeValue(writer, media, "UserLastPlayedTime", true);
            WriteAttributeValue(writer, media, "UserPlayCount", false);
            WriteAttributeValue(writer, media, "UserPlaycountAfternoon", false);
            WriteAttributeValue(writer, media, "UserPlaycountEvening", false);
            WriteAttributeValue(writer, media, "UserPlaycountMorning", false);
            WriteAttributeValue(writer, media, "UserPlaycountNight", false);
            WriteAttributeValue(writer, media, "UserPlaycountWeekday", false);
            WriteAttributeValue(writer, media, "UserPlaycountWeekend", false);
            WriteAttributeValue(writer, media, "UserRating", false);
            WriteAttributeValue(writer, media, "UserServiceRating", false);
            WriteAttributeValue(writer, media, "WM/AlbumArtist", false);
            WriteAttributeValue(writer, media, "WM/AlbumTitle", false);
            WriteAttributeValue(writer, media, "WM/Category", false);
            WriteAttributeValue(writer, media, "WM/Composer", false);
            WriteAttributeValue(writer, media, "WM/Conductor", false);
            WriteAttributeValue(writer, media, "WM/ContentDistributor", false);
            WriteAttributeValue(writer, media, "WM/ContentGroupDescription", false);
            WriteAttributeValue(writer, media, "WM/EncodingTime", true);
            WriteAttributeValue(writer, media, "WM/Genre", false);
            WriteAttributeValue(writer, media, "WM/InitialKey", false);
            WriteAttributeValue(writer, media, "WM/Language", false);
            WriteAttributeValue(writer, media, "WM/Lyrics", false);
            WriteAttributeValue(writer, media, "WM/MCDI", true);
            WriteAttributeValue(writer, media, "WM/MediaClassPrimaryID", false);
            WriteAttributeValue(writer, media, "WM/MediaClassSecondaryID", false);
            WriteAttributeValue(writer, media, "WM/Mood", false);
            WriteAttributeValue(writer, media, "WM/ParentalRating", false);
            WriteAttributeValue(writer, media, "WM/Period", false);
            WriteAttributeValue(writer, media, "WM/ProtectionType", false);
            WriteAttributeValue(writer, media, "WM/Provider", true);
            WriteAttributeValue(writer, media, "WM/ProviderRating", true);
            WriteAttributeValue(writer, media, "WM/ProviderStyle", true);
            WriteAttributeValue(writer, media, "WM/Publisher", false);
            WriteAttributeValue(writer, media, "WM/SubscriptionContentID", false);
            WriteAttributeValue(writer, media, "WM/SubTitle", false);
            WriteAttributeValue(writer, media, "WM/TrackNumber", false);
            WriteAttributeValue(writer, media, "WM/UniqueFileIdentifier", false);
            WriteAttributeValue(writer, media, "WM/WMCollectionGroupID", true);
            WriteAttributeValue(writer, media, "WM/WMCollectionID", true);
            WriteAttributeValue(writer, media, "WM/WMContentID", true);
            WriteAttributeValue(writer, media, "WM/Writer", false);

            writer.WriteEndElement();
        }

        private void LogVerbose(string text)
        {
            if (loggingSwitch.TraceVerbose)
                Logger.WriteLine(text);
        }

        internal void BackupLibraryData(string fullFilePath, XmlWriter writer) // XmlDocument xDoc)
        {
            IWMPPlaylist playlist = Player.mediaCollection.getByAttribute("SourceURL", fullFilePath);
            if (playlist.count == 1)
            {
                IWMPMedia media = playlist.get_Item(0);
                string mediaType = media.getItemInfo("MediaType");
                LogVerbose(String.Format("{0} found in media library - media type: {1}",
                    fullFilePath, mediaType));
                if (mediaType == "audio") // TODO: allow other media types
                {
                    BackupLibraryData(playlist.get_Item(0), writer); // xDoc);
                }
            }
            else
                LogVerbose(String.Format("{0} NOT found in media library ({1})",
                    fullFilePath, playlist.count));
            return;
        }

        private void WriteAttributeValue(XmlWriter writer, IWMPMedia media, string attribute, bool isReadOnly)
        {
            try
            {
                if (loggingSwitch.TraceVerbose)
                {
                    string testValue = RemoveInvalidXmlChars(media.getItemInfo(attribute));
                    Logger.WriteLine(media.sourceURL + "::" + attribute + "::" + testValue);
                }
            }
            catch {}

            writer.WriteStartElement(attribute.Replace("WM/", "WM_"));

            writer.WriteAttributeString("IsReadOnly", isReadOnly.ToString());

            // TODO: correctly backup/restore multi-valued attributes
            // see http://sourceforge.net/projects/metadatabackup/forums/forum/1105146/topic/3775002
            writer.WriteValue(RemoveInvalidXmlChars(media.getItemInfo(attribute)));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        // from http://prettycode.org/2009/05/07/hexadecimal-value-0x-is-an-invalid-character/
        public bool IsLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */        ||
                 character == 0xA /* == '\n' == 10  */        ||
                 character == 0xD /* == '\r' == 13  */        ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }

        private string RemoveInvalidXmlChars(string input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in input)
            {
                if (c == 0)
                {
                    sb.Append("<NULL>");
                }
                else if (IsLegalXmlChar(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

    }
}
