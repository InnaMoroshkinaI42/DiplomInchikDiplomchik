using InchikDiplomchik.ApplicatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InchikDiplomchik.pages
{
    /// <summary>
    /// Логика взаимодействия для registr.xaml
    /// </summary>
    public partial class registr : Page
    {
        public registr()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (DiplomchikEntities.GetContext().Employee.Count(x => x.Login == txtLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {

                DiplomchikEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnRegistr_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.GoBack();
        }

        private void txtPassword2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password != txtPassword2.Password)
            {
                btnNext.IsEnabled = false;
                txtPassword2.Background = Brushes.LightCoral;
                txtPassword2.BorderBrush = Brushes.Red;
            }
            else
            {
                btnNext.IsEnabled = true;
                txtPassword2.Background = Brushes.LawnGreen;
                txtPassword2.BorderBrush = Brushes.LightGreen;
            }
        }
    }
}
