using System.Windows;


namespace AdminSystem.Forms
{
    public partial class FormChooseSheet : Window
    {
        public FormChooseSheet()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnIndividual_Click(object sender, RoutedEventArgs e)
        {
            new FormSummaryStatement().ShowDialog();
        }

        private void BtnGeneral_Click(object sender, RoutedEventArgs e)
        {
            new FormSummaryStatementGeneral().ShowDialog();
        }
    }
}
