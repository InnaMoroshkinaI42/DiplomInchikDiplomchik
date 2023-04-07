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
using System.Windows.Shapes;

namespace InchikDiplomchik.pages
{
    /// <summary>
    /// Логика взаимодействия для PasswordEdit.xaml
    /// </summary>
    public partial class PasswordEdit : Window
    {
        private Employee _emplo = new Employee();
        public PasswordEdit(Employee employee)
        {
            InitializeComponent();

            if (employee != null)
                _emplo = employee;

            DataContext = _emplo;

            PasswordTBox.Text = Passd.Password;
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



        private void IcoasswordN1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IcoasswordN1.Visibility = Visibility.Hidden;
            IconPasswN2.Visibility = Visibility.Visible;
            PasswordTBox.Text = Passd.Password;
            Passd.Visibility = Visibility.Hidden;
            PasswordTBox.Visibility = Visibility.Visible;
        }

        private void IconPasswN2_MouseLeave(object sender, MouseEventArgs e)
        {
            IconPasswN2.Visibility = Visibility.Hidden;
            IcoasswordN1.Visibility = Visibility.Visible;
            Passd.Visibility = Visibility.Visible;
            PasswordTBox.Visibility = Visibility.Hidden;
        }

        private void IconPasswN2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IconPasswN2.Visibility = Visibility.Hidden;
            IcoasswordN1.Visibility = Visibility.Visible;
            Passd.Visibility = Visibility.Visible;
            PasswordTBox.Visibility = Visibility.Hidden;
        }



        private void IcoasordN1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IcoasordN1.Visibility = Visibility.Hidden;
            IconPasN2.Visibility = Visibility.Visible;
            PaordTBox.Text = Pas1.Password;
            Pas1.Visibility = Visibility.Hidden;
            PaordTBox.Visibility = Visibility.Visible;
        }

        private void IconPasN2_MouseLeave(object sender, MouseEventArgs e)
        {
            IconPasN2.Visibility = Visibility.Hidden;
            IcoasordN1.Visibility = Visibility.Visible;
            Pas1.Visibility = Visibility.Visible;
            PaordTBox.Visibility = Visibility.Hidden;
        }

        private void IconPasN2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IconPasN2.Visibility = Visibility.Hidden;
            IcoasordN1.Visibility = Visibility.Visible;
            Pas1.Visibility = Visibility.Visible;
            PaordTBox.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var PassObj = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id && x.Password == Password.Password);
                if (PassObj == null)
                {
                    Password.BorderBrush = Brushes.Red;
                    MessageBox.Show("Пароль введен неправильно!", "Ошибка при изменении пароля", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    AccountHelpClass.Id = PassObj.ID_employee;
                    Password.BorderBrush = Brushes.Green;
                    oldPassw.Visibility = Visibility.Hidden;
                    NewPass.Visibility = Visibility.Visible;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DiplomchikEntities.GetContext().Employee.Count(x => x.ID_employee == AccountHelpClass.Id && x.Password == PasswordTBox.Text) > 0)
            {
                MessageBox.Show("Нельзя использовать свой старый пароль, придумайте новый!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                DiplomchikEntities.GetContext().SaveChanges();
                MessageBox.Show("Пароль успешно изменен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                
                Hiistoryy historyObj = new Hiistoryy()
                {
                    Id_Employee = AccountHelpClass.Id,
                    IdStatus = 7,
                    DateEvent = DateTime.Now
                };

                DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                DiplomchikEntities.GetContext().SaveChanges();

                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении пароля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Pas1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Passd.Password != Pas1.Password)
            {
                nextBTN.IsEnabled = false;
                Pas1.Background = Brushes.LightCoral;
                Pas1.BorderBrush = Brushes.Red;
            }
            else
            {
                nextBTN.IsEnabled = true;
                Pas1.Background = Brushes.LawnGreen;
                Pas1.BorderBrush = Brushes.LightGreen;
            }
        }
    }
}
