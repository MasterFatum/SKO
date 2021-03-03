using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using BLL.Concrete;
using Microsoft.Win32;

namespace UserSystem.FormsAddEducations
{
    
    public partial class FormViewItemsFull
    {
        FtpRepository ftpRepository = new FtpRepository();

        public int UserId { get; set; }
        public string MyUrlHyperlink { get; set; }
        public string FileName { get; set; }

        public FormViewItemsFull(int userId, string user, string category, string title, string description, string date, string hyperlink, string fileName, int evaluation = 0)
        {
            InitializeComponent();
            UserId = userId;
            TxbxUser.Text = user;
            TxbxCategory.Text = category;
            TxbxTitle.Text = title;
            TxbxDescription.Text = description;
            TxbxEvaluation.Text = evaluation.ToString();
            TxbxDate.Text = date;
            MyUrlHyperlink = hyperlink;
            FileName = fileName;

            if (String.IsNullOrEmpty(FileName))
            {
                BtnLocalMatherials.IsEnabled = false;
            }

            if (String.IsNullOrEmpty(MyUrlHyperlink))
            {
                Hyperlink.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            if (MyUrlHyperlink != String.Empty)
            {
                Process.Start(new ProcessStartInfo(MyUrlHyperlink));
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("Web-материалы данного курса отсутсвуют!", "", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void BtnLocalMatherials_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = @"Zip files (*.zip)|*.zip";
            saveFile.FileName = String.Format($"{TxbxUser.Text} {TxbxTitle.Text}");

            if (saveFile.ShowDialog() == true)
            {
                string fileLocalPath = saveFile.FileName;
                Task task = new Task(() => ftpRepository.DownloadFile(UserId.ToString(), fileLocalPath, FileName));
                task.Start();
            }
        }
    }
 }

