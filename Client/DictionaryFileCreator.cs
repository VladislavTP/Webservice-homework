using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;

namespace Client
{
    
    internal class DictionaryFileCreator
    {
        private const string path = @"C:\Users\User\Desktop\Book.txt";
        private const string uniqWordsPath = @"C:\Users\User\Desktop\UniqueWords.txt";

        static void Main(string[] args)
        {
            string bookText = "";
            using (StreamReader sr = new StreamReader(path))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    line = sr.ReadLine();
                    bookText += "\n";
                    bookText += line;
                }
            }

            Console.WriteLine("Client starts");
            string uri = "http://localhost:8080/Service1";
            BasicHttpBinding binding = new BasicHttpBinding("MyBind");
            var channel = new ChannelFactory<IService1>(binding);
            var endPoint = new EndpointAddress(uri);
            
            var proxy = channel.CreateChannel(endPoint);
            var finaldic = proxy?.GetDic(bookText);

            WriteWordsInFile(finaldic);
            Console.ReadLine();

        }

        public static void CreateWordCountFile()
        {
            try
            {
                if (!File.Exists(uniqWordsPath))
                {
                    var uniqWordsFile = File.Create(uniqWordsPath);
                    uniqWordsFile.Close();
                }
                else
                {
                    File.Delete(uniqWordsPath);
                    var uniqWordsFile = File.Create(uniqWordsPath);
                    uniqWordsFile.Close();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private static void WriteWordsInFile(Dictionary<string, int> uniqueWordsDic)
        {
            using (var sw = new StreamWriter(uniqWordsPath, false, Encoding.UTF8))
            {

                var maxWidthKeyColumn = uniqueWordsDic.Max(s => s.Key.Length);
                var maxWidthValueColumn = uniqueWordsDic.Max(s => s.Value.ToString().Length);
                var formatKeyColumn = string.Format("{{0, -{0}}}|", maxWidthKeyColumn);
                var formatValueColumn = string.Format("{{0, -{0}}}|", maxWidthValueColumn);

                sw.Write(formatKeyColumn, "Words");
                sw.Write("|");
                sw.Write(formatValueColumn, "Count");
                sw.WriteLine();
                sw.WriteLine("----------------------------------------------------");

                foreach (var keyValuePair in uniqueWordsDic)
                {
                    sw.Write(formatKeyColumn, keyValuePair.Key);
                    sw.Write("|");
                    sw.Write(formatValueColumn, keyValuePair.Value.ToString());
                    sw.WriteLine();
                }
            }
            Console.WriteLine("Recording completed");
        }



    }
}
