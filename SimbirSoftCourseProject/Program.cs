using System;
using System.Threading.Tasks;
using SimbirSoftCourseProject.Classes;

namespace SimbirSoftCourseProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DownloadWebPage downloadWebPage= new DownloadWebPage("https://www.thetimes.co.uk/", "thetimes");
            await downloadWebPage.GetHTML();
            
            CounterWord counterWord = new CounterWord($@"../../../SavePages/thetimes.txt");
            counterWord.ParseHTML();
        }
    }
}