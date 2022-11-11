using DO;
using static Dal.DataSource;

namespace Dal;
/// <summary>
/// structure for actions on products array
/// </summary>
public struct DalProduct
{
    /// <summary>
    /// generate id and insert new product to the array
    /// </summary>
    /// <param name="p">product to insert</param>
    /// <returns>the id of the product</returns>
    public int Create(Product p)
    {
        // p.ID = Config.ProductID;
        Random r = new Random();
        int tID;
        //generate random id
        do
        {
            tID = r.Next(10000000, 99999999);
            foreach (Product item in Read())
            {
                if (item.ID == tID)
                {
                    tID = 0;
                    break;
                }
            }
        } while (tID == 0);
        p.ID = tID;
        s_products[Config.s_productIndex] = p;
        return p.ID;
    }
    /// <summary>
    /// get product by id
    /// </summary>
    /// <param name="id">id of requested product</param>
    /// <returns>requested product</returns>
    /// <exception cref="Exception">when product is not exist throw exeption: "Product is not found</exception>
    public Product Read(int id)
    {
        for (int i = 0; i < Config.s_productIndex; i++)
        {
            if (s_products[i].ID == id)
                return s_products[i];
        }
        throw new Exception("Product is not found");
    }
    /// <summary>
    /// get all the products
    /// </summary>
    /// <returns>array of the products</returns>
    public Product[] Read()
    {
        Product[] p = new Product[Config.s_productIndex];
        for (int i = 0; i < Config.s_productIndex; i++)
        {
            p[i] = s_products[i];
        }
        return p;
    }
    /// <summary>
    /// update specific product by id
    /// </summary>
    /// <param name="p">product to update</param>
    public void Update(Product p)
    {
        for (int i = 0; i < Config.s_productIndex; i++)
        {
            if (s_products[i].ID == p.ID)
                s_products[i] = p;
        }
    }
    /// <summary>
    /// delete product by id
    /// </summary>
    /// <param name="id">id of product to delete</param>
    public void Delete(int id)
    {
        int i = 0;
        while (s_products[i].ID != id) { i++; }
        s_products[i] = s_products[--Config.s_productIndex];
    }

}
