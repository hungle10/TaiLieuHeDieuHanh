using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace bai2
{
    internal class Program
    {
        static int[] arr = new int[1000];
        static void Main(string[] args)
        {
            Console.Write("Enter the number of elements: ");
            int n = int.Parse(Console.ReadLine());
            Thread t = Fibo(n);
            Console.ReadKey();  
        }
        static Thread Fibo(int n)
        {
            Thread threadParent = new Thread(() =>
            {
                Thread threadChild = new Thread(() =>
                {
                    arr[0] = 0;
                    arr[1] = 1;
                    int number = n;
                    if (number > 2)
                    {
                        for (int i = 2; i < number; ++i) //loop starts from 2 because 0 and 1 are already printed    
                        {
                            arr[i] = arr[i-1] + arr[i-2];
                       
                        }
                    }

                });
                threadChild.Start();
                //threadParent gọi phương thức threadChild.Join() nghĩa là threadParent phải đợi threadChild thực hiện xong 
                //threadParent mới được thực thi tiếp
                threadChild.Join();
                Console.WriteLine("Set up sequence data to the arr has been done");
                Console.WriteLine("Start showing the elements");
                for(int i = 0; i < n; ++i)
                {
                    Console.Write(arr[i]+" ");
                }    
            });
            threadParent.Start();
            //thread Main() gọi threadParent.Join() =>thread Main phải đợi threadParent(chính là thread Fibo) thực hiện xong
            threadParent.Join();    
            return threadParent;    
        }
    }
}
