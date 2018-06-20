using Ads.Database;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Ads.Services
{
    public class AdsDbContext : DbContext, IAdsDbContext
    {
        public Advert GetById(int advertId)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Adverts.Where(s => s.Id == advertId).FirstOrDefault();
                if (item == null)
                {
                    throw new NullReferenceException($"Advert {advertId} not found");
                }
                return item;
            }
        }

        public List<Advert> GetAll()
        {
            using (var context = new AdvertsEntities())
            {
                return context.Adverts.ToList();
            }
        }

        public List<Advert> GetAllByUserId(int userId)
        {
            using (var context = new AdvertsEntities())
            {
                return context.Adverts.Where(x => x.UserId == userId).ToList();
            }
        }

        public Advert Add(Advert advert)
        {
            using (var context = new AdvertsEntities())
            {
                context.Adverts.Add(advert);
                context.SaveChanges();
            }

            return GetById(advert.Id);
        }

        public Advert Update(Advert advert)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Adverts.Where(s => s.Id == advert.Id).FirstOrDefault();
                if (item == null)
                {
                    throw new NullReferenceException($"Advert {advert.Id} not found");
                }
                context.Entry(item).CurrentValues.SetValues(advert);
                context.SaveChanges();
            }

            return advert;
        }

        public void Delete(int advertId)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Adverts.FirstOrDefault(s => s.Id == advertId);
                if (item == null)
                {
                    throw new NullReferenceException($"Advert {advertId} not found");
                }
                context.Adverts.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
