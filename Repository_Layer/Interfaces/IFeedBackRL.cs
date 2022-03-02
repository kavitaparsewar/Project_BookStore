using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface IFeedBackRL
    {
        string AddFeedback(FeedBackModel feedbackmodel);
        List<FeedBackModel> AllFeedBacks(int BookId);
    }
}
