using Microsoft.AspNetCore.Mvc;
using SchemeForFarmers.Areas.Bidder.Models;
using SchemeForFarmers.Models;
using System;

namespace SchemeForFarmers.Areas.Bidder.Controllers
{
    public class BidderHomeController : Controller
    {
        private readonly SchemeForFarmersContext context;

        public BidderHomeController(SchemeForFarmersContext context)
        {
            this.context = context;
        }


        [Area("Bidder")]
        [Route("Bidder/")]
        public IActionResult Index()
        {
                var id = HttpContext.Session.GetInt32("UserSession");
                var user = context.BidderDetails.Where(x => x.BidderId == id).FirstOrDefault();
                HttpContext.Session.SetString("UserName", user.BName);

            return View();
            //List<CropBidDetail> bidData = new List<CropBidDetail>();
            //var bid = new CropBidDetail();

            //var data = context.CropAuctionDetails.Where(x => x.IsApproved == true && x.IsSold == null).ToList();
            //if (data.Count == 0)
            //{
            //    ViewBag.record = false;
            //    return View(bidData);
            //}
            //else
            //{
            //    foreach (var item in data)
            //    {
            //        bid = context.CropBidDetails.Where(x=>x.CropId == item.CropId).OrderBy(x=>x.Bid).LastOrDefault();
            //        bidData.Add(bid);
            //    }

            //    return View(bidData);
            //}
        }

        [Area("Bidder")]
        [Route("Bidder/BidOnCrop/{id}")]
        public IActionResult BidOnCrop(int id)
        {
            var bids = context.CropBidDetails.Where(x => x.CropId == id).OrderBy(x => x.Bid).LastOrDefault();
            HttpContext.Session.SetInt32("cropId", id);
            ViewBag.CropId = id;
            return View(bids);
        }

        [Area("Bidder")]
        [Route("Bidder/BidOnCrop/{id}")]
        [HttpPost]
        public void BidOnCrop(int newBid, int cropId)
        {
            CropBidDetail bidDetail = new CropBidDetail();
            var data = context.CropAuctionDetails.Where(x => x.CropId == cropId).FirstOrDefault();
            bidDetail.BidderId = (int)HttpContext.Session.GetInt32("UserSession");
            bidDetail.CropId = cropId;
            bidDetail.CropType = data.CropType;
            bidDetail.CropName = data.CropName;
            bidDetail.BasePrice = data.BasePrice;
            bidDetail.Quantity = data.Quantity;
            bidDetail.Bid = newBid;
            context.CropBidDetails.Add(bidDetail);
            HttpContext.Session.SetString("newbid", "newbid");
            context.SaveChanges();

        }

        [Area("Bidder")]
        [HttpPost]
        public int CheckBid(int newBid, int cropId)
        {
            if (newBid == 0)
            {
                return 2;
            }

            else if (newBid % 5 == 0)
            {
                var bid = context.CropBidDetails.Where(x => x.CropId == cropId).FirstOrDefault();
                if (bid.Bid != null)
                {
                    if (newBid > bid.Bid)
                    {
                        BidOnCrop(newBid, cropId);
                        return 1;
                    }
                    else
                        return 2;
                }
                else
                {
                    if (newBid > bid.BasePrice)
                    {
                        BidOnCrop(newBid, cropId);
                        return 1;
                    }
                    else
                        return 2;
                }

            }

            else
                return 0;
        }

        [Area("Bidder")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserSession");
            HttpContext.Session.SetInt32("LogOut", 4);
            return RedirectToAction("Login", "Home", new { area = "" });
        }

        [Area("Bidder")]
        [Route("Bidder/removeSession")]
        public void removeSession()
        {
            HttpContext.Session.Remove("newbid");

        }
    }
}
