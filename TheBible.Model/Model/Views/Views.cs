using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBible.Model.Model.Views
{
    public class AllTranslations
    {
        public long TranslationID { get; set; }
        public string TranslationShortName { get; set; }
        public string TranslationLongName { get; set; }
        public int TranslationIndex { get; set; }
    }

    public class AllBooks
    {
        public long TranslationID { get; set; }
        public string TranslationShortName { get; set; }
        public string TranslationLongName { get; set; }
        public int TranslationIndex { get; set; }
        public long BookID { get; set; }
        public int BookIndex { get; set; }
        public string BookName { get; set; }
        public string BookShortName { get; set; }
        public string Testament { get; set; }
        public int TotalChapters { get; set; }
    }

    public class AllVerses
    {
        public long TranslationID { get; set; }
        public string TranslationShortName { get; set; }
        public string TranslationLongName { get; set; }
        public int TranslationIndex { get; set; }
        public long BookID { get; set; }
        public int BookIndex { get; set; }
        public string BookName { get; set; }
        public string BookShortName { get; set; }
        public string Testament { get; set; }
        public int TotalChapters { get; set; }
        public long VerseID { get; set; }
        public int ChapterIndex { get; set; }
        public int VerseIndex { get; set; }
        public string VerseText { get; set; }
    }

    public class GenKJVOne
    {
        public long TranslationID { get; set; }
        public string TranslationShortName { get; set; }
        public string TranslationLongName { get; set; }
        public int TranslationIndex { get; set; }
        public long BookID { get; set; }
        public int BookIndex { get; set; }
        public string BookName { get; set; }
        public string BookShortName { get; set; }
        public string Testament { get; set; }
        public int TotalChapters { get; set; }
        public long VerseID { get; set; }
        public int ChapterIndex { get; set; }
        public int VerseIndex { get; set; }
        public string VerseText { get; set; }
    }
}
