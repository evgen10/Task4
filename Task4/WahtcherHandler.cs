using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
namespace Task4
{
    public static class WahtcherHandler
    {
        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.Name);
        }

      



        public static void MoveFile(object source, FileSystemEventArgs e)
        {
            string extention = Path.GetExtension(e.FullPath);                             
                     
            
            switch (extention)
            {
                case ".txt":
                    {

                        string destinationFolderPath = @"C:\Users\iammr\Desktop\Task4\Task4\bin\Debug\Папка\TEXT\"+e.Name;

                        if (File.Exists(destinationFolderPath))
                        {
                            File.Delete(destinationFolderPath);
                        }
               

                        File.Move(e.FullPath, destinationFolderPath);
                        File.Delete(e.FullPath);                   
                        

                        break;
                    }


                default:
                    break;
            }


        }
    }
}
