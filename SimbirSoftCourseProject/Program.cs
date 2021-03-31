using System;
using System.Threading.Tasks;
using SimbirSoftCourseProject.Classes;

namespace SimbirSoftCourseProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DownloadWebPage downloadWebPage= new DownloadWebPage("https://animego.org/", "animego");
            await downloadWebPage.GetHTML();
            
            CounterWord counterWord = new CounterWord($@"../../../SavePages/animego.txt");
            counterWord.ParseHTML();
        }
    }
}