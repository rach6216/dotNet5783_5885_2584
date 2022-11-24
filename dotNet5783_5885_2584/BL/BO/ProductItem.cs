
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
    /// if the product in stock
    /// </summary>
    public bool InStock { get; set; }
    /// <summary>
    /// the amount of the product in the order
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// description of the product object
    /// </summary>
    /// <returns>string with the description</returns>
    public override string ToString() => $@"
        Product ID: {ID}: {Name}, 
        category: {Category.ToString()}
    	Price: {Price}
    	Amount in stock: {InStock}
    	Amount: {Amount}

        ";
}
