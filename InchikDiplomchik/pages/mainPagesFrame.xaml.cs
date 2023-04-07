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
    /// Логика взаимодействия для mainPagesFrame.xaml
    /// </summary>
    public partial class mainPagesFrame : Page
    {
        public mainPagesFrame()
        {
            InitializeComponent();
           
            AppFrame1.frameMAIN = FRAME2;
            FRAME2.Navigate(new statistics());
        }
    }
}
