

using BlApi;
using BO;

namespace BlImplementation;

internal class Product : IProduct
{
    public void AddProduct(BO.Product product)
    {
        ProductForList products=new ProductForList();

    }

    public void DelProduct(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Product Read(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Product Read(int id, BO.Cart cart)
    {
        throw new NotImplementedException();
    }

    public List<ProductForList> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(BO.Product product)
    {
        throw new NotImplementedException();
    }
}
