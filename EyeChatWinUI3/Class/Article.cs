using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.IO;

namespace EyeChatWinUI3.Class
{
    public class Article
    {
        public string HeaderArticle { get; set; }
        public string DescriptionArticle { get; set; }
        public string category { get; set; }
        public string ArticleAuthor { get; set; }
        public string ArticleContent { get; set; }

        private static readonly string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Articles.json");

        public static ObservableCollection<Article> LoadArticlesFromJson()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    var articles = JsonConvert.DeserializeObject<ObservableCollection<Article>>(json);
                    return articles ?? new ObservableCollection<Article>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du chargement : " + ex.Message);
            }
            return new ObservableCollection<Article>();
        }

        public static void SaveArticlesToJson(ObservableCollection<Article> articles)
        {
            try
            {
                string json = JsonConvert.SerializeObject(articles, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la sauvegarde : " + ex.Message);
            }
        }
    }
}
