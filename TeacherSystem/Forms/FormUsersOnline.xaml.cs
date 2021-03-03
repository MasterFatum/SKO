using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BLL.Concrete;

namespace TeacherSystem.FormsAddEducations
{
    public partial class FormUsersOnline : Window
    {
        public FormUsersOnline()
        {
            InitializeComponent();
            
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                IEnumerable<UsersInOnline> users = new UserRepository().UsersInOnline();

                var usersInOnlines = users as UsersInOnline[] ?? users.ToArray();
                LblUsersCount.Content = usersInOnlines.Count();

                foreach (var usersInOnline in usersInOnlines)
                {
                    LbUserOnline.Items.Add($"{usersInOnline.LastName} {usersInOnline.FirstName} {usersInOnline.MiddleName} ({usersInOnline.Position})");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
