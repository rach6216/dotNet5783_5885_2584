using DO;
using static Dal.DataSource;

namespace Dal;

public struct DalProduct
{
    public int Create(Product p)
    {
        p.ID = Config.ProductID;
        _products[Config._productIndex++] = p;
        return p.ID;
    }
    public Product Read(int id)
    {
        for (int i = 0; i < Config._productIndex; i++)
        {
            if (_products[i].ID == id)
                return _products[i];
        }
        throw new Exception("Product is not found");
    }
    public Product[] Read()
    {
        Product[] p = new Product[Config._productIndex];
        for (int i = 0; i < Config._productIndex; i++)
        {
            p[i] = _products[i];
        }
        return p;
    }
    public void Update(Product p)
    {
        for (int i = 0; i < Config._productIndex; i++)
        {
            if (_products[i].ID == p.ID)
                _products[i] = p;
        }
    }
    public void Delete(int id)
    {
        int i = 0;
        while (_products[i].ID != id) { i++; }
        while (i < Config._productIndex - 1)
        {
            _products[i] = _products[i + 1];
        }
        Config._productIndex--;
    }

}
