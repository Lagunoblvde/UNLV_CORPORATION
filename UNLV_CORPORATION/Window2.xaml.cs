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

namespace UNLV_CORPORATION
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void ViborTablici_Cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ViborTablici_Cbx.SelectedIndex)
            {
                case 0:
                    Frame.Navigate(new Page14());
                    break;

                case 1:
                    Frame.Navigate(new Page15());
                    break;

                case 2:
                    Frame.Navigate(new Page16());
                    break;

                case 3:
                    Frame.Navigate(new Page17());
                    break;

                case 4:
                    Frame.Navigate(new Page18());
                    break;

                case 5:
                    Frame.Navigate(new Page19());
                    break;

                case 6:
                    Frame.Navigate(new Page20());
                    break;
                default:
                    break;
            }
        }
    }
}
