

using BlApi;
using BO;
using System.Data;
using System.Reflection;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal? _dal = DalApi.Factory.Get();

    #region Create
    /// <summary>
    /// create new product 
    /// </summary>
    /// <param name="product">bo product</param>
    /// <returns>bo product</returns>
    public int AddProduct(BO.Product product)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        try
        {
            if (product.ID < 0)
            {
                throw new BO.ExceptionInvalidInput("product id is negative");
            }
            if (product.Price < 0)
            {
                throw new BO.ExceptionInvalidInput("product price is negative");
            }
            if (product.InStock < 0)
            {
                throw new BO.ExceptionInvalidInput("product instock is negative");
            }
            if (product.Name == "")
            {
                throw new BO.ExceptionInvalidInput("product name is required");
            }
            DO.Product p = new() { Category=(DO.Category?)product.Category};
             p=p.GenericParse(product);
            // p = new () { Category = (DO.Category?)product.Category, Name = product.Name, Price = product.Price, InStock = product.InStock };
            Console.WriteLine(p);
            int id = _dal.Product.Create(p);
            return id;
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("product doesn't exist", exp);
        }

    }

    #endregion

    #region Read

    /// <summary>
    /// get product by ID
    /// </summary>
    /// <param name="id">product id</param>
    /// <returns>BO.Product obj</returns>
    /// <exception cref="BO.ExceptionInvalidInput">if the input is negative</exception>
    /// <exception cref="BO.ExceptionEntityNotFound"></exception>
    public BO.Product Read(Func<DO.Product?, bool>? f)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        try
        {
            if (f == null)
                throw new DO.ExceptionEntityNotFound("product didn't find by ID");

            DO.Product p = _dal.Product.Read(x => f(x));
            BO.Product boP = new()
            {
                Category = (BO.Category?)p.Category,
                ID = p.ID,
                InStock = p.InStock,
                Name = p.Name,
                Price = p.Price
            };
            return boP;

        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("product didn't find by ID", exp);
        }
    }

    /// <summary>
    /// get product item by id
    /// </summary>
    /// <param name="id">product id</param>
    /// <param name="cart">user cart</param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionInvalidInput">if the product id is negative</exception>
    /// <exception cref="BO.ExceptionEntityNotFound">if the product doesn't exist</exception>
    public BO.ProductItem Read(BO.Cart cart, Func<DO.Product?, bool> f = null!)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        cart.Items ??= new List<BO.OrderItem?>() { };
        try
        {

            DO.Product p = _dal.Product.Read(f != null ? x => f(x) : null);
            int productIndex = cart.Items.FindIndex(x => x?.ProductID == p.ID);
            int amount = productIndex == -1 ? 0 : cart.Items[productIndex]!.Amount;
            BO.ProductItem boProductItem = new() { Category = (BO.Category?)p.Category, ID = p.ID, InStock = p.InStock > 0, Name = p.Name, Price = p.Price, Amount = amount > 0 ? amount : 0 };
            return boProductItem;
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("Product entity not found", exp);
        }

    }

    /// <summary>
    /// create list of product to show
    /// </summary>
    /// <returns>product for list IEnumerable</returns>
    public IEnumerable<BO.ProductForList?> ReadAll(Func<DO.Product?, bool>? f = null)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        IEnumerable<DO.Product?> doProducts;
        List<BO.ProductForList?> products = new();
        if (f == null)
        {
            doProducts = _dal.Product.ReadAll();
        }
        else
        {
            doProducts = _dal.Product.ReadAll(f);

        }
        foreach (var p in doProducts)
        {
            if (p != null)
                products.Add(new BO.ProductForList() { Category = (BO.Category?)p?.Category, Price = p?.Price ?? 0, ID = p?.ID ?? 0, Name = p?.Name });
        }
        return products;
    }
    #endregion

    #region Update
    /// <summary>
    /// update product
    /// </summary>
    /// <param name="product">updated product</param>
    /// <exception cref="BO.ExceptionInvalidInput">if one of the updated product details is incorrect</exception>
    /// <exception cref="BO.ExceptionEntityNotFound">if the product doesn't exist</exception>
    public void UpdateProduct(BO.Product product)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        if (product.ID < 0)
        {
            throw new BO.ExceptionInvalidInput("product id is negative");
        }
        if (product.Price < 0)
        {
            throw new BO.ExceptionInvalidInput("product price is negative");
        }
        if (product.InStock < 0)
        {
            throw new BO.ExceptionInvalidInput("product instock is negative");
        }
        if (product.Name == "")
        {
            throw new BO.ExceptionInvalidInput("product name is required");
        }
        try
        {
            DO.Product p = new() { Category = (DO.Category?)product.Category, ID = product.ID, InStock = product.InStock, Name = product.Name, Price = product.Price };
            _dal.Product.Update(p);
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't update product,product not found", exp);
        }

    }
    #endregion

    #region Delete
    /// <summary>
    /// delete product by id
    /// </summary>
    /// <param name="id">product id</param>
    /// <exception cref="BO.ExceptionDeleteEntityDependence">if the product is in existing orders </exception>
    /// <exception cref="BO.ExceptionEntityNotFound">if the product doesn't exist</exception>
    public void DelProduct(int id)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        IEnumerable<DO.OrderItem?> orderItems = _dal.OrderItem.ReadAll();
        foreach (var item in orderItems)
        {
            if (item?.ProductID == id)
            {
                throw new BO.ExceptionDeleteEntityDependence("can't delete product that exist in orders");
            }
        }
        try
        {
            _dal.Product.Delete(id);
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't delete product, product doesn't exist", exp);
        }
    }
    #endregion

}

