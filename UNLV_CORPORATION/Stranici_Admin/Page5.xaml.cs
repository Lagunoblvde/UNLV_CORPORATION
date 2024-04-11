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
    /// Логика взаимодействия для Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page5()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Cheque.Include("Clients").Include("Employees").ToList();
            this.Loaded += (s, e) =>
            {
                var postEmployees = context.Clients.ToList();
                foreach (var cls in postEmployees)
                {
                    Odin_Cbx.Items.Add(new ComboBoxItem() { Content = cls.FirstNameClient, Tag = cls.ID_Client });
                }
                var roles = context.Employees.ToList();
                foreach (var cls in roles)
                {
                    Dva_Cbx.Items.Add(new ComboBoxItem() { Content = cls.FirstNameEmployee, Tag = cls.ID_Employee });
                }
            };
        }


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Cheque cheque = new Cheque();

            // Заполнение полей через TextBox
            if (int.TryParse(Name_Tbx.Text, out int number))
            {
                cheque.NumberCheque = number;
            }
            else
            {
                MessageBox.Show("Ошибка: Неверный формат номера чека.");
                return;
            }

            // Проверка и заполнение поля через ComboBox для клиента
            if (Odin_Cbx.SelectedItem != null)
            {
                ComboBoxItem clientItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                if (int.TryParse(clientItem.Tag.ToString(), out int clientID))
                {
                    cheque.Client_ID = clientID;
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

            // Проверка и заполнение поля через ComboBox для сотрудника
            if (Dva_Cbx.SelectedItem != null)
            {
                ComboBoxItem employeeItem = (ComboBoxItem)Dva_Cbx.SelectedItem;

                if (int.TryParse(employeeItem.Tag.ToString(), out int employeeID))
                {
                    cheque.Employee_ID = employeeID;
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранного значения для сотрудника.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для сотрудника.");
                return;
            }

            try
            {
                context.Cheque.Add(cheque);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Cheque.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении чека: {ex.Message}");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Cheque selectedCheque = (Cheque)Tablica_Dgr.SelectedItem;

                // Обновление данных через TextBox
                if (int.TryParse(Name_Tbx.Text, out int number))
                {
                    selectedCheque.NumberCheque = number;
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат номера чека.");
                    return;
                }

                // Обновление данных через ComboBox для клиента
                if (Odin_Cbx.SelectedItem != null)
                {
                    ComboBoxItem clientItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                    if (int.TryParse(clientItem.Tag.ToString(), out int clientID))
                    {
                        selectedCheque.Client_ID = clientID;
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

                // Обновление данных через ComboBox для сотрудника
                if (Dva_Cbx.SelectedItem != null)
                {
                    ComboBoxItem employeeItem = (ComboBoxItem)Dva_Cbx.SelectedItem;

                    if (int.TryParse(employeeItem.Tag.ToString(), out int employeeID))
                    {
                        selectedCheque.Employee_ID = employeeID;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранного значения для сотрудника.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для сотрудника.");
                    return;
                }

                try
                {
                    context.SaveChanges();
                    Tablica_Dgr.ItemsSource = context.Cheque.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении чека: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите чек для изменения.");
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Cheque selectedCheque = (Cheque)Tablica_Dgr.SelectedItem;

                context.Cheque.Remove(selectedCheque);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Cheque.ToList();
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите чек для удаления.");
            }
        }



        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem is Cheque selected)
            {
                Name_Tbx.Text = Convert.ToString(selected.NumberCheque);
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
                                    .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.Employee_ID);
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
