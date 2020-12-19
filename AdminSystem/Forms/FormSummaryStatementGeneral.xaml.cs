using System.Windows;
using Bll.Concrete;
using BLL.Concrete;

namespace AdminSystem.Forms
{
    public partial class FormSummaryStatementGeneral
    {
        readonly UserRepository userRepository = new UserRepository();

        public FormSummaryStatementGeneral()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridTable.ItemsSource = userRepository.GetAllUsersName();

            new OtherRepository().SettingDataGridSummaryStatementGeneral(DataGridTable);
        }

        private void BtnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            new CourseRepository().ExportToExcel(DataGridTable);
        }
    }
}
