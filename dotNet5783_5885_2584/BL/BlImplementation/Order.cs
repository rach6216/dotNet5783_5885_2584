﻿
using BlApi;
using BO;
using Dal;
using DO;
using System;
using System.Linq;
using System.Security.Cryptography;

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
            if (doOrder.DeliveryDate == null||doOrder.DeliveryDate==DateTime.MinValue)
            {
                doOrder.DeliveryDate = DateTime.Now;
                boOrder.DeliveryDate = DateTime.Now;
                _dal.Order.Update(doOrder);
                boOrder.Status = BO.OrderStatus.OrderIsDelivered;
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
            List< Tuple<DateTime? , string >> tracking = new();
            if (doOrder.OrderDate != null&&doOrder.OrderDate!=DateTime.MinValue)
            {
                oStatus = BO.OrderStatus.OrderIsConfirmed;
                tracking.Add((doOrder.OrderDate, "order created").ToTuple());
            }
             if (doOrder.ShipDate != null && doOrder.ShipDate != DateTime.MinValue)
            {
                oStatus = BO.OrderStatus.OrderIsShiped;
                tracking.Add((doOrder.ShipDate, "order shiped").ToTuple());
            }
            if(doOrder.DeliveryDate != null && doOrder.DeliveryDate != DateTime.MinValue)
            {
                oStatus = BO.OrderStatus.OrderIsDelivered;
                tracking.Add((doOrder.DeliveryDate, "order delivered").ToTuple());
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

            doOrder = _dal.Order.Read(f);
            IEnumerable<DO.OrderItem?> doOrderItems = _dal.OrderItem.ReadAll(x => x?.OrderID == doOrder.ID);
            List<BO.OrderItem?> Items = new();

            OrderStatus oStatus = BO.OrderStatus.OrderIsConfirmed;
            if (doOrder.ShipDate != null && doOrder.ShipDate != DateTime.MinValue)
                oStatus = BO.OrderStatus.OrderIsShiped;
            if (doOrder.DeliveryDate != null && doOrder.DeliveryDate != DateTime.MinValue)
                oStatus = BO.OrderStatus.OrderIsDelivered;
            Items = doOrderItems.Where(x => x != null).Select(item => new BO.OrderItem()
            {
                ID = item?.ID ?? 0,
                Amount = item?.Amount ?? 0,
                ProductID = item?.ProductID ?? 0,
                Price = item?.Price ?? 0,
                TotalPrice = item?.Amount * item?.Price ?? 0,
                ProductName = _dal.Product.Read(x => x?.ID == item?.ProductID).Name
            }).ToList<BO.OrderItem?>();
            double total = (from oi in doOrderItems
                            select new { totalSum = oi?.Amount * oi?.Price })
                           .Sum(x => x.totalSum) ?? default;

            
      
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
        List<OrderForList> orderForList =(from item in orders
         let orderStatus = CalculateStatus(item)
         let totalPrice = TotalPrice(item?.ID ?? default)
         where item!=null
         select new BO.OrderForList(){ ID=item?.ID??0,
             CustomerName = item?.CustomerName,
             Status = orderStatus,
             Amount = totalPrice.Item1,
             TotalPrice = totalPrice.Item2
         }).ToList();
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
            if (doOrder.ShipDate == null || doOrder.ShipDate ==DateTime.MinValue)
            {
                doOrder.ShipDate = DateTime.Now;
                boOrder.ShipDate = DateTime.Now;
                _dal.Order.Update(doOrder);
                boOrder.Status = BO.OrderStatus.OrderIsShiped;
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
                if (oiIndex > -1 && order.Items[oiIndex] != null)
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
                        order.Status = BO.OrderStatus.OrderIsConfirmed;
                    }
                    return order;
                }
                else

                {
                    order.Items.Add(orderItem);
                    _dal.OrderItem.Create(new DO.OrderItem(orderItem.ProductID, orderID, orderItem.Price, orderItem.Amount));
                    order.TotalPrice += orderItem.TotalPrice;
                    order.Status = BO.OrderStatus.OrderIsConfirmed;
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
        IEnumerable<DO.OrderItem?> l = _dal.OrderItem.ReadAll(x => x?.OrderID == oID && x != null);
        int amount=l.Count();
        double totalPrice = l.Sum(x => x?.Amount * x?.Price) ?? default;
        return (amount, totalPrice);
    }
    BO.OrderStatus CalculateStatus(DO.Order? order)
    {
        BO.OrderStatus orderStatus = BO.OrderStatus.OrderIsConfirmed;
        if (order?.DeliveryDate != null && order?.DeliveryDate!=DateTime.MinValue)
            orderStatus = BO.OrderStatus.OrderIsDelivered;
        else if (order?.ShipDate != null&& order?.ShipDate != DateTime.MinValue)
            orderStatus = BO.OrderStatus.OrderIsShiped;
        return orderStatus;
    }
    public int? GetRandomOrder()
    {
        if (_dal == null)
            throw new BO.ExceptionNullDal();
        IEnumerable<DO.Order?> l = _dal.Order.ReadAll(x => x?.DeliveryDate == DateTime.MinValue && x != null);
        l.OrderBy(x =>  (x?.ShipDate != DateTime.MinValue) ? x?.ShipDate : x?.OrderDate);       
        return l.FirstOrDefault()?.ID;
    }
}
