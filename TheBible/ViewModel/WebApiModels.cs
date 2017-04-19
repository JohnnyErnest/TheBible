using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBible.ViewModel.WebApiModels
{
    public class Translations
    {
        [JsonProperty("id")]
        public long ID { get; set; }
        [JsonProperty("translationShortName")]
        public string TranslationShortName { get; set; }
        [JsonProperty("translationLongName")]
        public string TranslationLongName { get; set; }
    }

    public class Books
    {
        [JsonProperty("id")]
        public long ID { get; set; }
        [JsonProperty("bookName")]
        public string BookName { get; set; }
        [JsonProperty("bookShortName")]
        public string BookShortName { get; set; }
        [JsonProperty("bookIndex")]
        public int BookIndex { get; set; }
        [JsonProperty("testament")]
        public string Testament { get; set; }
        [JsonProperty("totalChapters")]
        public int TotalChapters { get; set; }
        [JsonProperty("translationID")]
        public long TranslationID { get; set; }
    }

    public class Verses
    {
        [JsonProperty("id")]
        public long ID { get; set; }
        [JsonProperty("bookID")]
        public long BookID { get; set; }
        [JsonProperty("chapterIndex")]
        public int ChapterIndex { get; set; }
        [JsonProperty("verseIndex")]
        public int VerseIndex { get; set; }
        [JsonProperty("verseText")]
        public string VerseText { get; set; }
    }
}
