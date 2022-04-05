using System.ComponentModel.DataAnnotations;

public class CreatedRecipeDTO 
{
    public string idMeal { get; set; }
    [StringLength(30)]
    public string strMeal { get; set; }
    [StringLength(100)]
    public string strMealThumb { get; set; }
    [StringLength(500)]
    // [ForeignKey("User")]
    public string GoogleId {get; set; }
}