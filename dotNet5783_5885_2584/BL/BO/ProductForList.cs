
using DO;

namespace BO;

public class ProductForList
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
    /// description of the product object
    /// </summary>
    /// <returns>string with the description</returns>
    public override string ToString() => $@"
        Product ID: {ID},
        Product name: {Name}, 
        category: {Category.ToString()}
    	Price: {Price}
        ";
}
