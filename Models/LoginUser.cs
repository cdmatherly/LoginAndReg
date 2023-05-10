#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
// Add this using statement to access NotMapped
using System.ComponentModel.DataAnnotations.Schema;
namespace LoginAndReg.Models;
[NotMapped]
public class LoginUser
{
    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string LoginEmail { get; set; }
    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string LoginPassword { get; set; }
}