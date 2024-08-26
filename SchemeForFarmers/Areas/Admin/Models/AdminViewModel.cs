using SchemeForFarmers.Areas.Bidder.Models;
using SchemeForFarmers.Areas.Farmer.Models;

namespace SchemeForFarmers.Areas.Admin.Models
{
    public class AdminViewModel
    {
        public List<FarmerDetail> Farmers { get; set; }
        public List<BidderDetail> Bidders { get; set; }
        public List<CropAuctionDetail> CropsForAuction  { get; set; }
        public List<CropBidDetail> CropsBids { get; set; }
        public List<InsuranceDetail> Insurances { get; set; }
        public List<ClaimInsurance> InsurancesForClaim { get; set; }

    }
}
