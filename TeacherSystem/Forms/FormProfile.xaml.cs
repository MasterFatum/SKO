using System;
using System.Windows;
using BLL.Concrete;

namespace UserSystem.FormsAddEducations
{
    public partial class FormProfile
    {

        public FormProfile(int id, string lastname, string firstname, string middlename, string position, string email)
        {
            InitializeComponent();

            TxbxUserId.Text = id.ToString();
            TxbxLastname.Text = lastname;
            TxbxFirstname.Text = firstname;
            TxbxMiddlename.Text = middlename;
            TxbxPosition.Text = position;
            TxbxEmail.Text = email;
        }

        UserRepository userRepository = new UserRepository();

        private void BtnChangeProfile_Click(object sender, RoutedEventArgs e)
        {
            if (TxBlEdit.Text == " Изменить")
            {
                TxBlEdit.Text = " Сохранить";
                TxbxLastname.IsEnabled = true;
                TxbxFirstname.IsEnabled = true;
                TxbxMiddlename.IsEnabled = true;
                TxbxPosition.IsEnabled = true;
                TxbxEmail.IsEnabled = true;
            }
            else if (TxBlEdit.Text == " Сохранить")
            {
                TxBlEdit.Text = " Изменить";
                TxbxLastname.IsEnabled = false;
                TxbxFirstname.IsEnabled = false;
                TxbxMiddlename.IsEnabled = false;
                TxbxPosition.IsEnabled = false;
                TxbxEmail.IsEnabled = false;

                userRepository.EditUser(Convert.ToInt32(TxbxUserId.Text), 
                    TxbxLastname.Text.Trim(), 
                    TxbxFirstname.Text.Trim(), 
                    TxbxMiddlename.Text.Trim(), 
                    TxbxPosition.Text.Trim(), TxbxEmail.Text.Trim());
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
