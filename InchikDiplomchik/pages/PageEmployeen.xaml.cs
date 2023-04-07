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
    /// Логика взаимодействия для PageEmployeen.xaml
    /// </summary>
    public partial class PageEmployeen : Page
    {
        public PageEmployeen()
        {
            InitializeComponent();
            listview.ItemsSource = DiplomchikEntities.GetContext().Employee.ToList();
            tt1.Text = listview.Items.Count.ToString();

            postEmp.SelectedValuePath = "ID_Post";
            postEmp.DisplayMemberPath = "NamePost";
            postEmp.ItemsSource = DiplomchikEntities.GetContext().Post.ToList();

            var dpsh = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.DateOfBirth == DateTime.Today);
            var dpsh1 = DiplomchikEntities.GetContext().Employee.Where(x => x.DateOfBirth == DateTime.Today).Count();

            if (dpsh1 > 0)
            {
                borDatBth.Visibility = Visibility.Visible;
                TxhappyEmp.Text = "Сегодня день рождения у " + dpsh.Post.NamePost + " " + dpsh.FIO;
            }
        }

        private void filtr_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageEmployeen());
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            listview.ItemsSource = DiplomchikEntities.GetContext().Employee.ToList();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void addd_Click(object sender, RoutedEventArgs e)
        {
            ClassAddEdit.Id = 1;
            AppFrame.framelMain.Navigate(new AddEmployee(null));
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

        private void exitt_Click(object sender, RoutedEventArgs e)
        {
                AppFrame.framelMain.Navigate(new MenuAdmin());
            
        }

        public void Filtr()
        {
            var Serachlist = DiplomchikEntities.GetContext().Employee.ToList();
            int number = Convert.ToInt32(postEmp.SelectedValue);
            if (nameFIO.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.FIO.ToString().ToLower().Contains(nameFIO.Text.ToLower())).ToList();
            }
            if (numberTel.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.Telephon.ToString().ToLower().Contains(numberTel.Text.ToLower())).ToList();
            }
            if (postEmp.SelectedIndex > -1)
            {
                Serachlist = Serachlist.Where(x => x.Id_post == number).ToList();
            }
            if (numberPasport.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.Pasport.ToString().ToLower().Contains(numberPasport.Text.ToLower())).ToList();
            }
            listview.ItemsSource = Serachlist.ToList();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton).Tag.ToString() == "1")
            {
                listview.ItemsSource = DiplomchikEntities.GetContext().Employee.Where(x => x.Id_statusJob == 1).ToList();
                tt1.Text = listview.Items.Count.ToString();
            }
            else if ((sender as RadioButton).Tag.ToString() == "2")
            {
                listview.ItemsSource = DiplomchikEntities.GetContext().Employee.Where(x => x.Id_statusJob == 2).ToList();
                tt1.Text = listview.Items.Count.ToString();
            }
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

        private void postEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void emplButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageEmployeen());
        }

        private void hisButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new HistoryUsers());
        }

        private void numberPasport_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void stagesDevelopment_Click(object sender, RoutedEventArgs e)
        {
            projectsEmplo a = new projectsEmplo((sender as Button).DataContext as Employee);
            a.Show();
        }

        private void redd_Click(object sender, RoutedEventArgs e)
        {
            ClassAddEdit.Id = 2;
            AppFrame.framelMain.Navigate(new AddEmployee((sender as Button).DataContext as Employee));
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            var productRemov = listview.SelectedItems.Cast<Employee>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {productRemov.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 19,
                        DateEvent = DateTime.Now
                    };

                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);

                    /* var dhh = AppConnect.modelOdb.orderStatus.ToArray()[0];
                     var gjkf = AppConnect.modelOdb.Order.Where(x => x.Id_orderStatus == dhh.ID_OrderStatus).ToList();
                     if (gjkf.Count > 0)
                     {
                         gjkf = gjkf.ToArray()[0].Id_orderStatus == 5;
                     }*/

                    DiplomchikEntities.GetContext().Employee.RemoveRange(productRemov);
                    DiplomchikEntities.GetContext().SaveChanges();
                    MessageBox.Show("Удаление успешно выполнено!", "Уведмление", MessageBoxButton.OK, MessageBoxImage.Information);
                    listview.ItemsSource = DiplomchikEntities.GetContext().Employee.ToList();
                    tt1.Text = listview.Items.Count.ToString();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
