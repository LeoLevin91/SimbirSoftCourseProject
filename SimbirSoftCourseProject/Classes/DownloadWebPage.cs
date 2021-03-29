using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using NLog;

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

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        
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
                        Logger.Error($"{e.Message}.");
                        Logger.Trace(e.StackTrace);
                    }
                }
                else
                {
                    Logger.Error($"Status Code Server Ansver: {result.StatusCode}.\n " +
                                 $"Не удалось подключится к серверу");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Error($"{e.Message}.\n" +
                             $"{urlAddress}: должен быть абсолютным URL или необходимо задать BaseAddress");
                Logger.Trace(e.StackTrace);
            }
            catch (HttpRequestException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Error($"{e.Message}.\n" +
                             $"Не удалось выполнить запрос из-за ключевой проблемы, например подключения к сети," +
                             $"ошибки DNS, проверки сертификата сервера или времени ожидания.");
                Logger.Trace(e.StackTrace);
            }
            catch (TaskCanceledException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Logger.Error($"{e.Message}.+/n" +
                             $"Не удалось выполнить запрос из-за истечения времени ожидания.");
                Logger.Trace(e.StackTrace);
            }
            finally
            {
                Console.WriteLine("Программа закончила работу");
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