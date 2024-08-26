using Microsoft.AspNetCore.Mvc;
using SchemeForFarmers.Areas.Bidder.Models;
using SchemeForFarmers.Areas.Farmer.Models;
using SchemeForFarmers.Models;
using System.ComponentModel;
using System;

namespace SchemeForFarmers.Areas.Farmer.Controllers
{
    public class FarmerHomeController : Controller
    {
        private readonly SchemeForFarmersContext context;
     
        public FarmerHomeController(SchemeForFarmersContext context)
        {
            this.context = context;
        }

        [Area("Farmer")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserSession");
            HttpContext.Session.SetInt32("LogOut", 4);
            return RedirectToAction("Login", "Home", new { area = "" });
        }
    }
}
