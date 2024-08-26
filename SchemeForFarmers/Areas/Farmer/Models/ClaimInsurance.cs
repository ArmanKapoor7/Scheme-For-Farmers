using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchemeForFarmers.Areas.Farmer.Models
{
    public partial class ClaimInsurance
    {
        public int ClaimId { get; set; }
        public int InsuranceId { get; set; }
        public string InsuranceCompany { get; set; } = null!;
        public decimal TotalSumInsured { get; set; }
        public string FarmerName { get; set; } = null!;
        public string CauseOfLoss { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfLoss { get; set; }
        public bool? IsApproved { get; set; }
    }
}
