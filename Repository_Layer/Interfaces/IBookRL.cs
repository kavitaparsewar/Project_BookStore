using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{

   public interface IBookRL
    {
        public string AddBook(BookModel bookmodel);
        public string UpdateBook(BookModel bookmodel);
        public bool DeleteBook(long BookId);
        public List<BookModel> GetAllBook();
       
    }
}
