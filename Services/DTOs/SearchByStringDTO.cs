using System.Text.Json.Serialization;

public class SearchResultDTO 
{
    public string idMeal { get; set; }
    public string strMeal { get; set; }
    public string strMealThumb {get; set;}
}

public class ListSearchDTO
{
    [JsonPropertyName("meals")]
    public List<SearchResultDTO>? ListOfRecipes { get; set; }
}