using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.IO;

namespace EyeChatWinUI3.Class
{
    public class User
    {
        public string Username { get; set; }
        public string Status { get; set; }
        public string AuxiliarState { get; set; }
        public string Room { get; set; }
        public string Avatar { get; set; }

        private static readonly string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Users.json");

        public static ObservableCollection<User> LoadUsersFromJson()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    var users = JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
                    return users ?? new ObservableCollection<User>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du chargement : " + ex.Message);
            }
            return new ObservableCollection<User>();
        }

        public static void SaveUsersToJson(ObservableCollection<User> users)
        {
            try
            {
                string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la sauvegarde : " + ex.Message);
            }
        }

    }
}
