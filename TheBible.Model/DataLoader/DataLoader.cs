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
    public class DataLoader
    {
        public ObservableCollection<Translation> Translations { get; set; }
        public ObservableCollection<BookVoiceName> BookVoiceNames { get; set; }
        public event EventHandler Completed;

        async private Task LoadBookVoiceNames()
        {
            BookVoiceNames = new ObservableCollection<BookVoiceName>();

            // Load the Translation File
            Translation FileTranslation = new Translation();
            StorageFile FileStorage = await Package.Current.InstalledLocation.GetFileAsync(@"Assets\BibleVoiceNames.xml");

            Windows.Data.Xml.Dom.XmlDocument doc = await XmlDocument.LoadFromFileAsync(FileStorage);
            var rootNode = doc.DocumentElement.SelectSingleNode("/VoiceNames");
            var childNodes = rootNode.SelectNodes("Item");
            foreach(var child in childNodes)
            {
                string voiceName = child.Attributes.GetNamedItem("voiceName").InnerText;
                string actualName = child.Attributes.GetNamedItem("actualName").InnerText;
                int totalChapters = Int32.Parse(child.Attributes.GetNamedItem("totalChapters").InnerText);

                BookVoiceNames.Add(new BookVoiceName() { ActualBookName = actualName, VoiceBookName = voiceName, TotalChapters = totalChapters });
            }
        }

        async private Task<Translation> LoadXMLFile(string fileName)
        {
            // Load the Translation File
            Translation FileTranslation = new Translation();
            StorageFile FileStorage = await Package.Current.InstalledLocation.GetFileAsync(fileName);

            Windows.Data.Xml.Dom.XmlDocument doc = await XmlDocument.LoadFromFileAsync(FileStorage);
            var translationNode = doc.DocumentElement.SelectSingleNode("/Bible/Translation");
            var booksNode = doc.DocumentElement.SelectSingleNode("/Bible/Books");

            FileTranslation.TranslationShortName = translationNode.Attributes.GetNamedItem("shortName").InnerText;
            FileTranslation.TranslationLongName = translationNode.Attributes.GetNamedItem("longName").InnerText;
            FileTranslation.Books = new List<Book>();

            foreach (var book in booksNode.SelectNodes("Book"))
            {
                Book bookObject = new Book();

                var bookNumber = Int32.Parse(book.Attributes.GetNamedItem("bookNumber").InnerText);
                var bookName = book.Attributes.GetNamedItem("bookName").InnerText;
                var bookShortName = book.Attributes.GetNamedItem("bookShortName").InnerText;
                var testament = book.Attributes.GetNamedItem("testament").InnerText;
                bookObject.Index = bookNumber;
                bookObject.BookName = bookName;
                bookObject.BookShortName = bookShortName;
                bookObject.Testament = (testament == "Old") ? Testament.Old : Testament.New;

                bookObject.Chapters = new List<Chapter>();
                var chapters = book.SelectNodes("Chapter");
                foreach (var chapter in chapters)
                {
                    Chapter chapterObject = new Chapter();

                    var chapterNumber = Int32.Parse(chapter.Attributes.GetNamedItem("chapterNumber").InnerText);
                    chapterObject.Index = chapterNumber;

                    chapterObject.Verses = new List<Verse>();
                    var verses = chapter.SelectNodes("Verse");
                    foreach (var verse in verses)
                    {
                        Verse verseObject = new Verse();

                        // Even though in computers the index comes
                        // first, remember that the word of God is
                        // always more important than simply memorizing
                        // a book, chapter, and verse number. It's
                        // the living word of God.
                        // Also take heart in knowning that God is alive
                        // and inspired me to write this Bible app.
                        // He guides me daily on his paths, I am
                        // but a humble servant unto his word.
                        // Yesterday I was in the car and God turned
                        // my head on the highway just in time to see
                        // a beautiful cross I had never seen on I-45.
                        var verseNumber = Int32.Parse(verse.Attributes.GetNamedItem("verseNumber").InnerText);
                        var verseText = verse.InnerText;
                        verseObject.Index = verseNumber;
                        verseObject.Text = verseText;
                        chapterObject.Verses.Add(verseObject);
                    }
                    bookObject.Chapters.Add(chapterObject);
                }
                FileTranslation.Books.Add(bookObject);
            }

            return FileTranslation;
        }

        async public void LoadXMLDocuments()
        {
            await LoadBookVoiceNames();

            Translations = new ObservableCollection<Translation>();

            Translations.Add(await LoadXMLFile(@"Assets\KJV.xml"));
            Translations.Add(await LoadXMLFile(@"Assets\NIV.xml"));

            if (this.Completed != null)
            {
                this.Completed(this, new EventArgs());
            }
        }

        public DataLoader()
        {
            // If we don't already have the Bible loaded we'll load
            // it here from the included XML files.
            if (Translations == null)
            {
                LoadXMLDocuments();
            }
        }
    }
}
