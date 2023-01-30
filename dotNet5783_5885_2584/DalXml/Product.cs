using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;


using System.Xml.Linq;

public class Product : DalApi.IProduct
{
    string ProductFile = @"Product.xml";
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
    public DO.Category? Category { get; set; }
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

        XElement Products = XMLTools.LoadListFromXMLElement(ProductFile);
        if (entity.ID == 0)
        {
            Random r = new();
            entity.ID = r.Next(10000000, 99999999);
        }
        XElement? per1 = (from p in Products.Elements()
                          where int.Parse(p.Element("ID")!.Value) == entity.ID
                          select p).FirstOrDefault();
        if (per1 != null)
            throw new DO.ExceptionIdAlreadyExist(entity.ID + " -Duplicate product ID");
        XElement productElement = new XElement("Product", new XElement("ID", entity.ID),
                              new XElement("Name", entity.Name),
                              new XElement("Price", entity.Price.ToString()),
                              new XElement("InStock", entity.InStock.ToString()),
                              new XElement("Category", entity.Category.ToString()));

        Products.Add(productElement);
        XMLTools.SaveListToXMLElement(Products, ProductFile);
        return entity.ID;
    }

    public void Delete(int id)
    {
        XElement ProductData = XMLTools.LoadListFromXMLElement(ProductFile);
        XElement? product = (from p in ProductData.Elements()
                             where int.Parse(p.Element("ID")!.Value) == id
                             select p).FirstOrDefault();
        if (product != null)
        {
            product.Remove();
            XMLTools.SaveListToXMLElement(ProductData, ProductFile);
        }
    }

    public void Update(DO.Product entity)
    {

        XElement ProductData = XMLTools.LoadListFromXMLElement(ProductFile);
        XElement? product = (from p in ProductData.Elements()
                             where int.Parse(p.Element("ID")!.Value) == entity.ID
                             select p).FirstOrDefault();
        if (product != null)
        {
            product!.Element("Name")!.Value = entity.Name ?? "";
            product!.Element("Price")!.Value = entity.Price.ToString();
            product!.Element("InStock")!.Value = entity.InStock.ToString();
            product!.Element("Category")!.Value = entity.Category.ToString() ?? "Family";
            XMLTools.SaveListToXMLElement(ProductData, ProductFile);
        }
    }

    public IEnumerable<DO.Product?> ReadAll(Func<DO.Product?, bool>? f = null)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(ProductFile);
            IEnumerable<DO.Product?>? products = ProductData.Elements().Select(x =>
            {
                DO.Product product = new();
                product.ID = int.Parse(x.Element("ID")!.Value);
                product.Category = (DO.Category)Enum.Parse(typeof(DO.Category), x.Element("Category")!.Value);
                product.InStock = int.Parse(x.Element("InStock")!.Value);
                product.Price = double.Parse(x.Element("Price")!.Value);
                product.Name = x.Element("Name")!.Value;
                return (DO.Product?)product;
            }).Where(x => f == null || f(x));
            return products;
        }

        catch (DO.XMLFileLoadCreateException e)
        {
            throw new DO.XMLFileLoadCreateException(e.Message, e);
        }
    }

    public DO.Product Read(Func<DO.Product?, bool>? f)
    {
        try
        {
            XElement ProductData = XMLTools.LoadListFromXMLElement(ProductFile);
            DO.Product? product = ProductData.Elements().Where(x => x != null).Select(x => new DO.Product()
            {
                ID = int.Parse(x.Element("ID")!.Value),
                Category = (DO.Category)Enum.Parse(typeof(DO.Category), x.Element("Category")!.Value),
                InStock = int.Parse(x.Element("InStock")!.Value),
                Price = double.Parse(x.Element("Price")!.Value),
                Name = x.Element("Name")!.Value
            }).FirstOrDefault(x => f(x));
            if (product == null)
                throw new DO.ExceptionEntityNotFound("product is not exist");
            return (DO.Product)product;
        }
        catch (DO.XMLFileLoadCreateException e)
        {
            throw new DO.XMLFileLoadCreateException(e.Message, e);
        }

    }
}

