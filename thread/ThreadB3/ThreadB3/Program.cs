using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ThreadB3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a number (N) to find prime numbers up to N: ");
            int N = int.Parse(Console.ReadLine());

            Console.Write("Enter the number of threads (T): ");
            int T = int.Parse(Console.ReadLine());

            // Calculate the range for each thread
            int rangePerThread = N / T;

            // Create a list to store the prime numbers found by each thread
            List<int>[] primeLists = new List<int>[T];

            // Create and start the threads
            Thread[] threads = new Thread[T];
            for (int i = 0; i < T; i++)
            {
                int threadIndex = i; // Capture the loop variable in a local variable
                int start = threadIndex * rangePerThread + 1;
                int end;
                if (threadIndex == T - 1)
                {
                    end = N;
                }
                else
                {
                    end = (threadIndex + 1) * rangePerThread;
                }
                primeLists[threadIndex] = new List<int>();
                threads[threadIndex] = new Thread(() => FindPrimes(start, end, primeLists[threadIndex]));
                threads[threadIndex].Start();
            }


            // Wait for all threads to finish
              foreach (Thread thread in threads)
             {
                 thread.Join();
             }
           

            // Output prime numbers found by all threads
            Console.WriteLine($"Prime numbers up to {N}:");

            for (int i = 0; i < T; i++)
            {
                foreach (int prime in primeLists[i])
                {
                    Console.Write(prime + " ");
                }
            }
            Console.ReadKey();
        }

        static void FindPrimes(int start, int end, List<int> primeList)
        {
            for (int num = start; num <= end; num++)
            {
                if (IsPrime(num))
                {
                    primeList.Add(num);
                }
            }
        }

        static bool IsPrime(int num)
        {
            if (num <= 1)
                return false;

            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                    return false;
            }

            return true;
        }
    }
}
