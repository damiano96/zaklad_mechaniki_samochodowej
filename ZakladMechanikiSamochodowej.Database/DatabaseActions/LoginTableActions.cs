﻿using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Database.DatabaseActions
{
    public class LoginTableActions
    {
        public static void SaveUser(User user)
        {
            var database = new DatabaseConnection();
            database.LoginTable.Add(user);
            database.SaveChanges();
        }

        public static List<User> GetAllAcceptedUsers()
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.LoginTable.Where(u => u.IsNew == false).ToList();
        }

        public static User? TryGetUserByName(string name)
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.LoginTable.SingleOrDefault(u => u.Username == name);
        }

        public static User? GetUserById(int id)
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.LoginTable.SingleOrDefault(u => u.Id == id);
        }

        public static List<User> GetAllNewUsers()
        {
            using var dbContext = new DatabaseConnection();
            return dbContext.LoginTable.Where(u => u.IsNew == true).ToList();
        }

        public static void UpdateUserInformation(User user)
        {
            using var dbContext = new DatabaseConnection();
            dbContext.Update(user);
            dbContext.SaveChanges();
        }

        public static void RemoveUser(User user)
        {
            using var dbContext = new DatabaseConnection();

            OrdersTableActions.RemoveAllUserOrders(user.Id);
            dbContext.Remove(user);
            dbContext.SaveChanges();
        }

        public static void AcceptUser(User user)
        {
            using var dbContext = new DatabaseConnection();
            user.IsNew = false;
            dbContext.Update(user);
            dbContext.SaveChanges();
        }

        public static List<User> FindMatchingAcceptedUsers(string phrase)
        {
            return GetAllAcceptedUsers().Where(u => u.Username.Contains(phrase)).ToList();
        }
    }
}
