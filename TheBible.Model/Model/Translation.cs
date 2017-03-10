using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBible.Model
{
    public class Translation
    {
        /// <summary>
        /// The short name of the Translation i.e. NIV, KJV
        /// </summary>
        public string TranslationShortName { get; set; }

        /// <summary>
        /// The long name of the Translation i.e. New International Version
        /// </summary>
        public string TranslationLongName { get; set; }

        /// <summary>
        /// The Books of the Translation containing the text of the Bible
        /// </summary>
        public List<Book> Books;
    }
}
