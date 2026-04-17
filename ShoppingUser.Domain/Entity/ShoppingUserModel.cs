using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ShoppingUser", Schema = "ShoppingUserSchema")]
public class ShoppingUserModel
{
    [Key]
    public Guid UserId {get; set;}
    public required string Password {get; set;}
    public required string Email {get; set;}
    public required string Name {get; set;}
    public required string Phone {get; set;}

}
