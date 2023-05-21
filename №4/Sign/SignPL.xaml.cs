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

namespace _4.Sign
{
    /// <summary>
    /// Логика взаимодействия для SignPL.xaml
    /// </summary>
    public partial class SignPL : Page
    {
        public SignPL()
        {
            InitializeComponent();
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ColorPage());
        }
    }
}
