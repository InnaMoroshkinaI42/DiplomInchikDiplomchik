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
    /// Логика взаимодействия для AddService.xaml
    /// </summary>
    public partial class AddService : Page
    {
        int IdPic = 0;
        private Service product89 = new Service();
        private Service _servicHotel = new Service();
        BitmapImage _image;
        public AddService(Service service)
        {
            InitializeComponent();

            dis.ItemsSource = DiplomchikEntities.GetContext().DISCOUN.ToList();

            if (service != null)
                _servicHotel = service;

            DataContext = _servicHotel;

            if (_servicHotel.Photo34 != null)
            {//конвертация из базы
                MemoryStream ms = new MemoryStream();
                ms.Write(_servicHotel.Photo34, 0, 150);
                _image = ConvertToBitmap(_servicHotel.Photo34);
                ImgDoc.Source = _image;
            }


        }

        public BitmapImage ConvertToBitmap(byte[] value)
        {
            BitmapImage bitmap = new BitmapImage();
            try
            {
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(value);
                bitmap.EndInit();
                return bitmap;
            }
            catch
            {
                System.Windows.MessageBox.Show("Ошибка изображения в базе данных");
            }
            return null;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framelMain.GoBack();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            try
            {
                if (ClassAddEdit.Id == 1)
                {
                    _servicHotel.NameService = nameService.Text;
                    _servicHotel.Description = desctSer.Text;
                    _servicHotel.Cost = Convert.ToInt32(costSer.Text);
                    _servicHotel.Id_discount = ((DISCOUN)dis.SelectedItem).ID_DISCOUNTT;
                    _servicHotel.Photo34 = product89.Photo34;

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
                else if (ClassAddEdit.Id==2)
                {
                    Hiistoryy historyObj = new Hiistoryy()
                    {
                        Id_Employee = AccountHelpClass.Id,
                        IdStatus = 11,
                        DateEvent = DateTime.Now
                    };

                    if (IdPic > 0)
                    {
                        _servicHotel.Photo34 = product89.Photo34;
                    }
                    DiplomchikEntities.GetContext().Hiistoryy.Add(historyObj);  
                    DiplomchikEntities.GetContext().SaveChanges();
                    System.Windows.MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);                
                }
               AppFrame.framelMain.GoBack();
            }
            catch (Exception Ex)
            {
                podpos1.Visibility = Visibility.Visible;
                nameService.BorderBrush = Brushes.Red;
                
                podpos0.Visibility = Visibility.Visible;
                costSer.BorderBrush = Brushes.Red;

                podposDis.Visibility = Visibility.Visible;
                System.Windows.MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void costSer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _image = null;
            ImgDoc.Source = null;
        }
    }
}
