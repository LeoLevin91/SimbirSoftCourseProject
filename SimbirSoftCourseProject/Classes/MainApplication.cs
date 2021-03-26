namespace SimbirSoftCourseProject.Classes
{
    /*
     * Данный класс агрегирует (композиция) в себе два других класса (<DownloadWebPage>, <CounterWord>)
     * А также производит логгирование ошибок в отдельный файл
     */
    public class MainApplication
    {
        public DownloadWebPage downloadWebPage { get; set; } = new DownloadWebPage();
        public CounterWord counterWord { get; set; } = new CounterWord();
    }
}