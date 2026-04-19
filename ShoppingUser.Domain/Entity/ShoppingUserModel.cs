using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ShoppingUser", Schema = "ShoppingUserSchema")]
public class ShoppingUserModel
{
    [Key]
    public Guid UserId {get; set;}
    [CustomPasswordValidation]
    public required string Password {get; set;}
    public required string Email {get; set;}
    [MaxLength(20, ErrorMessage = "Name should not exceeds 20 chars.")]
    [Required(ErrorMessage = "Name is Mandatory")]

    public required string Name {get; set;}
    public required string Phone {get; set;}

}

// Attribute level anootation validation
public class CustomPasswordValidation: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var password = value.ToString();
        if(password.Length > 20)
        {
            return new ValidationResult("Password should not exceed 20 chars.");
        }
        if (password.StartsWith("$"))
        {
            return new ValidationResult("Password should not start with $ symbol");
        }
        return ValidationResult.Success;
    }
}
