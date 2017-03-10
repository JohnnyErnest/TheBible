using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBible.Model
{
    public class Book
    {
        /// <summary>
        /// The name of the Book i.e. 1 John
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// The short name of the Book
        /// </summary>
        public string BookShortName { get; set; }

        /// <summary>
        /// The index of the Book in the Bible i.e. Genesis = 1, Exodus = 2
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// An enumeration of the Book whether Old or New Testament
        /// </summary>
        public Testament Testament { get; set; }

        /// <summary>
        /// A listing of Chapters in the Book
        /// </summary>
        public List<Chapter> Chapters { get; set; }
    }
}
