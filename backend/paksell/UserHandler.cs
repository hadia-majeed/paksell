using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace paksell.Db
{
    public class UserHandler
    {
        public List<User> GetUsers()
        {
            using (paksellContext context = new paksellContext())
            {
                return context.User.ToList();
            }
        }
        public User? GetUser(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                return context.User.FirstOrDefault(a => a.Id == id);

            }

        }
        public User? GetUser(string loginId, string password)
        {
            using (paksellContext context = new paksellContext())
            {
                return (from u in context.User
                        where u.LoginId == loginId && u.Password == password
                        select u).FirstOrDefault();
            }
        }
        public User? AddUser(User user)
        {
            using (paksellContext context = new paksellContext())
            {
                context.Add(user);
                context.SaveChanges();
                return user;
            }
        }
        public User? UpdateUser(User user)
        {
            using (paksellContext context = new paksellContext())
            {
                context.Update(user);
                context.SaveChanges();
                return user;
            }
        }

        public User? DeleteUser(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                var found = context.User.FirstOrDefault(a =>a.Id == id);
                if (found != null)
                {
                    context.Remove(found);
                    context.SaveChanges();
                    return found;
                }
                return null;
            }
        }

      








    }

}
