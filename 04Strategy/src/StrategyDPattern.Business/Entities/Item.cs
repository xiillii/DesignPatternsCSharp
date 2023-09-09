namespace StrategyDPattern.Business.Entities;

public class Item
{
    public int Id { get; set; }
    public string? UpcCode { get; }
    public double? Price { get; }
    public string? Name { get; set; }

    public Item(string? upc, string? name, double? price)
    {
        UpcCode = upc;
        Name = name;
        Price = price;
    }

    public Item(int id, string? upc, string? name, double? price)
        : this(upc, name, price)
    {
        Id = id;
    }

    public override string? ToString()
    {
        var cad = string.Format("{0, -18} {1, -20} {2, 10:c2}",
                                UpcCode,
                                Name,
                                Price);
        return cad;
    }
}