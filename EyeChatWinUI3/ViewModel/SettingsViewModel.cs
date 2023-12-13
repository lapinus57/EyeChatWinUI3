using EyeChatWinUI3.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeChatWinUI3.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WaitingRoom> _WaitingRooms;
        public ObservableCollection<WaitingRoom> WaitingRooms
        {
            get => _WaitingRooms;
            set
            {
                _WaitingRooms = value;
                OnPropertyChanged(nameof(WaitingRooms));
            }
        }

        private ObservableCollection<ExamRoom> _ExamRooms;
        public ObservableCollection<ExamRoom> ExamRooms
        {
            get => _ExamRooms;
            set
            {
                _ExamRooms = value;
                OnPropertyChanged(nameof(ExamRooms));
            }
        }

        private ObservableCollection<SpeedMessage> _speedMessages;
        public ObservableCollection<SpeedMessage> SpeedMessages
        {
            get => _speedMessages;
            set
            {
                _speedMessages = value;
                OnPropertyChanged(nameof(SpeedMessages));
            }
        }

        private ObservableCollection<Planning> _plannings;
        public ObservableCollection<Planning> Plannings
        {
            get => _plannings;
            set
            {
                _plannings = value;
                OnPropertyChanged(nameof(Plannings));
            }
        }

        private ObservableCollection<ExamList> _examLists;
        public ObservableCollection<ExamList> ExamLists
        {
            get => _examLists;
            set
            {
                _examLists = value;
                OnPropertyChanged(nameof(ExamLists));
            }
        }

        // Implémentation de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
