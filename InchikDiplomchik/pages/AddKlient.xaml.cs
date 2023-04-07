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
    /// Логика взаимодействия для AddKlient.xaml
    /// </summary>
    public partial class AddKlient : Page
    {
        private Client _client = new Client();
        public AddKlient(Client clientSelect)
        {
            InitializeComponent();
            tipZakaza.ItemsSource= DiplomchikEntities.GetContext().ClientType.ToList();

            if (clientSelect != null)
                _client = clientSelect;

            DataContext = _client;

            if (ClassAddEdit.Id == 1)
            {
                dateDateOfBirth.SelectedDate = DateTime.Today;
            }
            else
            {
                dateDateOfBirth.IsEnabled = false;
                dateDateOfBirth.SelectedDate = _client.DateOfBirth;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_client.FIO))
            {
                podpos.Visibility = Visibility.Visible;
                FIO1.BorderBrush = Brushes.Red;
                errors.Append("Укажите ФИО клиента ");
            }
            if (string.IsNullOrWhiteSpace(_client.INN))
            {
                podpos1.Visibility = Visibility.Visible;
                inn.BorderBrush = Brushes.Red;
                errors.Append("Укажите ИНН клиента ");
            }
            if (string.IsNullOrWhiteSpace(_client.Address))
            {
                podpos2.Visibility = Visibility.Visible;
                adres.BorderBrush = Brushes.Red;
                errors.Append("Укажите адрес клиента ");
            }
            if (string.IsNullOrWhiteSpace(_client.Telephone))
            {
                telephon.BorderBrush = Brushes.Red;
                errors.Append("Укажите телефон клиента ");
            }
            if (string.IsNullOrWhiteSpace(_client.Email))
            {
                podpos3.Visibility = Visibility.Visible;
                email1.BorderBrush = Brushes.Red;
                errors.Append("Укажите email клиента ");
            }
            if (string.IsNullOrWhiteSpace(_client.Pasport))
            {
                podpos4.Visibility = Visibility.Visible;
                pasportt.BorderBrush = Brushes.Red;
                errors.Append("Укажите паспорт клиента ");
            }

             if (errors.Length > 0)
             {
                 MessageBox.Show(errors.ToString());
                 return;
             }

          try
            {
                if (ClassAddEdit.Id == 1)
                {
                     if (_client.ID_client == 0)
                        DiplomchikEntities.GetContext().Client.Add(_client);

                    DiplomchikEntities.GetContext().SaveChanges();
                    Hiistoryy historyObj1 = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 3,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj1);
                    MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (ClassAddEdit.Id==2)
                {
                     Hiistoryy historyObj = new Hiistoryy()
                     {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 5,
                        DateEvent = DateTime.Now
                     };
                     DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                     DiplomchikEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                AppFrame.framelMain.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.GoBack();
        }

        private void FIO1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void inn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PersonalPaymentAcc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
