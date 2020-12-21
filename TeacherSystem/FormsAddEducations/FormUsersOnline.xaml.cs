using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL.Concrete;

namespace TeacherSystem.FormsAddEducations
{
    /// <summary>
    /// Логика взаимодействия для FormUsersOnline.xaml
    /// </summary>
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
            IEnumerable<UsersInOnline> users = new UserRepository().UsersInOnline();

            LblUsersCount.Content = users.Count();

            foreach (var usersInOnline in users)
            {
                LbUserOnline.Items.Add($"{usersInOnline.LastName} {usersInOnline.FirstName} {usersInOnline.MiddleName} ({usersInOnline.Position})");
            }
        }
    }
}
