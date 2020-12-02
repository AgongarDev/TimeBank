using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeBank.Core.Models;

namespace TimeBank.Bussines.Utilities
{
    public class CommonLib
    {
        public static bool ValidateNumEntrance(string insertData)
        {
            try
            {
                int dataInt = int.Parse(insertData);
                return false;
            }
            catch (Exception)
            {
                Console.WriteLine("IncorrectValue");
                return false;
            }
        }

        public static int TokenListToHours(List<Token> credit)
        {
            int hours = 0;

            foreach (Token t in credit)
            {
                hours += t.Hours;
            }
            return hours;
        }

        public static void ToXml<T> (T serialObj, string folderN, string fileN)
        {
            Serializer ser = new Serializer();
            
            string path = Directory.GetCurrentDirectory();
            using StreamWriter outputFile = new StreamWriter(Path.Combine(path, $@"..\..\..\{folderN}\{fileN}"));
            string xmlOutputData = ser.Serialize<T>(serialObj);
            outputFile.WriteLine(xmlOutputData);
        }

        public static T FromXml<T> (string folderN, string fileN) where T : class
        {
            Serializer ser = new Serializer();

            string path = Directory.GetCurrentDirectory() + $@"\..\..\..\{folderN}\{fileN}";
            string xmlInputData = File.ReadAllText(path);

            return (T)ser.Deserialize<T> (xmlInputData);
        }
    }
}
