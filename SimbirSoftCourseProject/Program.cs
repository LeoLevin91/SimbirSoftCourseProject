using System;
using System.Threading.Tasks;
using SimbirSoftCourseProject.Classes;

namespace SimbirSoftCourseProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DownloadWebPage downloadWebPage= new DownloadWebPage();
            downloadWebPage.urlAddress = "http://www.contoso.com/";
            await downloadWebPage.settingUpConnection();
        }
    }
}