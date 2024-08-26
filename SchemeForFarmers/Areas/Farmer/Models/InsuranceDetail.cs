using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchemeForFarmers.Areas.Farmer.Models
{
    public partial class InsuranceDetail
    {
      
        public int InsuranceId { get; set; }
      
        public int FarmerId { get; set; }
        [Required]
        public string CropType { get; set; } = null!;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage ="Area should be greater than 1")]
        public int Year { get; set; }
        [Required]
        public string CropName { get; set; } = null!;
        [Required]

        [Range(1, 10000, ErrorMessage = "Value should be between 1 and 10000")]
        public int Area { get; set; }
        public string InsuranceCompany { get; set; } = null!;
        public int SumInsured { get; set; }
        public decimal SharePremium { get; set; }
        public decimal PremiumAmount { get; set; }
        public int TotalSumInsured { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsClaimed { get; set; }
        public bool? IsApproved { get; set; }
    }
}
