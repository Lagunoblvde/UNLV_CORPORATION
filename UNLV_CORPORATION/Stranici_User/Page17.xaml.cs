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

namespace UNLV_CORPORATION
{
    /// <summary>
    /// Логика взаимодействия для Page17.xaml
    /// </summary>
    public partial class Page17 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page17()
        {
            InitializeComponent();
            Tablica_Dqr.ItemsSource = context.ClientStatus.Include("Stocks").ToList();

        }
    }
}
