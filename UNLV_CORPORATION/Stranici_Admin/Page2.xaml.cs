using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1 ();
        public Page2()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Employees.Include("PostEmployee").Include("Roles").ToList();
            this.Loaded += (s, e) =>
            {
                var postEmployees = context.PostEmployee.ToList();
                foreach (var cls in postEmployees)
                {
                    Odin_Cbx.Items.Add(new ComboBoxItem() { Content = cls.NamePost, Tag = cls.ID_Post });
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
            Employees emp = new Employees();

            // Заполнение полей через TextBox
            emp.FirstNameEmployee = Name_Tbx.Text;
            emp.SurNameEmployee = Surname_Tbx.Text;
            emp.MiddleNameEmployee = Mid_Tbx.Text;

            // Проверка и заполнение полей через ComboBox
            if (Odin_Cbx.SelectedItem != null && Dva_Cbx.SelectedItem != null)
            {
                ComboBoxItem postItem = (ComboBoxItem)Odin_Cbx.SelectedItem;
                ComboBoxItem roleItem = (ComboBoxItem)Dva_Cbx.SelectedItem;

                if (int.TryParse(postItem.Tag.ToString(), out int postID) && int.TryParse(roleItem.Tag.ToString(), out int roleID))
                {
                    emp.Post_ID = postID;
                    emp.Role_ID = roleID;

                    try
                    {
                        context.Employees.Add(emp);
                        context.SaveChanges();
                        Tablica_Dgr.ItemsSource = context.Employees.Include("PostEmployee").Include("Roles").ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении сотрудника: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранных значений для должности или роли.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для должности и роли.");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Employees selectedEmployee = (Employees)Tablica_Dgr.SelectedItem;

                // Обновление данных через TextBox
                selectedEmployee.FirstNameEmployee = Name_Tbx.Text;
                selectedEmployee.SurNameEmployee = Surname_Tbx.Text;
                selectedEmployee.MiddleNameEmployee = Mid_Tbx.Text;

                // Обновление данных через ComboBox
                if (Odin_Cbx.SelectedItem != null && Dva_Cbx.SelectedItem != null)
                {
                    ComboBoxItem postItem = (ComboBoxItem)Odin_Cbx.SelectedItem;
                    ComboBoxItem roleItem = (ComboBoxItem)Dva_Cbx.SelectedItem;

                    if (int.TryParse(postItem.Tag.ToString(), out int postID) && int.TryParse(roleItem.Tag.ToString(), out int roleID))
                    {
                        selectedEmployee.Post_ID = postID;
                        selectedEmployee.Role_ID = roleID;

                        try
                        {
                            context.SaveChanges();
                            Tablica_Dgr.ItemsSource = context.Employees.Include("PostEmployee").Include("Roles").ToList();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при обновлении сотрудника: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранных значений для должности или роли.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для должности и роли.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите сотрудника для изменения.");
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Employees selectedEmployee = Tablica_Dgr.SelectedItem as Employees;

                // Проверяем, есть ли связанные записи в таблице Roles
                bool hasRoles = context.Roles.Any(r => r.ID_Role == selectedEmployee.Role_ID);

                // Проверяем, есть ли связанные записи в таблице PostEmployee
                bool hasPosts = context.PostEmployee.Any(p => p.ID_Post == selectedEmployee.Post_ID);

                if (!hasRoles && !hasPosts)
                {
                    context.Employees.Remove(selectedEmployee);
                    context.SaveChanges();
                    Tablica_Dgr.ItemsSource = context.Employees.ToList();
                }
                else
                {
                    MessageBox.Show("Нельзя удалить этого сотрудника, так как у него есть связи с другими таблицами.");
                }
            }
        }


        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem is Employees selected)
            {
                Name_Tbx.Text = selected.FirstNameEmployee;
                Surname_Tbx.Text = selected.SurNameEmployee;
                Mid_Tbx.Text = selected.MiddleNameEmployee;

                var comboBoxItem1 = Odin_Cbx.Items
                                        .Cast<ComboBoxItem>()
                                        .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.Post_ID);
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
                                    .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.Role_ID);
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
