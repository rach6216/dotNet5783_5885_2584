
namespace BO;

/// <summary>
/// enum of the watches category
/// </summary>
public enum Category
{
    Family, Race, Sport, SUV, Tiny, Motorcycle, VIP, Big
}
/// <summary>
/// order status
/// </summary>
public enum OrderStatus
{
    OrderIsConfirmed, OrderIsShiped, OrderIsDelivered
}

/// <summary>
/// Entities options for the main
/// </summary>
public enum Entity
{
    Exit, Products, Orders, OrderItems
};
/// <summary>
/// CRUD options for the entities part in the main
/// </summary>
public enum CRUDOp
{
    Exit, Create, Read, ReadAll, Update, Delete, ReadByProductAndOrder, ReadAllByOrder
}


/// <summary>
/// CRUD options for the product part in the main
/// </summary>
public enum CrudProduct
{
    Exit,Create, Read, ReadAll, Update, Delete, ReadByIDAndCart
}

/// <summary>
/// CRUD options for the order part in the main
/// </summary>
public enum CrudOrder
{
    Exit, Read, ReadAll, UpdateOrder, ShipOrder, DeliveryOrder, OrderTracking
}

/// <summary>
/// CRUD options for the cart part in the main
/// </summary>
public enum CrudCart
{
    Exit,AddProduct, UpdateProductAmount,ConfirmOrder
}