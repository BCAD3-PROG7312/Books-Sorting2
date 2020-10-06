using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Sorting2
{
    class BookDetails2
    {
        //public string AuthorDetails { get; set; }
        public string BookNumber { get; set; }
        public string AuthorDetails { get; set; }
        public BookDetails2(string BookNumber, string AuthorDetails)
        {
            this.BookNumber = BookNumber;
            this.AuthorDetails = AuthorDetails;
        }
        public BookDetails2()
        {

        }
    }
}
