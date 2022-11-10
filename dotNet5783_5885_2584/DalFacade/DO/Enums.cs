namespace DO;

public struct Enums
{
    /// <summary>
    /// categories for the store
    /// </summary>
    public enum Category
    {
        Family, Race, Sport, SUV, Tiny, Motorcycle, VIP, Big
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
        Exit, Create, Read, ReadAll, Delete, Update, ReadByProductAndOrder, ReadAllByOrder
    }
}

