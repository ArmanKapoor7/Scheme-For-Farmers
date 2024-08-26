using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchemeForFarmers.Models
{
    public partial class LogInView
    {
        public int UsertypeId { get; set; }

        [Required(ErrorMessage = "Email field is required")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Email not valid")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password field is required")]  /*For custom error message*/
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public int UserId { get; set; }
    }
}
