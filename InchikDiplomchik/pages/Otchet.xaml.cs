using InchikDiplomchik.ApplicatModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InchikDiplomchik.pages
{
    /// <summary>
    /// Логика взаимодействия для Otchet.xaml
    /// </summary>
    public partial class Otchet : Page
    {
        int IdPic = 0;
        private Service product89 = new Service();
        private Service _servicHotel = new Service();
        private Client _client = new Client();
        private Order _order = new Order(); 
        private Post _post = new Post();
        private ClientType _ocli = new ClientType();
        private DISCOUN _dISCOUN = new DISCOUN();
        private Stages _stages = new Stages();
        private Employee _empl = new Employee();
        private StatusHistory _statusHis = new StatusHistory();
        public Otchet()
        {
            InitializeComponent();
            //добавление клиента

            tipZakaza.SelectedValuePath = "ID_ClientType";
            tipZakaza.DisplayMemberPath = "Type";
            tipZakaza.ItemsSource = DiplomchikEntities.GetContext().ClientType.ToList();

            dateDateOfBirth.Text = DateTime.Today.ToString();
            //услуга
            dis.SelectedValuePath = "ID_DISCOUNTT";
            dis.DisplayMemberPath = "NAME_DISC";
            dis.ItemsSource = DiplomchikEntities.GetContext().DISCOUN.ToList();
            //заказ
            klientFIO.SelectedValuePath = "ID_client";
            klientFIO.DisplayMemberPath = "FIO";
            klientFIO.ItemsSource = DiplomchikEntities.GetContext().Client.ToList();

            uskega.SelectedValuePath = "ID_service";
            uskega.DisplayMemberPath = "NameService";
            uskega.ItemsSource = DiplomchikEntities.GetContext().Service.ToList();

            skiddka.SelectedValuePath = "ID_DISCOUNTT";
            skiddka.DisplayMemberPath = "NAME_DISC";
            skiddka.ItemsSource = DiplomchikEntities.GetContext().DISCOUN.ToList();

            statusOpl.SelectedValuePath = "ID_payment_status";
            statusOpl.DisplayMemberPath = "NamePaymentStatus";
            statusOpl.ItemsSource = DiplomchikEntities.GetContext().PaymentStatus.ToList();

            statusZakaza.SelectedValuePath = "ID_payment_status";
            statusZakaza.DisplayMemberPath = "NamePaymentStatus";
            statusZakaza.ItemsSource = DiplomchikEntities.GetContext().orderStatus.ToList();

            manager.SelectedValuePath = "ID_employee";
            manager.DisplayMemberPath = "FIO";
            manager.ItemsSource = DiplomchikEntities.GetContext().Employee.Where(x => x.ID_employee == AccountHelpClass.Id).ToList();
            dateOrder.Text = DateTime.Today.ToString();
            srokOrder.Text = DateTime.Today.ToString();
            statusZakaza.IsEnabled = false;

            postCMB.SelectedValuePath = "ID_Post";
            postCMB.DisplayMemberPath = "NamePost";
            postCMB.ItemsSource = DiplomchikEntities.GetContext().Post.ToList();

            stEmpl.SelectedValuePath = "ID_ArhivStatus";
            stEmpl.DisplayMemberPath = "NameStatus";
            stEmpl.ItemsSource = DiplomchikEntities.GetContext().ArhivStatusEmp.ToList();

            var servissAddSer = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (servissAddSer.Id_post == 1)
            {
                manager.ItemsSource = DiplomchikEntities.GetContext().Employee.ToList();
            }

            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (servissAdd.Id_post == 1)
            {
                stacButAdmin.Visibility = Visibility.Visible;
                exitt.Visibility = Visibility.Hidden;
            }
            else
            {
                DobPost.Visibility = Visibility.Hidden;
                DobTipKlient.Visibility = Visibility.Hidden;
                DobServ.Visibility = Visibility.Hidden;
                DobSkid.Visibility = Visibility.Hidden;
                DobStages.Visibility = Visibility.Hidden;
                DobStatusHistory.Visibility = Visibility.Hidden;
                DobEmployee.Visibility = Visibility.Hidden;
                stacButAdmin.Visibility = Visibility.Hidden;
                exitt.Visibility = Visibility.Visible;
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

        private void myCabinet_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PersonalAccount( null));
        }

        private void emplButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageEmployeen());
        }

        private void hisButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new HistoryUsers());
        }

        private void klient_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageKlient());
        }

        private void uslug_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageServis());
        }

        private void dob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openDialog = new OpenFileDialog();
                openDialog.Filter = "Image files (*.BMP, *.JPG, *.GIP, *.TIP, *.PNG, *.ICO, *.EMF, *.WMF) | *.bmp; *.jpg, *.gif; *.tip; *.png; *.ico; *.emf; *.wwp";
                openDialog.ShowDialog();
                if (openDialog.CheckFileExists && openDialog.FileName.Length > 0)
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openDialog.FileName);
                    bitmap.DecodePixelWidth = 250;
                    bitmap.EndInit();
                    ImgDoc.Source = bitmap;
                    product89.Photo34 = File.ReadAllBytes(openDialog.FileName);
                    IdPic = 1;
                    borderIm.Background = Brushes.Transparent;
                    borderIm.BorderBrush = Brushes.Transparent;
                    dob.Visibility = Visibility.Hidden;
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Ошибка загрузки изображения", "Ошибка");
            }
        }

        //ДОБАВЛЕНИЕ КЛИЕНТА
        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
                    System.Windows.MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (ClassAddEdit.Id == 2)
                {
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 5,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    DiplomchikEntities.GetContext().SaveChanges();
                    System.Windows.MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                AppFrame.framelMain.GoBack();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }
        //Добавиление услуги
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClassAddEdit.Id == 1)
                {
                    if (_servicHotel.ID_service == 0)
                        DiplomchikEntities.GetContext().Service.Add(_servicHotel);
                    System.Windows.MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 10,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    DiplomchikEntities.GetContext().SaveChanges();
                }
                else if (ClassAddEdit.Id == 2)
                {
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 11,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    DiplomchikEntities.GetContext().SaveChanges();
                    System.Windows.MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                AppFrame.framelMain.GoBack();
            }
            catch (Exception Ex)
            {
                podpos1456.Visibility = Visibility.Visible;
                nameService.BorderBrush = Brushes.Red;

                podpos9092.Visibility = Visibility.Visible;
                costSer.BorderBrush = Brushes.Red;

                podposDis839.Visibility = Visibility.Visible;
                System.Windows.MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //заказ
        private void Button_Click_2(object sender, RoutedEventArgs e)
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
                    DiplomchikEntities.GetContext().SaveChanges();
                    System.Windows.MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (ClassAddEdit.Id == 2)
                {
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 4,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    DiplomchikEntities.GetContext().SaveChanges();
                    System.Windows.MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                AppFrame.framelMain.GoBack();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }

        private void uskega_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Usl = Convert.ToInt32(uskega.SelectedValue);
            skiddka.ItemsSource = DiplomchikEntities.GetContext().DISCOUN.Where(x => x.ID_DISCOUNTT == Usl).ToList();
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
        //должность
        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (tbDol.Text == servissAdd.Post.NamePost)
            {
                System.Windows.MessageBox.Show("Поле с таким наименованием уже есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (_post.ID_Post == 0)
                    DiplomchikEntities.GetContext().Post.Add(_post);
                try
                {
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 12,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    DiplomchikEntities.GetContext().SaveChanges();
                    System.Windows.MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnsave1_Click(object sender, RoutedEventArgs e)
        {
            var servissAdd = DiplomchikEntities.GetContext().Client.FirstOrDefault(x => x.Id_clientType == _ocli.ID_ClientType);
            if (tbNameType.Text == servissAdd.ClientType.Type)
            {
                System.Windows.MessageBox.Show("Поле с таким наименованием уже есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (_ocli.ID_ClientType == 0)
                    DiplomchikEntities.GetContext().ClientType.Add(_ocli);
                try
                {
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 13,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    DiplomchikEntities.GetContext().SaveChanges();
                    System.Windows.MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnsavedis_Click(object sender, RoutedEventArgs e)
        {
            if (_dISCOUN.ID_DISCOUNTT == 0)
                DiplomchikEntities.GetContext().DISCOUN.Add(_dISCOUN);
            try
            {
                Hiistoryy historyObj = new Hiistoryy()
                {
                    Id_Employee = AccountHelpClass.Id,
                    IdStatus = 14,
                    DateEvent = DateTime.Now
                };
                DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                DiplomchikEntities.GetContext().SaveChanges();
                System.Windows.MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnsavestages_Click(object sender, RoutedEventArgs e)
        {

            if (_stages.ID_stages == 0)
                DiplomchikEntities.GetContext().Stages.Add(_stages);
            try
            {
                Hiistoryy historyObj = new Hiistoryy()
                {
                    Id_Employee = AccountHelpClass.Id,
                    IdStatus = 15,
                    DateEvent = DateTime.Now
                };
                DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                DiplomchikEntities.GetContext().SaveChanges();
                System.Windows.MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }

        }

        private void btnsaveHis_Click(object sender, RoutedEventArgs e)
        {
            if (_statusHis.ID_Status == 0)
                DiplomchikEntities.GetContext().StatusHistory.Add(_statusHis);
            try
            {
                Hiistoryy historyObj = new Hiistoryy()
                {
                    Id_Employee = AccountHelpClass.Id,
                    IdStatus = 16,
                    DateEvent = DateTime.Now
                };
                DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                DiplomchikEntities.GetContext().SaveChanges();
                System.Windows.MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClassAddEdit.Id == 1)
                {
                    if (_empl.ID_employee == 0)
                        DiplomchikEntities.GetContext().Employee.Add(_empl);
                    DiplomchikEntities.GetContext().SaveChanges();
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 17,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    DiplomchikEntities.GetContext().SaveChanges();
                    System.Windows.MessageBox.Show("Информация добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.framelMain.GoBack();
                }
                else if (ClassAddEdit.Id == 2)
                {
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 18,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    DiplomchikEntities.GetContext().SaveChanges();
                    System.Windows.MessageBox.Show("Информация сохранена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.framelMain.GoBack();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
