using System;
using System.Windows;
using System.Windows.Controls;
using Bll.Concrete;
using BLL.Concrete;
using BLL.Entities;

namespace UserSystem.FormsAddEducations
{
    public partial class FormOtherUsersCourses
    {
        public FormOtherUsersCourses()
        {
            InitializeComponent();
        }

        public int UserId { get; set; }
        public string Lastname { get; set; }
        public string Category { get; set; }
        public new string Title { get; set; }
        public string Description { get; set; }
        public int Evaluation { get; set; }
        public string Date { get; set; }
        public string Hyperlink { get; set; }
        public string Filename { get; set; }

        readonly CourseRepository courseRepository = new CourseRepository();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CbxUsers.ItemsSource = new UserRepository().GetFioUsers();
        }

        private void BtnShowOtherUsersCourses_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CbxUsers.SelectedIndex != -1)
                {

                    //АНАЛИЗИРУЕМ COMBOBOX

                    if (CbxCategory.SelectedIndex == 0)
                    {
                        String usernameFio = CbxUsers.SelectedItem.ToString();

                        String[] words = usernameFio.Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        DataGridOtherUsersCategory.ItemsSource =
                            courseRepository.GetCoursesByFio(words[0], words[1], words[2]);

                        new OtherRepository().SettingDataGridUsers(DataGridOtherUsersCategory);
                    }
                    else if (CbxCategory.SelectedIndex != 0)
                    {
                        String usernameFio = CbxUsers.SelectedItem.ToString();

                        String[] words = usernameFio.Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        DataGridOtherUsersCategory.ItemsSource = 
                            courseRepository.GetCoursesByFio(words[0], words[1], words[2], ((ComboBoxItem)CbxCategory.SelectedItem).Content.ToString());

                        new OtherRepository().SettingDataGridUsers(DataGridOtherUsersCategory);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Выберите пользователя!", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void DataGridOtherUsersCategory_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                var items = DataGridOtherUsersCategory.CurrentItem as Course;

                if (items == null)
                {
                    return;
                }

                UserId = items.UserId;
                Lastname = CbxUsers.SelectedItem.ToString();
                Category = items.Category;
                Title = items.Title;
                Description = items.Description;
                Evaluation = items.Evaluation ?? 0;
                Date = items.Date ?? "Дата отсутствует";
                Hyperlink = items.Hyperlink;
                Filename = items.FileName;

                new FormViewItemsFull(UserId, Lastname, Category, Title, Description, Date, Hyperlink, Filename, Evaluation).ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
