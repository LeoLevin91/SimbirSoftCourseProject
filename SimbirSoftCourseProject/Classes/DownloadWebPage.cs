using System;
using System.Net;
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
        // TODO Удалить переменную <urlAdress> и передавать url в конструкторе
        public string urlAddress { get; set; }
        
        public string GET_METHOD { get; set; } = "GET";
        public int ConnectTime { get;} = 12000;

        private HttpClient httpClient= new HttpClient();
        

        /*
         * Данный метод предназначается для установления соединения с сайтом
         */
        public async Task settingUpConnection()
        {
            
            // Пытаемся установить соединение
            HttpResponseMessage result = await httpClient.GetAsync(urlAddress);
            if (result.ReasonPhrase == "OK")
            {
                // Выводим страницу на экран
                string gettedHTML = await httpClient.GetStringAsync(urlAddress);
                Console.WriteLine(gettedHTML);
            }
            else
            {
                Console.WriteLine("There was a problem!");
            }
            // Получаем HTML страницу
            //var content = await httpClient.GetStringAsync(urlAddress);
            // Получим header
            // var header = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, urlAddress));
            // Console.WriteLine(result.StatusCode);
            // //Console.WriteLine(content);
            // Console.WriteLine(header);


            // TODO Получение HTML кода страницы
            // WebClient client = new WebClient();
            // string htmlCode = client.DownloadString(urlAddress);
            // Console.WriteLine(htmlCode);
            
        }

    }
}