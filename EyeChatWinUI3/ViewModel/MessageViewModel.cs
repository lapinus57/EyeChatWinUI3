using EyeChatWinUI3.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace EyeChatWinUI3.ViewModel
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WaitingRoom> _Waitingrooms;
        public ObservableCollection<WaitingRoom> WaitingRooms
        {
            get => _Waitingrooms;
            private set
            {
                _Waitingrooms = value;
                OnPropertyChanged(nameof(WaitingRooms));
            }
        }

        private ObservableCollection<SpeedMessage> _speedMessages;
        public ObservableCollection<SpeedMessage> SpeedMessages
        {
            get => _speedMessages;
            private set
            {
                _speedMessages = value;
                OnPropertyChanged(nameof(SpeedMessages));
            }
        }

        public MessageViewModel()
        {
            WaitingRooms = WaitingRoom.LoadWaitingRoomsFromJson();
            var allMessages = SpeedMessage.LoadSpeedMessagesFromJson();
            SpeedMessages = new ObservableCollection<SpeedMessage>(allMessages.Where(m => m.Load));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}