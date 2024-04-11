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
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();
        public Page3()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Users.Include("Avt").ToList();
            this.Loaded += (s, e) =>
            {
                var postEmployees = context.Avt.ToList();
                foreach (var cls in postEmployees)
                {
                    Odin_Cbx.Items.Add(new ComboBoxItem() { Content = cls.Login_user, Tag = cls.ID_Avt });
                }
            };
            }
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Users user = new Users();

            // Заполнение полей через TextBox
            user.FirstNameUser = Name_Tbx.Text;
            user.SurNameUser = SurName_Tbx.Text;

            // Проверка и заполнение поля через ComboBox
            if (Odin_Cbx.SelectedItem != null)
            {
                ComboBoxItem avtItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                if (int.TryParse(avtItem.Tag.ToString(), out int avtID))
                {
                    user.Avt_ID = avtID;

                    try
                    {
                        context.Users.Add(user);
                        context.SaveChanges();
                        Tablica_Dgr.ItemsSource = context.Users.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранного значения для авторизации.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для авторизации.");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Users selectedUser = (Users)Tablica_Dgr.SelectedItem;

                // Обновление данных через TextBox
                selectedUser.FirstNameUser = Name_Tbx.Text;
                selectedUser.SurNameUser = SurName_Tbx.Text;

                // Обновление данных через ComboBox
                if (Odin_Cbx.SelectedItem != null)
                {
                    ComboBoxItem avtItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                    if (int.TryParse(avtItem.Tag.ToString(), out int avtID))
                    {
                        selectedUser.Avt_ID = avtID;

                        try
                        {
                            context.SaveChanges();
                            Tablica_Dgr.ItemsSource = context.Users.ToList();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при обновлении пользователя: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранного значения для авторизации.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для авторизации.");
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
                Users selectedUser = (Users)Tablica_Dgr.SelectedItem;

                context.Users.Remove(selectedUser);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Users.ToList();
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите пользователя для удаления.");
            }
        }


        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem is Users selected)
            {
                Name_Tbx.Text = selected.FirstNameUser;
                SurName_Tbx.Text = selected.SurNameUser;
                var comboBoxItem1 = Odin_Cbx.Items
                                        .Cast<ComboBoxItem>()
                                        .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.Avt_ID);
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
