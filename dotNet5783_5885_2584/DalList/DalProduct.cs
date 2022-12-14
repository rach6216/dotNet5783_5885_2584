using DO;
using DalApi;
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
       if(p.ID == 0)
        {
            Random r = new ();
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
        if(f == null)
            throw new ExceptionEntityNotFound("Product is not found");
        Product? p = s_products.Find(x =>f(x));
        if ( p?.ID != 0)
            return new Product() { Price = p?.Price??0, Category = p?.Category, ID = p?.ID??0, InStock = p?.InStock??0, Name = p?.Name };
        throw new ExceptionEntityNotFound("Product is not found");
    }
    /// <summary>
    /// get all the products
    /// </summary>
    /// <returns>array of the products</returns>
    public IEnumerable<Product?> ReadAll(Func<Product?, bool>? f = null)
    {
        List<Product?> products = s_products;
        if(f != null)
        {
            products = s_products.FindAll(x => f(x));
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
        for(int i = 0; i < s_products.Count; i++)
        {
            if (s_products[i]?.ID == p.ID)
            {
                s_products[i] = p;
                return;
            }
        }
        throw new ExceptionEntityNotFound("the product to update is not found");
    }
    #endregion

    #region Delete
    /// <summary>
    /// delete product by id
    /// </summary>
    /// <param name="id">id of product to delete</param>
    public void Delete(int id)
    {
       bool flag=  s_products.Remove(Read(x=>x?.ID==id));
        if (!flag)
        {
            throw new ExceptionEntityNotFound("Order for delete is not found");
        }
    }

   
    #endregion

}
