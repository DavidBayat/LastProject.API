using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    //public string GoogleId {get; set;}

    public List<Recipe>? Recipes { get; set; }
    
}
