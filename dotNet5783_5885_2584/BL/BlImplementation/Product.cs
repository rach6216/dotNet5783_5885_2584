

using BlApi;
namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal _dal = new Dal.DalList();
    /// <summary>
    /// create new product 
    /// </summary>
    /// <param name="product">bo product</param>
    /// <returns>bo product</returns>
    public BO.Product AddProduct(BO.Product product)
    {
        try
        {
            if (product.ID > 0)
            {
                throw new BO.ExceptionInvalidInput("product id is negative");
            }
            if (product.Price > 0)
            {
                throw new BO.ExceptionInvalidInput("product price is negative");
            }
            if (product.InStock > 0)
            {
                throw new BO.ExceptionInvalidInput("product instock is negative");
            }
            if (product.Name != "")
            {
                throw new BO.ExceptionInvalidInput("product name is required");
            }
            DO.Product p = new DO.Product() { Category = (DO.Enums.Category)product.Category, Name = product.Name, Price = product.Price, InStock = product.InStock, ID = product.ID };
            _dal.Product.Create(p);
            return product;
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("product doesn't exist", exp);
        }
      
    }

    /// <summary>
    /// delete product by id
    /// </summary>
    /// <param name="id">product id</param>
    /// <exception cref="BO.ExceptionDeleteEntityDependence">if the product is in existing orders </exception>
    /// <exception cref="BO.ExceptionEntityNotFound">if the product doesn't exist</exception>
    public void DelProduct(int id)
    {     
           IEnumerable<DO.OrderItem> orderItems=_dal.OrderItem.Read();
            foreach (var item in orderItems)
            {
                if (item.ProductID == id)
                {
                    throw new BO.ExceptionDeleteEntityDependence("can't delete product that exist in orders");
                }
            }
        try
        {
            _dal.Product.Delete(id);
        }catch(DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't delete product, product doesn't exist", exp);
        }       
    }
    /// <summary>
    /// get product by ID
    /// </summary>
    /// <param name="id">product id</param>
    /// <returns>BO.Product obj</returns>
    /// <exception cref="BO.ExceptionInvalidInput">if the input is negative</exception>
    /// <exception cref="BO.ExceptionEntityNotFound"></exception>
    public BO.Product Read(int id)
    {
        try
        {
            if (id > 0)
            {
                DO.Product p=_dal.Product.Read(id);
                BO.Product boP = new BO.Product() { Category=(BO.Category)p.Category,ID=p.ID,
                    InStock=p.InStock,Name=p.Name,Price=p.Price};
                return boP;
            }
            else
            {
                throw new BO.ExceptionInvalidInput("can't read a negative ID");
            }
        }catch (DO.ExceptionEntityNotFound exp)
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
    public BO.ProductItem Read(int id, BO.Cart cart)
    {
        try
        {
            if (id > 0)
            {
                DO.Product p = _dal.Product.Read(id);
                int amount = cart.Items.Find(x => x.ProductID == p.ID).Amount;
                BO.ProductItem boProductItem = new BO.ProductItem() { Category=(BO.Category)p.Category,ID=p.ID,InStock=p.InStock>0,Name=p.Name,Price=p.Price,Amount=amount>0?amount:0};
                return boProductItem;
            }
            else
            {
                throw new BO.ExceptionInvalidInput("can't find product with negative id");
            }
        }catch(DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("Product entity not found", exp);
        }
        
    }

    /// <summary>
    /// create list of product to show
    /// </summary>
    /// <returns>product for list IEnumerable</returns>
    public IEnumerable<BO.ProductForList> ReadAll()
    {
        List<DO.Product> doProducts = new List<DO.Product>();
        List<BO.ProductForList> products = new List<BO.ProductForList>();
        foreach (DO.Product p in doProducts)
        {
            products.Add(new BO.ProductForList() { Category = (BO.Category)p.Category, Price = p.Price, ID = p.ID, Name = p.Name });
        }
        return products;
        throw new NotImplementedException();
    }


    public void UpdateProduct(BO.Product product)
    {
        if (product.ID > 0)
        {
            throw new BO.ExceptionInvalidInput("product id is negative");
        }
        if (product.Price > 0)
        {
            throw new BO.ExceptionInvalidInput("product price is negative");
        }
        if (product.InStock > 0)
        {
            throw new BO.ExceptionInvalidInput("product instock is negative");
        }
        if (product.Name != "")
        {
            throw new BO.ExceptionInvalidInput("product name is required");
        }
        try
        {
            DO.Product p=new DO.Product() { Category=(DO.Enums.Category)product.Category,ID=product.ID,InStock=product.InStock,Name=product.Name,Price=product.Price};
            _dal.Product.Update(p);
        }
        catch(DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't update product,product not found", exp);
        }

    }


}
