
using BlApi;
using BO;


namespace BlImplementation;

internal class Order : IOrder
{
    private DalApi.IDal _dal = new Dal.DalList();

    public BO.Order DeliveryOrder(int id)
    {
        try
        {
            DO.Order doOrder = _dal.Order.Read(x => x.Value.ID == id);
            BO.Order boOrder = Read(id);
            if (doOrder.DeliveryDate == DateTime.MinValue)
            {
                doOrder.DeliveryDate = DateTime.Now;
                boOrder.DeliveryDate = DateTime.Now;
                _dal.Order.Update(doOrder);
            }
            return boOrder;

        }
        catch (DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't set delivery, order is not exist ",exp);
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
        try
        {
            DO.Order doOrder = _dal.Order.Read(x=>x.Value.ID==id);
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
            throw new BO.ExceptionEntityNotFound("can't track order,order doesn't exist",exp);
        }

    }

    /// <summary>
    /// read order BY ID
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionEntityNotFound">if the product doesn't exist </exception>
    /// <exception cref="BO.ExceptionInvalidInput">if the id is negative</exception>
    public BO.Order Read(int id)
    {

        if (id > 0) try
            {
                DO.Order doOrder;
                doOrder = _dal.Order.Read(x => x.Value.ID == id);
                IEnumerable<DO.OrderItem?> doOrderItems = _dal.OrderItem.ReadAll(x => x.Value.ID == id);
                List<BO.OrderItem?> Items = new();
                double total = 0;
                OrderStatus oStatus;
                foreach (var item in doOrderItems)
                {
                    if(item != null)
                    {
                        Items.Add(new BO.OrderItem()
                        {
                            ID = item.Value.ID,
                            Amount = item.Value.Amount,
                            ProductID = item.Value.ProductID,
                            Price = item.Value.Price,
                            TotalPrice = item.Value.Amount * item.Value.Price,
                            ProductName = _dal.Product.Read(x=>x.Value.ID==id).Name
                        });
                        total += item.Value.Amount * item.Value.Price;
                    }
                   
                }
                if (doOrder.DeliveryDate != DateTime.MinValue)
                    oStatus = BO.OrderStatus.OrderIsDelivered;
                else if (doOrder.ShipDate != DateTime.MinValue)
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
        else
        {
            throw new BO.ExceptionInvalidInput("can't get negative id");
        }

    }


    /// <summary>
    /// build porder for list 
    /// </summary>
    /// <returns>order for list</returns>
    public IEnumerable<BO.OrderForList> ReadAll()
    {
        IEnumerable<DO.Order?> orders = _dal.Order.ReadAll();
        List<OrderForList> orderForList = new();
        foreach (DO.Order order in orders)
        {
            BO.OrderStatus orderStatus = BO.OrderStatus.OrderIsConfirmed;
            if (order.DeliveryDate != DateTime.MinValue)
                orderStatus = BO.OrderStatus.OrderIsDelivered;
            else if (order.ShipDate != DateTime.MinValue)
                orderStatus = BO.OrderStatus.OrderIsShiped;
            (int, double) amountAndPrice = TotalPrice(order.ID);
            orderForList.Add(new BO.OrderForList()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
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
        try
        {
            DO.Order doOrder = _dal.Order.Read(x => x.Value.ID == id);
            BO.Order boOrder = Read(doOrder.ID);
            if (doOrder.ShipDate == DateTime.MinValue)
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
        try {
            DO.OrderItem doOrderItem = _dal.OrderItem.Read(x=>x.Value.ID==orderItem.ID);
            BO.Order order = Read(orderID);
            int oiIndex = order.Items.FindIndex(x => x.ID == orderItem.ID);
            if (oiIndex > -1)
            {
                if (orderItem.Amount == 0)
                {
                    _dal.OrderItem.Delete(doOrderItem.ID);
                    order.Items.RemoveAt(oiIndex);
                }
                else
                {

                    order.TotalPrice = order.TotalPrice - order.Items[oiIndex].TotalPrice + orderItem.TotalPrice;
                    order.Items[oiIndex].Amount = orderItem.Amount;
                    doOrderItem.Amount = orderItem.Amount;
                    order.Items[oiIndex].TotalPrice = orderItem.TotalPrice;
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
        }catch(DO.ExceptionEntityNotFound exp)
        {
            throw new BO.ExceptionEntityNotFound("can't update order, order doesn't exist",exp);
        }
        
           
    }


    private (int, double) TotalPrice(int oID)
    {
        double totalPrice = 0;
        int amount = 0;
        foreach (DO.OrderItem order in _dal.OrderItem.ReadAll(x=>x.Value.OrderID==oID))
        {
            amount++;
            totalPrice += order.Price;
        }
        return (amount, totalPrice);
    }

}
