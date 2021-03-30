using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;



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
        SortedDictionary<string, int> words = new SortedDictionary<string, int>();

        public CounterWord(string filepath)
        {
            Filepath = filepath;
        }

        public void ParseHTML()
        {
            // Получить только тег body
            
            // Создадим объект документа
            var htmlDoc = new HtmlDocument();
            // загрузим документ для работы
            htmlDoc.Load(Filepath);
            // Получим сожержимое тегов внутри body
            var body = htmlDoc.DocumentNode.SelectSingleNode("//body").InnerText;
            
            // разбить строку по заданным разделителям
            string[] splitBodyContent = body.Split(_splitSymbols, StringSplitOptions.RemoveEmptyEntries);
            // Удалить из массива пустые строки и числа с помощью LINQ
            splitBodyContent = splitBodyContent.Where(x => !string.IsNullOrEmpty(x) && !int.TryParse(x, out _)).ToArray();
            // Установим все буквы в каждом слове в верхний регистр с помощью LINQ
            splitBodyContent = splitBodyContent.Select(item => item.ToUpper()).ToArray();

            foreach (var item in splitBodyContent)
            {
                Console.WriteLine(item);
            }
                
        }  
    }
}