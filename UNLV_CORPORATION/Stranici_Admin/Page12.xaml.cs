using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для Page12.xaml
    /// </summary>
    public partial class Page12 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page12()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Client_History.Include("Clients").Include("View_History").ToList();
            this.Loaded += (s, e) =>
            {
                var postEmployees = context.Clients.ToList();
                foreach (var cls in postEmployees)
                {
                    Odin_Cbx.Items.Add(new ComboBoxItem() { Content = cls.FirstNameClient, Tag = cls.ID_Client });
                }
                var roles = context.View_History.ToList();
                foreach (var cls in roles)
                {
                    Dva_Cbx.Items.Add(new ComboBoxItem() { Content = cls.Viewdate, Tag = cls.ID_ViewHistory });
                }
            };

        }


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Client_History clientHistory = new Client_History();

            // Проверка и заполнение поля через ComboBox для клиента
            if (Odin_Cbx.SelectedItem != null)
            {
                ComboBoxItem clientItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                if (int.TryParse(clientItem.Tag.ToString(), out int clientID))
                {
                    clientHistory.Client_ID = clientID;
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранного значения для клиента.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для клиента.");
                return;
            }

            // Проверка и заполнение поля через ComboBox для истории просмотров
            if (Tablica_Dgr.SelectedItem != null)
            {
                ComboBoxItem viewHistoryItem = (ComboBoxItem)Dva_Cbx.SelectedItem;

                if (int.TryParse(viewHistoryItem.Tag.ToString(), out int viewHistoryID))
                {
                    clientHistory.ViewHistory_ID = viewHistoryID;
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранного значения для истории просмотров.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для истории просмотров.");
                return;
            }

            try
            {
                context.Client_History.Add(clientHistory);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Client_History.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении истории клиента: {ex.Message}");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Client_History selectedClientHistory = (Client_History)Tablica_Dgr.SelectedItem;

                // Обновление данных через ComboBox для клиента
                if (Odin_Cbx.SelectedItem != null)
                {
                    ComboBoxItem clientItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                    if (int.TryParse(clientItem.Tag.ToString(), out int clientID))
                    {
                        selectedClientHistory.Client_ID = clientID;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранного значения для клиента.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для клиента.");
                    return;
                }

                // Обновление данных через ComboBox для истории просмотров
                if (Dva_Cbx.SelectedItem != null)
                {
                    ComboBoxItem viewHistoryItem = (ComboBoxItem)Dva_Cbx.SelectedItem;

                    if (int.TryParse(viewHistoryItem.Tag.ToString(), out int viewHistoryID))
                    {
                        selectedClientHistory.ViewHistory_ID = viewHistoryID;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранного значения для истории просмотров.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для истории просмотров.");
                    return;
                }

                try
                {
                    context.SaveChanges();
                    Tablica_Dgr.ItemsSource = context.Client_History.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении истории клиента: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите историю клиента для изменения.");
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Client_History selectedClientHistory = (Client_History)Tablica_Dgr.SelectedItem;

                context.Client_History.Remove(selectedClientHistory);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Client_History.ToList();
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите историю клиента для удаления.");
            }
        }


        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem is Client_History selected)
            {
                var comboBoxItem1 = Odin_Cbx.Items
                                       .Cast<ComboBoxItem>()
                                       .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.Client_ID);
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

                var comboBoxItem = Dva_Cbx.Items
                                    .Cast<ComboBoxItem>()
                                    .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.ViewHistory_ID);
                if (comboBoxItem != null)
                {
                    Dva_Cbx.SelectedItem = comboBoxItem;
                    Dva_Cbx.Text = comboBoxItem.Content.ToString();
                }
                else
                {
                    Dva_Cbx.SelectedItem = null;
                    Dva_Cbx.Text = string.Empty;
                }
            }
        }
    }
}
