using Ads.Database;
using System.Collections.Generic;

namespace Ads.Services
{
    public interface IUserDbContext
    {
        User Add(User user);
        void Delete(int userId);
        User GetById(int userId);
        User Update(User user);
        User FindByEmail(string email);
    }
}