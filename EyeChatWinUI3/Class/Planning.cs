using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.IO;

namespace EyeChatWinUI3.Class
{
    public class Planning
    {
        public string day { get; set; }
        public string user { get; set; }

        private static readonly string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Planning.json");

        public static ObservableCollection<Planning> LoadPlanningFromJson()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    var plannings = JsonConvert.DeserializeObject<ObservableCollection<Planning>>(json);
                    return plannings ?? new ObservableCollection<Planning>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du chargement : " + ex.Message);
            }
            return new ObservableCollection<Planning>();
        }

        public static void SavePlanningToJson(ObservableCollection<Planning> plannings)
        {
            try
            {
                string json = JsonConvert.SerializeObject(plannings, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la sauvegarde : " + ex.Message);
            }
        }
    }
}
