using Business_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class BookBL : IBookBL
    {
        IBookRL bookRL;
        public BookBL(IBookRL bookBL)
        {
            this.bookRL = bookBL;
        }
        public string AddBook(BookModel bookmodel)
        {
            try
            {
                return bookRL.AddBook(bookmodel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UpdateBook(BookModel bookmodel)
        {
            try
            {
                return bookRL.UpdateBook(bookmodel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteBook(long BookId)
        {
            try
            {
                if (bookRL.DeleteBook(BookId))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<BookModel> GetAllBook()
        {
            try
            {
                return bookRL.GetAllBook();
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
