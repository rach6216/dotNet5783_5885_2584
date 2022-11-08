namespace DO;
using static Enums;
/// <summary>
/// structure for the store products
/// </summary>
public struct Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; } 
    public int InStock { get; set; }
    /// <summary>
    /// ctor for products
    /// </summary>
    /// <param name="myID" default="000000" >unique product Id</param>
    /// <param name="myName">product name</param>
    /// <param name="myPrice">product price</param>
    /// <param name="myCategory">product category(enum)</param>
    /// <param name="myInstock" >number of products in the stock</param>
    public Product(string myName,double myPrice,Category myCategory,int myInstock, int myID=000000)
    {
        ID = myID;
        Name = myName;  
        Price = myPrice;
        Category = myCategory;
        InStock = myInstock;
    }
    /// <summary>
    /// description of the product object
    /// </summary>
    /// <returns>string with the description</returns>
    public override string ToString() => $@"
        Product ID: {ID}: {Name}, 
        category: {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";

}
