using Business_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookController : ControllerBase
    {
        public IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }    
        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook(BookModel bookmodel)
        {
            try
            {
                var result = bookBL.AddBook(bookmodel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Book Added" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sorry,Book not added" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("UpdateBook")]
        public IActionResult UpdateBook(BookModel bookmodel)
        {

            try
            {
                var result = bookBL.UpdateBook(bookmodel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Updated Book Details" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sorry,Book not Updated" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(long BookId)
        {
            try
            {
                if (bookBL.DeleteBook(BookId))
                {
                    return this.Ok(new { Success = true, message = "Book removed" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("GetAllBooks")]
        public List<BookModel> GetAllBook()
        {
            try
            {
                return bookBL.GetAllBook();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
