using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.IO;


namespace EyeChatWinUI3.Class
{
    public class Computer
    {
        public string ComputerID { get; set; }
        public string ComputerUser { get; set; }
        public string ComputerIp { get; set; }

        private static readonly string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Computers.json");

        public static ObservableCollection<Computer> LoadComputersFromJson()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    var computers = JsonConvert.DeserializeObject<ObservableCollection<Computer>>(json);
                    return computers ?? new ObservableCollection<Computer>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du chargement : " + ex.Message);
            }
            return new ObservableCollection<Computer>();
        }

        public static void SaveComputersToJson(ObservableCollection<Computer> computers)
        {
            try
            {
                string json = JsonConvert.SerializeObject(computers, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la sauvegarde : " + ex.Message);
            }
        }
    }
}
