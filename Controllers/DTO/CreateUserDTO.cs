using System.ComponentModel.DataAnnotations;

public class CreateUserDTO
{
    [StringLength(500)]
    public string GoogleId {get; set;}
    [StringLength(30)]
    public string? Name { get; set; }
    [StringLength(50)]
    public string? Email {get; set;}
}