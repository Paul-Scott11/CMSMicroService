using CMSMicroService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSMicroService
{
    public class Common
    {
        public static void AddUserToDB(ApiDBContext context, string username)
        {         
            var UserToAdd = new Models.UserModel
            {               
                username = username,
            };

            context.Users.Add(UserToAdd);

            context.SaveChanges();
            
        }

        public static void UpdateDBUser(ApiDBContext context, int userId, string username)
        {
            var UserToUpdate = new UserModel
            {
                userId = userId,
                username = username
            };

            context.Users.UpdateRange(UserToUpdate);

            context.SaveChanges();
        }

        public static void DeleteDBUser(ApiDBContext context, int userId)
        {
            var employer = new UserModel { userId = userId };
            context.Users.Remove(employer);
            context.SaveChanges();
        }

        public static bool CheckUserExists(ApiDBContext context, string username)
        {
            //Check if the user exists
            if (context.Users.Any(o => o.username == username))
            {
                return true;
            }
                return false;
        }

        public static bool CheckUserExistsById(ApiDBContext context, int userId)
        {
            //Check if the user exists
            if (context.Users.Any(o => o.userId == userId))
            {
                return true;
            }
            return false;
        }

        public static int GetMaxID(ApiDBContext context)
        {
            return context.Users.OrderByDescending(u => u == null ? 0 : u.userId).FirstOrDefault().userId;
        }

        }
}
