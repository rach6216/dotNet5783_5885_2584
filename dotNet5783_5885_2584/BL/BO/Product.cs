namespace BO;

public struct Product
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
    #region To string
    /// <summary>
    /// description of the product object
    /// </summary>
    /// <returns>string with the description</returns>
    public override string ToString() => $@"
        Product ID: {ID}: {Name}, 
        category: {Category.ToString()}
    	Price: {Price}
    	Amount in stock: {InStock}
";

    #endregion
}
