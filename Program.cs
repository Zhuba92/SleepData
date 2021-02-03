using System;
using System.IO;

namespace SleepData
{
    class Program
    {
       static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // TODO: create data file
                 // create data file
                StreamWriter sw = new StreamWriter("data.txt");
                 // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());
                 // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                Console.WriteLine(dataDate);

                // random number generator
                Random rnd = new Random();

                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                // TODO: parse data file
                string file = "data.txt";
                if(System.IO.File.Exists(file))
                {
                    StreamReader sr = new StreamReader(file);
                    while(!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string date = line.Substring(0, line.IndexOf(","));
                        DateTime numDate = DateTime.Parse(date);
                        string[] hours = line.Split(",");
                        string[] hoursNoBars = hours[1].Split("|");

                        Console.WriteLine($"Week of {numDate:MMM} {numDate:dd}, {numDate:yyyy}");
                        Console.WriteLine($"Mo Tu We Th Fr Sa Su Tot Avg");
                        Console.WriteLine("-- -- -- -- -- -- -- --- ---");

                        double total = 0;
    
                        foreach(var section in hoursNoBars)
                        {
                            int sectionNum = Convert.ToInt32(section);
                            total += sectionNum;
                        }

                        foreach(var section in hoursNoBars)
                        {
                            Console.Write("{0,3}",section + " ");
                        }

                        double avg = (total / 7);
                        String displayAvg = string.Format("{0:0.0}", avg);

                        Console.Write("{0,3}",total);
                        Console.Write("{0,4}",displayAvg);
                        Console.WriteLine("");
                        Console.WriteLine("");
                    }
                }
            }
        }
    }
}
