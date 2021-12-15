﻿namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Services;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _context;

        public OrderRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Orders.Find(id).IsDeleted = true;
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.AsNoTracking().Where(p => p.IsDeleted == false);
        }

        public Order GetById(int id)
        {
            Order existingOrder = _context.Orders.AsNoTracking().FirstOrDefault(o => o.Id == id && o.IsDeleted == false);

            return existingOrder;
        }

        public Order Insert(Order item, IEnumerable<int> orderLinesIds)
        {
            item.OrderStatus = _context.OrderStatuses.FirstOrDefault(s => s.Name == "Готовится");

            foreach (int orderLineId in orderLinesIds)
            {
                OrderLine existingOrderLine = _context.OrderLines.Find(orderLineId);
                item.OrderLines.Add(existingOrderLine);
            }

            item.Date = System.DateTime.Now;
            item.Price = PriceCountingService.GetPriceForOrder(item);

            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public Order Patch(int id, Order item, IEnumerable<int> orderLinesIds)
        {
            var existingItem = _context.Orders
                .Include(o => o.OrderLines)
                .Include(o => o.OrderStatus)
                .FirstOrDefault(o => o.Id == id && o.IsDeleted == false);

            if (item.OrderStatus != null && item.OrderStatus != existingItem.OrderStatus)
            {
                existingItem.OrderStatus = item.OrderStatus;
            }

            if (orderLinesIds != null)
            {
                ChangeOrderLines(existingItem, orderLinesIds);
                existingItem.Price = PriceCountingService.GetPriceForOrder(existingItem);
            }

            var entity = _context.Update(existingItem);
            _context.SaveChanges();

            return entity.Entity;
        }

        public Order Update(int id, Order item, List<int> orderLinesIds)
        {
            var existingItem = _context.Orders
                .Include(o => o.OrderLines)
                .Include(o => o.OrderStatus)
                .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);

            existingItem.OrderStatus = item.OrderStatus;
            existingItem.Date = item.Date;

            if (orderLinesIds.Count != 0)
            {
                existingItem = ChangeOrderLines(existingItem, orderLinesIds);
                existingItem.Price = PriceCountingService.GetPriceForOrder(existingItem);
            }
            else
            {
                existingItem.OrderLines = new List<OrderLine>();
            }

            var entity = _context.Update(existingItem);
            _context.SaveChanges();

            return entity.Entity;
        }

        private Order ChangeOrderLines(Order existingItem, IEnumerable<int> orderLinesIds)
        {
            IEnumerable<int> existingItemOrderLines = existingItem.OrderLines.Select(line => line.Id);

            IEnumerable<int> remains = existingItemOrderLines.Intersect(orderLinesIds);

            IEnumerable<int> toRemove = existingItemOrderLines.Except(remains);

            IEnumerable<int> toAdd = existingItemOrderLines.Except(remains);

            existingItem.OrderLines = existingItem.OrderLines.Where(l => !toRemove.Contains(l.Id)).ToList();

            foreach (int id in toAdd)
            {
                existingItem.OrderLines.Add(_context.OrderLines.FirstOrDefault(line => line.Id == id));
            }

            return existingItem;
        }
    }
}