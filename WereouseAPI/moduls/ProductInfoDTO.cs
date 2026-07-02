namespace WereouseAPI.moduls;
public class ProductInfoDTO
{
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public required string Location {get; set;}

    public DateTime ArrivedDate {get;set;}


}