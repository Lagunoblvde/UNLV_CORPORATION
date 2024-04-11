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
    /// Логика взаимодействия для Page11.xaml
    /// </summary>
    public partial class Page11 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page11()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.View_History.Include("Goods").ToList();
            this.Loaded += (s, e) =>
            {
                var postEmployees = context.Goods.ToList();
                foreach (var cls in postEmployees)
                {
                    Odin_Cbx.Items.Add(new ComboBoxItem() { Content = cls.NameGood, Tag = cls.ID_Good });
                }
            };
        }


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(Name_Tbx.Text, out DateTime viewDate))
            {
                View_History clientStatus = new View_History();
                clientStatus.Viewdate = viewDate;

                if (Odin_Cbx.SelectedItem != null)
                {
                    ComboBoxItem stocksItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                    if (int.TryParse(stocksItem.Tag.ToString(), out int stocksID))
                    {
                        clientStatus.Good_ID = stocksID;

                        try
                        {
                            context.View_History.Add(clientStatus);
                            context.SaveChanges();
                            Tablica_Dgr.ItemsSource = context.View_History.ToList();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при добавлении даты просмотра: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранного значения для даты просмотра.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для даты просмотра.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Введите корректную дату в поле с именем.");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                if (DateTime.TryParse(Name_Tbx.Text, out DateTime viewDate))
                {
                    ClientStatus selectedClientStatus = (ClientStatus)Tablica_Dgr.SelectedItem;
                    selectedClientStatus.NameStatusC = Name_Tbx.Text;

                    if (Odin_Cbx.SelectedItem != null)
                    {
                        ComboBoxItem stocksItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                        if (int.TryParse(stocksItem.Tag.ToString(), out int stocksID))
                        {
                            selectedClientStatus.Stocks_ID = stocksID;

                            try
                            {
                                context.SaveChanges();
                                Tablica_Dgr.ItemsSource = context.View_History.ToList();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка при обновлении даты просмотра: {ex.Message}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ошибка: Неверный формат выбранного значения для склада.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Выберите значение для даты просмотра.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Введите корректную дату в поле с именем.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите дату просмотра для изменения.");
            }
        }


        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                View_History selectedClientStatus = (View_History)Tablica_Dgr.SelectedItem;

                context.View_History.Remove(selectedClientStatus);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.View_History.ToList();
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите дату просмотра для удаления.");
            }
        }

        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                var selected = Tablica_Dgr.SelectedItem as View_History;
                Name_Tbx.Text = Convert.ToString(selected.Viewdate);
                var comboBoxItem1 = Odin_Cbx.Items
                                       .Cast<ComboBoxItem>()
                                       .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.Good_ID);
                if (comboBoxItem1 != null)
                {
                    Odin_Cbx.SelectedItem = comboBoxItem1;
                    Odin_Cbx.Text = comboBoxItem1.Content.ToString();
                }
                else
                {
                    Odin_Cbx.SelectedItem = null;
                    Odin_Cbx.Text = string.Empty;
                }
            }
        }
    }
}
