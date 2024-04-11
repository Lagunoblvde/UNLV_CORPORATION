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
    /// Логика взаимодействия для Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page6()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Roles.ToList();
        }
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Roles role = new Roles();
            role.NameRole = Name_Tbx.Text;
            context.Roles.Add(role);
            context.SaveChanges();
            Tablica_Dgr.ItemsSource = context.Roles.ToList();
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                var selected = Tablica_Dgr.SelectedItem as Roles;
                selected.NameRole = Name_Tbx.Text;
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Roles.ToList();
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                context.Roles.Remove(Tablica_Dgr.SelectedItem as Roles);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Roles.ToList();
            }
        }

        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem is Roles selected && selected != null)
            {
                Name_Tbx.Text = selected.NameRole;
            }
        }
    }
}
