using System.ComponentModel.DataAnnotations;

namespace WebApiShop
{
    public class LogInUser
    {
        [EmailAddress, Required]
        public string UserName { get; set; }
       
        [StringLength(8, ErrorMessage = "password Can be between 4 till 8 chars", MinimumLength = 4), Required]
        public string Password { get; set; }
    }
}
