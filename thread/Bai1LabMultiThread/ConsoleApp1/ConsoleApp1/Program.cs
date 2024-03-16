using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            // Create and start the parent thread
            Thread parentThread = new Thread(() =>
            {
                Console.WriteLine("Parent thread started.");

                // Create and start the child thread
                Thread childThread = new Thread(() =>
                {
                    Console.WriteLine("Child thread started.");
                    Thread.Sleep(2000); // Simulate some work
                    Console.WriteLine("Child thread completed.");
                });
                childThread.Start();

                // Wait for the child thread to complete
                childThread.Join();

                Console.WriteLine("Parent thread completed.");
            });
            parentThread.Start();

            // Wait for the parent thread to complete
            parentThread.Join();

            Console.WriteLine("All threads completed.");
            Console.ReadKey();  
        }
    }

}
