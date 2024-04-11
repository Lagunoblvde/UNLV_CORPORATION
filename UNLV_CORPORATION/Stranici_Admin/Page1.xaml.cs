using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();
        public Page1()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Clients.Include("ClientStatus").Include("Roles").ToList();
            this.Loaded += (s, e) =>
            {
                var clientStatuses = context.ClientStatus.ToList();
                foreach (var cls in clientStatuses)
                {
                    Odin_Cbx.Items.Add(new ComboBoxItem() { Content = cls.NameStatusC, Tag = cls.ID_StatusC });
                }
                var roles = context.Roles.ToList();
                foreach (var cls in roles)
                {
                    Dva_Cbx.Items.Add(new ComboBoxItem() { Content = cls.NameRole, Tag = cls.ID_Role });
                }
            };
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Clients client = new Clients();

            // Заполнение полей через TextBox
            client.FirstNameClient = Name_Tbx.Text;
            client.SurNameClient = Surname_Tbx.Text;
            client.MiddleNameClient = Mid_Tbx.Text;
            client.PhoneNumberClient = PNumber_Tbx.Text;

            // Проверка и заполнение полей через ComboBox
            if (Odin_Cbx.SelectedItem != null && Dva_Cbx.SelectedItem != null)
            {
                ComboBoxItem statusItem = (ComboBoxItem)Odin_Cbx.SelectedItem;
                ComboBoxItem roleItem = (ComboBoxItem)Dva_Cbx.SelectedItem;

                if (int.TryParse(statusItem.Tag.ToString(), out int statusID) && int.TryParse(roleItem.Tag.ToString(), out int roleID))
                {
                    client.StatusClient_ID = statusID;
                    client.Role_ID = roleID;

                    try
                    {
                        context.Clients.Add(client);
                        context.SaveChanges();
                        Tablica_Dgr.ItemsSource = context.Clients.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранных значений для статуса или роли.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для статуса и роли.");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Clients selectedClient = (Clients)Tablica_Dgr.SelectedItem;

                // Обновление данных через TextBox
                selectedClient.FirstNameClient = Name_Tbx.Text;
                selectedClient.SurNameClient = Surname_Tbx.Text;
                selectedClient.MiddleNameClient = Mid_Tbx.Text;
                selectedClient.PhoneNumberClient = PNumber_Tbx.Text;

                // Обновление данных через ComboBox
                if (Odin_Cbx.SelectedItem != null && Dva_Cbx.SelectedItem != null)
                {
                    ComboBoxItem statusItem = (ComboBoxItem)Odin_Cbx.SelectedItem;
                    ComboBoxItem roleItem = (ComboBoxItem)Dva_Cbx.SelectedItem;

                    if (int.TryParse(statusItem.Tag.ToString(), out int statusID) && int.TryParse(roleItem.Tag.ToString(), out int roleID))
                    {
                        selectedClient.StatusClient_ID = statusID;
                        selectedClient.Role_ID = roleID;

                        try
                        {
                            context.SaveChanges();
                            Tablica_Dgr.ItemsSource = context.Clients.ToList();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при обновлении клиента: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранных значений для статуса или роли.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для статуса и роли.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите клиента для изменения.");
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Clients selectedClient = (Clients)Tablica_Dgr.SelectedItem;

                // Проверяем, есть ли связанные записи в таблице Roles
                bool hasRoles = context.Roles.Any(r => r.ID_Role == selectedClient.Role_ID);

                // Проверяем, есть ли связанные записи в таблице ClientStatus
                bool hasStatus = context.ClientStatus.Any(s => s.ID_StatusC == selectedClient.StatusClient_ID);

                if (!hasRoles && !hasStatus)
                {
                    context.Clients.Remove(selectedClient);
                    context.SaveChanges();
                    Tablica_Dgr.ItemsSource = context.Clients.ToList();
                }
                else
                {
                    MessageBox.Show("Нельзя удалить этого клиента, так как у него есть связи с другими таблицами.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите клиента для удаления.");
            }
        }


        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem is Clients selectedAuth)
            {
                Name_Tbx.Text = selectedAuth.FirstNameClient;
                Surname_Tbx.Text = selectedAuth.SurNameClient;
                Mid_Tbx.Text = selectedAuth.MiddleNameClient;
                PNumber_Tbx.Text = selectedAuth.PhoneNumberClient.ToString();

                var comboBoxItem1 = Odin_Cbx.Items
                                        .Cast<ComboBoxItem>()
                                        .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selectedAuth.StatusClient_ID);
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
                                    .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selectedAuth.Role_ID);
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
