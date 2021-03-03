using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Bll.Concrete;
using BLL.Concrete;
using BLL.Entities;
using MessageBox = System.Windows.MessageBox;

namespace UserSystem.FormsAddEducations
{
    public partial class FormEdit
    {
        CourseRepository courseRepository = new CourseRepository();
        FtpRepository ftpRepository = new FtpRepository();

        public int Id { get; set; }
        public int UserIdEdit { get; set; }
        public string CategoryEdit { get; set; }
        public string TitleEdit { get; set; }
        public string DescriptionEdit { get; set; }
        public string HyperlinkEdit { get; set; }
        public string FilePath { get; set; }
        public string FilePathNew { get; set; }
        public string DateEdit { get; set; }
        public string FileNameGuid { get; set; }
        public string FileNameOld { get; set; }


        public FormEdit(int id, int userIdEdit, string categoryEdit, string title, string description, string hyperlink, string filePath)
        {
            InitializeComponent();

            Id = id;
            UserIdEdit = userIdEdit;
            TxbxEditCategory.Text = categoryEdit;
            TxbxEditTitle.Text = title;
            TxbxEditDescription.Text = description;
            TxbxHyperlink.Text = hyperlink;
            FilePath = filePath;
        }

        private void BtnEditExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnEditSave_Click(object sender, RoutedEventArgs e)
        {
            Course course = null;

            if (TxbxFilePath.Text == String.Empty)
            {
                course = new Course
                {
                    Id = Id,
                    UserId = UserIdEdit,
                    Category = CategoryEdit,
                    Title = TxbxEditTitle.Text.Trim(),
                    Description = TxbxEditDescription.Text.Trim(),
                    Hyperlink = TxbxHyperlink.Text.Trim(),
                    FileName = FilePath
                };
            }
            

            if (TxbxFilePath.Text != String.Empty)
            {
                FileNameGuid = Guid.NewGuid().ToString();
                FileNameOld = FilePath;
                FilePath = FileNameGuid;

                course = new Course
                {
                    Id = Id,
                    UserId = UserIdEdit,
                    Category = CategoryEdit,
                    Title = TxbxEditTitle.Text.Trim(),
                    Description = TxbxEditDescription.Text.Trim(),
                    Hyperlink = TxbxHyperlink.Text.Trim(),
                    FileName = FilePath
                };

                Task taskDeleteOdlFile = new Task(() => ftpRepository.DeleteFile("/" + UserIdEdit + "/", FileNameOld));
                taskDeleteOdlFile.Start();

                Task taskAddNewFile = new Task(() => ftpRepository.UploadFile("/" + UserIdEdit + "/", FilePathNew, FileNameGuid));
                taskAddNewFile.Start();

                
            }

            courseRepository.EditCourse(course);
            Close();
            
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxbxEditTitle.Text = String.Empty;
            TxbxEditDescription.Text = String.Empty;
            TxbxHyperlink.Text = String.Empty;
        }

        private void BtnBrowseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = @"(*.zip)|*.zip";


            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                long fileLength = new FileInfo(openFileDialog.FileName).Length;

                if (fileLength > 52428800)
                {
                    MessageBox.Show("Размер файла превышает допустимый! Размер файла не должен превышать 50 MB.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    FilePathNew = openFileDialog.FileName;
                    TxbxFilePath.Text = FilePathNew;
                }
            }
        }
    }
}
