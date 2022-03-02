using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class FeedBackModel
    {
		public int FeedBackId { get; set; }
		public int UserId { get; set; }
		public int BookId { get; set; }
		public string Comment{ get; set; }
		public int Ratings { get; set; }
		public UserRegistration User { get; set; }
	}
}
