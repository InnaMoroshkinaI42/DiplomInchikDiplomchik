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
    /// Логика взаимодействия для WindowInformation.xaml
    /// </summary>
    public partial class WindowInformation : Window
    {
        public WindowInformation(Order order)
        {
            InitializeComponent();

            //dgDataClient.ItemsSource = AppConnect.modelOdb.Contract.Where(x => x.Id_order == order.ID_order).ToList();
            dgDataClient1.ItemsSource = DiplomchikEntities.GetContext().StagesDevelopment.Where(x => x.Id_Order == order.ID_order).ToList();

            number.Text = "Заказ № " + Convert.ToString(order.ID_order);

            

            //квитанция
            naimenPayment.Text = order.Service.NameService;
            LC.Text = order.Client.PaymentPersonalAccountNumber;
            Fiooo.Text = order.Client.FIO;
            Adr.Text = order.Client.Address;
            costt.Text = order.Service.Cost.ToString();
            cost5.Text = order.Service.Cost.ToString();
            cost6.Text = order.Service.Cost.ToString();

            dateOtch.Text = DateTime.Now.ToString();


            //договор
            fiozak.Text = order.Client.FIO.ToString();
            pasportDan.Text = order.Client.Pasport.ToString();
            adressDoc.Text = order.Client.Address.ToString();
            dataZakaza.Text = order.Date.ToString();
            dataOkonZakaza.Text = order.Srok.ToString();

            var emply = DiplomchikEntities.GetContext().Employee.Where(x => x.ID_employee == AccountHelpClass.Id).ToList();
            managerr.Text = emply[0].FIO.ToString();

            stoimost.Text = order.Service.Cost.ToString() + " рублей";
            fioKlienta.Text = order.Client.FIO.ToString();
            adressKlienta.Text = order.Client.Address.ToString();
            INNKlienta.Text = order.Client.INN.ToString();
            FIOManager.Text = emply[0].FIO.ToString();
            pasportManager.Text = order.Employee.Pasport.ToString();
            adressManager.Text = order.Employee.Adress.ToString();
            INNManager.Text = order.Employee.INN.ToString();

            // var ser1 = AppConnect.modelOdb.Service.Where(x => x.ID_service == order.Id_service).ToString();
            //servis.Text = Convert.ToString( serviceOBJ.NameService);

           

        }

        private void clos_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void pec_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs1.SelectedItem as TabItem;
            PrintDialog printObj = new PrintDialog();
            if (ti.Name == "dogovor1")
            {
                if (printObj.ShowDialog() == true)
                {
                   
                    printObj.PrintVisual(dog, "");
                    MessageBox.Show("Сохранение документа произведено успешно!");
                }
                else
                {
                    MessageBox.Show("Пользователь прервал печать! ");
                }
            }
            if (ti.Name == "stadi")
            {
                if (printObj.ShowDialog() == true)
                {
                    printObj.PrintVisual(dgDataClient1, "");
                    MessageBox.Show("Сохранение документа произведено успешно!");
                }
                else
                {
                    MessageBox.Show("Пользователь прервал печать! ");
                }
            }
            if (ti.Name == "kw")
            {
                if (printObj.ShowDialog() == true)
                {
                    printObj.PrintVisual(Kvitanzii, "");
                    MessageBox.Show("Сохранение документа произведено успешно!");
                }
                else
                {
                    MessageBox.Show("Пользователь прервал печать! ");
                }
            }
        }
    }
}
