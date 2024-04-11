using Newtonsoft.Json;
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
using System.IO;
using static UNLV_CORPORATION.Page10;

namespace UNLV_CORPORATION
{
    /// <summary>
    /// Логика взаимодействия для Page21.xaml
    /// </summary>
    public partial class Page21 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();
        public Page21()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.PostEmployee.ToList();
        }

        public class Import2Json
        {
            public string NamePost { get; set; }
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NamePost_Tbx.Text))
            {
                PostEmployee article = new PostEmployee();
                article.NamePost = NamePost_Tbx.Text.Trim();

                try
                {
                    context.PostEmployee.Add(article);
                    context.SaveChanges();
                    Tablica_Dgr.ItemsSource = context.PostEmployee.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении должности: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите название должности.");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                if (!string.IsNullOrWhiteSpace(NamePost_Tbx.Text))
                {
                    var selected = Tablica_Dgr.SelectedItem as PostEmployee;
                    selected.NamePost = NamePost_Tbx.Text.Trim();

                    try
                    {
                        context.SaveChanges();
                        Tablica_Dgr.ItemsSource = context.PostEmployee.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при изменении должности: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите название должности.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите должность для изменения.");
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                var selectedPost = Tablica_Dgr.SelectedItem as PostEmployee;

                try
                {
                    context.PostEmployee.Remove(selectedPost);
                    context.SaveChanges();
                    Tablica_Dgr.ItemsSource = context.PostEmployee.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении должности: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите должность для удаления.");
            }
        }


        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                var selected = Tablica_Dgr.SelectedItem as PostEmployee;
                NamePost_Tbx.Text = selected.NamePost;
            }
        }

        private void Import_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string jsonFilePath = @"C:\Users\Vladi\Desktop\import2.json";

                var json = File.ReadAllText(jsonFilePath);
                var articles = JsonConvert.DeserializeObject<List<Import2Json>>(json);

                if (articles == null)
                {
                    MessageBox.Show("Нет данных для импорта.");
                    return;
                }

                foreach (var articleJson in articles)
                {
                    var article = new PostEmployee { NamePost = articleJson.NamePost };
                    context.PostEmployee.Add(article);
                }

                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.PostEmployee.ToList();
                MessageBox.Show("Импорт завершен успешно.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при импорте: {ex.Message}");
            }
        }
    }
}
