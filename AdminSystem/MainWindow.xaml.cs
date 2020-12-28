using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AdminSystem.Forms;
using BLL.Concrete;
using BLL.Entities;

namespace AdminSystem
{
    public partial class MainWindow
    {
        readonly UserRepository userRepository = new UserRepository();

        public string User { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public new string Title { get; set; }
        public string Description { get; set; }
        public int? Evaluation { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }

        public MainWindow(User user)
        {
            InitializeComponent();

            User = user.Lastname;

            DataGridAllUsers.ItemsSource = userRepository.GetAllUser();

            LblAllUsers.Content = userRepository.GetAllUsersCount();
        }

        private void BtnMainExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (e.Cancel == false)
            {
                Application.Current.Shutdown();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new OtherRepository().SettingDataGridAdmins(DataGridAllUsers);
        }

        private void DataGridAllUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var items = DataGridAllUsers.CurrentItem as User;

                if (items == null)
                {
                    return;
                }

                Id = items.Id;
                Lastname = items.Lastname;
                Firstname = items.Firstname;
                Middlename = items.Middlename;

                new FormViewCoursesUser(Id, Lastname, Firstname, Middlename, User).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnMainUsersManage_Click(object sender, RoutedEventArgs e)
        {
            new FormUsersManager(User).ShowDialog();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView viewSource = CollectionViewSource.GetDefaultView(DataGridAllUsers.ItemsSource);

            if (TxbxSearch.Text.Trim() == String.Empty)
            {
                viewSource.Filter = null;
            }
            else
            {
                viewSource.Filter = o =>
                {
                    User u = o as User;

                    return u.Lastname.ToString().Contains(TxbxSearch.Text.Trim());

                };
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGridAllUsers.ItemsSource = new UserRepository().GetAllUser();
            TxbxSearch.Text = String.Empty;

            new OtherRepository().SettingDataGridAdmins(DataGridAllUsers);
        }

        private void BtnSummaryStatement_Click(object sender, RoutedEventArgs e)
        {
            new FormChooseSheet().ShowDialog();
        }

        private void TxbxSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TxbxSearch.Text.Length == 1)
            {
                TxbxSearch.Text = TxbxSearch.Text.ToUpper();
                TxbxSearch.Select(TxbxSearch.Text.Length, 0);
            }
        }

        private void BtnUsersIsOnline_Click(object sender, RoutedEventArgs e)
        {
            new FormUsersOnline().ShowDialog();
        }
    }
}
