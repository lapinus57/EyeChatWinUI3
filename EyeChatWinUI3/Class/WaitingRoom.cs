using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Windows.Storage;

namespace EyeChatWinUI3.Class
{
    public class WaitingRoom
    {
        public string WaitingRoomName { get; set; }
        public string WaitingRoomDescription { get; set; }
        public string WaitingRoomTilte { get; set; }

        // ... autres propriétés et méthodes ...
        

        private static readonly string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "WaitingRoom.json");

        public static ObservableCollection<WaitingRoom> LoadWaitingRoomsFromJson()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    var rooms = JsonConvert.DeserializeObject<ObservableCollection<WaitingRoom>>(json);
                    return rooms ?? new ObservableCollection<WaitingRoom>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du chargement : " + ex.Message);
            }
            return new ObservableCollection<WaitingRoom>();
        }

        public static void SaveWaitingRoomsToJson(ObservableCollection<WaitingRoom> rooms)
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
