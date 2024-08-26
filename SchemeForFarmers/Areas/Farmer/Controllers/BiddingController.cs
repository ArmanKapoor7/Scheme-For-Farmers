using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchemeForFarmers.Areas.Farmer.Models;
using SchemeForFarmers.Areas.Bidder.Models;
using SchemeForFarmers.Models;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System;

namespace SchemeForFarmers.Areas.Farmer.Controllers
{
    public class BiddingController : Controller
    {
        private readonly SchemeForFarmersContext context;
        IWebHostEnvironment env;

        public BiddingController(SchemeForFarmersContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        [Area("Farmer")]
        [Route("Farmer/")]
        [Route("Farmer/Bidding")]
        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("UserSession");
            var user = context.FarmerDetails.Where(x => x.FarmerId == id).FirstOrDefault();
            HttpContext.Session.SetString("UserName", user.FName);
            return View();
        }

        [Area("Farmer")]
        public IActionResult CropSellRequest()
        {
            CropInsuranceDetailViewModel viewModel = new CropInsuranceDetailViewModel();
            viewModel.CropTypeList = new List<SelectListItem>();
            viewModel.CropTypeList.Add(new SelectListItem { Text = "Select Crop Type", Value = "" });
            viewModel.CropTypeList.Add(new SelectListItem { Text = "Kharif", Value = "Kharif" });
            viewModel.CropTypeList.Add(new SelectListItem { Text = "Rabi", Value = "Rabi" });


            return View(viewModel);
        }


        [Area("Farmer")]
        [HttpPost]
        public JsonResult GetCropListbyType(string type)
        {
            var crops = context.CropInsuranceDetails.Where(x => x.CropType == type).ToList();

            return Json(new { data = crops });
        }

        [Area("Farmer")]
        [HttpPost]
        public IActionResult CropSellRequest(CropInsuranceDetailViewModel cropDetail, string cropDropdown, IFormFile ph)
        {
            string pdffilename = "";
            string pdffolder = "";
            string pdffilepath = "";
            string phDoc = "";

            if (phDoc != null)
            {
                pdffolder = Path.Combine(env.WebRootPath, "PDF");
                phDoc = Guid.NewGuid().ToString() + "_" + ph.FileName;
                pdffilepath = Path.Combine(pdffolder, phDoc);
                ph.CopyTo(new FileStream(pdffilepath, FileMode.Create));

            }


            var crop = context.CropInsuranceDetails.Where(x => x.CropName == cropDropdown).FirstOrDefault();
            var newCrop = new CropAuctionDetail();

            newCrop = cropDetail.cropAuctionDetail;
            newCrop.FarmerId = (int)HttpContext.Session.GetInt32("UserSession");
            newCrop.CropName = cropDropdown;
            newCrop.Msp = crop.Msp;
            newCrop.SoilPhCertificate = phDoc;
            var basePrice = (((crop.Msp) * cropDetail.cropAuctionDetail.Quantity) / 1000);
            if (basePrice > 5)
                newCrop.BasePrice = basePrice - (basePrice % 5);
            else 
                newCrop.BasePrice = basePrice;


            context.CropAuctionDetails.Add(newCrop);
            HttpContext.Session.SetString("cropauction", "cropauction");
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Area("Farmer")]
        public IActionResult SoldCropHistory()
        {
            var id = HttpContext.Session.GetInt32("UserSession");
            var soldCropsbyID = context.CropAuctionDetails.Where(x => x.IsSold == true && x.FarmerId == id).ToList();
            
            if(soldCropsbyID.Count == 0)
            {
                ViewBag.record = false;
                return View(soldCropsbyID);
            }
            else
                return View(soldCropsbyID);
        }

        [Area("Farmer")]
        public IActionResult Marketplace()
        {
            var id = HttpContext.Session.GetInt32("UserSession");
            var cropInAuction = context.CropAuctionDetails.Where(x => x.FarmerId == id && x.IsApproved == true && x.IsSold == null).ToList();

            if(cropInAuction.Count == 0)
            {
                ViewBag.record = false;
                return View(cropInAuction);
            }
            else
                return View(cropInAuction);
        }

        [Area("Farmer")]
        public IActionResult CropMarketplace(int id)
        {
            var cropDetail = context.CropAuctionDetails.Where(x => x.CropId == id).FirstOrDefault();
            var currentBid = context.CropBidDetails.Where(x => x.CropId == id).OrderBy(y => y.Bid).LastOrDefault();
            List<CropBidDetail> bidList = new List<CropBidDetail>();
            if (currentBid!= null)
            {
                bidList = context.CropBidDetails.Where(x => x.CropId == id && x.Bid != currentBid.Bid).OrderByDescending(y => y.Bid).ToList();
            }
            else
            {
                currentBid = new CropBidDetail();
                currentBid.Bid = null;
                bidList = context.CropBidDetails.Where(x => x.CropId == id).ToList();

            }

            MarketPlaceViewModel marketPlace = new MarketPlaceViewModel()
            {
                CropDetail = cropDetail,
                CurrentBid = currentBid,
                Bids = bidList,
            };

            return View(marketPlace);
        }

        [Area("Farmer")]
        public void removeSession()
        {
            HttpContext.Session.Remove("cropauction");
            HttpContext.Session.Remove("newinsurance");
            HttpContext.Session.Remove("newclaim");
            HttpContext.Session.Remove("claimed");

        }
    }
}
