using Microsoft.AspNetCore.Mvc;
using SchemeForFarmers.Areas.Admin.Controllers;
using SchemeForFarmers.Areas.Bidder.Models;
using SchemeForFarmers.Areas.Farmer.Models;
using SchemeForFarmers.Models;
using System.Diagnostics;

namespace SchemeForFarmers.Controllers
{
    //Scaffold-DbContext "server=DESKTOP-28DDJPU\SQLEXPRESS; database=SchemeForFarmers; trusted_connection=true;"  Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

    public class HomeController : Controller
    {
        private readonly SchemeForFarmersContext context;
        IWebHostEnvironment env;
        public HomeController(SchemeForFarmersContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }


        public IActionResult Login()
        {
            var userId = HttpContext.Session.GetInt32("UserSession");
            if (userId != null)
            {
                var data = context.LogInViews.Where(x=>x.UserId==userId).FirstOrDefault();
                switch (data.UsertypeId)
                {
                    case 1:
                        {
                            return RedirectToAction("index", "Home", new { area = "Admin" });
                        }

                    case 2:
                        {
                            return RedirectToAction("index", "Home", new { area = "Farmer" });
                        }

                    case 3:
                        {
                            return RedirectToAction("index", "Home", new { area = "Bidder" });
                        }
                }

            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(LogInView user)
        {
            var data = context.LogInViews.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (data != null)
            {
                switch (data.UsertypeId)
                {
                    case 1:
                        {
                            HttpContext.Session.SetInt32("UserSession", data.UserId);
                            return RedirectToAction("index", "Home", new { area = "Admin" });

                        }
                    case 2:
                        {
                            var farmer = context.FarmerDetails.Where(x => x.Email == data.Email).FirstOrDefault();
                            if(farmer.IsVerified != true)
                            {
                                ViewBag.notVerified = true; 
                                return View();
                            }
                            else
                            {
                                HttpContext.Session.SetInt32("UserSession", data.UserId);
                                return RedirectToAction("index", "Home", new { area = "Farmer" });
                            }
                         
                        }
                    case 3:
                        {
                            var bidder = context.BidderDetails.Where(x => x.Email == data.Email).FirstOrDefault();
                            if (bidder.IsVerified != true)
                            {
                                ViewBag.notVerified = true;
                                return View();
                            }
                            else
                            {
                                HttpContext.Session.SetInt32("UserSession", data.UserId);
                                return RedirectToAction("index", "Home", new { area = "Bidder" });
                            }
                        }
                }
            }

            else
            {
                HttpContext.Session.SetInt32("Wrong", 1);
                return View();
            }

            return View();
        }

        public IActionResult ForgotPassword()
        {
            ForgotPassword newPassword = new ForgotPassword();
            return View(newPassword);
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassword info)
        {
            var data = context.LogInViews.Where(x => x.Email == info.Email).FirstOrDefault();
            if (info.Password != null)
            {
                if (data != null)
                {
                    switch (data.UsertypeId)
                    {
                        case 1:
                            {
                                var User = context.AdminDetails.Find(data.UserId);
                                if(User.Password == info.Password)
                                {
                                    HttpContext.Session.SetString("Old", "new");
                                    return View(info);
                                }
                                else
                                {
                                    User.Password = info.Password;
                                    context.AdminDetails.Update(User);
                                    context.SaveChanges();
                                    HttpContext.Session.SetInt32("Reset", 2);
                                    return RedirectToAction("Login");

                                }
                            }
                        case 2:
                            {
                                var User = context.FarmerDetails.Find(data.UserId);
                                if (User.Password == info.Password)
                                {
                                    HttpContext.Session.SetString("Old", "new");
                                    return View(info);
                                }
                                else
                                {
                                    User.Password = info.Password;
                                    context.FarmerDetails.Update(User);
                                    context.SaveChanges();
                                    HttpContext.Session.SetInt32("Reset", 2);
                                    return RedirectToAction("Login");
                                }
                            }
                        case 3:
                            {
                                var User = context.BidderDetails.Find(data.UserId);
                                if (User.Password == info.Password)
                                {
                                    HttpContext.Session.SetString("Old", "new");
                                    return View(info);
                                }
                                else
                                {
                                    User.Password = info.Password;
                                    context.BidderDetails.Update(User);
                                    context.SaveChanges();
                                    HttpContext.Session.SetInt32("Reset", 2);
                                    return RedirectToAction("Login");
                                }
                            }
                        default:
                            {
                                return NotFound();
                            }
                    }

                }
                else
                {
                    return NotFound();
                }
            }

            else
            {

                if (data != null)
                {
                    info.Password = "1";
                    return View(info);
                }
                else
                {

                    HttpContext.Session.SetString("Match","match");
                    return View(info);
                }
            }
        }


        public IActionResult BidderRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BidderRegistration(BidderDetail newBidder, IFormFile aadhar, IFormFile pan, IFormFile license)
        {
            string pdffilename = "";
            string pdffolder = "";
            string pdffilepath = "";
            string aadharDoc = "", panDoc = "", licenseDoc = "";

            if (aadhar != null)
            {
                pdffolder = Path.Combine(env.WebRootPath, "PDF");
                aadharDoc = Guid.NewGuid().ToString() + "_" + aadhar.FileName;
                pdffilepath = Path.Combine(pdffolder, aadharDoc);
                aadhar.CopyTo(new FileStream(pdffilepath, FileMode.Create));

                panDoc = Guid.NewGuid().ToString() + "_" + pan.FileName;
                pdffilepath = Path.Combine(pdffolder, panDoc);
                pan.CopyTo(new FileStream(pdffilepath, FileMode.Create));

                licenseDoc = Guid.NewGuid().ToString() + "_" + license.FileName;
                pdffilepath = Path.Combine(pdffolder, licenseDoc);
                license.CopyTo(new FileStream(pdffilepath, FileMode.Create));
            }

            BidderDetail bidder = new BidderDetail();

            bidder = newBidder;
            bidder.BAadhar = aadharDoc;
            bidder.BPan = panDoc;
            bidder.BLicense = licenseDoc;

            context.BidderDetails.Add(bidder);
            HttpContext.Session.SetInt32("NewUser", 3);
            context.SaveChanges();
            return RedirectToAction("Login", "Home", new { area = "" });
        }


        public IActionResult FarmerRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FarmerRegistration(FarmerDetail newFarmer, IFormFile aadhar, IFormFile pan, IFormFile certificate)
        {
            string pdffilename = "";
            string pdffolder = "";
            string pdffilepath = "";
            string aadharDoc = "", panDoc = "", certificateDoc = "";

            if (aadhar != null)
            {
                pdffolder = Path.Combine(env.WebRootPath, "PDF");
                aadharDoc = Guid.NewGuid().ToString() + "_" + aadhar.FileName;
                pdffilepath = Path.Combine(pdffolder, aadharDoc);
                aadhar.CopyTo(new FileStream(pdffilepath, FileMode.Create));

                panDoc = Guid.NewGuid().ToString() + "_" + pan.FileName;
                pdffilepath = Path.Combine(pdffolder, panDoc);
                pan.CopyTo(new FileStream(pdffilepath, FileMode.Create));

                certificateDoc = Guid.NewGuid().ToString() + "_" + certificate.FileName;
                pdffilepath = Path.Combine(pdffolder, certificateDoc);
                certificate.CopyTo(new FileStream(pdffilepath, FileMode.Create));
            }

            FarmerDetail farmer = new FarmerDetail();

            farmer = newFarmer;
            farmer.FAadhar = aadharDoc;
            farmer.FPan = panDoc;
            farmer.FCertificate = certificateDoc;
            farmer.IsVerified = false;

            context.FarmerDetails.Add(newFarmer);
            HttpContext.Session.SetInt32("NewUser", 3);
            context.SaveChanges();
            return RedirectToAction("Login", "Home", new { area = "" });
        }

        public void removeSession()
        {
            HttpContext.Session.Remove("Wrong");
            HttpContext.Session.Remove("Reset");
            HttpContext.Session.Remove("NewUser");
            HttpContext.Session.Remove("LogOut");
            HttpContext.Session.Remove("Old");
            HttpContext.Session.Remove("Match");

        }
    }

}
