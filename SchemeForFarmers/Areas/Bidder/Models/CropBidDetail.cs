using System;
using System.Collections.Generic;

namespace SchemeForFarmers.Areas.Bidder.Models
{
    public partial class CropBidDetail
    {
        public int BidId { get; set; }
        public int CropId { get; set; }
        public string CropType { get; set; } = null!;
        public string CropName { get; set; } = null!;
        public int Quantity { get; set; }
        public int BasePrice { get; set; }
        public int? BidderId { get; set; }
        public int? Bid { get; set; }
    }
}
