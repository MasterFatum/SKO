using System;
using System.Collections.Generic;
using BLL.Concrete;
using BLL.Entities;

namespace BLL.Abstract
{
    interface IUserRepository
    {
        void AddUser(User user);

        void DeleteUser(int id);

        void EditUser(int id, string lastname, string firstname, string middlename, string position, string email);

        void EditUser(int id, string lastname, string firstname, string middlename, string position, string email, string password, string privilege);

        IEnumerable<User> GetAllUser();

        IEnumerable<UserEvaluationSummary> GetAllUsersName();

        List<String> GetFioUsers();

        User ValidationUser(string username, string password);

        User ValidationAdmin(string username, string password);

        void SokoDispose();

        string GetAllUsersCount();

        int GetUserIdByFio(string lastname, string firstname, string middlename);

    }
}
