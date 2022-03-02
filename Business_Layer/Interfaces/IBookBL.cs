using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface IBookBL
    {
        public string AddBook(BookModel bookmodel);      
        public string UpdateBook( BookModel bookmodel);
        public bool DeleteBook(long BookId);
        public List<BookModel> GetAllBook();

    }
}
