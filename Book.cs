using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class Book
    {
        public int Id{ get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int YearOfPublish{ get; set; }
        public int Pages { get; set; }
        public int AuthorId { get; set; }

    }
}
