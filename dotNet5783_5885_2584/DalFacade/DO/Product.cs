namespace DO;
using static Enums;
/// <summary>
/// structure for the store products
/// </summary>
public struct Product
{
    #region Fields and Props for products
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
    /// ctor for products
    /// </summary>
    /// <param name="myID" default="000000" >unique product Id</param>
    /// <param name="myName">product name</param>
    /// <param name="myPrice">product price</param>
    /// <param name="myCategory">product category(enum)</param>
    /// <param name="myInstock" >number of products in the stock</param>
    #endregion

    #region Constructor for products
    public Product(string myName, double myPrice, Category myCategory, int myInstock, int myID = 000000)
    {
        ID = myID;
        Name = myName;
        Price = myPrice;
        Category = myCategory;
        InStock = myInstock;
    }
    #endregion

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
