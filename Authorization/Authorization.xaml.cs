using System;
using System.Windows;
using System.Windows.Controls;
using BLL.Concrete;
using BLL.Entities;

namespace Authorization
{
    public partial class MainWindow
    {
        UserRepository userRepository = new UserRepository();

        private const string ConnectionStringInside = "data source=NETSCHOOL;initial catalog=SOKO;integrated security=False;User ID=SOKOUser;Password=Admin;MultipleActiveResultSets=True;App=EntityFramework";
        private const string ConnectionStringOutside = "data source=212.44.132.205,1435;initial catalog=SOKO;integrated security=False;User ID=SOKOUser;Password=Admin;MultipleActiveResultSets=True;App=EntityFramework";

        public MainWindow()
        {
            InitializeComponent();

            if (Properties.Settings.Default.IsSaveUser)
            {
                TxbxLogin.Text = Properties.Settings.Default.Username;
                TxbxPassword.Password = Properties.Settings.Default.Password;
                ChkBoxSaveUser.IsChecked = Properties.Settings.Default.IsSaveUser;
            }
        }
        
        private void BtnAuthorizeExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new Registrations().ShowDialog();
        }

        private void BtnAuthorize_Click(object sender, RoutedEventArgs e)
        {

            if (CbxSetConnectionString.SelectedIndex != - 1)
            {
                try
                {
                    switch (((ComboBoxItem)CbxAuthorizeAs.SelectedItem).Content.ToString())
                    {


                        //АВТОРИЗАЦИЯ ПОЛЬЗОВАТЕЛЯ
                        case "Преподаватель":
                            User user = userRepository.ValidationUser(TxbxLogin.Text.Trim(), TxbxPassword.Password);

                            if (user != null)
                            {

                                if (ChkBoxSaveUser.IsChecked == true)
                                {
                                    Properties.Settings.Default.Username = TxbxLogin.Text.Trim();
                                    Properties.Settings.Default.Password = TxbxPassword.Password.Trim();
                                    Properties.Settings.Default.IsSaveUser = true;

                                    Properties.Settings.Default.Save();
                                }
                                if (ChkBoxSaveUser.IsChecked == false)
                                {
                                    Properties.Settings.Default.Username = String.Empty;
                                    Properties.Settings.Default.Password = String.Empty;
                                    Properties.Settings.Default.IsSaveUser = false;

                                    Properties.Settings.Default.Save();
                                }


                                Visibility = Visibility.Collapsed;
                                Visibility = Visibility.Hidden;
                                new TeacherSystem.MainWindow(user).ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                            }
                            break;

                        //АВТОРИЗАЦИЯ АДМИНИСТРАТОРА
                        case "Администратор":
                            User userAdmin = userRepository.ValidationAdmin(TxbxLogin.Text.Trim(), TxbxPassword.Password);

                            if (userAdmin != null)
                            {
                                if (ChkBoxSaveUser.IsChecked == true)
                                {
                                    Properties.Settings.Default.Username = TxbxLogin.Text.Trim();
                                    Properties.Settings.Default.Password = TxbxPassword.Password.Trim();
                                    Properties.Settings.Default.IsSaveUser = true;

                                    Properties.Settings.Default.Save();
                                }
                                if (ChkBoxSaveUser.IsChecked == false)
                                {
                                    Properties.Settings.Default.Username = String.Empty;
                                    Properties.Settings.Default.Password = String.Empty;
                                    Properties.Settings.Default.IsSaveUser = false;

                                    Properties.Settings.Default.Save();
                                }

                                Visibility = Visibility.Collapsed;
                                Visibility = Visibility.Hidden;
                                new AdminSystem.MainWindow(userAdmin).ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Application.Current.Shutdown();
                }
            }
            else
            {
                MessageBox.Show("Выберите тип подключения к сети!", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

        }

        private void AuthorizeForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (e.Cancel == false)
            {
                Application.Current.Shutdown();
            }
        }

        private void CbxSetConnectionString_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbxSetConnectionString.SelectedIndex == 0)
            {
                new OtherRepository().SetConnectionString(ConnectionStringInside);
            }
            else if (CbxSetConnectionString.SelectedIndex == 1)
            {
                new OtherRepository().SetConnectionString(ConnectionStringOutside);
            }
            
        }
    }
}
