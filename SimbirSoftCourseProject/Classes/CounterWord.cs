using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using NLog;


namespace SimbirSoftCourseProject.Classes
{
    /*
     * Данный класс производит парсинг сохраненной в документ WEB-страницы по соответствующим разделителям,
     * после чего подсчитывает количество уникальных символов 
     */
    public class CounterWord
    {
        public string Filepath { get; set; }
        private readonly char[] _splitSymbols = {'\t', '\r', '\n', ' ', ',', '.', '!', '?', '\"',';',':', '[', ']', '(', ')'};
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public CounterWord(string filepath)
        {
            Filepath = filepath;
        }

        public void ParseHTML()
        {
            try
            {
                // Создадим объект документа
                var htmlDoc = new HtmlDocument();
                // загрузим документ для работы
                htmlDoc.Load(Filepath);
                // Получим сожержимое тегов внутри body
                var body = htmlDoc.DocumentNode.SelectSingleNode("//body").InnerText;

                // разбить строку по заданным разделителям
                string[] splitBodyContent = body.Split(_splitSymbols, StringSplitOptions.RemoveEmptyEntries);
                // Удалить из массива пустые строки и числа с помощью LINQ
                splitBodyContent = splitBodyContent.Where(x => !string.IsNullOrEmpty(x) && !int.TryParse(x, out _))
                    .ToArray();
                // Установим все буквы в каждом слове в верхний регистр с помощью LINQ
                splitBodyContent = splitBodyContent.Select(item => item.ToUpper()).ToArray();

                // Подсчет уникальных слов
                Dictionary<string, int> counts = splitBodyContent.GroupBy(x => x).ToDictionary(g => g.Key,
                    g => g.Count()
                );

                // Отсортируем словарь
                var cc = counts.OrderByDescending(pair => pair.Value);

                foreach (var item in cc)
                {
                    Console.WriteLine($"{item.Key} = {item.Value}");
                }
            }
            catch (Exception e)
            {
                // Логирование
                Logger.Error($"{e.Message}.");
                Logger.Trace(e.StackTrace);
            }
            finally
            {
                Logger.Info("Завершение работы программы");
            }
            
            
        }  
    }
}