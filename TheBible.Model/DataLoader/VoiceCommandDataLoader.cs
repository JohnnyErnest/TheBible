using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace TheBible.Model.DataLoader
{
    public class VoiceCommandDataLoader
    {
        public ObservableCollection<BookVoiceName> BookVoiceNames { get; set; }

        async public Task LoadBookVoiceNames()
        {
            BookVoiceNames = new ObservableCollection<BookVoiceName>();

            // Load the Translation File
            Translation FileTranslation = new Translation();
            StorageFile FileStorage = await Package.Current.InstalledLocation.GetFileAsync(@"Assets\BibleVoiceNames.xml");

            Windows.Data.Xml.Dom.XmlDocument doc = await XmlDocument.LoadFromFileAsync(FileStorage);
            var rootNode = doc.DocumentElement.SelectSingleNode("/VoiceNames");
            var childNodes = rootNode.SelectNodes("Item");
            foreach (var child in childNodes)
            {
                string voiceName = child.Attributes.GetNamedItem("voiceName").InnerText;
                string actualName = child.Attributes.GetNamedItem("actualName").InnerText;
                int totalChapters = Int32.Parse(child.Attributes.GetNamedItem("totalChapters").InnerText);

                BookVoiceNames.Add(new BookVoiceName() { ActualBookName = actualName, VoiceBookName = voiceName, TotalChapters = totalChapters });
            }
        }

    }
}
