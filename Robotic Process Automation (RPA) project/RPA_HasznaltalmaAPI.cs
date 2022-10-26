using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Excel = Microsoft.Office.Interop.Excel;
using WindowsInput;
using System.Threading;

namespace RPAprojectFW
{
    public class HasznaltalmaAPI
    {
        public IWebDriver driver;
        string url;
        string searchWord;
        string workingDirectory;

        public void StartSession(string url, string searchWord, string workingDirectory = null)
        {
            this.url = url;
            this.searchWord = searchWord;
            if (workingDirectory != null)
            {
                this.workingDirectory = workingDirectory;
            }
            else
            {
                this.workingDirectory = AppContext.BaseDirectory;
            }

            driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch (WebDriverException ex)
            {
                throw new Exception("Az elérni kívánt weboldal nem elérhető!");
            }
        }

        public List<Telefon> DownloadFirstPage()
        {
            List<Telefon> telefonok = new List<Telefon>();

            driver.FindElement(By.Id("l_search_field")).SendKeys(searchWord);
            driver.FindElement(By.XPath("/html/body/div[2]/div[1]/header/div/div[1]/div[3]/form/div/div/button")).Click();

            for (int i = 1; i < 60; i++)
            {
                Telefon t = new Telefon();
                IWebElement title;
                try
                {
                    title = driver.FindElement(By.XPath($"/html/body/div[2]/div[3]/div/section/div[{i}]/div[2]/div[2]/h4/a"));
                }
                catch
                {
                    continue;
                }

                t.Nev = title.Text;
                t.Allapot = driver.FindElement(By.XPath($"/html/body/div[2]/div[3]/div/section/div[{i}]/div[2]/div[2]/p/span[2]")).Text;
                t.Datum = DateTime.Parse(driver.FindElement(By.XPath($"/html/body/div[2]/div[3]/div/section/div[{i}]/div[2]/div[1]")).Text);

                IWebElement price;
                try
                {
                    price = driver.FindElement(By.XPath($"/html/body/div[2]/div[3]/div/section/div[{i}]/div[3]/div[2]/div/span[2]"));
                }
                catch
                {
                    price = driver.FindElement(By.XPath($"/html/body/div[2]/div[3]/div/section/div[{i}]/div[3]/div[2]/div/span"));
                }
                string ar = price.Text.Replace(" ", "");
                ar = ar.Replace("Ft", "");
                t.Ar = int.Parse(ar);

                telefonok.Add(t);

                Console.WriteLine(i);
            }

            return telefonok;

        }

        public List<Telefon> CompareToDatabase(List<Telefon> telefonok)
        {
            List<Telefon> ujTelefonok = new List<Telefon>();

            foreach (Telefon t in telefonok)
            {
                if (dbManager.IsTelefonNew(t))
                {
                    ujTelefonok.Add(t);
                }
            }

            return ujTelefonok;
        }

        public void AddTelefonsToDatabase(List<Telefon> telefonok)
        {
            foreach (Telefon t in telefonok)
            {
                dbManager.AddTelefon(t);
            }
        }

        public void ExportToExcel(List<Telefon> telefonok)
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp != null)
            {
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets.Add();

                int sor = 1;
                foreach (Telefon t in telefonok)
                {
                    excelWorksheet.Cells[sor, 1] = t.Nev;
                    excelWorksheet.Cells[sor, 2] = t.Allapot;
                    excelWorksheet.Cells[sor, 3] = t.Datum;
                    excelWorksheet.Cells[sor, 4] = t.Ar;
                    sor++;
                }

                excelApp.ActiveWorkbook.SaveAs(String.Concat(workingDirectory, "telefonok.xls"), Excel.XlFileFormat.xlWorkbookNormal);
                excelWorkbook.Close();
                excelApp.Quit();

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorksheet);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorkbook);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            driver.Dispose();
        }

        public void SaveCurrentPage(string url)
        {
            InputSimulator i = new InputSimulator();
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);


            i.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.CONTROL);
            i.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_S);
            i.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.CONTROL);

            Thread.Sleep(2000);

            i.Keyboard.TextEntry("teszt.html");
        }
    }
}
