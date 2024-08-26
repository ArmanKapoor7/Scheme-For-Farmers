using Microsoft.AspNetCore.Mvc;
using SchemeForFarmers.Areas.Admin.Models;
using SchemeForFarmers.Areas.Bidder.Models;
using SchemeForFarmers.Areas.Farmer.Models;
using SchemeForFarmers.Models;

namespace SchemeForFarmers.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly SchemeForFarmersContext context;

        public AdminHomeController(SchemeForFarmersContext context)
        {
            this.context = context;
        }


        [Area("Admin")]
        [Route("Admin/")]
        public IActionResult Index()
        {
            return View();
        }


        [Area("Admin")]
        public IActionResult FarmerDetails()
        {
            return View(context.FarmerDetails.ToList());
        }


        [Area("Admin")]
        public IActionResult BidderDetails()
        {
            return View(context.BidderDetails.ToList());
        }


        [Area("Admin")]
        public IActionResult CropsDetails()
        {
            return View(context.CropAuctionDetails.ToList());
        }


        [Area("Admin")]
        public IActionResult CropsBidsDetails()
        {
            return View(context.CropAuctionDetails.Where(x => x.IsApproved == true).ToList());
        }

        [Area("Admin")]
        public IActionResult BidDetails(int id)
        {
            return View(context.CropBidDetails.Where(x => x.CropId == id).ToList());
        }

        [Area("Admin")]
        public IActionResult InsuranceDetails()
        {
            return View(context.InsuranceDetails.ToList());
        }


        [Area("Admin")]
        public IActionResult AuctionRoom()
        {
            return View();
        }


        [Area("Admin")]
        public IActionResult StopAuction(int id)
        {
            var lastBid = context.CropBidDetails.Where(x => x.CropId == id).OrderBy(x => x.Bid).LastOrDefault();
            var crop = context.CropAuctionDetails.Where(x => x.CropId == id).FirstOrDefault();
            crop.IsSold = true;
            crop.SoldDate = DateTime.Now;
            if (lastBid.Bid != null)
            {
                crop.SoldPrice = lastBid.Bid;
            }
            else
            {
                crop.SoldPrice = 0;
            }

            crop.TotalPrice = crop.SoldPrice + ((crop.Msp / 100) * crop.Quantity);
            context.CropAuctionDetails.Update(crop);
            context.SaveChanges();
            return RedirectToAction("AuctionRoom");
        }


        [Area("Admin")]
        public IActionResult ClaimInsuranceDetails()
        {
            return View(context.ClaimInsurances.ToList());
        }


        [Area("Admin")]
        public IActionResult UserValidation()
        {
            var newFarmer = context.FarmerDetails.Where(x => x.IsVerified == null).ToList();
            var newBidder = context.BidderDetails.Where(x => x.IsVerified == null).ToList();

            if (newFarmer.Count == 0)
                ViewBag.Frecord = false;

            if (newBidder.Count == 0)
                ViewBag.Brecord = false;

            AdminViewModel adminView = new AdminViewModel()
            {
                Farmers = newFarmer,
                Bidders = newBidder,
            };
            return View(adminView);
        }

        [Area("Admin")]
        public IActionResult ApproveUser(int id)
        {
            var farmer = context.FarmerDetails.Where(x => x.FarmerId == id).FirstOrDefault();
            var bidder = context.BidderDetails.Where(x => x.BidderId == id).FirstOrDefault();
            var cropAuction = context.CropAuctionDetails.Where(x => x.CropId == id).FirstOrDefault();
            var insuranceDetail = context.InsuranceDetails.Where(x => x.InsuranceId == id).FirstOrDefault();
            var claimInsurance = context.ClaimInsurances.Where(x => x.ClaimId == id).FirstOrDefault();

            if (farmer != null)
            {
                farmer.IsVerified = true;
                context.FarmerDetails.Update(farmer);
                context.SaveChanges();
            }
            if (bidder != null)
            {
                bidder.IsVerified = true;
                context.BidderDetails.Update(bidder);
                context.SaveChanges();
            }
            else if (cropAuction != null)
            {
                cropAuction.IsApproved = true;
                var newCropForAuction = new CropBidDetail()
                {
                    CropId = cropAuction.CropId,
                    CropType = cropAuction.CropType,
                    CropName = cropAuction.CropName,
                    Quantity = cropAuction.Quantity,
                    BasePrice = cropAuction.BasePrice,
                    BidderId = null,
                    Bid = null
                };
                context.CropBidDetails.Add(newCropForAuction);
                context.CropAuctionDetails.Update(cropAuction);
                context.SaveChanges();
            }
            else if (insuranceDetail != null)
            {
                insuranceDetail.IsVerified = true;
                context.InsuranceDetails.Update(insuranceDetail);
                context.SaveChanges();
            }
            else if (claimInsurance != null)
            {
                claimInsurance.IsApproved = true;
                var data = context.InsuranceDetails.Find(claimInsurance.InsuranceId);
                data.IsApproved = true;
                context.InsuranceDetails.Update(data);
                context.ClaimInsurances.Update(claimInsurance);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Area("Admin")]
        public IActionResult RejectUser(int id)
        {
            var farmer = context.FarmerDetails.Where(x => x.FarmerId == id).FirstOrDefault();
            var bidder = context.BidderDetails.Where(x => x.BidderId == id).FirstOrDefault();
            var cropAuction = context.CropAuctionDetails.Where(x => x.CropId == id).FirstOrDefault();
            var insuranceDetail = context.InsuranceDetails.Where(x => x.InsuranceId == id).FirstOrDefault();
            var claimInsurance = context.ClaimInsurances.Where(x => x.ClaimId == id).FirstOrDefault();

            if (farmer != null)
            {
                farmer.IsVerified = false;
                context.FarmerDetails.Update(farmer);
                context.SaveChanges();
            }
            else if (bidder != null)
            {
                bidder.IsVerified = false;
                context.BidderDetails.Update(bidder);
                context.SaveChanges();
            }
            else if (cropAuction != null)
            {
                cropAuction.IsApproved = false;
                context.CropAuctionDetails.Update(cropAuction);
                context.SaveChanges();
            }
            else if (insuranceDetail != null)
            {
                insuranceDetail.IsVerified = false;
                context.InsuranceDetails.Update(insuranceDetail);
                context.SaveChanges();
            }
            else if (claimInsurance != null)
            {
                claimInsurance.IsApproved = false;
                var data = context.InsuranceDetails.Find(claimInsurance.InsuranceId);
                data.IsApproved = false;
                context.InsuranceDetails.Update(data);
                context.ClaimInsurances.Update(claimInsurance);
                context.SaveChanges();
            }
            return View("index");
        }



        [Area("Admin")]
        public IActionResult CropsAuctionValidation()
        {
            var data = context.CropAuctionDetails.Where(x => x.IsApproved == null).ToList();
            if (data.Count == 0)
                ViewBag.record = false;

            return View(data);
        }


        [Area("Admin")]
        public IActionResult InsuranceValidation()
        {
            var data = context.InsuranceDetails.Where(x => x.IsVerified == null).ToList();
            if (data.Count == 0)
                ViewBag.record = false;

            return View(data);
        }


        [Area("Admin")]
        public IActionResult ClaimInsuranceValidation()
        {
            var data = context.ClaimInsurances.Where(x => x.IsApproved == null).ToList();
            if (data.Count == 0)
                ViewBag.record = false;

            return View(data);
        }


        [Area("Admin")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserSession");
            HttpContext.Session.SetInt32("LogOut", 4);
            return RedirectToAction("Login", "Home", new { area = "" });
        }
    }
}
