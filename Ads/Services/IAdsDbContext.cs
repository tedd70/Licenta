using Ads.Database;
using System.Collections.Generic;

namespace Ads.Services
{
    public interface IAdsDbContext
    {
        Advert Add(Advert advert);
        void Delete(int advertId);
        Advert GetById(int advertId);
        Advert Update(Advert advert);
        List<Advert> GetAll();
        List<Advert> GetAllByUserId(int userId);
    }
}