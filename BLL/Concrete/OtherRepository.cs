using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace BLL.Concrete
{
    public class OtherRepository
    {
        public void SettingDataGridUsers(DataGrid dataGrid)
        {
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
            dataGrid.Columns[1].Visibility = Visibility.Hidden;
            dataGrid.Columns[4].Visibility = Visibility.Hidden;
            dataGrid.Columns[8].Visibility = Visibility.Hidden;
            dataGrid.Columns[10].Visibility = Visibility.Hidden;

            dataGrid.Columns[2].Header = "Категория";
            dataGrid.Columns[3].Header = "Название";
            dataGrid.Columns[5].Header = "Баллы";
            dataGrid.Columns[6].Header = "Оценивающий";
            dataGrid.Columns[7].Header = "Дата создания";
            dataGrid.Columns[9].Header = "Дата редактирования";
            dataGrid.Columns[10].Visibility = Visibility.Hidden;

        }

        public void SettingDataGridAdmins(DataGrid dataGrid)
        {
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
            dataGrid.Columns[5].Visibility = Visibility.Hidden;
            dataGrid.Columns[7].Visibility = Visibility.Hidden;

            dataGrid.Columns[1].Header = "Фамилия";
            dataGrid.Columns[2].Header = "Имя";
            dataGrid.Columns[3].Header = "Отчество";
            dataGrid.Columns[4].Header = "Должность";
            dataGrid.Columns[6].Header = "Email";
            dataGrid.Columns[8].Header = "Дата регистрации";
            dataGrid.Columns[9].Visibility = Visibility.Hidden;
            dataGrid.Columns[10].Header = "Онлайн";

        }

        public void SettingDataGridSummaryStatement(DataGrid dataGrid)
        {
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
            dataGrid.Columns[1].Visibility = Visibility.Hidden;

            dataGrid.Columns[2].Header = "Категория";
            dataGrid.Columns[3].Visibility = Visibility.Hidden;
            dataGrid.Columns[4].Visibility = Visibility.Hidden; 
            dataGrid.Columns[5].Header = "Оценка";
            dataGrid.Columns[6].Visibility = Visibility.Hidden;
            dataGrid.Columns[7].Visibility = Visibility.Hidden;
            dataGrid.Columns[8].Visibility = Visibility.Hidden;
            dataGrid.Columns[9].Visibility = Visibility.Hidden;
            dataGrid.Columns[10].Visibility = Visibility.Hidden;

        }

        public void SettingDataGridSummaryStatementGeneral(DataGrid dataGrid)
        {
            dataGrid.Columns[0].Header = "Фамилия";
            dataGrid.Columns[1].Header = "Имя";
            dataGrid.Columns[2].Header = "Отчество";
            dataGrid.Columns[3].Header = "Общий балл";
        }
    }
}
