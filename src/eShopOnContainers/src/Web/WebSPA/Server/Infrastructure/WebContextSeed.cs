﻿namespace WebSPA.Infrastructure
{
    using ILogger = Microsoft.Extensions.Logging.ILogger;

    public class WebContextSeed
    {
        public static void Seed(IApplicationBuilder applicationBuilder, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var log = loggerFactory.CreateLogger<WebContextSeed>();

            var settings = applicationBuilder
                .ApplicationServices.GetRequiredService<IOptions<AppSettings>>().Value;

            bool useCustomizationData = settings.UseCustomizationData;
            string contentRootPath = env.ContentRootPath;
            string webroot = env.WebRootPath;

            if (useCustomizationData)
            {
                GetPreconfiguredImages(contentRootPath, webroot, log);
            }
        }

        private static void GetPreconfiguredImages(string contentRootPath, string webroot, ILogger log)
        {
            try
            {
                string imagesZipFile = Path.Combine(contentRootPath, "Setup", "images.zip");
                if (!File.Exists(imagesZipFile))
                {
                    log.LogError("Zip file '{ZipFileName}' does not exists.", imagesZipFile);
                    return;
                }

                string imagePath = Path.Combine(webroot, "assets", "images");
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }
                string[] imageFiles = Directory.GetFiles(imagePath).Select(file => Path.GetFileName(file)).ToArray();

                using var zip = ZipFile.Open(imagesZipFile, ZipArchiveMode.Read);
                foreach (var entry in zip.Entries)
                {
                    if (!imageFiles.Contains(entry.Name))
                    {
                        string destinationFilename = Path.Combine(imagePath, entry.Name);
                        if (File.Exists(destinationFilename))
                        {
                            File.Delete(destinationFilename);
                        }
                        entry.ExtractToFile(destinationFilename);
                    }
                    else
                    {
                        log.LogWarning("Skipped file '{FileName}' in zipfile '{ZipFileName}'", entry.Name, imagesZipFile);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, "ERROR in GetPreconfiguredImages: {Message}", ex.Message);
            }
        }
    }
}
