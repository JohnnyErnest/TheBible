using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBible.ViewModel
{
    public class BookNames
    {
        public string BookName { get; set; }

        // Thanks to Kevin Gosse on the following StackExchange:
        // http://stackoverflow.com/questions/42741843/uwp-setting-a-comboboxs-selecteditem-when-the-itemsource-is-linq/42742024#42742024

        public override bool Equals(object obj)
        {
            var bookNames = obj as BookNames;

            return bookNames != null && this.BookName.Equals(bookNames.BookName);
        }

        public override int GetHashCode()
        {
            return this.BookName?.GetHashCode() ?? 0;

        }
    }
}
