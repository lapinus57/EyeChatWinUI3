using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace EyeChatWinUI3.Class
{
    public class ExamRoom
    {
        public string ExamRoomName { get; set; }
        public string ExamRoomDescription { get; set; }
       
        // ... autres propriétés et méthodes ...
        

        private static readonly string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "ExamRoom.json");

        public static ObservableCollection<ExamRoom> LoadExamRoomsFromJson()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    var rooms = JsonConvert.DeserializeObject<ObservableCollection<ExamRoom>>(json);
                    return rooms ?? new ObservableCollection<ExamRoom>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du chargement : " + ex.Message);
            }
            return new ObservableCollection<ExamRoom>();
        }

        public static void SaveExamRoomsToJson(ObservableCollection<ExamRoom> rooms)
        {
            try
            {
                string json = JsonConvert.SerializeObject(rooms, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la sauvegarde : " + ex.Message);
            }
        }
    }
}
