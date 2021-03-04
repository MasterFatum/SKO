using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Bll.Concrete;
using BLL.Concrete;
using BLL.Entities;
using TeacherSystem.FormsAddEducations;
using UserSystem.FormsAddEducations;

namespace TeacherSystem
{
    public partial class MainWindow
    {
        readonly CourseRepository courseRepository = new CourseRepository();
        readonly OtherRepository otherRepository = new OtherRepository();
        readonly FtpRepository ftpRepository = new FtpRepository();

        public MainWindow(User user)
        {
            InitializeComponent();

            DataGridMain.ItemsSource = courseRepository.GetCoursesByUserId(user.Id);

            TxbxAllRating.Text = courseRepository.AllRating(user.Id);

            CbxMainShowCategory.SelectedIndex = -1;

            TxbxUserId.Text = user.Id.ToString();
            TxbxUserLastname.Text = user.Lastname;
            TxbxUserFirstname.Text = user.Firstname;
            TxbxUserMiddlename.Text = user.Middlename;
            TxbxUserPosition.Text = user.Position;
            Email = user.Email;

            UserIsOnline = new UserRepository().UserIsOnline(user.Id);

        }

        private void MainForm_Loaded(object sender, RoutedEventArgs e)
        {
            otherRepository.SettingDataGridUsers(DataGridMain);
        }

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
        public string Date { get; set; }
        public string Hyperlink { get; set; }
        public string FileName { get; set; }
        public bool UserIsOnline { get; set; }

        private void BtnMainExit_Click(object sender, RoutedEventArgs e)
        {
            UserIsOnline = new UserRepository().UserIsOffline(Convert.ToInt32(TxbxUserId.Text));
            Application.Current.Shutdown();
        }

        private void BtnMainAdd_Click(object sender, RoutedEventArgs e)
        {
            new FormChooseCategory(Convert.ToInt32(TxbxUserId.Text)).ShowDialog();
        }

        private void BtnMainUpdate_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Course> allCourseses = courseRepository.GetCoursesByUserId(Convert.ToInt32(TxbxUserId.Text));
            DataGridMain.ItemsSource = allCourseses;
            CbxMainShowCategory.SelectedIndex = -1;
            otherRepository.SettingDataGridUsers(DataGridMain);
        }

        private void DataGridMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var items = DataGridMain.CurrentItem as Course;

                if (items == null)
                {
                    return;
                }

                Id = items.Id;
                UserId = items.UserId;
                Category = items.Category;
                Title = items.Title;
                Description = items.Description;
                Evaluation = items.Evaluation;
                Hyperlink = items.Hyperlink;
                FileName = items.FileName;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnMainDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить данную запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                courseRepository.DeleteCourse(Id, UserId);

                if (!String.IsNullOrEmpty(FileName))
                {
                    ftpRepository.DeleteFile(UserId.ToString(), FileName);
                }
                
                BtnMainUpdate_Click(null, null);
            }
        }

        private void BtnMainEdit_Click(object sender, RoutedEventArgs e)
        {
            new FormEdit(Id, UserId, Category, Title, Description, Hyperlink, FileName).ShowDialog();
        }

        private void CbxMainShowCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbxMainShowCategory.SelectedIndex != -1)
            {
                DataGridMain.ItemsSource = courseRepository.GetCoursesByCategory(Convert.ToInt32(TxbxUserId.Text), (((ComboBoxItem)CbxMainShowCategory.SelectedItem).Content.ToString()));

                new OtherRepository().SettingDataGridUsers(DataGridMain);
            }
        }

        private void BtnMainOtherUsersCourses_Click(object sender, RoutedEventArgs e)
        {
            new FormOtherUsersCourses().ShowDialog();
        }

        private void DataGridMain_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var items = DataGridMain.CurrentItem as Course;

                if (items == null)
                {
                    return;
                }

                UserId = items.UserId;
                Category = items.Category;
                Title = items.Title;
                Description = items.Description;
                Evaluation = items.Evaluation ?? 0;
                Date = items.Date ?? "Дата отсутствует";
                Hyperlink = items.Hyperlink;
                FileName = items.FileName;

                new FormViewItemsFull(UserId, "Вы", Category, Title, Description, Date, Hyperlink, FileName, Evaluation.Value).ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new FormProfile(Convert.ToInt32(TxbxUserId.Text), 
                TxbxUserLastname.Text, TxbxUserFirstname.Text, 
                TxbxUserMiddlename.Text, TxbxUserPosition.Text, Email).ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (e.Cancel == false)
            {
                UserIsOnline = new UserRepository().UserIsOffline(Convert.ToInt32(TxbxUserId.Text));
                Application.Current.Shutdown();
            }
        }


        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            new FormInfo().ShowDialog();
        }

        readonly SolidColorBrush orange = new SolidColorBrush(Colors.Orange);
        readonly SolidColorBrush white = new SolidColorBrush(Colors.White);
        readonly SolidColorBrush green = new SolidColorBrush(Colors.Green);

        private void DataGridMain_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Course course = (Course)e.Row.DataContext;

            if (course.Evaluation == null)
            {
                e.Row.Background = orange;
            }
            if (course.Evaluation != null && course.DateEdit == null)
            {
                e.Row.Background = white;
            }
            if (course.Evaluation != null && course.DateEdit != null)
            {
                e.Row.Background = green;
            }
        }

        private void BtnUsersIsOnline_Click(object sender, RoutedEventArgs e)
        {
            new FormUsersOnline().ShowDialog();
        }
    }
}
