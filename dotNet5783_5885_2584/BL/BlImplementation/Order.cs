
using BlApi;
using BO;


namespace BlImplementation;

internal class Order : IOrder
{
    private DalApi.IDal? _dal = DalApi.Factory.Get();

    public BO.Order DeliveryOrder(int id)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        try
        {
            DO.Order doOrder = _dal.Order.Read(x => x?.ID == id);
            BO.Order boOrder = Read(x => x?.ID == id);
            if (doOrder.DeliveryDate == null)
            {
                doOrder.DeliveryDate = DateTime.Now;
                boOrder.DeliveryDate = DateTime.Now;
                _dal.Order.Update(doOrder);
            }
            return boOrder;

        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't set delivery, order is not exist ", exp);
        }
    }


    /// <summary>
    /// track your order
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>obj type order tracking with details on the order</returns>
    /// <exception cref="BO.ExceptionEntityNotFound">if the order doesn't exist</exception>
    public BO.OrderTracking OrderTracking(int id)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        try
        {
            DO.Order doOrder = _dal.Order.Read(x => x?.ID == id);
            BO.OrderStatus oStatus = 0;
            List<(DateTime? d, string s)> tracking = new();
            if (doOrder.OrderDate != null)
            {
                oStatus = BO.OrderStatus.OrderIsConfirmed;
                tracking.Add((doOrder.OrderDate, "order created"));
            }
            else if (doOrder.ShipDate != null)
            {
                oStatus = BO.OrderStatus.OrderIsShiped;
                tracking.Add((doOrder.ShipDate, "order shiped"));
            }
            else
            {
                oStatus = BO.OrderStatus.OrderIsDelivered;
                tracking.Add((doOrder.DeliveryDate, "order delivered"));
            }

            return new BO.OrderTracking() { ID = doOrder.ID, Status = oStatus, Tracking = tracking };
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't track order,order doesn't exist", exp);
        }

    }

    /// <summary>
    /// read order BY ID
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionEntityNotFound">if the product doesn't exist </exception>
    /// <exception cref="BO.ExceptionInvalidInput">if the id is negative</exception>
    public BO.Order Read(Func<DO.Order?, bool>? f)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        try
        {
            DO.Order doOrder;
            if (f == null)
                throw new DO.ExceptionEntityNotFound("can't read order-order not found");

            doOrder = _dal.Order.Read(x => f(x));
            IEnumerable<DO.OrderItem?> doOrderItems = _dal.OrderItem.ReadAll(x => x?.OrderID == doOrder.ID);
            List<BO.OrderItem?> Items = new();
            double total = 0;
            OrderStatus oStatus;
            foreach (var item in doOrderItems)
            {
                if (item != null)
                {
                    Items.Add(new BO.OrderItem()
                    {
                        ID = item?.ID??0,
                        Amount = item?.Amount??0,
                        ProductID = item?.ProductID??0,
                        Price = item?.Price??0,
                        TotalPrice = item?.Amount??0 * item?.Price??0,
                        ProductName = _dal.Product.Read(x => x?.ID == item?.ID).Name
                    });
                    total += item?.Amount??0 * item?.Price??0;
                }

            }
            if (doOrder.DeliveryDate !=null)
                oStatus = BO.OrderStatus.OrderIsDelivered;
            else if (doOrder.ShipDate != null)
                oStatus = BO.OrderStatus.OrderIsShiped;
            else
                oStatus = BO.OrderStatus.OrderIsConfirmed;
            BO.Order order = new()
            {
                CustomerAddress = doOrder.CustomerAddress,
                CustomerEmail = doOrder.CustomerEmail,
                CustomerName = doOrder.CustomerName,
                ID = doOrder.ID,
                OrderDate = doOrder.OrderDate,
                ShipDate = doOrder.ShipDate,
                DeliveryDate = doOrder.DeliveryDate,
                Items = Items,
                TotalPrice = total,
                Status = oStatus
            };
            return order;
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't read order-order not found", exp);
        }

    }


    /// <summary>
    /// build porder for list 
    /// </summary>
    /// <returns>order for list</returns>
    public IEnumerable<BO.OrderForList?> ReadAll(Func<DO.Order?, bool>? f = null)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        IEnumerable<DO.Order?> orders = _dal.Order.ReadAll(f != null ? x => f(x) : null);
        List<OrderForList> orderForList = new();
        foreach (var order in orders)
        {
                BO.OrderStatus orderStatus = BO.OrderStatus.OrderIsConfirmed;
                if (order?.DeliveryDate != null)
                    orderStatus = BO.OrderStatus.OrderIsDelivered;
                else if (order?.ShipDate != null)
                    orderStatus = BO.OrderStatus.OrderIsShiped;
                (int, double) amountAndPrice = TotalPrice(order?.ID??0);
                orderForList.Add(new BO.OrderForList()
                {
                    ID = order?.ID??0,
                    CustomerName = order?.CustomerName,
                    Status = orderStatus,
                    Amount = amountAndPrice.Item1,
                    TotalPrice = amountAndPrice.Item2
                });
            

        }
        return orderForList;
    }


    /// <summary>
    /// update ship date
    /// </summary>
    /// <param name="id">order id of the ship order</param>
    /// <returns>bo order updated</returns>
    /// <exception cref="BO.ExceptionEntityNotFound"></exception>
    public BO.Order ShipOrder(int id)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        try
        {
            DO.Order doOrder = _dal.Order.Read(x => x?.ID == id);
            BO.Order boOrder = Read(x => doOrder.ID == x?.ID);
            if (doOrder.ShipDate == null)
            {
                doOrder.ShipDate = DateTime.Now;
                boOrder.ShipDate = DateTime.Now;
                _dal.Order.Update(doOrder);
            }
            return boOrder;
        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't ship order,order is not exist", exp);
        }
    }

    /// <summary>
    /// update order item in order according to the
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionEntityNotFound"></exception>
    public BO.Order UpdateOrder(int orderID, BO.OrderItem orderItem)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        try
        {
            DO.OrderItem doOrderItem = _dal.OrderItem.Read(x => x?.ID == orderItem.ID);
            BO.Order order = Read(x => x?.ID == orderID);
            if (order != null && order.Items != null)
            {
                int oiIndex = order.Items.FindIndex(x => x?.ID == orderItem.ID);
                if (oiIndex > -1 && order.Items[oiIndex]!=null)
                {
                    if (orderItem.Amount == 0)
                    {
                        _dal.OrderItem.Delete(doOrderItem.ID);
                        order.Items.RemoveAt(oiIndex);
                    }
                    else
                    {

                        order.TotalPrice = order.TotalPrice - order.Items[oiIndex]!.TotalPrice + orderItem.TotalPrice;
                        order.Items[oiIndex]!.Amount = orderItem.Amount;
                        doOrderItem.Amount = orderItem.Amount;
                        order.Items[oiIndex]!.TotalPrice = orderItem.TotalPrice;
                        _dal.OrderItem.Update(doOrderItem);
                    }
                    return order;
                }
                else

                {
                    order.Items.Add(orderItem);
                    _dal.OrderItem.Create(new DO.OrderItem(orderItem.ProductID, orderID, orderItem.Price, orderItem.Amount));
                    order.TotalPrice += orderItem.TotalPrice;
                    return order;
                }
            }
            else
            {
                throw new DO.ExceptionEntityNotFound("can't update order, order doesn't exist");

            }

        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't update order, order doesn't exist", exp);
        }


    }


    private (int, double) TotalPrice(int oID)
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        double totalPrice = 0;
        int amount = 0;
        foreach (var order in _dal.OrderItem.ReadAll(x => x?.OrderID == oID))
        {
            if (order != null)
            {
                amount++;
                totalPrice += order?.Price??0;
            }
           
        }
        return (amount, totalPrice);
    }

}
