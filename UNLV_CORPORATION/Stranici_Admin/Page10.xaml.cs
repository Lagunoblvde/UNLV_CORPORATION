using Newtonsoft.Json;
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
using System.IO;

namespace UNLV_CORPORATION
{
    /// <summary>
    /// Логика взаимодействия для Page10.xaml
    /// </summary>
    public partial class Page10 : Page
    {
        UNLV_CORPORATIONEntities1 context = new UNLV_CORPORATIONEntities1();

        public Page10()
        {
            InitializeComponent();
            Tablica_Dgr.ItemsSource = context.Good_Article.ToList();
        }
        public class ImportJson
        {
            public string NameArticle { get; set; }
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Name_Tbx.Text))
            {
                Good_Article article = new Good_Article();
                article.NameArticle = Name_Tbx.Text;
                context.Good_Article.Add(article);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Good_Article.ToList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, напишите корректное имя Артикля Товара");
            }
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null && !string.IsNullOrWhiteSpace(Name_Tbx.Text))
            {
                var selected = Tablica_Dgr.SelectedItem as Good_Article;
                selected.NameArticle = Name_Tbx.Text;
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Good_Article.ToList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите статью и введите действительное название статьи");
            }
        }

        private void Del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                context.Good_Article.Remove(Tablica_Dgr.SelectedItem as Good_Article);
                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Good_Article.ToList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите Артикль Товара для удаления");
            }
        }

        private void Tablica_Dgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tablica_Dgr.SelectedItem != null)
            {
                var selected = Tablica_Dgr.SelectedItem as Good_Article;
                Name_Tbx.Text = selected.NameArticle;
            }
        }

        private void Import_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string jsonFilePath = @"C:\Users\Vladi\Desktop\import.json";

                var json = File.ReadAllText(jsonFilePath);
                var articles = JsonConvert.DeserializeObject<List<ImportJson>>(json);

                if (articles == null)
                {
                    MessageBox.Show("Нет данных для импорта.");
                    return;
                }

                foreach (var articleJson in articles)
                {
                    var article = new Good_Article { NameArticle = articleJson.NameArticle };
                    context.Good_Article.Add(article);
                }

                context.SaveChanges();
                Tablica_Dgr.ItemsSource = context.Good_Article.ToList();
                MessageBox.Show("Импорт завершен успешно.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при импорте: {ex.Message}");
            }
        }
    }
}
