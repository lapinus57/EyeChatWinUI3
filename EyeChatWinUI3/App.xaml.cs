using EyeChatWinUI3.Class;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EyeChatWinUI3
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 

        

        public App()
        {
            this.InitializeComponent();
            
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>

       

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {

            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("TextScale", out var savedScaleObj))
            {
                if (savedScaleObj is double savedScale)
                {
                    Application.Current.Resources["GlobalTextScale"] = savedScale;
                    Application.Current.Resources["GlobalFontSizeH"] = 16 * savedScale;
                    Application.Current.Resources["GlobalFontSizeH1"] = 14 * savedScale;
                    Application.Current.Resources["GlobalFontSizeH2"] = 12 * savedScale;
                    Application.Current.Resources["GlobalFontSizeH3"] = 10 * savedScale;
                }
            }


            try
            {
                m_window = new MainWindow();
                
                hwnd = WinRT.Interop.WindowNative.GetWindowHandle(m_window);
                WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
                AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
                appWindow.Title = "EyeChat";
                var titleBar = appWindow.TitleBar;
                //titleBar.ButtonForegroundColor = Colors.Red;
                var themeName = ApplicationData.Current.LocalSettings.Values["SelectedTheme"] as string;
                if (!string.IsNullOrEmpty(themeName))
                {
                    ElementTheme theme = themeName == "Clair" ? ElementTheme.Light : ElementTheme.Dark;
                    if (m_window.Content is FrameworkElement rootElement)
                    {
                        rootElement.RequestedTheme = theme;
                        if (theme == ElementTheme.Light)
                        {
                            
                            titleBar.ButtonForegroundColor = Colors.Black;
                        }
                        else
                        {
                            titleBar.ButtonForegroundColor = Colors.White;
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                // Gérer l'exception ou journaliser pour le débogage
                Debug.WriteLine("Exception caught: " + ex.Message);
            }
            m_window.Activate();
        }

        public static Window MainWindow
        {
            get { return ((App)Application.Current).m_window; }
        }

        private Window m_window;

        public IntPtr hwnd { get; private set; }
    }
}
