using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBible.Model
{
    public class Chapter
    {
        /// <summary>
        /// The Chapter number e.g. James 5
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// A listing of Verses within the Chapter.
        /// </summary>
        public List<Verse> Verses;

        /// <summary>
        /// The following property retrieves the text for the chapter
        /// with numbers prepended.
        /// </summary>
        public string Text
        {
            get
            {
                int verseNumber = 1;
                StringBuilder sb = new StringBuilder();
                foreach(Verse verse in Verses)
                {
                    sb.Append(String.Format("{0}. {1} ",verseNumber, verse.Text));
                    verseNumber++;
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// This property is the same as Text but appends a 
        /// new line after each verse.
        /// </summary>
        public string TextLineSeparated
        {
            get
            {
                int verseNumber = 1;
                StringBuilder sb = new StringBuilder();
                foreach (Verse verse in Verses)
                {
                    sb.AppendLine(String.Format("{0}. {1}", verseNumber, verse.Text));
                    verseNumber++;
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Returns the Chapter text (non-line separated);
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
