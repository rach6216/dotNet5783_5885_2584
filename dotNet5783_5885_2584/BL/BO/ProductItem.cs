
using DO;

namespace BO;

public class ProductItem
{
    /// <summary>
    /// unique product Id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// product name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// product category(enum)
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// number of products in the stock
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// the amount of the product in the order
    /// </summary>
    public int Amount { get; set; }
}
