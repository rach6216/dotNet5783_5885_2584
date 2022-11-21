
using BO;

namespace BlApi;
/// <summary>
/// interface of product
/// </summary>
public interface IProduct
{
    /// <summary>
    /// method that get all products
    /// </summary>
    /// <returns>list of all products</returns>
    public IEnumerable<ProductForList> ReadAll();
    /// <summary>
    /// method that get product by id for manager screen
    /// </summary>
    /// <param name="id">product id</param>
    /// <returns>product by id</returns>
    public Product Read(int id);
    /// <summary>
    /// method that get product by id for client screen
    /// </summary>
    /// <param name="id">product id</param>
    /// <param name="cart"></param>
    /// <returns>prduct item or exception if product not exiest</returns>
    public Product Read(int id, Cart cart);
    /// <summary>
    /// method that add product for manager screen
    /// </summary>
    /// <param name="product"></param>
    public void AddProduct(Product product);
    /// <summary>
    /// method that add product for manager screen
    /// </summary>
    /// <param name="id">product id</param>
    public void DelProduct(int id);
    /// <summary>
    /// method that update product for manager screen
    /// </summary>
    /// <param name="id">product id</param>
    public void UpdateProduct(Product product);
}
