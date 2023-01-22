using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class Product:IProduct
{
    #region Fields and Props for products
    /// <summary>
    /// unique product Id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// product name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// product category(enum)
    /// </summary>
    public Category? Category { get; set; }
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

    public int Create(DO.Product entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Product entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Product?> ReadAll(Func<DO.Product?, bool>? f = null)
    {
        throw new NotImplementedException();
    }

    public DO.Product Read(Func<DO.Product?, bool>? f)
    {
        throw new NotImplementedException();
    }
}

