using Ads.Database;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Ads.Services
{
    public class UserDbContext : DbContext, IUserDbContext
    {
        public User FindByEmail(string email)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Users.FirstOrDefault(s => s.Email == email);
                return item;
            }
        }

        public User GetById(int userId)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Users.FirstOrDefault(s => s.Id == userId);
                if (item == null)
                {
                    throw new NullReferenceException($"User {userId} not found");
                }
                return item;
            }
        }

        public User Add(User user)
        {
            using (var context = new AdvertsEntities())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return GetById(user.Id);
        }

        public User Update(User user)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Users.FirstOrDefault(s => s.Id == user.Id);
                if (item == null)
                {
                    throw new NullReferenceException($"User {user.Id} not found");
                }
                context.Entry(item).CurrentValues.SetValues(user);
                context.SaveChanges();
            }

            return user;
        }

        public void Delete(int userId)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Users.FirstOrDefault(s => s.Id == userId);
                if (item == null)
                {
                    throw new NullReferenceException($"User {userId} not found");
                }
                context.Users.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
