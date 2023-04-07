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
    /// Логика взаимодействия для AddZakaz.xaml
    /// </summary>
    public partial class AddZakaz : Page
    {    
        private Order _order = new Order();
        
        public AddZakaz(Order orderSelect)
        {
            InitializeComponent();

           
            klientFIO.ItemsSource = DiplomchikEntities.GetContext().Client.ToList();

            uskega.SelectedValuePath = "ID_service";
            uskega.DisplayMemberPath = "NameService";
            uskega.ItemsSource = DiplomchikEntities.GetContext().Service.ToList();
            skiddka.ItemsSource = DiplomchikEntities.GetContext().DISCOUN.ToList();
            statusOpl.ItemsSource = DiplomchikEntities.GetContext().PaymentStatus.ToList();
            statusZakaza.ItemsSource = DiplomchikEntities.GetContext().orderStatus.ToList();
            manager.ItemsSource = DiplomchikEntities.GetContext().Employee.Where(x => x.ID_employee == AccountHelpClass.Id).ToList();


            if (orderSelect != null)
            {
                this._order = orderSelect;
            }
            DataContext = _order;

            if (ClassAddEdit.Id == 1)
            {
                dateOrder.SelectedDate = DateTime.Now;
                srokOrder.SelectedDate = DateTime.Today;
                statusZakaza.IsEnabled = false;
            }
            else
            {
                dateOrder.SelectedDate = _order.Date;
                srokOrder.SelectedDate = _order.Srok;
            }


            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);

            if (servissAdd.Id_post == 1)
            {
                manager.ItemsSource = DiplomchikEntities.GetContext().Employee.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           try
            {
                if (ClassAddEdit.Id == 1)
                {
                    Order orderObj = new Order()
                    {
                        Date = Convert.ToDateTime(dateOrder.Text),
                        Srok = Convert.ToDateTime(srokOrder.Text),
                        Client = klientFIO.SelectedItem as Client,
                        Service = uskega.SelectedItem as Service,
                        PaymentStatus = statusOpl.SelectedItem as PaymentStatus,
                        Employee = manager.SelectedItem as Employee,
                        Id_orderStatus = 1
                    };
                    DiplomchikEntities.GetContext().Order.Add(orderObj);
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 2,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    StagesDevelopment development = new StagesDevelopment()
                    {
                        Id_Order = _order.ID_order,
                        Id_stages = 1,
                        Date = Convert.ToDateTime(dateOrder.Text),
                        Id_status = 2,
                        
                    };
                    StagesDevelopment development1 = new StagesDevelopment()
                    {
                        Id_Order = _order.ID_order,
                        Id_stages = 2,
                        Date = Convert.ToDateTime(dateOrder.Text),
                        Id_status = 2,
                        
                    }; 
                    StagesDevelopment development2 = new StagesDevelopment()
                    {
                        Id_Order = _order.ID_order,
                        Id_stages = 3,
                        Date = Convert.ToDateTime(dateOrder.Text),
                        Id_status = 2,
                        
                    }; 
                    StagesDevelopment development3 = new StagesDevelopment()
                    {
                        Id_Order = _order.ID_order,
                        Id_stages = 4,
                        Date = Convert.ToDateTime(srokOrder.Text),
                        Id_status = 2,
                        
                    };
                    DiplomchikEntities.GetContext().StagesDevelopment.Add(development);
                    DiplomchikEntities.GetContext().StagesDevelopment.Add(development1);
                    DiplomchikEntities.GetContext().StagesDevelopment.Add(development2);
                    DiplomchikEntities.GetContext().StagesDevelopment.Add(development3);
                    DiplomchikEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (ClassAddEdit.Id==2)
                {
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 4,
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClassAddEdit.Id = 1;
            AppFrame.framelMain.Navigate(new AddKlient(null));
        }

        private void klientFIO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //PersonalAccountNumber.Text = AppConnect.modelOdb.Client.Where(x => x.ID_client.ToString().Contains(PersonalAccountNumber.ToString())).ToString();
            
        }

        private void uskega_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            skiddka.ItemsSource = DiplomchikEntities.GetContext().DISCOUN.Where(x => x.ID_DISCOUNTT == ((Service)uskega.SelectedItem).Id_discount).ToList();
            int st = Convert.ToInt32(uskega.SelectedValue);
            var fdl = DiplomchikEntities.GetContext().Service.FirstOrDefault(x => x.ID_service == st);

            stoimosto.Text = fdl.Stom.ToString();
        }

        private void skiddka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sk = Convert.ToInt32(skiddka.SelectedValue);
            int st = Convert.ToInt32(uskega.SelectedValue);
            var tdt = DiplomchikEntities.GetContext().Service.FirstOrDefault(x => x.Id_discount == sk);
            var fdl = DiplomchikEntities.GetContext().Service.FirstOrDefault(x => x.ID_service == st);

            
            if (tdt != null)
            {
                stoimosto.Text = tdt.Stom.ToString();
            }
            else 
            {
                skiddka.IsEnabled = false;
                stoimosto.Text = fdl.Stom.ToString();
            }
                      
        }
    }
}
