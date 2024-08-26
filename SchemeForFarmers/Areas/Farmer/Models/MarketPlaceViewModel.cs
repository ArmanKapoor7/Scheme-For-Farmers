using SchemeForFarmers.Areas.Bidder.Models;
using SchemeForFarmers.Areas.Farmer.Models;
using System;
using System.Collections.Generic;

namespace SchemeForFarmers.Areas.Farmer.Models
{
    public partial class MarketPlaceViewModel
    {
        public CropAuctionDetail CropDetail { get; set; }

        public CropBidDetail CurrentBid { get; set; }

        public List<CropBidDetail> Bids { get; set; }
    }
}
