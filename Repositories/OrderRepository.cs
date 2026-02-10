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
            return await _eventDressRentalContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Dress).FirstOrDefaultAsync(o => o.Id == id);

        }
        public async Task<List<Order>> GetOrdersByUserId(int userId)
        {
            return await _eventDressRentalContext.Orders.Where(o => o.UserId == userId).Include(o => o.OrderItems).ThenInclude(oi => oi.Dress).OrderBy(o=>o.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetAllOrders()
        {
            return await _eventDressRentalContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Dress).OrderBy(o => o.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetOrderByDates(DateOnly date)
        {
          return await _eventDressRentalContext.Orders.Where(o => o.EventDate <= date && o.EventDate >= DateOnly.FromDateTime(DateTime.Now)).Include(o => o.OrderItems).ThenInclude(oi => oi.Dress).ToListAsync();
        }
    }
}
