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
    /// Логика взаимодействия для Zakaz.xaml
    /// </summary>
    public partial class Zakaz : Page
    {
        public Zakaz(Order zakaz23)
        {
            InitializeComponent();
            //ListZakazov.ItemsSource = AppConnect.modelOdb.Order.ToList();

              FIOZakaza.SelectedValuePath = "ID_client";
              FIOZakaza.DisplayMemberPath = "FIO";
              FIOZakaza.ItemsSource = DiplomchikEntities.GetContext().Client.ToList();

            costZakaza.SelectedValuePath = "ID_payment_status";
              costZakaza.DisplayMemberPath = "NamePaymentStatus";
              costZakaza.ItemsSource = DiplomchikEntities.GetContext().PaymentStatus.ToList();

            tupeZakaza.SelectedValuePath = "ID_OrderStatus";
            tupeZakaza.DisplayMemberPath = "NameOrderStatus";
            tupeZakaza.ItemsSource = DiplomchikEntities.GetContext().orderStatus.ToList();

            listview.ItemsSource = DiplomchikEntities.GetContext().Order.Where(x=> x.Id_employee == AccountHelpClass.Id).ToList();
            tt1.Text = listview.Items.Count.ToString();

            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (servissAdd.Id_post == 1)
            {
                listview.ItemsSource = DiplomchikEntities.GetContext().Order.ToList();
                tt1.Text = listview.Items.Count.ToString();

                stacButAdmin.Visibility = Visibility.Visible;
                exitt.Visibility = Visibility.Hidden;
            }
            else
            {
                stacButAdmin.Visibility = Visibility.Hidden;
                exitt.Visibility = Visibility.Visible;
                tt1.Text = listview.Items.Count.ToString();
            }
        }
        private void edit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new Zakaz(null));
        }

        private void filtr_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new Zakaz(null));
        }

        private void addd_Click(object sender, RoutedEventArgs e)
        {
            ClassAddEdit.Id = 1;
            AppFrame.framelMain.Navigate(new AddZakaz(null));
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

        private void zakaz_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new Zakaz(null));
        }

        private void statist_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new statistics());
        }

        private void otchet_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new Otchet());
        }

        private void othet1_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printObj = new PrintDialog();
            if (printObj.ShowDialog() == true)
            {
                printObj.PrintVisual(listview, "");
            }
            else
            {
                MessageBox.Show("Пользователь прервал печать! ");
            }
            
        }

        

        private void del_Click_1(object sender, RoutedEventArgs e)
        {
            var productRemov = listview.SelectedItems.Cast<Order>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {productRemov.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DiplomchikEntities.GetContext().Order.RemoveRange(productRemov);
                    DiplomchikEntities.GetContext().SaveChanges(); 
                    
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 8,
                        DateEvent = DateTime.Now
                    };

                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    MessageBox.Show("Удаление успешно выполнено!", "Уведмление", MessageBoxButton.OK, MessageBoxImage.Information);
                    listview.ItemsSource = DiplomchikEntities.GetContext().Order.Where(x => x.Id_employee == AccountHelpClass.Id).ToList();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void redd_Click(object sender, RoutedEventArgs e)
        {
            ClassAddEdit.Id = 2;
            AppFrame.framelMain.Navigate(new AddZakaz((sender as Button).DataContext as Order));
        }

        public void Filtr()
        {
            var Serachlist = DiplomchikEntities.GetContext().Order.Where(x => x.Id_employee == AccountHelpClass.Id).ToList();
            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            var Serachlist1 = DiplomchikEntities.GetContext().Order.ToList();
            int number = Convert.ToInt32(FIOZakaza.SelectedValue);
            int numberTyp = Convert.ToInt32(tupeZakaza.SelectedValue);
            int numberCost = Convert.ToInt32(costZakaza.SelectedValue);
            if (servissAdd.Id_post != 1)
            {        
                if (DateZakaza.SelectedDate != null)
                    Serachlist = Serachlist.Where(eve => eve.Date >= DateZakaza.SelectedDate).ToList();
                if (DateZakaza1.SelectedDate != null)
                    Serachlist =  Serachlist.Where(eve => eve.Date <= DateZakaza1.SelectedDate).ToList();

                if (numberZakaza.Text != "")
                {
                    Serachlist = Serachlist.Where(x => x.ID_order.ToString().ToLower().Contains(numberZakaza.Text.ToLower())).ToList();
                }
                if (tupeZakaza.SelectedIndex>-1)
                {
                    Serachlist = Serachlist.Where(x => x.Id_orderStatus == numberTyp).ToList();
                }
                if (FIOZakaza.SelectedIndex > -1)
                {
                    Serachlist = Serachlist.Where(x => x.Id_client == number).ToList();
                }
                if (costZakaza.SelectedIndex>-1)
                {
                    Serachlist = Serachlist.Where(x => x.Id_paymentStatus == numberCost).ToList();
                }
                listview.ItemsSource = Serachlist.ToList();
            }
            else
            {

                if (DateZakaza.SelectedDate != null)
                    Serachlist1 = Serachlist1.Where(eve => eve.Date >= DateZakaza.SelectedDate).ToList();
                if (DateZakaza1.SelectedDate != null)
                    Serachlist1 = Serachlist1.Where(eve => eve.Date <= DateZakaza1.SelectedDate).ToList();

                if (numberZakaza.Text != "")
                {
                    Serachlist1 = Serachlist1.Where(x => x.ID_order.ToString().ToLower().Contains(numberZakaza.Text.ToLower())).ToList();
                }
                if (tupeZakaza.SelectedIndex > -1)
                {
                    Serachlist1 = Serachlist1.Where(x => x.Id_orderStatus == numberTyp).ToList();
                }
                if (FIOZakaza.SelectedIndex > -1)
                {
                   Serachlist1 = Serachlist1.Where(x => x.Id_client == number).ToList();
                }
                if (costZakaza.SelectedIndex > -1)
                {
                    Serachlist1 = Serachlist1.Where(x => x.Id_paymentStatus == numberCost).ToList();
                }
                listview.ItemsSource = Serachlist1.ToList();
            }

        }

        private void numberZakaza_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();           
        }
        
        private void tupeZakaza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void FIOZakaza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();

        }

        private void costZakaza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }

      

        private void klient_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageKlient());
        }

        private void stagesDevelopment_Click(object sender, RoutedEventArgs e)
        {
            WindowInformation a = new WindowInformation((sender as Button).DataContext as Order);
            a.Show();
        }

        private void myCabinet_Click(object sender, RoutedEventArgs e)
        {
            
            AppFrame.framelMain.Navigate(new PersonalAccount(null));
        }

        private void uslug_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageServis());
        }

        private void DateZakaza_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }
    }
}
