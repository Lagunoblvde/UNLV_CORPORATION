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
    /// Логика взаимодействия для Page7.xaml
    /// </summary>
    public partial class Page7 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page7()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Avt.Include("Roles").ToList();
            this.Loaded += (s, e) =>
            {
                var postEmployees = context.Roles.ToList();
                foreach (var cls in postEmployees)
                {
                    Odin_Cbx.Items.Add(new ComboBoxItem() { Content = cls.NameRole, Tag = cls.ID_Role });
                }
            };
            }


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Avt avt = new Avt();

            // Заполнение полей через TextBox
            avt.Login_user = Login_Tbx.Text;
            avt.Password_user = Password_Tbx.Password;

            // Проверка и заполнение поля через ComboBox
            if (Odin_Cbx.SelectedItem != null)
            {
                ComboBoxItem roleItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                if (int.TryParse(roleItem.Tag.ToString(), out int roleID))
                {
                    avt.Role_ID = roleID;

                    try
                    {
                        context.Avt.Add(avt);
                        context.SaveChanges();
                        Tablica_Dgr.ItemsSource = context.Avt.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранного значения для роли.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для роли.");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Avt selectedAvt = (Avt)Tablica_Dgr.SelectedItem;

                // Обновление данных через TextBox
                selectedAvt.Login_user = Login_Tbx.Text;
                selectedAvt.Password_user = Password_Tbx.Password;

                // Обновление данных через ComboBox
                if (Odin_Cbx.SelectedItem != null)
                {
                    ComboBoxItem roleItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                    if (int.TryParse(roleItem.Tag.ToString(), out int roleID))
                    {
                        selectedAvt.Role_ID = roleID;

                        try
                        {
                            context.SaveChanges();
                            Tablica_Dgr.ItemsSource = context.Avt.ToList();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при обновлении пользователя: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранного значения для роли.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для роли.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите пользователя для изменения.");
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Avt selectedAvt = (Avt)Tablica_Dgr.SelectedItem;

                // Проверяем, есть ли связанные записи в таблице Roles
                bool hasRoles = context.Roles.Any(r => r.ID_Role == selectedAvt.Role_ID);

                if (!hasRoles)
                {
                    context.Avt.Remove(selectedAvt);
                    context.SaveChanges();
                    Tablica_Dgr.ItemsSource = context.Avt.ToList();
                }
                else
                {
                    MessageBox.Show("Нельзя удалить этого пользователя, так как у него есть связи с другими таблицами.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите пользователя для удаления.");
            }
        }


        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem is Avt selected)
            {
                Login_Tbx.Text = selected.Login_user;
                Password_Tbx.Password = selected.Password_user;
                var comboBoxItem1 = Odin_Cbx.Items
                                       .Cast<ComboBoxItem>()
                                       .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.Role_ID);
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
