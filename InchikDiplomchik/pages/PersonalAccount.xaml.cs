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
    /// Логика взаимодействия для PersonalAccount.xaml
    /// </summary>
    public partial class PersonalAccount : Page
    {
        //private Employee _emp = new Employee();
        public PersonalAccount(Service service)
        {
            InitializeComponent();

            listView.ItemsSource = DiplomchikEntities.GetContext().Employee.Where(x => x.ID_employee == AccountHelpClass.Id).ToList();
           
            var emply = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id );
            txtNameUser.Text = emply.FIO.ToString();
            txtEmailUser.Text = emply.Email.ToString();
            txtPhoneUser.Text = emply.Telephon.ToString();

            fio.Text = emply.FIO.ToString();
            pasport.Text = emply.Pasport.ToString();
            adres1.Text = emply.Adress.ToString();
            phone.Text = emply.Telephon.ToString();
            emal.Text = emply.Email.ToString();
            birth.Text = emply.DateOfBirth.ToString();
            posttt.Text = emply.Post.NamePost.ToString();
            innn.Text = emply.INN.ToString();

          
         
            posttt.SelectedValuePath = "ID_Post";
            posttt.DisplayMemberPath = "NamePost";
            posttt.ItemsSource = DiplomchikEntities.GetContext().Post.ToList();
           
            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (servissAdd.Id_post == 1)
            {
                
                stacButAdmin.Visibility = Visibility.Visible;
                exitt.Visibility = Visibility.Hidden;
                listview.ItemsSource = DiplomchikEntities.GetContext().Order.Where(x => x.Srok > DateTime.Now && (x.Id_orderStatus == 1 || x.Id_orderStatus == 2)).ToList();
            }
            else
            {
                countOrder.Text = emply.Order.Count().ToString();
                prib.Text = emply.Order.Sum(x => x.Service.Cost).ToString() + " руб.";
                sredf.Text = emply.Order.Average(x => x.Service.Cost).ToString() + " руб.";

                listview.ItemsSource = DiplomchikEntities.GetContext().Order.Where(x=>x.Id_employee== AccountHelpClass.Id).Where(x => x.Srok > DateTime.Now && (x.Id_orderStatus == 1 || x.Id_orderStatus == 2)).ToList();
                posttt.IsEnabled = false;
                stacButAdmin.Visibility = Visibility.Hidden;
                exitt.Visibility = Visibility.Visible;
            }

            if (listview.Items.Count>5)
            {
                listview.Height = 300;
            }

           
        }

        private void myCabinet_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PersonalAccount(null));
        }

        private void zakaz_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new Zakaz(null));
        }

        private void klient_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageKlient());
        }

        private void statist_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new statistics());
        }

        private void otchet_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new Otchet());
        }

        private void exitt_Click(object sender, RoutedEventArgs e)
        {
            var servissAddSer = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (servissAddSer.Id_post == 1)
            {
                AppFrame.framelMain.Navigate(new MenuAdmin());
            }
            else
            {
                AppFrame.framelMain.Navigate(new Menu());
            }
        }

        private void redact_Click(object sender, RoutedEventArgs e)
        {
            WindowRedact.Visibility = Visibility.Visible;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                   Employee employeeObj = new Employee()
                    {
                        FIO = fio.Text,
                        Pasport = pasport.Text,
                        Adress = adres1.Text,
                        Telephon = phone.Text,
                        Email = emal.Text,
                        DateOfBirth =Convert.ToDateTime(birth.Text),
                        Post = posttt.SelectedItem as Post,
                        INN = innn.Text 
                    };
                DiplomchikEntities.GetContext().Employee.Add(employeeObj);

                Hiistoryy historyObj = new Hiistoryy()
                {
                    Id_Employee = AccountHelpClass.Id,
                    IdStatus = 6,
                    DateEvent = DateTime.Now
                };

                DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                DiplomchikEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.framelMain.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void redactPass_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите изменить данные?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PasswordEdit a = new PasswordEdit((sender as Button).DataContext as Employee);
                    a.Show();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void redPass_Click(object sender, RoutedEventArgs e)
        {

        }

        private void uslug_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageServis());
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var emply = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id );
            if (emply.Id_post != 1)
            {
                if ((sender as RadioButton).Tag.ToString() == "1")
                {
                    DateTime date = DateTime.Now;
                    countOrder.Text = emply.Order.Where(x=>x.Date.Month==date.Month).Count().ToString();
                    prib.Text = emply.Order.Where(x => x.Date.Month == date.Month).Sum(x => x.Service.Cost).ToString() + " руб.";
                    sredf.Text = emply.Order.Where(x => x.Date.Month == date.Month).Average(x => x.Service.Cost).ToString() + " руб.";
                }
                else if ((sender as RadioButton).Tag.ToString() == "2")
                {
                    DateTime date = DateTime.Now;
                    countOrder.Text = emply.Order.Where(x => x.Date.Year == date.Year).Count().ToString();
                    prib.Text = emply.Order.Where(x => x.Date.Year == date.Year).Sum(x => x.Service.Cost).ToString() + " руб.";
                    sredf.Text = emply.Order.Where(x => x.Date.Year == date.Year).Average(x => x.Service.Cost).ToString() + " руб.";

                }
                else if ((sender as RadioButton).Tag.ToString() == "3")
                {
                    countOrder.Text = emply.Order.Count().ToString();
                    prib.Text = emply.Order.Sum(x => x.Service.Cost).ToString() + " руб.";
                    sredf.Text = emply.Order.Average(x => x.Service.Cost).ToString() + " руб.";
                }
            }
        }

        private void hisButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new HistoryUsers());
        }

        private void emplButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageEmployeen());
        }
    }
}
