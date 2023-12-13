using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.IO;


namespace EyeChatWinUI3.Class
{
    public class SpeedMessage
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public string Destinataire { get; set; }
        public string Message { get; set; }
        public string Options { get; set; }
        public bool Load { get; set; }

        private static readonly string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "SpeedMessages.json");

        public static ObservableCollection<SpeedMessage> LoadSpeedMessagesFromJson()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    var messages = JsonConvert.DeserializeObject<ObservableCollection<SpeedMessage>>(json);
                    return messages ?? new ObservableCollection<SpeedMessage>();
                }
            }
            catch (Exception ex)
            {
                // Gérer ou enregistrer l'exception
                Console.WriteLine("Erreur lors du chargement : " + ex.Message);
            }

            return new ObservableCollection<SpeedMessage>();
        }

        public static void SaveSpeedMessagesToJson(ObservableCollection<SpeedMessage> messages)
        {
            try
            {
                string json = JsonConvert.SerializeObject(messages, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                // Gérer ou enregistrer l'exception
                Console.WriteLine("Erreur lors de la sauvegarde : " + ex.Message);
            }
        }
    }
}
