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
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        private Employee _empl = new Employee();
        public AddEmployee(Employee employee)
        {
            InitializeComponent();    
            
            if (employee != null)
                _empl = employee;

            DataContext = _empl; 
            
            postCMB.ItemsSource = DiplomchikEntities.GetContext().Post.ToList();
            stEmpl.ItemsSource = DiplomchikEntities.GetContext().ArhivStatusEmp.ToList();

            if (ClassAddEdit.Id == 1)
            {
                dateDateOfBirth.SelectedDate = DateTime.Now;              
            }
            else
            {
                dateDateOfBirth.IsEnabled = false;
                dateDateOfBirth.SelectedDate = _empl.DateOfBirth;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClassAddEdit.Id==1)
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
                    MessageBox.Show("Информация добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.framelMain.GoBack();
                }
                else if (ClassAddEdit.Id==2)
                {
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 18,
                        DateEvent = DateTime.Now
                    };
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);
                    DiplomchikEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.framelMain.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.GoBack();
        }

        private void INN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
