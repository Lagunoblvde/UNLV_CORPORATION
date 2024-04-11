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
    /// Логика взаимодействия для Page13.xaml
    /// </summary>
    public partial class Page13 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page13()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Cheque_Good.Include("Goods").Include("Cheque").ToList();
            this.Loaded += (s, e) =>
            {
                var postEmployees = context.Goods.ToList();
                foreach (var cls in postEmployees)
                {
                    Odin_Cbx.Items.Add(new ComboBoxItem() { Content = cls.NameGood, Tag = cls.ID_Good });
                }
                var roles = context.Cheque.ToList();
                foreach (var cls in roles)
                {
                    Dva_Cbx.Items.Add(new ComboBoxItem() { Content = cls.NumberCheque, Tag = cls.ID_Cheque });
                }
            };
        }


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            Cheque_Good chequeGood = new Cheque_Good();

            // Заполнение полей через TextBox
            if (decimal.TryParse(Sum_Tbx.Text, out decimal sum))
            {
                chequeGood.SumOfCheque = sum;
            }
            else
            {
                MessageBox.Show("Ошибка: Неверный формат суммы чека.");
                return;
            }

            // Проверка и заполнение поля через ComboBox для товара
            if (Odin_Cbx.SelectedItem != null)
            {
                ComboBoxItem goodItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                if (int.TryParse(goodItem.Tag.ToString(), out int goodID))
                {
                    chequeGood.Good_ID = goodID;
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранного значения для товара.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для товара.");
                return;
            }

            // Проверка и заполнение поля через ComboBox для чека
            if (Dva_Cbx.SelectedItem != null)
            {
                ComboBoxItem chequeItem = (ComboBoxItem)Dva_Cbx.SelectedItem;

                if (int.TryParse(chequeItem.Tag.ToString(), out int chequeID))
                {
                    chequeGood.Cheque_ID = chequeID;
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат выбранного значения для чека.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите значение для чека.");
                return;
            }

            try
            {
                context.Cheque_Good.Add(chequeGood);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Cheque_Good.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара в чек: {ex.Message}");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Cheque_Good selectedChequeGood = (Cheque_Good)Tablica_Dgr.SelectedItem;

                // Обновление данных через TextBox
                if (decimal.TryParse(Sum_Tbx.Text, out decimal sum))
                {
                    selectedChequeGood.SumOfCheque = sum;
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат суммы чека.");
                    return;
                }

                // Обновление данных через ComboBox для товара
                if (Odin_Cbx.SelectedItem != null)
                {
                    ComboBoxItem goodItem = (ComboBoxItem)Odin_Cbx.SelectedItem;

                    if (int.TryParse(goodItem.Tag.ToString(), out int goodID))
                    {
                        selectedChequeGood.Good_ID = goodID;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранного значения для товара.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для товара.");
                    return;
                }

                // Обновление данных через ComboBox для чека
                if (Dva_Cbx.SelectedItem != null)
                {
                    ComboBoxItem chequeItem = (ComboBoxItem)Dva_Cbx.SelectedItem;

                    if (int.TryParse(chequeItem.Tag.ToString(), out int chequeID))
                    {
                        selectedChequeGood.Cheque_ID = chequeID;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Неверный формат выбранного значения для чека.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Выберите значение для чека.");
                    return;
                }

                try
                {
                    context.SaveChanges();
                    Tablica_Dgr.ItemsSource = context.Cheque_Good.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении товара в чеке: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите товар в чеке для изменения.");
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                Cheque_Good selectedChequeGood = (Cheque_Good)Tablica_Dgr.SelectedItem;

                context.Cheque_Good.Remove(selectedChequeGood);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Cheque_Good.ToList();
            }
            else
            {
                MessageBox.Show("Ошибка: Выберите товар в чеке для удаления.");
            }
        }



        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem is Cheque_Good selected)
            {
                Sum_Tbx.Text = Convert.ToString(selected.SumOfCheque);
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

                var comboBoxItem = Dva_Cbx.Items
                                    .Cast<ComboBoxItem>()
                                    .FirstOrDefault(i => i.Tag != null && (int)i.Tag == selected.Cheque_ID);
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
