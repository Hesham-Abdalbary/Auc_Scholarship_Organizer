using Auc_Scholarship_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.Repositories
{
    public class UserRepository
    {
        private Context context;
        public UserRepository()
        {
            context = new Context();
        }

        public User GetUser(Func<User, bool> condition)
        {
            return context.User.AsNoTracking().Where(condition).FirstOrDefault();
        }

        public User GetUserAndApplication(Func<User, bool> condition)
        {
            var user = context.User.Include("StudentApplication").AsNoTracking().Where(condition).FirstOrDefault();
            return user;
        }

        public IEnumerable<User> GetUsers(Func<User, bool> condition)
        {
            if (condition != null)
                return context.User.AsNoTracking().Where(condition);
            else
                return context.User;
        }

        public IEnumerable<User> GetUsersAndApplications(Func<User, bool> condition)
        {
            if (condition != null)
                return context.User.Include("StudentApplication").AsNoTracking().Where(condition);
            else
                return context.User;
        }

        public User Add(User entity)
        {
            var addedUser = context.User.Add(entity);
            context.SaveChanges();
            return addedUser;
        }

        public int Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            return context.SaveChanges();
        }
    }
}