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
    /// Логика взаимодействия для PageServis.xaml
    /// </summary>
    public partial class PageServis : Page
    {
        public PageServis()
        {
            InitializeComponent();
            listview.ItemsSource = DiplomchikEntities.GetContext().Service.ToList();

            var count_col = listview.Items.Count;
            tt1.Text = count_col.ToString();

            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);

            if (servissAdd.Id_post == 1)
            {
                addd.Visibility = Visibility.Visible; 
                stacButAdmin.Visibility = Visibility.Visible;
                exitt.Visibility = Visibility.Hidden;
            }
            else
            {
                stacButAdmin.Visibility = Visibility.Hidden;
                exitt.Visibility = Visibility.Visible;
            }             
        }

        private void addd_Click(object sender, RoutedEventArgs e)
        {
            ClassAddEdit.Id = 1;
            AppFrame.framelMain.Navigate(new AddService(null));
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

        public void Filtr()
        {
            var Serachlist = DiplomchikEntities.GetContext().Service.ToList();
            if (nameSer.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.NameService.ToString().ToLower().Contains(nameSer.Text.ToLower())).ToList();
            }
            if (costSer.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.Cost.ToString().ToLower().Contains(costSer.Text.ToLower())).ToList();
            }
            if (desSer.Text != "")
            {
                Serachlist = Serachlist.Where(x => x.Description.ToString().ToLower().Contains(desSer.Text.ToLower())).ToList();
            }
            listview.ItemsSource = Serachlist.ToList();
        }

        private void nameSer_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void costSer_TextChanged(object sender, TextChangedEventArgs e)
        {
           Filtr();
           tt1.Text = listview.Items.Count.ToString();
        }

        private void desSer_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void filtr_Click(object sender, RoutedEventArgs e)
        {
            listview.ItemsSource = DiplomchikEntities.GetContext().Service.ToList();
            tt1.Text = listview.Items.Count.ToString();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageServis());
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

        private void otchet_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new Otchet());
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (servissAdd.Id_post != 1)
            {
                MessageBox.Show("Ошибка "+ "Данное действие для Вас недоступно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                var productRemov = listview.SelectedItems.Cast<Service>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить следующие {productRemov.Count()} элементов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        DiplomchikEntities.GetContext().Service.RemoveRange(productRemov);
                        DiplomchikEntities.GetContext().SaveChanges();

                        Hiistoryy historyObj = new Hiistoryy()
                        {
                            Id_Employee = AccountHelpClass.Id,
                            IdStatus = 20,
                            DateEvent = DateTime.Now
                        };

                        DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                        MessageBox.Show("Удаление успешно выполнено!", "Уведмление", MessageBoxButton.OK, MessageBoxImage.Information);
                        listview.ItemsSource = DiplomchikEntities.GetContext().Service.ToList();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void redd_Click(object sender, RoutedEventArgs e)
        {
            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (servissAdd.Id_post != 1)
            {
                MessageBox.Show("Ошибка " + "Данное действие для Вас недоступно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                ClassAddEdit.Id = 2;
                AppFrame.framelMain.Navigate(new AddService((sender as Button).DataContext as Service));
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

        private void stag_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
