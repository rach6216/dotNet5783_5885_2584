using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;

namespace Dal
{
    internal struct DalUser : IUser
    {
        /// <summary>
        /// add order to orders list of specific user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="orderID"></param>
        public void AddOrder(int userID, int orderID)
        {
            DO.User user = Read(x => x?.ID == userID);
            user.Orders.Add(orderID);
            Update(user);
        }

        public int Create(User entity)
        {
            int id = Config.UserID;
            entity.ID = id;
            s_users.Add(entity);
            return id;
        }

        public void Delete(int id)
        {
           s_users.RemoveAll(x=>x?.ID==id);
        }

        public int Login(string username, string password)
        {
            User user = Read(x => x?.UserName == username);
            if (user.Password != password)
                throw new ExceptionEntityNotFound("user not found");//ניצור שגיאה של סיסמה לא נכונה   
            return user.ID;
        }

        public User Read(Func<User?, bool>? f)
        {
            try
            {
                return s_users.FirstOrDefault(f) ?? new User();

            }
            catch(ArgumentNullException exp)
            {
                throw exp;
            }

        }

        public IEnumerable<User?> ReadAll(Func<User?, bool>? f = null)
        {
            return (from user in s_users select user);
        }

        public void Update(User entity)
        {
            s_users.RemoveAll(x=>x?.ID==entity.ID);
            s_users.Add(entity);
        }
    }
}
