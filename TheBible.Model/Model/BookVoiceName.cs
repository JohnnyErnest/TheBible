using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBible.Model
{
    /// <summary>
    /// This is an intermediary class that defines a set of Books and their related voice names
    /// </summary>
    public class BookVoiceName
    {
        public string VoiceBookName { get; set; }
        public string ActualBookName { get; set; }
        public int TotalChapters { get; set; }
    }
}
