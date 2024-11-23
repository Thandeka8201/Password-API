using Password_API_Assessment.Interfaces;
using System;
using System.IO;
using System.IO.Compression;

namespace Password_API_Assessment.Data
{
    public class FileCompressor : IFileCompressor
    {
        public void CreateZipFile(string zipFileName, string[] filesToInclude)
        {
            Console.WriteLine("Creating ZIP file...");

            using (FileStream zipToCreate = new FileStream(zipFileName, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
            {
                foreach (var file in filesToInclude)
                {
                    if (File.Exists(file))
                    {
                        try
                        {
                            AddFileToZip(archive, file);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to add {file} to ZIP: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File not found: {file}");
                    }
                }
            }

            Console.WriteLine($"ZIP file created: {zipFileName}");
        }

        private void AddFileToZip(ZipArchive archive, string filePath)
        {
            // Create a new entry for the file
            string entryName = Path.GetFileName(filePath); // File name in the ZIP
            var zipEntry = archive.CreateEntry(entryName, CompressionLevel.Fastest);

            // Write the file's content to the ZIP entry
            using (var entryStream = zipEntry.Open())
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fileStream.CopyTo(entryStream);
            }
        }
    }
}


