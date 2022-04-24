namespace Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(string id)
        {
            User user = _context.User
                .Include(b => b.Basket).ThenInclude(ol => ol.OrderLines).ThenInclude(pv => pv.PizzaVariation).ThenInclude(p => p.Pizza)
                .Include(o => o.Orders)
                .IgnoreAutoIncludes()
                .FirstOrDefault(u => u.Id == id);

            return user;
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User InitializeBasket(string id)
        {
            User user = _context.User
                .Include(b => b.Basket).ThenInclude(ol => ol.OrderLines)
                .Include(o => o.Orders)
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == id);

            user.Basket = new Basket();

            var entity = _context.Update(user);
            _context.SaveChanges();
            return entity.Entity;
        }

        public User InitializeBasketByPhone(string phone)
        {
            User user = _context.User
                .Include(b => b.Basket).ThenInclude(ol => ol.OrderLines)
                .Include(o => o.Orders)
                .AsNoTracking()
                .FirstOrDefault(u => u.UserName == phone);

            user.Basket = new Basket();

            var entity = _context.Update(user);
            _context.SaveChanges();
            return entity.Entity;
        }
    }
}
