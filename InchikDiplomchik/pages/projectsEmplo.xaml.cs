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
    /// Логика взаимодействия для projectsEmplo.xaml
    /// </summary>
    public partial class projectsEmplo : Window
    {
        public projectsEmplo(Employee employee)
        {
            InitializeComponent();

            dgPro.ItemsSource = DiplomchikEntities.GetContext().Order.Where(x=>x.Id_employee==employee.ID_employee).ToList();
            fioemp.Text = Convert.ToString(employee.Post.NamePost +" " + employee.FIO);
            var server= new Service();
            itog.Text = "ИТОГО: " + DiplomchikEntities.GetContext().Order.Where(x => x.Id_employee == employee.ID_employee)
                .Sum(x => x.Service.Cost).ToString() + " ₽";
            
            TxtVsego.Text = DiplomchikEntities.GetContext().Order.Where(x => x.Id_employee == employee.ID_employee).Count().ToString();
            TxtNew.Text = DiplomchikEntities.GetContext().Order.Where(x => x.Id_employee == employee.ID_employee).Where(x=>x.Id_orderStatus==1).Count().ToString();
            TxtVRaz.Text = DiplomchikEntities.GetContext().Order.Where(x => x.Id_employee == employee.ID_employee).Where(x=>x.Id_orderStatus==2).Count().ToString();
            TxtGotov.Text = DiplomchikEntities.GetContext().Order.Where(x => x.Id_employee == employee.ID_employee).Where(x=>x.Id_orderStatus==3).Count().ToString();
            TxtNet.Text = DiplomchikEntities.GetContext().Order.Where(x => x.Id_employee == employee.ID_employee).Where(x=>x.Srok < DateTime.Now && (x.Id_orderStatus==1 || x.Id_orderStatus==2)).Count().ToString();
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printObj = new PrintDialog();
            if (printObj.ShowDialog() == true)
            {
                btnPec.Visibility = Visibility.Hidden;
                printObj.PrintVisual(stPanel, "");
            }
            else
            {
                MessageBox.Show("Пользователь прервал печать! ");
            }
        }

    }
}
