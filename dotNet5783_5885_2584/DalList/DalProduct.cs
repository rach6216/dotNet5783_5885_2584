using DO;
using DalApi;
using System.Linq;
using static Dal.DataSource;

namespace Dal;
/// <summary>
/// structure for actions on products array
/// </summary>
internal struct DalProduct : IProduct
{
    #region Create
    /// <summary>
    /// generate id and insert new product to the array
    /// </summary>
    /// <param name="p">product to insert</param>
    /// <returns>the id of the product</returns>
    public int Create(Product p)
    {
        if (p.ID == 0)
        {
            Random r = new();
            int tID;
            //generate random id
            do
            {
                tID = r.Next(10000000, 99999999);
                s_products.ForEach(x => { if (x?.ID == tID) tID = 0; });
            } while (tID == 0);
            p.ID = tID;
        }
        s_products.Add(p);
        return p.ID;
    }
    #endregion

    #region Read
    /// <summary>
    /// get product by condition
    /// </summary>
    /// <param name="id">id of requested product</param>
    /// <returns>requested product</returns>
    /// <exception cref="Exception">when product is not exist throw exeption: "Product is not found</exception>

    public Product Read(Func<Product?, bool>? f)
    {
        if (f == null)
            throw new ExceptionEntityNotFound("Product is not found");
        try
        {
            return s_products.Select(x => new Product() { Price = x?.Price ?? 0, Category = x?.Category, ID = x?.ID ?? 0, InStock = x?.InStock ?? 0, Name = x?.Name }).First(x => f(x) && x.ID != 0);
        }
        catch
        {
            throw new ExceptionEntityNotFound("Product is not found");
        }
    }
    /// <summary>
    /// get all the products
    /// </summary>
    /// <returns>array of the products</returns>
    public IEnumerable<Product?> ReadAll(Func<Product?, bool>? f = null)
    {
        IEnumerable<Product?> products = s_products;
        if (f != null)
        {
            products = s_products.Where(x => f(x));
        }
        return products;
    }
    #endregion

    #region Update
    /// <summary>
    /// update specific product by id
    /// </summary>
    /// <param name="p">product to update</param>
    public void Update(Product p)
    {
        if (1 > s_products.RemoveAll(x => x?.ID == p.ID && x != null))
            throw new ExceptionEntityNotFound("the product to update is not found");
        s_products.Add(p);
    }
    #endregion

    #region Delete
    /// <summary>
    /// delete product by id
    /// </summary>
    /// <param name="id">id of product to delete</param>
    public void Delete(int id)
    {
        if (1 > s_products.RemoveAll(x => x?.ID == id))
            throw new ExceptionEntityNotFound("Product to delete is not found");
    }
    #endregion

}
