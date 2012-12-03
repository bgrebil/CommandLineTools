using System;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace DeleteFiles
{
   public class DeleteFilesProcessor
   {
      public bool ProcessFiles(DeleteFilesOptions options)
      {
         if (!Directory.Exists(options.Path)) {
            ShowMessage("Path does not exist: " + options.Path);
         }

         return ProcessFolder(options.Path, options);
      }

      protected bool ProcessFolder(string activeFolder, DeleteFilesOptions options)
      {
         bool success = true;
         foreach (var file in Directory.EnumerateFiles(activeFolder, options.FileSpec)) {
            try {
               if (IsFileToBeDeleted(file, options)) {
                  if (options.UseRecycleBin) {
                     FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                  }
                  else {
                     File.Delete(file);
                  }

                  ShowMessage("Deleting " + file);
               }
            }
            catch (Exception) {
               ShowMessage("Failed to delete file: " + file);
               success = false;
            }
         }

         if (options.Recursive) {
            foreach (var dir in Directory.EnumerateDirectories(activeFolder)) {
               success = ProcessFolder(dir, options);
               if (success && options.RemoveEmptyDirectories) {
                  if (!Directory.EnumerateFiles(dir).Any() && !Directory.EnumerateDirectories(dir).Any()) {
                     try {
                        if (options.UseRecycleBin) {
                           FileSystem.DeleteDirectory(dir, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                        }
                        else {
                           Directory.Delete(dir);
                        }
                     }
                     catch (Exception) {
                        ShowMessage("Failed to delete directory: " + dir);
                     }
                  }
               }
            }
         }

         return success;
      }

      private bool IsFileToBeDeleted(string file, DeleteFilesOptions options)
      {
         if (options.Seconds > -1) {
            var ftime = File.GetLastWriteTimeUtc(file);
            return DateTime.UtcNow > ftime.AddSeconds(options.Seconds);
         }

         if (options.Days > -1) {
            var ftime = File.GetLastWriteTime(file);
            return DateTime.Now.Date >= ftime.Date.AddDays(options.Days);
         }

         return true;
      }

      public event Action<string> ShowMessage = msg => Console.WriteLine(msg);
   }
}
