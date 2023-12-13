using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace EyeChatWinUI3.Class
{
    public class ExamList
    {
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public Color ExamColor { get; set; }
        public string ExamCodeMSGCreate { get; set; }
        public string ExamCodeMSGPass { get; set; }
        public string ExamAnnotation { get; set; }
        public string ExamDescription { get; set; }
        public int ExamPriority { get; set; }        
        public int ExamMinimalWaitingTime { get; set; }

        private static readonly string FilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "ExamList.json");

        public static ObservableCollection<ExamList> LoadExamListFromJson()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    var exams = JsonConvert.DeserializeObject<ObservableCollection<ExamList>>(json);
                    return exams ?? new ObservableCollection<ExamList>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du chargement : " + ex.Message);
            }
            return new ObservableCollection<ExamList>();
        }

        public static void SaveExamListToJson(ObservableCollection<ExamList> exams)
        {
            try
            {
                string json = JsonConvert.SerializeObject(exams, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la sauvegarde : " + ex.Message);
            }
        }
    }
}
