﻿using DO;
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
        // p.ID = Config.ProductID;
        Random r = new Random();
        int tID;
        //generate random id
        do
        {
            tID = r.Next(10000000, 99999999);
            s_products.ForEach(x => { if (x.ID == tID) tID = 0; });
        } while (tID == 0);
        p.ID = tID;
        s_products.Add(p);
        return p.ID;
    }
    #endregion

    #region Read
    /// <summary>
    /// get product by id
    /// </summary>
    /// <param name="id">id of requested product</param>
    /// <returns>requested product</returns>
    /// <exception cref="Exception">when product is not exist throw exeption: "Product is not found</exception>
    public Product Read(int id)
    {
        Product p = s_products.Find(x => x.ID == id);
        if (p.ID != 0)
            return p;
        throw new Exception("Product is not found");
    }
    /// <summary>
    /// get all the products
    /// </summary>
    /// <returns>array of the products</returns>
    public IEnumerable<Product> Read()
    {
        List<Product> products = s_products;
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
            if (s_products[i].ID == p.ID)
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
       bool flag=  s_products.Remove(Read(id));
        if (!flag)
        {
            throw new ExceptionEntityNotFound("Order for delete is not found");
        }
    }
    #endregion

}
