using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace bai1
{
    internal class Program
    {
        static List<int> list = new List<int> { 90, 81, 78, 95, 79, 72, 85 };
        static double averagevalue;
        static int minValue;
        static void Main(string[] args)
        {
            Thread tAvg = theAverageValueOfList();
            Thread tMin = theMinimumValueOfList();
            Thread tMax = theMaximumValueOfList();      

            Console.WriteLine("Thread tinh trung binh da hoan thanh");
            Console.ReadKey();
        }
        static Thread theAverageValueOfList()
        {
            Thread tParent = new Thread(() =>
            {
                Thread tWorker = new Thread(() =>
                {
                    calculateTheAverage();
                });
                tWorker.Start();
                tWorker.Join();
                Console.WriteLine("The avg value has been set");
            });
            tParent.Start();
            tParent.Join();
            Console.WriteLine("The avg value is " + averagevalue);
            return tParent;
         }
        static Thread theMinimumValueOfList()
        {
            Thread tParent = new Thread(() =>
            {
                Thread tWorker = new Thread(() =>
                {
                    findMin();
                });
                tWorker.Start();
                tWorker.Join();
                Console.WriteLine("The min value has been set");
            });
            tParent.Start();
            tParent.Join();
            Console.WriteLine("The min value is " + minValue);
            return tParent;
        }
        static Thread theMaximumValueOfList()
        {
            Thread tParent = new Thread(() =>
            {
                Thread tWorker = new Thread(() =>
                {
                    findMax();
                });
                tWorker.Start();
                tWorker.Join();
                Console.WriteLine("The max value has been set");
            });
            tParent.Start();
            tParent.Join();
            Console.WriteLine("The max value is " + minValue);
            return tParent;
        }
        static void findMax()
        {
            minValue = list.Max();
        }
        static void findMin()
        {
            minValue = list.Min();
        }
        static void calculateTheAverage()
        {
            double tamp=0;
            for (int i = 0; i < list.Count; i++)
                tamp = tamp + list[i];
            averagevalue= (double)(tamp / list.Count);
        }
    }
   

}
