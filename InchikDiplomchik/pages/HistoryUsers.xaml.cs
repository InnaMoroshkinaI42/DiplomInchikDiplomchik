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
    /// Логика взаимодействия для HistoryUsers.xaml
    /// </summary>
    public partial class HistoryUsers : Page
    {
        public HistoryUsers()
        {
            InitializeComponent();

            listview.ItemsSource = DiplomchikEntities.GetContext().Hiistoryy.ToList();
            tt1.Text = listview.Items.Count.ToString();

            cmbNameUsers.SelectedValuePath = "ID_employee";
            cmbNameUsers.DisplayMemberPath = "FIO";
            cmbNameUsers.ItemsSource = DiplomchikEntities.GetContext().Employee.ToList();

            cmbStatus.SelectedValuePath = "ID_Status";
            cmbStatus.DisplayMemberPath = "Name";
            cmbStatus.ItemsSource = DiplomchikEntities.GetContext().StatusHistory.ToList();
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

        private void exitt_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new MenuAdmin()); 
        }

        private void filtr_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Filtr()
        {
            var Serachlist = DiplomchikEntities.GetContext().Hiistoryy.ToList();
            int number = Convert.ToInt32(cmbNameUsers.SelectedValue);
            int numberStatus = Convert.ToInt32(cmbStatus.SelectedValue);
            if (cmbNameUsers.SelectedIndex>-1)
            {
                Serachlist = Serachlist.Where(x => x.Id_Employee == number).ToList();
            }
            if (cmbStatus.SelectedIndex>-1)
            {
                Serachlist = Serachlist.Where(x => x.IdStatus == numberStatus).ToList();
            }
            if (dtpDateEvent.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.DateEvent.ToString().ToLower().Contains(dtpDateEvent.Text.ToLower())).ToList();
            }
            if (dtpDateEvent.SelectedDate != null)
                Serachlist = Serachlist.Where(eve => eve.DateEvent == dtpDateEvent.SelectedDate).ToList();
            listview.ItemsSource = Serachlist.ToList();
        }


        private void dtpDateEvent_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
            
        }

        private void cmbNameUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
            
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
           
        }

        private void filtr_Click_1(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new HistoryUsers());
        }

        private void hisButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new HistoryUsers());
        }

        private void emplButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageEmployeen());
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Serachlist = DiplomchikEntities.GetContext().Hiistoryy.ToList();
            if (dtpDateEvent.SelectedDate != null)
                Serachlist = Serachlist.Where(eve => eve.DateEvent == dtpDateEvent.SelectedDate).ToList();
            listview.ItemsSource = Serachlist.ToList();
            tt1.Text = listview.Items.Count.ToString();
        }
    }
}
