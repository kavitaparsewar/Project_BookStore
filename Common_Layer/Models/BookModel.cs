using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class BookModel
    {	
    public long BookId { get; set; }
	public string BookName { get; set; }
	public string AuthorName { get; set; }
	public long TotalRating { get; set; }
	public long RatedCount { get; set; }
    public long DiscountPrice { get; set; }
    public long OriginalPrice  { get; set; }
    public string Description { get; set; }
    public string BookImage { get; set; }
    public long Quantity { get; set; }

	}
}
