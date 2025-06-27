using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace File_Name_Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the folder path");
            string FolderPath = Console.ReadLine();
            if (Directory.Exists(FolderPath))
            {
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("Files Found:");
                string[] FilesFound = Directory.GetFiles(FolderPath);
                foreach (string ActiveFile in FilesFound)
                {
                    Console.WriteLine(Path.GetFileName(ActiveFile));
                }
                Console.WriteLine("Enter what the current file name is without the count");
                string CurrentFileName = Console.ReadLine();
                Console.WriteLine("Enter the new name you want without the count");
                string NewFileName = Console.ReadLine();
                Console.WriteLine("Enter the file extension");
                string FileExtension = Console.ReadLine();
                string OldFilePath = Path.Combine(FolderPath, CurrentFileName);
                string NewFilePath = Path.Combine(FolderPath, NewFileName);
                Console.WriteLine(File.Exists(OldFilePath + '0' + FileExtension));
                Thread.Sleep(200);
                Console.Clear();
                for (int Count = 0; File.Exists(OldFilePath + Count.ToString() + FileExtension); Count++)
                {
                    string OldFullFilePath = OldFilePath + Count.ToString() + FileExtension;
                    string NewFullFilePath = NewFilePath + (Count).ToString() + FileExtension;
                    Console.WriteLine($"{Path.GetFileName(OldFullFilePath)} -> {Path.GetFileName(NewFullFilePath)}");
                    Thread.Sleep(300);

                    File.Move(OldFullFilePath, NewFullFilePath);
                }
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Done");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Invalid Directory");
            }
            Console.ReadLine();;
        }
    }
}
