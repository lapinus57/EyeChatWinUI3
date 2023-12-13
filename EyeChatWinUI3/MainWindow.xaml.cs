using ColorCode.Styling;
using CommunityToolkit.WinUI.UI;
using EyeChatWinUI3.Class;
using EyeChatWinUI3.Views;
using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.ClosedCaptioning;
using Windows.UI;
using Windows.UI.ViewManagement;
using WinRT.Interop;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EyeChatWinUI3
{
 

    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public ObservableCollection<WaitingRoom> WaitingRooms;
        public ObservableCollection<ExamRoom> ExamRooms;
        public ObservableCollection<SpeedMessage> SpeedMessages;
        public ObservableCollection<Planning> Plannings;
        public ObservableCollection<ExamList> ExamsList;

        public MainWindow()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;  // enable custom titlebar
            SetTitleBar(AppTitleBar);      // set user ui element as titlebar
            AppTitle.Text = "EyeChat";     // set titlebar text
            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();
            ContentFrame.Navigate(
                       typeof(Views.Message),
                       null,
                       new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo()
            );
            Instance = this;
            LoadWaitingRooms();
            LoadSpeedMessages();
            //Loadplannings();
            LoadExamRooms();
            LoadExamList();

           

        }


        public void ChangeTitleBarButtonColor(Color color)
        {
            
            AppWindow.TitleBar.ButtonForegroundColor = color;
        }


        private void NavigationViewControl_ItemInvoked(NavigationView sender,
                     NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                ContentFrame.Navigate(typeof(Views.Settings), null, args.RecommendedNavigationTransitionInfo);
               
            }
            else if (args.InvokedItemContainer != null && (args.InvokedItemContainer.Tag != null))
            {
                Type newPage = Type.GetType(args.InvokedItemContainer.Tag.ToString());
                ContentFrame.Navigate(
                       newPage,
                       null,
                       args.RecommendedNavigationTransitionInfo
                       );
            }
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(Views.Settings))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
            }
            else if (ContentFrame.SourcePageType != null)
            {
                NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(ContentFrame.SourcePageType.FullName.ToString()));
            }

            NavigationViewControl.Header = ((NavigationViewItem)NavigationViewControl.SelectedItem)?.Content?.ToString();
        }


        #region "Chargement des données"

        private void Loadplannings()
        {
            Plannings = Planning.LoadPlanningFromJson();
            if (Plannings.Count == 0)
            {
                // Si aucun planning n'a été chargé, on le crée 
                CreateDefaultPlannings();
            }
        }

        private void LoadWaitingRooms()
        {
            WaitingRooms = WaitingRoom.LoadWaitingRoomsFromJson();   
            if (WaitingRooms.Count == 0)
            {
                // Si aucune salle n'a été chargée, on les crée 
                CreateDefaultWaitingRooms();
            }

        }

        private void LoadExamRooms()
        {
            ExamRooms = ExamRoom.LoadExamRoomsFromJson();
            if (ExamRooms.Count == 0)
            {
                // Si aucune salle n'a été chargée, on les crée 
                CreateDefaultExamRooms();
            }

        }

        private void LoadSpeedMessages()
        {
            SpeedMessages = SpeedMessage.LoadSpeedMessagesFromJson();
            if (SpeedMessages.Count == 0)
            {
                // Si aucun message rapide n'a été chargé, on les crée
                CreateDefaultSpeedMessages();
            }
        }

        private void LoadExamList()
        {
            ExamsList = ExamList.LoadExamListFromJson();
            if (ExamsList.Count == 0)
            {
                // Si aucune salle n'a été chargée, on les crée 
                CreateDefaultExamList();
            }
           
        }

        #endregion


        #region "Initialisation des données de base"
        private void CreateDefaultSpeedMessages()
        {
            SpeedMessages.Add(new SpeedMessage { Index = 1, Title = "Chef tu peux venir", Destinataire = "MédecinRDC", Message = "tu peux venir STP dans la salle [ROOM]", Options = "0|2|A", Load = true });
            SpeedMessages.Add(new SpeedMessage { Index = 2, Title = "Alix tu peux venir", Destinataire = "Alix", Message = "tu peux venir STP dans la salle [ROOM]", Options = "0|2|A", Load = true });
            SpeedMessages.Add(new SpeedMessage { Index = 2, Title = "Benoit tu peux venir", Destinataire = "Benoit", Message = "tu peux venir STP dans la salle [ROOM]", Options = "0|2|A", Load = true });
            SpeedMessages.Add(new SpeedMessage { Index = 2, Title = "Esra tu peux venir", Destinataire = "Esra", Message = "tu peux venir STP dans la salle [ROOM]", Options = "0|2|A", Load = true });
            SpeedMessages.Add(new SpeedMessage { Index = 2, Title = "Caroline tu peux venir", Destinataire = "Caroline", Message = "tu peux venir STP dans la salle [ROOM]", Options = "0|2|A", Load = true });
            SpeedMessage.SaveSpeedMessagesToJson(SpeedMessages);
        }

        private void CreateDefaultWaitingRooms()
        {
            WaitingRooms.Add(new WaitingRoom { WaitingRoomName = "RDC", WaitingRoomDescription = "Patients au RDC", WaitingRoomTilte = "RDC" });
            WaitingRooms.Add(new WaitingRoom { WaitingRoomName = "1er", WaitingRoomDescription = "Patients au 1er", WaitingRoomTilte = "1er" });
            WaitingRooms.Add(new WaitingRoom { WaitingRoomName = "2ème", WaitingRoomDescription = "Patients au 2ème", WaitingRoomTilte = "2ème" });
            WaitingRooms.Add(new WaitingRoom { WaitingRoomName = "Tous", WaitingRoomDescription = "Tous les patients", WaitingRoomTilte = "Tous" });
            WaitingRoom.SaveWaitingRoomsToJson(WaitingRooms);
           
        }
        private void CreateDefaultPlannings()
        {
            Plannings.Add(new Planning { day = "Lundi", user = "Alix" });
            Plannings.Add(new Planning { day = "Mardi", user = "Benoit" });
            Plannings.Add(new Planning { day = "Mercredi", user = "Esra" });
            Plannings.Add(new Planning { day = "jeudi", user = "Caroline" });
            Plannings.Add(new Planning { day = "Vendredi", user = "Caroline" });
            Planning.SavePlanningToJson(Plannings);
        }

        private void CreateDefaultExamRooms()
        {
            ExamRooms.Add(new ExamRoom { ExamRoomName = "Ortho 1", ExamRoomDescription = "Orthoptiste 1"});
            ExamRooms.Add(new ExamRoom { ExamRoomName = "Ortho 2", ExamRoomDescription = "Orthoptiste 2" });
            ExamRooms.Add(new ExamRoom { ExamRoomName = "Ortho 3", ExamRoomDescription = "Orthoptiste 3" });
            ExamRooms.Add(new ExamRoom { ExamRoomName = "Ortho RDC", ExamRoomDescription = "Orthoptiste RDC" });
            ExamRooms.Add(new ExamRoom { ExamRoomName = "OCT 1", ExamRoomDescription = "OCT 1" });
            ExamRooms.Add(new ExamRoom { ExamRoomName = "OCT RDC", ExamRoomDescription = "OCT RDC" });
            ExamRoom.SaveExamRoomsToJson(ExamRooms);
        }

        private void CreateDefaultExamList()
        {
            ExamsList.Add(new ExamList { ExamID = 1, ExamName = "FO", ExamColor = Colors.Red, ExamAnnotation = null, ExamCodeMSGCreate = "FO=", ExamCodeMSGPass="FO&" });
            ExamsList.Add(new ExamList { ExamID = 2, ExamName = "AT-FO", ExamColor = Colors.IndianRed, ExamAnnotation = null, ExamCodeMSGCreate = "AT-FO=", ExamCodeMSGPass = "AT-FO&" });
            
            ExamsList.Add(new ExamList { ExamID = 3, ExamName = "SK", ExamColor = Colors.Yellow, ExamAnnotation = null, ExamCodeMSGCreate = "SK=", ExamCodeMSGPass = "SK&" });
            ExamsList.Add(new ExamList { ExamID = 4, ExamName = "AT-SK", ExamColor = Colors.LightYellow, ExamAnnotation = null, ExamCodeMSGCreate = "AT-SK=", ExamCodeMSGPass = "AT-SK&" });
            
            ExamsList.Add(new ExamList { ExamID = 5, ExamName = "CV", ExamColor = Colors.Blue, ExamAnnotation = null, ExamCodeMSGCreate = "CV=", ExamCodeMSGPass = "CV&" });
            ExamsList.Add(new ExamList { ExamID = 6, ExamName = "AT-CV", ExamColor = Colors.LightBlue, ExamAnnotation = null, ExamCodeMSGCreate = "AT-CV=", ExamCodeMSGPass = "AT-CV&" });

            ExamsList.Add(new ExamList { ExamID = 7, ExamName = "BO", ExamColor = Colors.Green, ExamAnnotation = null, ExamCodeMSGCreate = "BO=", ExamCodeMSGPass = "BO&" });
            ExamsList.Add(new ExamList { ExamID = 8, ExamName = "AT-BO", ExamColor = Colors.LightGreen, ExamAnnotation = null, ExamCodeMSGCreate = "AT-BO=", ExamCodeMSGPass = "AT-BO&" });

            ExamsList.Add(new ExamList { ExamID = 9, ExamName = "V3F", ExamColor = Colors.Purple, ExamAnnotation = null, ExamCodeMSGCreate = "V3F=", ExamCodeMSGPass = "V3F&" });

            ExamsList.Add(new ExamList { ExamID = 10, ExamName = "PH", ExamColor = Colors.Pink, ExamAnnotation = null, ExamCodeMSGCreate = "PH=", ExamCodeMSGPass = "PH&" });
            ExamsList.Add(new ExamList { ExamID = 11, ExamName = "AT-PH", ExamColor = Colors.LightPink, ExamAnnotation = null, ExamCodeMSGCreate = "AT-PH=", ExamCodeMSGPass = "AT-PH&" });

            ExamsList.Add(new ExamList { ExamID = 12, ExamName = "AT", ExamColor = Colors.Orange, ExamAnnotation = null, ExamCodeMSGCreate = "AT=", ExamCodeMSGPass = "AT&" });


            ExamList.SaveExamListToJson(ExamsList);

        }

        #endregion

    }
}
