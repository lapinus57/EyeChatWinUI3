using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeChatWinUI3.Class
{
    public class Patient
    {
        public string PatientID { get; set; }
        public string PatientTilte { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientAnnotation { get; set; }
        public string PatientPosition { get; set; }
        public string PatientExam { get; set; }
        public string PatientExam1 { get; set; }
        public string PatientExam2 { get; set; }       
        public string PatientExaminator { get; set; }
        public string PatientExaminator1 { get; set; }
        public string PatientExaminator2 { get; set; }
        public DateTime PatientTime { get; set; }
        public TimeSpan PatientTimer { get; set; }
        public DateTime PatientTime1 { get; set; }
        public TimeSpan PatientTimer1 { get; set; }
        public DateTime PatientTime2 { get; set; }
        public TimeSpan PatientTimer2 { get; set; }
        public string PatientStatus { get; set; }

        
    }
}
