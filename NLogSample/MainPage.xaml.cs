using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLogSample.Helpers;
using Xamarin.Forms;

namespace NLogSample
{
    public partial class MainPage : ContentPage
    {
        private readonly NLog.ILogger Logger = NLog.LogManager.GetCurrentClassLogger();

        public MainPage()
        {
            InitializeComponent();
            Logger.Info("MainPage Initialize");
        }

        string logPath = string.Empty;
        private bool IsStarted { get; set; } = false;
        private S3Service s3Service = new S3Service();

        void StartButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (IsStarted)
            {
                Logger.Debug("is started");
            }
            else
            {
                IsStarted = true;
                Logger.Debug("Start Timer");
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    if (IsStarted)
                    {
                        Logger.Warn("Run Timer");
                        return true;
                    }
                    else
                    {
                        Logger.Debug("End Timer");
                        return false;
                    }
                });
            }
        }

        void StopButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (IsStarted)
            {
                IsStarted = false;
            }
            else
            {
                Logger.Debug("Is Not Started");
            }
        }

        void CreateButton_Clicked(System.Object sender, System.EventArgs e)
        {
            logPath = FileService.CreateZipFile();
            Logger.Debug($"CreateZipFile {logPath}");
        }

        void UploadButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(logPath))
            {
                Logger.Debug("logPath is null");
            }
            else
            {
                Logger.Debug("start upload");
                S3Service.UploadFileAsync(logPath);
            }
        }
    }
}
