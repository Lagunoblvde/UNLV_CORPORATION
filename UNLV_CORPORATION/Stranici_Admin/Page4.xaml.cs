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
    /// Логика взаимодействия для Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page4()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Goods.Include("Good_Article").ToList();
            this.Loaded += (s, e) =>
            {
                var postEmployees = context.Good_Article.ToList();
                foreach (var cls in postEmployees)
                {
                    Odin_Cbx.Items.Add(new ComboBoxItem() { Content = cls.NameArticle, Tag = cls.ID_GoodArticl });
                }
            };
            }
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Goods good = new Goods();

            // Заполнение полей через TextBox
            good.NameGood = Name_Tbx.Text;

            if (decimal.TryParse(Cost_Tbx.Text, out decimal cost))
            {
                good.CostGood = cost;
            }
            else
            {
                MessageBox.Show("Ошибка: Неверный формат стоимости товара.");
                return;
            }

            // Проверка и заполнение поля через ComboBox
            if (Odin_Cbx.SelectedItem != null)
            {
                ComboBoxItem articleItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                if (int.TryParse(articleItem.Tag.ToString(), out int articleID))
                {
                    good.Article_ID = articleID;

                    try
                    {
                        context.Goods.Add(good);
                        context.SaveChanges();
                        Tablica_Dgr.ItemsSource = context.Goods.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранного значения для статьи товара.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для статьи товара.");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Goods selectedGood = (Goods)Tablica_Dgr.SelectedItem;

                // Обновление данных через TextBox
                selectedGood.NameGood = Name_Tbx.Text;

                if (decimal.TryParse(Cost_Tbx.Text, out decimal cost))
                {
                    selectedGood.CostGood = cost;
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат стоимости товара.");
                    return;
                }

                // Обновление данных через ComboBox
                if (Odin_Cbx.SelectedItem != null)
                {
                    ComboBoxItem articleItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                    if (int.TryParse(articleItem.Tag.ToString(), out int articleID))
                    {
                        selectedGood.Article_ID = articleID;

                        try
                        {
                            context.SaveChanges();
                            Tablica_Dgr.ItemsSource = context.Goods.ToList();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при обновлении товара: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранного значения для статьи товара.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для статьи товара.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите товар для изменения.");
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Goods selectedGood = (Goods)Tablica_Dgr.SelectedItem;

                context.Goods.Remove(selectedGood);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Goods.ToList();
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите товар для удаления.");
            }
        }


        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem is Goods selected)
            {
                Name_Tbx.Text = selected.NameGood;
                Cost_Tbx.Text = Convert.ToString(selected.CostGood);
                var comboBoxItem1 = Odin_Cbx.Items
                                        .Cast<ComboBoxItem>()
                                        .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.Article_ID);
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
