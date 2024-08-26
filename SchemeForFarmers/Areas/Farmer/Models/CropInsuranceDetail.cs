using System;
using System.Collections.Generic;

namespace SchemeForFarmers.Areas.Farmer.Models
{
    public partial class CropInsuranceDetail
    {
        public string InsuranceCompany { get; set; } = null!;
        public string CropType { get; set; } = null!;
        public string CropName { get; set; } = null!;
        public decimal SharePremium { get; set; }
        public int SumInsured { get; set; }
        public decimal ActurialRate { get; set; }
        public int Msp { get; set; }

    }
}
