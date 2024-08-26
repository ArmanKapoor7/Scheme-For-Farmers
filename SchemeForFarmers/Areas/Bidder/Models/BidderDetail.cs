using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchemeForFarmers.Areas.Bidder.Models
{
    public partial class BidderDetail
    {
        public int UsertypeId { get; set; }
        public int BidderId { get; set; }

        [Required(ErrorMessage = "Name field is required")]
        public string BName { get; set; } = null!;

        [Required(ErrorMessage = "Contact field is required")]
        [RegularExpression("^(?:(?:\\+|0{0,2})91(\\s*[\\-]\\s*)?|[0]?)?[6789]\\d{9}$", ErrorMessage = "Mobile No Invalid")]
        public string BContact { get; set; } = null!;

        [Required(ErrorMessage = "Address field is required")] 
        public string BAddress { get; set; } = null!;

        [Required(ErrorMessage = "Account No. field is required")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Account No should be 12 numbers")]
        public string BAccountNo { get; set; } = null!;

        [Required(ErrorMessage = "IFSC Code field is required")]
        public string BIfscCode { get; set; } = null!;

        [Required(ErrorMessage = "Aadhar Card field is required")]
        public string BAadhar { get; set; } = null!;

        [Required(ErrorMessage = "Pan Card field is required")]
        public string BPan { get; set; } = null!;

        [Required(ErrorMessage = "Trader's License field is required")]
        public string BLicense { get; set; } = null!;
        public bool? IsVerified { get; set; }

        [Required(ErrorMessage = "Email field is required")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Email not valid")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password field is required")]  
        [RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Password must contain Uppercase,Lowercase,Number and special character. Password length must be greater than 7 ")]
        public string Password { get; set; } = null!;
    }
}
