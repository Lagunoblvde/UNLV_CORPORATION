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
    /// Логика взаимодействия для Page9.xaml
    /// </summary>
    public partial class Page9 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page9()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.ClientStatus.Include("Stocks").ToList();
            this.Loaded += (s, e) =>
            {
                var postEmployees = context.Stocks.ToList();
                foreach (var cls in postEmployees)
                {
                    Odin_Cbx.Items.Add(new ComboBoxItem() { Content = cls.NameStock, Tag = cls.ID_Stocks });
                }
            };
        }


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            ClientStatus clientStatus = new ClientStatus();

            // Заполнение полей через TextBox
            clientStatus.NameStatusC = Name_Tbx.Text;

            // Проверка и заполнение поля через ComboBox
            if (Odin_Cbx.SelectedItem != null)
            {
                ComboBoxItem stocksItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                if (int.TryParse(stocksItem.Tag.ToString(), out int stocksID))
                {
                    clientStatus.Stocks_ID = stocksID;

                    try
                    {
                        context.ClientStatus.Add(clientStatus);
                        context.SaveChanges();
                        Tablica_Dgr.ItemsSource = context.ClientStatus.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении статуса клиента: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранного значения для склада.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для склада.");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                ClientStatus selectedClientStatus = (ClientStatus)Tablica_Dgr.SelectedItem;

                // Обновление данных через TextBox
                selectedClientStatus.NameStatusC = Name_Tbx.Text;

                // Обновление данных через ComboBox
                if (Odin_Cbx.SelectedItem != null)
                {
                    ComboBoxItem stocksItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                    if (int.TryParse(stocksItem.Tag.ToString(), out int stocksID))
                    {
                        selectedClientStatus.Stocks_ID = stocksID;

                        try
                        {
                            context.SaveChanges();
                            Tablica_Dgr.ItemsSource = context.ClientStatus.ToList();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при обновлении статуса клиента: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранного значения для склада.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для склада.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите статус клиента для изменения.");
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                ClientStatus selectedClientStatus = (ClientStatus)Tablica_Dgr.SelectedItem;

                context.ClientStatus.Remove(selectedClientStatus);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.ClientStatus.ToList();
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите статус клиента для удаления.");
            }
        }

        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem is ClientStatus selected)
            {
                Name_Tbx.Text = selected.NameStatusC;
                var comboBoxItem1 = Odin_Cbx.Items
                                       .Cast<ComboBoxItem>()
                                       .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.Stocks_ID);
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
