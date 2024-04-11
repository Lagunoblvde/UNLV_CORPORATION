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
    /// Логика взаимодействия для Page8.xaml
    /// </summary>
    public partial class Page8 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page8()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Stocks.ToList();
        }


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Stocks stonk = new Stocks();
            stonk.NameStock = Name_Tbx.Text;
            stonk.BeginingDate = Convert.ToDateTime(BDate_Tbx.Text);
            stonk.EndingDate = Convert.ToDateTime(EDate_Tbx.Text);
            context.Stocks.Add(stonk);
            context.SaveChanges();
            Tablica_Dgr.ItemsSource = context.Stocks.ToList();
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                var selected = Tablica_Dgr.SelectedItem as Stocks;
                selected.NameStock = Name_Tbx.Text;
                selected.BeginingDate = Convert.ToDateTime(BDate_Tbx.Text);
                selected.EndingDate = Convert.ToDateTime(EDate_Tbx.Text);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Stocks.ToList();
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                context.Stocks.Remove(Tablica_Dgr.SelectedItem as Stocks);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Stocks.ToList();
            }
        }

        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                var selected = Tablica_Dgr.SelectedItem as Stocks;
                Name_Tbx.Text = selected.NameStock;
                BDate_Tbx.Text = Convert.ToString(selected.BeginingDate);
                EDate_Tbx.Text = Convert.ToString(selected.EndingDate);
            }
        }
    }
}
