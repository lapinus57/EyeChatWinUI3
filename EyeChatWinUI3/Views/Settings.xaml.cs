using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ColorCode.Styling;
using EyeChatWinUI3.Class;
using EyeChatWinUI3.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Windows.Storage;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EyeChatWinUI3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class Settings : Page
    {
        private bool isInitializing = true;
        public SettingsViewModel ViewModelSetting { get; set; }

        public Settings()
        {
            this.InitializeComponent();
            Load_Variables();
            ViewModelSetting = new SettingsViewModel();
            this.DataContext = ViewModelSetting;

            // Votre code existant pour initialiser et charger les données
            ViewModelSetting.WaitingRooms = WaitingRoom.LoadWaitingRoomsFromJson();
            ViewModelSetting.SpeedMessages = SpeedMessage.LoadSpeedMessagesFromJson();
            ViewModelSetting.ExamRooms = ExamRoom.LoadExamRoomsFromJson();
            ViewModelSetting.ExamLists = ExamList.LoadExamListFromJson();
        }

        #region "Gestion des WaitingRooms"

        /// <summary>
        /// Gère le clic sur le bouton d'ajout de salle d'attente.
        /// Ajoute une nouvelle salle d'attente vide à la collection de salles d'attente.
        /// </summary>
        private void AddWaitingRoomsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModelSetting.WaitingRooms.Add(new WaitingRoom());
        }

        /// <summary>
        /// Gère le clic sur le bouton de suppression de salle d'attente.
        /// Supprime la salle d'attente sélectionnée de la collection.
        /// </summary>
        private void DeleteWaitingRoomsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.DataContext is WaitingRoom room)
            {
                ViewModelSetting.WaitingRooms.Remove(room);
            }
        }

        /// <summary>
        /// Gère le clic sur le bouton de sauvegarde des salles d'attente.
        /// Enregistre les modifications apportées aux salles d'attente dans le fichier JSON.
        /// </summary>
        private void SaveWaitingRoomsButton_Click(object sender, RoutedEventArgs e)
        {
            WaitingRoom.SaveWaitingRoomsToJson(ViewModelSetting.WaitingRooms);
        }

        #endregion

        #region "Gestion des ExamRooms"

        /// <summary>
        /// Gère le clic sur le bouton d'ajout de salle d'examen.
        /// Ajoute une nouvelle salle d'examen vide à la collection de salles d'examen.
        /// </summary>
        private void AddExamRoomsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModelSetting.ExamRooms.Add(new ExamRoom());
        }

        /// <summary>
        /// Gère le clic sur le bouton de suppression de salle d'examen.
        /// Supprime la salle d'examen sélectionnée de la collection.
        /// </summary>
        private void DeleteExamRoomsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.DataContext is ExamRoom room)
            {
                ViewModelSetting.ExamRooms.Remove(room);
            }
        }

        /// <summary>
        /// Gère le clic sur le bouton de sauvegarde des salles d'examen.
        /// Enregistre les modifications apportées aux salles d'examen dans le fichier JSON.
        /// </summary>
        private void SaveExamRoomsButton_Click(object sender, RoutedEventArgs e)
        {
            ExamRoom.SaveExamRoomsToJson(ViewModelSetting.ExamRooms);
        }

        #endregion


        #region "Gestion des SpeedMessages"

        /// <summary>
        /// Gère le clic sur le bouton d'ajout d'un SpeedMessage.
        /// Ajoute un nouveau SpeedMessage vide à la collection avec un index unique.
        /// </summary>
        private void AddSpeedMessageButton_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = ViewModelSetting.SpeedMessages.Any() ? ViewModelSetting.SpeedMessages.Max(msg => msg.Index) + 1 : 1;
            ViewModelSetting.SpeedMessages.Add(new SpeedMessage { Index = newIndex });
        }

        /// <summary>
        /// Gère le clic sur le bouton de sauvegarde des SpeedMessages.
        /// Enregistre les modifications apportées aux SpeedMessages dans le fichier JSON.
        /// </summary>
        private void SaveSpeedMessagesButton_Click(object sender, RoutedEventArgs e)
        {
            SpeedMessage.SaveSpeedMessagesToJson(ViewModelSetting.SpeedMessages);
        }

        /// <summary>
        /// Gère le clic sur le bouton de suppression d'un SpeedMessage.
        /// Supprime le SpeedMessage sélectionné de la collection et recalculer les index.
        /// </summary>
        private void DeleteSpeedMessage_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.DataContext is SpeedMessage message)
            {
                ViewModelSetting.SpeedMessages.Remove(message);
                RecalculateSpeedMessageIndices();
            }
        }

        /// <summary>
        /// Recalcule les index pour chaque SpeedMessage dans la collection.
        /// Assure que chaque SpeedMessage a un index unique et séquentiel.
        /// </summary>
        private void RecalculateSpeedMessageIndices()
        {
            int index = 1;
            foreach (var message in ViewModelSetting.SpeedMessages)
            {
                message.Index = index++;
            }
        }

        #endregion

        #region "Gestion des ExamLists"

        private void AddExamListButton_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = ViewModelSetting.ExamLists.Any() ? ViewModelSetting.ExamLists.Max(exam => exam.ExamID) + 1 : 1;
            ViewModelSetting.ExamLists.Add(new ExamList{ ExamID= newIndex});
        }   

        private void DeleteExamListButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.DataContext is ExamList list)
            {
                ViewModelSetting.ExamLists.Remove(list);
                RecalculateExamListIndices();
            }
        }
        private void RecalculateExamListIndices()
        {
            int index = 1;
            foreach (var list in ViewModelSetting.ExamLists)
            {
                list.ExamID = index++;
            }
        }
        private void SaveExamListButton_Click(object sender, RoutedEventArgs e)
        {
            ExamList.SaveExamListToJson(ViewModelSetting.ExamLists);
        }

        private void ComboBox_DropDownOpened(object sender, object e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.DataContext is ExamList examList)
            {
                var colorPicker = new ColorPicker
                {
                    Color = examList.ExamColor // Pré-sélectionner la couleur actuelle
                };

                colorPicker.ColorChanged += (s, args) =>
                {
                    // Mettre à jour la couleur sélectionnée
                    examList.ExamColor = colorPicker.Color;
                };

                var flyout = new Flyout
                {
                    Content = colorPicker
                };
                flyout.ShowAt(comboBox);
            }
        }
        #endregion



        private void Load_Variables()
        {
            
            // Récupérer la valeur de "TextScale" dans les paramètres de l'application
            object savedScaleObj = ApplicationData.Current.LocalSettings.Values["TextScale"];
            if (savedScaleObj is double savedScale)
            {
                Slidersize.Value = savedScale;
            }
            else
            {
                Slidersize.Value = 1.0; // Valeur par défaut si "TextScale" n'existe pas ou n'est pas un double
            }

            // Récupérer la valeur de "SelectedTheme" dans les paramètres de l'application
            var savedTheme = ApplicationData.Current.LocalSettings.Values["SelectedTheme"] as string;
            if (!string.IsNullOrEmpty(savedTheme))
            {
                foreach (ComboBoxItem item in ComboBox_Theme.Items.Cast<ComboBoxItem>())
                {
                    if ((string)item.Content == savedTheme)
                    {
                        // Sélectionner l'élément qui correspond au thème enregistré
                        ComboBox_Theme.SelectedItem = item;
                        break;
                    }
                }
            }

            isInitializing = false;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!isInitializing)
            {
                var comboBox = sender as ComboBox;
                var selectedTheme = comboBox?.SelectedItem as ComboBoxItem;



                if (selectedTheme != null)
                {
                    var themeName = selectedTheme.Content.ToString();
                    // Enregistrer le thème
                    ApplicationData.Current.LocalSettings.Values["SelectedTheme"] = themeName;

                    // Appliquer le thème
                    ElementTheme theme = themeName == "Clair" ? ElementTheme.Light : ElementTheme.Dark;

                    if (App.MainWindow.Content is FrameworkElement rootElement)
                    {
                        rootElement.RequestedTheme = theme;
                        if (theme == ElementTheme.Light)
                        {
                            MainWindow.Instance.ChangeTitleBarButtonColor(Microsoft.UI.Colors.Black);
                        }
                        else
                        {
                            MainWindow.Instance.ChangeTitleBarButtonColor(Microsoft.UI.Colors.White);
                        }

                    }
                }

            }
        }

        private void SliderSize_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (!isInitializing)
            {
                
                double scale = e.NewValue;
                ApplicationData.Current.LocalSettings.Values["TextScale"] = scale;
                Application.Current.Resources["GlobalFontSizeH"] = 16 * scale;
                Application.Current.Resources["GlobalFontSizeH1"] = 14 * scale;
                Application.Current.Resources["GlobalFontSizeH2"] = 12 * scale;
                Application.Current.Resources["GlobalFontSizeH3"] = 10 * scale;

            }
        }
        public static IEnumerable<T> FindChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        
    }
}
