using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;


namespace MomoFrame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Instance = this;

            //ResourceDictionary resourceDictionary = new ResourceDictionary();
            //resourceDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDefault.xaml") });
            //resourceDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDark.xaml") });
            //resourceDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml") });
            //Application.Current.Resources = resourceDictionary;
        }
    }

    

}
