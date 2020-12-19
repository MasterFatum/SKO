using System;
using System.Windows;
using Bll.Concrete;
using BLL.Concrete;

namespace AdminSystem.Forms
{
    public partial class FormSummaryStatement
    {
        public FormSummaryStatement()
        {
            InitializeComponent();
        }

        UserRepository userRepository = new UserRepository();

        CourseRepository courseRepository = new CourseRepository();

        OtherRepository otherRepository = new OtherRepository();

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CbxUsersSummaryStatement.ItemsSource = new UserRepository().GetFioUsers();
        }

        private void CbxUsersSummaryStatement_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (CbxUsersSummaryStatement.SelectedIndex != -1)
                {
                    String usernameFio = CbxUsersSummaryStatement.SelectedItem.ToString();

                    String[] words = usernameFio.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    DataGridSummaryStatement.ItemsSource =
                        courseRepository.GetSummaryStatementByFio(words[0], words[1], words[2]);

                    otherRepository.SettingDataGridSummaryStatement(DataGridSummaryStatement);

                    int userId = userRepository.GetUserIdByFio(words[0], words[1], words[2]);

                    LblSummary.Content = courseRepository.AllRating(userId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
