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
            while (true)
            {
                Console.WriteLine("Enter the folder path");
                string FolderPath = Console.ReadLine();
                FolderPath = FolderPath.Replace("'", "");
                int ActiveSlashPosition = FolderPath.LastIndexOf("/");
                string FolderName = FolderPath.Substring(ActiveSlashPosition + 1) + "X";
                if (Directory.Exists(FolderPath))
                {
                    DisplayFiles(FolderPath);
                    Console.WriteLine("Enter what the current file name");
                    string CurrentFileName = Console.ReadLine();
                    Console.WriteLine("Enter the file extension");
                    string FileExtension = Console.ReadLine();
                    Console.WriteLine("Enter the number it starts on");
                    int OffSet = int.Parse(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("1. Ascending item to folder name");
                    Console.WriteLine("2. Descending item to folder name");
                    Console.WriteLine("3. Directional item");
                    int Input = int.Parse(Console.ReadLine());
                    if (Input == 1 || Input == 2)
                    {
                        Console.WriteLine("Enter the new name you want without the count");
                        string NewFileName = Console.ReadLine();
                        switch (Input)
                        {
                            case 1:
                                ChangeFileNames(FolderPath, CurrentFileName, NewFileName, FileExtension,OffSet, false);
                                break;
                            case 2:
                                ChangeFileNames(FolderPath, CurrentFileName, NewFileName, FileExtension,OffSet, true);
                                break;
                        }
                    }
                }
                Console.ReadLine();
                Thread.Sleep(100);
                Console.Clear();
            }
        }
        static void DisplayFiles(string FolderPath)
        {
            Thread.Sleep(200);
            Console.Clear();
            if (Directory.Exists(FolderPath))
            {
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("Files Found:");
                string[] FilesFound = Directory.GetFiles(FolderPath);
                foreach (string ActiveFile in FilesFound)
                {
                    Thread.Sleep(100);
                    Console.WriteLine(Path.GetFileName(ActiveFile));
                }
            }
            else
            {
                Console.WriteLine("No Files Found");
            }
        }
        static void ChangeFileNames(string FolderPath, string OldName, string NewName, string FileExtension, int OffSet, bool Reverse)
        {
            Console.Clear();
            string OldFilePath = Path.Combine(FolderPath, OldName);
            string NewFilePath = Path.Combine(FolderPath, NewName + "X");
            int CountDifference = 1;
            if (Reverse == true)
            {
                CountDifference = -1;
            }
            int TempNumber = OffSet;
            for (int Count = 0; File.Exists(OldFilePath + (TempNumber + CountDifference).ToString() + FileExtension); Count++)
            {
                Thread.Sleep(200);
                TempNumber = OffSet + Count;
                if (Reverse == true)
                {
                    TempNumber = OffSet - Count;
                }
                string OldFullFilePath = OldFilePath + TempNumber.ToString() + FileExtension;
                string NewFullFilePath = NewFilePath + (Count + 1).ToString() + FileExtension;
                Console.WriteLine($"{Path.GetFileName(OldFullFilePath)} -> {Path.GetFileName(NewFullFilePath)}");
                File.Move(OldFullFilePath, NewFullFilePath);
            }
            Console.WriteLine(TempNumber);
            Console.WriteLine("Finished");
        }
    }
} 
