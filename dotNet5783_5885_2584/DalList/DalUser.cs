using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal struct DalUser : IUser
    {
        /// <summary>
        /// add order to orders list of specific user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="orderID"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Create(User entity)
        {
            int id = Config.UserID;
            entity.ID = id;
            s_users.Add(entity);
            return id;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            s_users.RemoveAll(x => x?.ID == id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public User Read(Func<User?, bool>? f)
        {
            User? user = new();
            if (f != null)
            {
                user = s_users.Find(x => f(x));
            }
              return user ?? default;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<User?> ReadAll(Func<User?, bool>? f = null)
        {
            return (from user in s_users select user);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(User entity)
        {
            s_users.RemoveAll(x => x?.ID == entity.ID);
            s_users.Add(entity);
        }


    }
}
