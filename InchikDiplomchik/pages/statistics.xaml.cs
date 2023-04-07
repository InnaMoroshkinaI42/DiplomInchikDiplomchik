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
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InchikDiplomchik.pages
{
    /// <summary>
    /// Логика взаимодействия для statistics.xaml
    /// </summary>
    public partial class statistics : Page
    {

        private DiplomchikEntities _condext = new DiplomchikEntities();
        public statistics()
        {
            InitializeComponent();
            ChartStatistic.ChartAreas.Add(new ChartArea("Количество заказов менеджера"));
            Charttistic.ChartAreas.Add(new ChartArea("Общая стоимость заказов"));
            
            ChartAdmin.ChartAreas.Add(new ChartArea("Количество заказов менеджера"));
            ChartAdmin2.ChartAreas.Add(new ChartArea("Количество заказов менеджера"));

            var currentSeries = new Series("Количество заказов")
            {
                IsValueShownAsLabel = true
            }; 
            
            var currentSeries1 = new Series("Общая стоимость заказов")
            {
                IsValueShownAsLabel = true
            };
            ChartStatistic.Series.Add(currentSeries);
            Charttistic.Series.Add(currentSeries1);
            var currentSeriesAdmin = new Series("Количество заказов менеджера")
            {
                IsValueShownAsLabel = true
            };
            var currentSeriesAdmin1 = new Series("Количество заказов")
            {
                IsValueShownAsLabel = true
            }; 
            ChartAdmin.Series.Add(currentSeriesAdmin); 
            ChartAdmin2.Series.Add(currentSeriesAdmin1);
           
            rs.SelectedValuePath = "ID_service";
            rs.DisplayMemberPath = "NameService";
            rs.ItemsSource = _condext.Service.ToList();

            EmplCMB.SelectedValuePath = "ID_employee";
            EmplCMB.DisplayMemberPath = "FIO";
            EmplCMB.ItemsSource = _condext.Employee.ToList();

            diagr.ItemsSource = Enum.GetValues(typeof(SeriesChartType));

            var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (servissAdd.Id_post == 1)
            {
                stacButAdmin.Visibility = Visibility.Visible;
                exitt.Visibility = Visibility.Hidden;
               
                txtEmp.Visibility = Visibility.Visible;
                EmplCMB.Visibility = Visibility.Visible;
                stackPanels.Visibility = Visibility.Collapsed;
                
                stacsAdmin.Visibility = Visibility.Visible;
                stRadio.Visibility = Visibility.Collapsed;
            }
            else
            {
                stacButAdmin.Visibility = Visibility.Hidden;
                exitt.Visibility = Visibility.Visible;
                statCount.Text ="Статистика всех заказов (" + servissAdd.FIO.ToString() + ") в виде полной стоимости";
                stKol.Text ="Статистика всех заказов (" + servissAdd.FIO.ToString() + ") в виде количества";
                stackPanels.Visibility = Visibility.Visible;
                txtEmp.Visibility = Visibility.Collapsed;
                EmplCMB.Visibility = Visibility.Collapsed;

                stacsAdmin.Visibility = Visibility.Collapsed;
                stRadio.Visibility = Visibility.Visible;
            }
         
        }

        private void Updatechart(object sender, SelectionChangedEventArgs e)
        {
            var emp = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
            if (emp.Id_post != 1)
            {
                if (rs.SelectedItem is Service currentEx  &&
                    diagr.SelectedItem is SeriesChartType currentType)
                {
                    Series currentSeries = ChartStatistic.Series.FirstOrDefault();
                    currentSeries.ChartType = currentType;
                    currentSeries.Points.Clear();

                    Series currentSeries1 = Charttistic.Series.FirstOrDefault();
                    currentSeries1.ChartType = currentType;
                    currentSeries1.Points.Clear();



                    var caregoriesList = _condext.orderStatus.ToList();
                    foreach (var category in caregoriesList)
                    {
                        currentSeries.Points.AddXY(category.NameOrderStatus,
                            _condext.Order.ToList().Where(p => p.Service == currentEx
                            && p.orderStatus == category).Where(x => x.Id_employee == AccountHelpClass.Id).Count());

                       
                    }

                    var caregoriesList1 = _condext.orderStatus.ToList();
                    foreach (var category in caregoriesList)
                    {
                        currentSeries1.Points.AddXY(category.NameOrderStatus,
                            _condext.Order.ToList().Where(p => p.Service == currentEx
                            && p.orderStatus == category).Where(x => x.Id_employee == AccountHelpClass.Id).Sum(x => x.Service.Cost));
                    }
                }
            }
            else
            {
                if (EmplCMB.SelectedItem is Employee currentEmp && rs.SelectedItem is Service currentSer &&
                    diagr.SelectedItem is SeriesChartType currentType)
                {
                    Series currentSeriesAdmin = ChartAdmin.Series.FirstOrDefault();
                currentSeriesAdmin.ChartType = currentType;
                currentSeriesAdmin.Points.Clear();
                    
                    Series currentSeriesAdmin2 = ChartAdmin2.Series.FirstOrDefault();
                currentSeriesAdmin2.ChartType = currentType;
                currentSeriesAdmin2.Points.Clear();


                var caregoriesListAdm = _condext.Service.ToList();
                foreach (var category in caregoriesListAdm)
                {
                    currentSeriesAdmin.Points.AddXY(category.NameService,
                        _condext.Order.ToList().Where(p => p.Employee == currentEmp
                        && p.Service == category).Count());
                       
                }

                var caregoriesListAdm2 = _condext.Employee.ToList();
                 foreach (var category in caregoriesListAdm2)
                 {
                        currentSeriesAdmin2.Points.AddXY(category.FIO,
                            _condext.Order.ToList().Where(p => p.Service == currentSer
                            && p.Employee == category).Count());
                 }
                }
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

        private void pechat_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printObj = new PrintDialog();
            if (printObj.ShowDialog() == true)
            {
                var servissAdd = DiplomchikEntities.GetContext().Employee.FirstOrDefault(x => x.ID_employee == AccountHelpClass.Id);
                if (servissAdd.Id_post == 1)
                {
                    printObj.PrintVisual(ннн, "");
                }
                else
                {
                    printObj.PrintVisual(ннн, "");
                }
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

       

        private void klient_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageKlient());
        }

        private void uslug_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageServis());
        }

        private void exitt_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void hisButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new HistoryUsers());
        }

        private void emplButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.Navigate(new PageEmployeen());
        }

        private void vKol_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton).Tag.ToString() == "1")
            {
                chart1.Visibility = Visibility.Visible;
                chart2.Visibility = Visibility.Hidden;

            }
            else if ((sender as RadioButton).Tag.ToString() == "2")
            {
                chart1.Visibility = Visibility.Hidden;
                chart2.Visibility = Visibility.Visible;
            }
        }
    }
}
