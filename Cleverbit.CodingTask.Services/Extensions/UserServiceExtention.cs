using Cleverbit.CodingTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cleverbit.CodingTask.Services.Extensions
{
    public static class UserServiceExtention
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}
