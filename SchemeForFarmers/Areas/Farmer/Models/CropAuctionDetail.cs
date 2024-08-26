using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchemeForFarmers.Areas.Farmer.Models
{
    public partial class CropAuctionDetail
    {
        public int CropId { get; set; }
        public int FarmerId { get; set; }
        public string CropType { get; set; } = null!;
        public string CropName { get; set; } = null!;

        [Range(1,1000, ErrorMessage = "Value should be between 1 and 1000")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "doc required")]
        public string SoilPhCertificate { get; set; } = null!;
        public int Msp { get; set; }
        public int BasePrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? SoldDate { get; set; }
        public int? SoldPrice { get; set; }
        public int? TotalPrice { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsSold { get; set; }
    }
}
