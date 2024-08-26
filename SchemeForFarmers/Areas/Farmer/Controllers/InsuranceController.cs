using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchemeForFarmers.Areas.Farmer.Models;
using SchemeForFarmers.Models;

namespace SchemeForFarmers.Areas.Farmer.Controllers
{
    public class InsuranceController : Controller
    {
        private readonly SchemeForFarmersContext context;

        public InsuranceController(SchemeForFarmersContext context)
        {
            this.context = context;
        }

        [Area("Farmer")]
        public IActionResult Index()
        {
            return View();
        }

        [Area("Farmer")]
        public IActionResult ApplyPolicy()
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
        public JsonResult CalculateInsurancebyType(int year, string cropType, string crop, int area)
        {
            var data = context.CropInsuranceDetails.Where(x => x.CropName == crop).FirstOrDefault();

            InsuranceDetail insurance = new InsuranceDetail();

            insurance.InsuranceCompany = data.InsuranceCompany;
            insurance.SumInsured = data.SumInsured;
            insurance.SharePremium = data.SharePremium;
            insurance.PremiumAmount = ((((data.SumInsured) * (data.SharePremium)) / 100) * area);
            insurance.CropName = data.CropName;
            insurance.Area = area;
            insurance.TotalSumInsured = (data.SumInsured) * area;


            return Json(new { data = insurance });
        }

        [Area("Farmer")]
        [HttpPost]
        public IActionResult ApplyPolicy(
            int year,
            string cropTypeDropdown,
            string cropDropdown,
            string insuranceCompany,
            int sumInsured,
            decimal sharePremium,
            decimal premiumAmount,
            int area,
            int totalSumInsured
            )
        {
            InsuranceDetail insurance = new InsuranceDetail();
            insurance.Year = year;
            insurance.CropType = cropTypeDropdown;
            insurance.CropName = cropDropdown;
            insurance.InsuranceCompany = insuranceCompany;
            insurance.SumInsured = sumInsured;
            insurance.SharePremium = sharePremium;
            insurance.PremiumAmount = premiumAmount;
            insurance.Area = area;
            insurance.TotalSumInsured = totalSumInsured;
            insurance.FarmerId = (int)HttpContext.Session.GetInt32("UserSession");

            context.InsuranceDetails.Add(insurance);
            HttpContext.Session.SetString("newinsurance", "newinsurance");
            context.SaveChanges();
            return Json(new { data = insurance });

        }

        [Area("Farmer")]
        public IActionResult ClaimInsurance()
        {
            return View();
        }

        [HttpPost]
        [Area("Farmer")]
        public IActionResult ClaimInsurance(ClaimInsurance claim, string otherReason)
        {
            var insurance = new InsuranceDetail();
            insurance = context.InsuranceDetails.Find(claim.InsuranceId);
            if (insurance == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (claim.CauseOfLoss == "Others")
                {
                    claim.CauseOfLoss = otherReason;

                    if (insurance.IsVerified == true && insurance.IsClaimed != true)
                    {
                        insurance.IsClaimed = true;
                        context.InsuranceDetails.Update(insurance);
                        context.ClaimInsurances.Add(claim);
                        HttpContext.Session.SetString("newclaim", "newclaim");
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        HttpContext.Session.SetString("claimed", "claimed");
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    if (insurance.IsVerified == true && insurance.IsClaimed != true)
                    {
                        insurance.IsClaimed = true;
                        context.InsuranceDetails.Update(insurance);
                        context.ClaimInsurances.Add(claim);
                        HttpContext.Session.SetString("newclaim", "newclaim");
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        HttpContext.Session.SetString("claimed", "claimed");
                        return RedirectToAction("Index");
                    }
                }
            }

        }
    }
}
