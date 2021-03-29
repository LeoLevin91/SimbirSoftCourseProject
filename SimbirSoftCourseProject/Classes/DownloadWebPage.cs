using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimbirSoftCourseProject.Classes
{
    /*
     * Данный класс производит загрузку WEB-страницы по указанному URL адресу
     * с помощью HTTP запроса.
     * Так же данный класс сохраняет скаченную страницу в текстовый документ
     */
    public class DownloadWebPage
    {
        public string urlAddress { get;}
        public string fileName { get; set; }
        

        private HttpClient httpClient= new HttpClient();
        
        /*
         * Конструктор (string URL, string fileName)
         */
        public DownloadWebPage(string urlAddress, string fileName)
        {
            this.urlAddress = urlAddress;
            this.fileName = fileName;
        }

        public DownloadWebPage(string urlAddress)
        {
            this.urlAddress = urlAddress;
            this.fileName = getFilenameFromURL(this.urlAddress);
        }

        /*
         * Данный метод предназначается для извлечения названия сайта из URL
         * TODO позже удалить или доделать
         * Проблема: Не все сайты имеют одинаковый формат url
         */
        private string getFilenameFromURL(string urlString)
        {
            Console.WriteLine(urlString.Split('.')[1]);
            return urlString.Split('.')[1];
        }

        /*
         * Данный метод предназначается для установления соединения с сервером
         */
        public async Task GetHTML()
        {
            try
            {
                HttpResponseMessage result = await httpClient.GetAsync(urlAddress);

                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        await SaveHTML();
                    }
                    catch (IOException e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Status Code Server Ansver: {result.StatusCode}");
                    Console.WriteLine("Не удалось подключится к серверу");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{urlAddress}: должен быть абсолютным URL или необходимо задать BaseAddress");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

            }
            catch (HttpRequestException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    $"Не удалось выполнить запрос из-за ключевой проблемы, например подключения к сети, " +
                    $"ошибки DNS, проверки сертификата сервера или времени ожидания.");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

            }
            catch (TaskCanceledException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Не удалось выполнить запрос из-за истечения времени ожидания.");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        
        /*
         * Данный метод предназначается для сохраненияHTML в файл
         */
        private async Task SaveHTML()
        {
            using (StreamWriter streamWriter = new StreamWriter($@"../../../SavePages/{this.fileName}.txt"))
            {
                string gettedHTML = await httpClient.GetStringAsync(urlAddress);
                streamWriter.WriteLine(gettedHTML);
            }
        }
    }
}