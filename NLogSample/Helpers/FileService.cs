using System;
using Xamarin.Forms;

namespace NLogSample.Helpers
{
    public class FileService
    {
        private static bool QuickZip(string directoryToZip, string destinationZipFullPath)
        {
            try
            {
                // Delete existing zip file if exists
                if (System.IO.File.Exists(destinationZipFullPath))
                    System.IO.File.Delete(destinationZipFullPath);

                if (!System.IO.Directory.Exists(directoryToZip))
                    return false;
                else
                {
                    System.IO.Compression.ZipFile.CreateFromDirectory(directoryToZip, destinationZipFullPath, System.IO.Compression.CompressionLevel.Optimal, true);
                    return System.IO.File.Exists(destinationZipFullPath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                return false;
            }
        }

        public static string CreateZipFile()
        {
            string zipFilename = string.Empty;
            if (NLog.LogManager.IsLoggingEnabled())
            {
                string folder;
                if (Device.RuntimePlatform == Device.iOS)
                    folder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "..", "Library");
                else if (Device.RuntimePlatform == Device.Android)
                    folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                else
                    throw new Exception("Could not show log: Platform undefined.");
                //Delete old zipfiles (housekeeping)
                try
                {
                    foreach (string fileName in System.IO.Directory.GetFiles(folder, "*.zip"))
                    {
                        System.IO.File.Delete(fileName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting old zip files: {ex.Message}");
                }
                string logFolder = System.IO.Path.Combine(folder, "logs");

                if (System.IO.Directory.Exists(logFolder))
                {
                    zipFilename = $"{folder}/{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.zip";
                    int filesCount = System.IO.Directory.GetFiles(logFolder, "*.csv").Length;
                    if (filesCount > 0)
                    {
                        if (!QuickZip(logFolder, zipFilename))
                            zipFilename = string.Empty;
                    }
                    else
                        zipFilename = string.Empty;
                }
                else
                    zipFilename = string.Empty;
            }
            else
                zipFilename = string.Empty;


            return zipFilename;
        }
    }
}
