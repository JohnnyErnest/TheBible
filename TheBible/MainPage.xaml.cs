using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TheBible.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TheBible
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// This is a helper property for setting the Translation SelectedIndex
        /// </summary>
        private int TranslationIndex
        {
            get
            {
                return cmb_Translation.SelectedIndex;
            }
            set
            {
                cmb_Translation.SelectedIndex = value;
            }
        }

        /// <summary>
        /// This is a helper property for setting the Book SelectedIndex
        /// </summary>
        private int BookIndex
        {
            get
            {
                return cmb_Book.SelectedIndex;
            }
            set
            {
                cmb_Book.SelectedIndex = value;
            }
        }

        /// <summary>
        /// This is a helper property for setting the Chapter SelectedIndex
        /// </summary>
        private int ChapterIndex
        {
            get
            {
                return cmb_Chapter.SelectedIndex;
            }
            set
            {
                cmb_Chapter.SelectedIndex = value;
            }
        }

        /// <summary>
        /// Retrieves the currently selected Chapter listing
        /// </summary>
        public IEnumerable<ChapterIndex> CurrentChapters
        {
            get
            {
                return from c in dataLoader.Translations[TranslationIndex].Books[BookIndex].Chapters select new ChapterIndex { Index = c.Index };
            }
        }

        /// <summary>
        /// Retrieves Genesis in the first loaded translation
        /// </summary>
        public IEnumerable<ChapterIndex> Genesis
        {
            get
            {
                return from c in dataLoader.Translations[0].Books[0].Chapters select new ChapterIndex { Index = c.Index };
            }
        }

        public ObservableCollection<BookNames> CurrentBooks
        {
            get
            {
                return new ObservableCollection<BookNames>(from b in dataLoader.Translations[TranslationIndex].Books select new BookNames { BookName = b.BookName });
            }
        }

        public ObservableCollection<BookNames> BooksInFirstTranslation
        {
            get
            {
                return new ObservableCollection<BookNames>(from b in dataLoader.Translations[0].Books select new BookNames { BookName = b.BookName });
            }
        }

        public ObservableCollection<TranslationsSources> CurrentTranslations
        {
            get
            {
                //return from t in dataLoader.Translations select new TranslationsSources { TranslationShortName = t.TranslationShortName };
                return new ObservableCollection<TranslationsSources>(from t in dataLoader.Translations select new TranslationsSources { TranslationShortName = t.TranslationShortName });
            }
        }

        /// <summary>
        /// Allows events to process on ComboBoxes and Back/Forward Buttons
        /// to change Chapters, you usually don't want to do this lots of
        /// times in one second if changing the Translation/Book/Chapter
        /// all at one time so you may set it to false first, update your
        /// variables, and then set it back to true so updating events will
        /// process correctly.
        /// </summary>
        public bool UpdateChapterText { get; set; }

        /// <summary>
        /// This property lets you know when all of the data has been loaded by the app.
        /// </summary>
        public bool DataLoaded { get; set; }

        /// <summary>
        /// The DataLoader object loads up the various Bible translation texts
        /// </summary>
        TheBible.Model.DataLoader.DataLoader dataLoader;

        public MainPage()
        {
            this.InitializeComponent();
            dataLoader = new Model.DataLoader.DataLoader();
            dataLoader.Completed += DataLoader_Completed;
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
        }

        public async void SetBookChapter(string book, int? chapter)
        {
            while (!DataLoaded)
            {
                await Task.Delay(100);
            }

            Model.BookVoiceName voice = dataLoader.BookVoiceNames.FirstOrDefault(b => b.VoiceBookName.ToLower() == book.ToLower());
            if (voice.TotalChapters >= chapter)
            {
                UpdateChapterText = false;
                BookNames newBook2 = new BookNames() { BookName = voice.ActualBookName };
                cmb_Book.SelectedItem = newBook2;
                cmb_Chapter.ItemsSource = CurrentChapters;
                UpdateChapterText = true;
                if (!chapter.HasValue)
                    chapter = 1;
                if (cmb_Chapter.Items.Count < chapter.Value)
                    chapter = cmb_Chapter.Items.Count;
                cmb_Chapter.SelectedIndex = chapter.Value - 1;
            }
        }

        private void DataLoader_Completed(object sender, EventArgs e)
        {
            DataLoaded = true;
            UpdateChapterText = false;
            cmb_Translation.ItemsSource = CurrentTranslations;
            cmb_Book.ItemsSource = BooksInFirstTranslation;
            cmb_Chapter.ItemsSource = Genesis;
            cmb_Translation.SelectedIndex = 0;
            cmb_Book.SelectedIndex = 0;
            UpdateChapterText = true;
            cmb_Chapter.SelectedIndex = 0;
        }
        
        private void translationChanged()
        {
            chapterChanged();
        }

        private void bookChanged()
        {
            UpdateChapterText = false;
            cmb_Chapter.ItemsSource = CurrentChapters;
            UpdateChapterText = true;
            cmb_Chapter.SelectedIndex = 0;
        }

        private void chapterChanged()
        {
            textBlock_Verses.Text = dataLoader.Translations[TranslationIndex].Books[BookIndex].Chapters[ChapterIndex].TextLineSeparated;
        }

        private void decrementChapter()
        {
            UpdateChapterText = false;
            if (this.cmb_Chapter.SelectedIndex == 0)
            {
                if (this.cmb_Book.SelectedIndex > 0)
                {
                    this.cmb_Book.SelectedIndex--;
                    UpdateChapterText = true;
                    this.cmb_Chapter.SelectedIndex = CurrentChapters.Count() - 1;
                }
            }
            else
            {
                UpdateChapterText = true;
                this.cmb_Chapter.SelectedIndex--;
            }
            UpdateChapterText = true;
        }

        private void incrementChapter()
        {
            UpdateChapterText = false;
            if (this.cmb_Chapter.SelectedIndex == this.cmb_Chapter.Items.Count - 1)
            {
                if (this.cmb_Book.SelectedIndex < this.cmb_Book.Items.Count - 1)
                {
                    this.cmb_Book.SelectedIndex++;
                    UpdateChapterText = true;
                    this.cmb_Chapter.SelectedIndex = 0;
                }
            }
            else
            {
                UpdateChapterText = true;
                this.cmb_Chapter.SelectedIndex++;
            }
            UpdateChapterText = true;
        }

        private void cmb_Translation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UpdateChapterText)
                translationChanged();
        }

        private void cmb_Book_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UpdateChapterText)
                bookChanged();
        }

        private void cmb_Chapter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UpdateChapterText)
                chapterChanged();
        }

        private void btn_Forward_Click(object sender, RoutedEventArgs e)
        {
            incrementChapter();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            decrementChapter();
        }

        private void btn_FullScreen_Click(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
            {
                sym_FullScreen.Symbol = Symbol.FullScreen;
                view.ExitFullScreenMode();
            }
            else
            {
                sym_FullScreen.Symbol = Symbol.BackToWindow;
                view.TryEnterFullScreenMode();
            }
        }
    }
}
