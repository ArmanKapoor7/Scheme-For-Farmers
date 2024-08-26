using System.ComponentModel.DataAnnotations;

namespace SchemeForFarmers.Models
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Email field is required")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Email not valid")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password field is required")]  /*For custom error message*/
        [RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Password must contain Uppercase,Lowercase,Number and special character. Password length must be greater than 7 ")]
        public string Password { get; set; } = null!;
    }
}
