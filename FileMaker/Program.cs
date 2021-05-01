using System;
using System.Threading.Tasks;
using System.Timers;

namespace FileMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootPath = @"c:\temp\";

            Console.WriteLine("Hello! Welcome to Creating files app!");
            Console.WriteLine("What are you creating? Text, Pdf, or Excel?");
            var createType = Console.ReadLine();
            
            // Create timer to keep track of how long passed
            CreateTimeOutTask();
            // Make TEXT
            if (createType.ToLower() == "text")
            {
                var tm = new TextMaker(rootPath, "MyTest.txt");
                // Takes longer the bigger the file
                Console.WriteLine("What is the size of the file in MB? (1-1000)");
                var createSize = Console.ReadLine();
                double cs = 0;
                // check if it was a number
                try
                {
                    cs = double.Parse(createSize);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Do you care what is in the file? Y or N");
                var isFilled = Console.ReadLine();
                if (isFilled.ToLower() == "y")
                {
                    tm.TextCreateByFilling(createSize);
                }
                else
                {
                    tm.TextCreateBySetLength(createSize);
                }
            }
            else if (createType.ToLower() == "pdf")
            {
                var pm = new PdfMaker(rootPath, "PdfTestData.pdf");
                pm.CreatePDF();
            }
            else if (createType.ToLower() == "excel")
            {
                var em = new ExcelMaker(rootPath + "TestExcelData.xlsx");
                em.CreateExcelFile();
            }
            else
            {
                Console.WriteLine("Not a Valid Choice!");
            }
        }

        public static void CreateTimeOutTask()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 60000;
            aTimer.Enabled = true;
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Still Active");
            Console.WriteLine("Time: " + e.SignalTime.ToString());
        }
    }
}
