using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Sorting2
{
    class BookDetails
    {
        //public string AuthorDetails { get; set; }
        public string BookNumber_AuthorDetails { get; set; }
        public BookDetails(string BookNumber_AuthorDetails)
        {
            this.BookNumber_AuthorDetails = BookNumber_AuthorDetails;
        }
        public BookDetails()
        {

        }
    }
}
