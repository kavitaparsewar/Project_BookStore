using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface IFeedBackBL
    {
        string AddFeedback(FeedBackModel feedbackmodel);
        List<FeedBackModel> AllFeedBacks(int BookId);
    }

}
