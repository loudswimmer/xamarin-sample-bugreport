using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NLogSample
{
    public partial class App : Application
    {
        private readonly NLog.ILogger Logger = NLog.LogManager.GetCurrentClassLogger();

        public App()
        {
            InitializeComponent();

            Logger.Info("App Initialize");
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            Logger.Info("OnStart");
        }

        protected override void OnSleep()
        {
            Logger.Info("OnSleep");
        }

        protected override void OnResume()
        {
            Logger.Info("OnResume");
        }
    }
}
