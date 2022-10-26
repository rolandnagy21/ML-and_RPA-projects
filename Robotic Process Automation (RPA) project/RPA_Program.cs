using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace RPAprojectFW
{
    class Program
    {
        static void Main(string[] args)
        {
            //inicializálás
            string url = args[0];
            string searchWord = args[1];
            string workingDirectory;
            if (args.Length < 3)
            {
                workingDirectory = null;
            }
            else
            {
                workingDirectory = args[2];
            }

            HasznaltalmaAPI api = new HasznaltalmaAPI();

            api.StartSession(url, searchWord, workingDirectory);

            List<Telefon> telefonok = api.DownloadFirstPage();


            List<Telefon> ujTelefonok = api.CompareToDatabase(telefonok);
            api.AddTelefonsToDatabase(ujTelefonok);
            api.ExportToExcel(ujTelefonok);

            

        }

    }
}
