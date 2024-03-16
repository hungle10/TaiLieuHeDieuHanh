using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bai3
{
    internal class Program
    {

        static List<int> lst=new List<int>();     
        static void Main(string[] args)
        {
            Console.Write("Enter the number : ");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i <= n; i++)
                lst.Add(i);
            Console.WriteLine("Enter the number of thread : ");   
            int t=int.Parse(Console.ReadLine());

            double T = (double)n / t;

            for(int i=0;i<t;i++)    
                for(int i = 0; i < T; i++)
                {
                    Thread thr = new Thread(() =>
                    {
                         
                    });
                }    
                    
            Console.ReadKey();
        }
        static Thread Prime(int n)
        {
            List<int> lst = new List<int>();
            Thread threadParent = new Thread(() =>
            {
                Thread threadChild = new Thread(() =>
                {
                });
                threadChild.Start();
                threadChild.Join();
      
                Console.WriteLine("Start showing the elements");
                for (int i = 0; i < lst.Count; ++i)
                {
                    Console.Write(lst[i] + " ");
                }
            });
            threadParent.Start();
            threadParent.Join();
            return threadParent;
        }
        static bool checkNT(int n)
        {
            if (n < 2) return false;
            double sq = System.Math.Sqrt(n);
            for (int i = 2; i <= sq; i++)
            {
                if (n % i==0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

