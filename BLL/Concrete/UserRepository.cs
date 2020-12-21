using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using BLL.Abstract;
using BLL.Entities;

namespace BLL.Concrete
{
    public class UserRepository : IUserRepository
    {
        readonly SkoContext skoContext = new SkoContext();

        public void AddUser(User user)
        {
            try
            {
                User userExists = skoContext.Users.FirstOrDefault(em => em.Email == user.Email);

                if (userExists == null)
                {
                    skoContext.Users.Add(user);

                    skoContext.SaveChanges();

                    MessageBox.Show($"Пользователь {user.Email} успешно зарегистрирован в системе!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Пользователь с таким Email уже существует! Пожалуйста введите другой Email!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        public void DeleteUser(int id)
        {
            try
            {
                User user = skoContext.Users.Find(id);

                if (user != null)
                {
                    skoContext.Users.Remove(user);
                    skoContext.SaveChanges();

                    MessageBox.Show("Пользователь успешно удалён!", "", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EditUser(int id, string lastname, string firstname, string middlename, string position, string email)
        {
            try
            {
                var user = skoContext.Users.Find(id);

                if (user != null)
                {
                    user.Lastname = lastname;
                    user.Firstname = firstname;
                    user.Middlename = middlename;
                    user.Position = position;
                    user.Email = email;

                    skoContext.Users.AddOrUpdate(user);

                    skoContext.SaveChanges();

                    MessageBox.Show("Ваш аккаунт успешно отредактирован!", "Редактирование пользователя", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EditUser(int id, string lastname, string firstname, string middlename, string position, string email, string password, string privilege)
        {
            try
            {
                var user = skoContext.Users.Find(id);

                if (user != null)
                {
                    user.Id = id;
                    user.Lastname = lastname;
                    user.Firstname = firstname;
                    user.Middlename = middlename;
                    user.Position = position;
                    user.Email = email;
                    user.Password = password;
                    user.Privilege = privilege;

                    skoContext.Users.AddOrUpdate(user);

                    skoContext.SaveChanges();

                    MessageBox.Show("Аккаунт успешно отредактирован!", "Редактирование пользователя", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public IEnumerable<User> GetAllUser()
        {
            try
            {
                IEnumerable<User> users = skoContext.Users.ToList();

                return users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }

        public IEnumerable<UserEvaluationSummary> GetAllUsersName()
        {
            try
            {
                IEnumerable<UserEvaluationSummary> users = skoContext.Users
                    .Select(x => new UserEvaluationSummary
                    {
                        LastName = x.Lastname,
                        FirstName = x.Firstname,
                        MidName = x.Middlename,
                        EvaluationSum = x.Courses.Sum(y => y.Evaluation)
                    })
                    .ToList();

                return users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }

        public List<String> GetFioUsers()
        {
            try
            {
                List<User> users = skoContext.Users.OrderBy(l => l.Lastname).ToList();

                List<String> userFio = new List<string>();

                foreach (var user in users)
                {
                    userFio.Add(String.Format($"{user.Lastname} {user.Firstname} {user.Middlename}"));
                }

                return userFio;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }

        public User ValidationUser(string username, string password)
        {
            try
            {
                User user = null;

                var findUser = skoContext.Users.FirstOrDefault(u => u.Email == username);

                if (findUser != null)
                {
                    if (findUser.Password == password)
                    {
                        user = findUser;

                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public User ValidationAdmin(string username, string password)
        {
            try
            {
                User user = null;

                var findUser = skoContext.Users.Where(p => p.Privilege == "Admin").FirstOrDefault(u => u.Email == username);

                if (findUser != null)
                {
                    if (findUser.Password == password)
                    {
                        user = findUser;
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }

        public void SokoDispose()
        {
            skoContext.Dispose();
        }

        public string GetAllUsersCount()
        {
            try
            {
                return skoContext.Users.Count().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public int GetUserIdByFio(string lastname, string firstname, string middlename)
        {
            User userId = null;

            try
            { 
                userId = skoContext.Users.Where(l => l.Lastname == lastname).Where(f => f.Firstname == firstname)
                    .FirstOrDefault(m => m.Middlename == middlename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (userId != null && userId.Id != 0)
            {
                return userId.Id;
            }
            return 0;
        }

        public bool UserIsOnline(int id)
        {
            try
            {
                var user = skoContext.Users.Find(id);

                if (user != null)
                {
                    user.IsOnline = true;

                    skoContext.Users.AddOrUpdate(user);

                    skoContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        public bool UserIsOffline(int id)
        {
            try
            {
                var user = skoContext.Users.Find(id);

                if (user != null)
                {
                    user.IsOnline = false;

                    skoContext.Users.AddOrUpdate(user);

                    skoContext.SaveChanges();

                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }

        public IEnumerable<UsersInOnline> UsersInOnline()
        {
            try
            {
                IEnumerable<UsersInOnline> users = skoContext.Users.Where(o => o.IsOnline)
                    .Select(x => new UsersInOnline
                    {
                        LastName = x.Lastname,
                        FirstName = x.Firstname,
                        MiddleName = x.Middlename,
                        Position = x.Position
                    })
                    .ToList();

                return users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }
    }
    public class UserEvaluationSummary
    {

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public int? EvaluationSum { get; set; }
    }

    public class UsersInOnline
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }
    }
}
