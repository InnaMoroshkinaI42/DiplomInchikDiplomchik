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
    /// Логика взаимодействия для next.xaml
    /// </summary>
    public partial class next : Page
    {
        public next()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x
                    => x.Login == txtLogin.Text && x.Password == Password.Password);
                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    AccountHelpClass.Id = userObj.ID_employee;
                    switch (userObj.Id_post)
                    {
                        case 1:
                            MessageBox.Show("Добро пожаловать в систему, Администратор " + userObj.FIO + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.framelMain.Navigate(new MenuAdmin());
                            break;
                        case 2:
                            MessageBox.Show("Добро пожаловать в систему, старший менеджер " + userObj.FIO + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.framelMain.Navigate(new Menu());
                            break; 
                        case 3:
                            MessageBox.Show("Добро пожаловать в систему, менеджер " + userObj.FIO + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.framelMain.Navigate(new Menu());
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }

                Hiistoryy historyObj = new Hiistoryy()
                {
                    Id_Employee = AccountHelpClass.Id,
                    IdStatus = 1,
                    DateEvent = DateTime.Now
                };

                DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                DiplomchikEntities.GetContext().SaveChanges();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void btnRegistr_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new registr());
        }

        private void IconPasswordN1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IconPasswordN1.Visibility = Visibility.Hidden;
            IconPasswordN2.Visibility = Visibility.Visible;
            PasswordTextBox.Text = Password.Password;
            Password.Visibility = Visibility.Hidden;
            PasswordTextBox.Visibility = Visibility.Visible;
        }

        private void IconPasswordN2_MouseLeave(object sender, MouseEventArgs e)
        {
            IconPasswordN2.Visibility = Visibility.Hidden;
            IconPasswordN1.Visibility = Visibility.Visible;
            Password.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Hidden;
        }

        private void IconPasswordN2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IconPasswordN2.Visibility = Visibility.Hidden;
            IconPasswordN1.Visibility = Visibility.Visible;
            Password.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Hidden;
        }
    }
}
