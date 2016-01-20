using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeySignups.Data;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            HockeySignupsDb db = new HockeySignupsDb(Settings1.Default.ConStr);
            //var e = new Event {Date = DateTime.Today.AddDays(3), MaxPeople = 20};
            //db.AddEvent(e);
            //Console.WriteLine(e.Id);
            db.GetEvents().ToList().ForEach(e =>
            {
                Console.WriteLine(e.Date + " " + e.MaxPeople + " " + e.Id);
            });
            Console.ReadKey(true);
        }
    }
}
