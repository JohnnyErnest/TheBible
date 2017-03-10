using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBible.Model
{
    public class Verse
    {
        /// <summary>
        /// The Verse Number e.g. James 5:1
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The Verses of the Living Word of God
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Returns The Verses of the Living Word of God
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
