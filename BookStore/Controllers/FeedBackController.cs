using Business_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        public IFeedBackBL feedbackBL;
        public FeedBackController(IFeedBackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }


        [HttpPost]
        [Route("AddFeedback")]
        public IActionResult AddFeedback([FromBody] FeedBackModel feedbackmodel)
        {
            try
            {
                string result = this.feedbackBL.AddFeedback(feedbackmodel);
                if (result.Equals("Feedback Added "))
                {
                    return this.Ok(new UserResponse<string>() { status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetFeedback")]
        public IActionResult AllFeedBacks(int BookId)
        {
            try
            {
                var result = this.feedbackBL.AllFeedBacks(BookId);
                if (result != null)
                {
                    return this.Ok(new { status = true, Message = "Retrival successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = "Retrival unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { status = false, Message = ex.Message });
            }
        }

    }
}
