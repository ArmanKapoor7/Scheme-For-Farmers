using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchemeForFarmers.Areas.Farmer.Models
{
    public class CropInsuranceDetailViewModel
    {
        public CropAuctionDetail cropAuctionDetail { get; set; }

        public List<SelectListItem> CropTypeList { get; set; }

        public InsuranceDetail insuranceDetail { get; set; }
    }
}
