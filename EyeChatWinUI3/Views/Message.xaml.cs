using ColorCode.Styling;
using EyeChatWinUI3.Class;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using EyeChatWinUI3;
using System.Collections.ObjectModel;
using EyeChatWinUI3.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EyeChatWinUI3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Message : Page
    {

        public MessageViewModel ViewModelMessage { get; private set; }
        public Message()
        {
            this.InitializeComponent();
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            ViewModelMessage = new MessageViewModel();
            this.DataContext = ViewModelMessage;
            InitializeSpeedMessageMenu();
        }

        private void InitializeSpeedMessageMenu()
        {
            foreach (var speedMessage in ViewModelMessage.SpeedMessages.Where(m => m.Load))
            {
                var menuItem = new MenuFlyoutItem
                {
                    Text = speedMessage.Title
                };
                menuItem.Click += (s, e) =>
                {
                    SendTextBox.Text = speedMessage.Message;
                };

                SpeedMessageMenuFlyout.Items.Add(menuItem);
            }
        }


    }
}
