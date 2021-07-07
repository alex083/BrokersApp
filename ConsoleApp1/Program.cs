using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        public static string Source = @"D:\\Source";
        public static string Destination = @"D:\\Destination";
        public static List<string> Brokers = new List<string>();
        public static List<string> DateCreation = new List<string>();
        public static List<string> ListOfTckFiles = new List<string>();//Список который хранит расположения файлов
        public static void CreateBrokesDirs(string path, List<string> subpath) //Функция для создания папок прокеров в коневой папке Destination
        {
            for (int i = 0; i < Brokers.Count; i++)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                dirInfo.CreateSubdirectory(subpath[i]+@"\"+DateCreation[i]);
            }
        }
        public static void GetBrokersName(string dirName) //Функция для получения имен брокеро из названий подпакок
        {
            List<string> tmpBrokers = new List<string>();
            //Console.WriteLine("Подкаталоги:");
            string[] dirs = Directory.GetDirectories(dirName);
            foreach (string s in dirs)
            {
                tmpBrokers.Add(s);
            }
            Regex regex = new Regex(@"(?<=Source\\)(.*?)($)");
            for (int i = 0; i < tmpBrokers.Count; i++)
            {
                MatchCollection matches = regex.Matches(tmpBrokers[i]);
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                        Brokers.Add(match.Value);
                        //Console.WriteLine(match.Value);
                }
                else
                {
                    Console.WriteLine("Совпадений не найдено");
                }
            }
        }
        public static void GetTckFiles(string PathToFolder) //Функция для получения всех файлов и сохранении их в ListOfTckFiles
        {
            string[] AllFiles = Directory.GetFiles(PathToFolder, "*.tck", SearchOption.AllDirectories);
            foreach (string filename in AllFiles)
            {
                ListOfTckFiles.Add(filename);
                
            }
        }
        public static void GetCretionDate(List <string> path)//Функция для сбора дат создания файлов чтобы отсортировать
        {
            List<DateTime> tmpDates = new List<DateTime>();
            for (int i = 0; i < path.Count; i++)
            {
                FileInfo fileInf = new FileInfo(path[1]);
                if (fileInf.Exists)
                {
                    //Console.WriteLine(fileInf.CreationTime);
                    tmpDates.Add(fileInf.CreationTime);
                    DateCreation.Add(""+ tmpDates[i].Year+@"\"+tmpDates[i].Month);
                   // Console.WriteLine(DateCreation[i]);
                }
            }
        }
        
        static void Main(string[] args)
        {
            GetBrokersName(Source);
             GetTckFiles(Source);
            GetCretionDate(ListOfTckFiles);
            CreateBrokesDirs(Destination,Brokers);
           Console.WriteLine(ListOfTckFiles.Count);
            ConverTck.RewriteTck(ListOfTckFiles);
        }

    }
}

