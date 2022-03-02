using Business_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class FeedBackBL : IFeedBackBL
    {
        IFeedBackRL feedbackRL;
        public FeedBackBL(IFeedBackRL feedbackBL)
        {
            this.feedbackRL = feedbackBL;
        }

        public string AddFeedback(FeedBackModel feedbackmodel)
        {
            try
            {
                return this.feedbackRL.AddFeedback(feedbackmodel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<FeedBackModel> AllFeedBacks(int BookId)
        {
            try
            {
                return this.feedbackRL.AllFeedBacks(BookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
