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
    /// Логика взаимодействия для PageKlient.xaml
    /// </summary>
    public partial class PageKlient : Page
    {
        public PageKlient()
        {
            InitializeComponent();
            listview.ItemsSource = DiplomchikEntities.GetContext().Client.ToList();

            var count_col = listview.Items.Count;
            tt1.Text = count_col.ToString();

            tipklienta.SelectedValuePath = "ID_ClientType";
            tipklienta.DisplayMemberPath = "Type";
            tipklienta.ItemsSource = DiplomchikEntities.GetContext().ClientType.ToList();

            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (servissAdd.Id_post == 1)
            {
                stacButAdmin.Visibility = Visibility.Visible;
                exitt.Visibility = Visibility.Hidden;
            }
            else
            {
                stacButAdmin.Visibility = Visibility.Hidden;
                exitt.Visibility = Visibility.Visible;
            }
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

        private void uslug_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageServis());
        }

        private void statist_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new statistics());
        }

        private void otchet_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new Otchet());
        }

        private void addd_Click(object sender, RoutedEventArgs e)
        {
            ClassAddEdit.Id = 1;
            AppFrame.framelMain.Navigate(new AddKlient(null));
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            listview.ItemsSource = DiplomchikEntities.GetContext().Client.ToList();
        }

        private void filtr_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageKlient());
        }

        public void Filtr()
        {
            var Serachlist = DiplomchikEntities.GetContext().Client.ToList();
            int number = Convert.ToInt32(tipklienta.SelectedValue);
            if (nameKompapy.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.Name_company.ToString().ToLower().Contains(nameKompapy.Text.ToLower())).ToList();
            }
            if (numberINN.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.INN.ToString().ToLower().Contains(numberINN.Text.ToLower())).ToList();
            }
            if (tipklienta.SelectedIndex>-1)
            {
                Serachlist = Serachlist.Where(x => x.Id_clientType == number).ToList();
            }
            if (numberPasport.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.Pasport.ToString().ToLower().Contains(numberPasport.Text.ToLower())).ToList();
            }
            if (nameFIO.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.FIO.ToString().ToLower().Contains(nameFIO.Text.ToLower())).ToList();
            }
            if (numberTel.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.Telephone.ToString().ToLower().Contains(numberTel.Text.ToLower())).ToList();
            }
            listview.ItemsSource = Serachlist.ToList();

        }

        private void numberZakaza_TextChanged(object sender, TextChangedEventArgs e)
        {
           Filtr();
           tt1.Text = listview.Items.Count.ToString();
        }

        private void numberINN_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void tipklienta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void numberPasport_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void nameFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void numberTel_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
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

        private void redd_Click(object sender, RoutedEventArgs e)
        {
            ClassAddEdit.Id = 2;
            AppFrame.framelMain.Navigate(new AddKlient((sender as Button).DataContext as Client));
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
