using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EventDressRentalContext _eventDressRentalContext;
        public OrderRepository(EventDressRentalContext eventDressRentalContext)
        {
            _eventDressRentalContext = eventDressRentalContext;
        }

        public async Task<Order> AddOrder(Order order)
        {
            await _eventDressRentalContext.Orders.AddAsync(order);
            await _eventDressRentalContext.SaveChangesAsync();
            return order;
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _eventDressRentalContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Dress).Include(s => s.Status).FirstOrDefaultAsync(o => o.Id == id);

        }
        public async Task<List<Order>> GetOrdersByUserId(int userId)
        {
            return await _eventDressRentalContext.Orders.Where(o => o.UserId == userId).Include(o => o.OrderItems).ThenInclude(oi => oi.Dress).Include(s => s.Status).OrderBy(o=>o.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetAllOrders()
        {
            return await _eventDressRentalContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Dress).OrderBy(o => o.OrderDate).Include(s => s.Status).ToListAsync();
        }
        public async Task<List<Order>> GetUnpackedOrdersUntilDate(DateOnly date)
        {
          return await _eventDressRentalContext.Orders.Where(o => o.EventDate <= date && o.StatusId == 1).Include(o => o.OrderItems).ThenInclude(oi => oi.Dress).ToListAsync();
        }
        public async Task UpdateStatusOrder(Order order)
        {
            await _eventDressRentalContext.Orders
            .Where(d => d.Id == order.Id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(d => d.StatusId, order.StatusId));
        }
        public async Task UpdateOrder(Order order)
        {
            _eventDressRentalContext.Orders.Update(order);
            await _eventDressRentalContext.SaveChangesAsync();
        }
        public async Task<bool> IsExistsOrderById(int id)
        {
            return await _eventDressRentalContext.Orders.AnyAsync(c => c.Id == id);
        }
        }
    }
