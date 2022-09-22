using MomoFrame.Windows.UserControlView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TouchSocket.Core.Config;
using TouchSocket.Sockets;

namespace MomoFrame.Windows.WindowView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;

        public bool IsGameEntryShutdown = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Awake();
        }

        public void Awake()
        {
            Instance = this;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            GameRoot.GameRootStart();

            //Test.test01();

            Init();
        }


        public void Init()
        {
            Task.Run(() =>
            {
                
            });
      
        }


        /// <summary>
        /// 窗体发生关闭前执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
      
        }

        /// <summary>
        /// 窗体发生关闭后执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (IsGameEntryShutdown)
            {
                UnityGameFramework.Runtime.GameEntry.Shutdown(UnityGameFramework.Runtime.ShutdownType.Quit);
            }
        }

      
    }
}
