#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
// Add this using statement to access NotMapped
using System.ComponentModel.DataAnnotations.Schema;
namespace LoginAndReg.Models;
public class User
{        
    [Key]        
    public int UserId { get; set; }
    
    [Required]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    
    [Required]        
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }         
    
    [Required]
    [EmailAddress(ErrorMessage = "Email must be valid!")]
    [UniqueEmail]
    public string Email { get; set; }        
    
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password { get; set; }          
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;        
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    
    // This does not need to be moved to the bottom
    // But it helps make it clear what is being mapped and what is not
    [NotMapped]
    // There is also a built-in attribute for comparing two fields we can use!
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords must match")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
}