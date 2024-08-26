using System;
using System.Collections.Generic;

namespace SchemeForFarmers.Areas.Farmer.Models
{
    public partial class SellRequestView
    {
        public int FarmerId { get; set; }
        public string CropType { get; set; } = null!;
        public string CropName { get; set; } = null!;
        public int Quantity { get; set; }
        public string SoilPhCertificate { get; set; } = null!;
    }
}
