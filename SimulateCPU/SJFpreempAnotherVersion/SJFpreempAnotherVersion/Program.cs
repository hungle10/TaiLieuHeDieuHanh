using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SJFpreempAnotherVersion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Process> processes = new List<Process>();
            Console.WriteLine("Enter the number of processes");
            int soluong=int.Parse(Console.ReadLine());      

            for (int i = 0; i < soluong; i++)
            {
                Console.WriteLine("Enter the arrival time for process " + i);
                int arvTime = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the CPU brust time for process " + i);
                int brustTime = int.Parse(Console.ReadLine());
                Process pc = new Process(i, arvTime, brustTime);
                processes.Add(pc);
            }
            ProcessGrantChart[] arr = new ProcessGrantChart[1000];

            int maxArrivalTime = -999;
            foreach (Process p in processes)
            {
                if(p.arrivalTime > maxArrivalTime)
                {
                    maxArrivalTime = p.arrivalTime;         
                }    
            }
            int arrivalTime = 0;
            int index = 0;
            List<Process> processesCheckAT = new List<Process>();
            while (arrivalTime<=maxArrivalTime || check(processes))
            {
                if (processesCheckAT.Count <= soluong)
                {
                    foreach (Process p in processes)
                    {
                        if (p.arrivalTime == arrivalTime)
                        {
                            bool exists = processesCheckAT.Any(process => process.id == p.id);
                            if (!exists)
                            {
                                processesCheckAT.Add(p);
                            }

                        }

                    }
                }
                else
                {

                }
               Process minBTMaxAT = processesCheckAT
              .Where(p => p.cpuBrustTime > 0)
              .OrderBy(p => p.cpuBrustTime)  // Sắp xếp các phần tử theo Burst Time tăng dần
              .ThenBy(p => p.arrivalTime)  // Tiếp tục sắp xếp các phần tử theo Arrival Time giảm dần
              .FirstOrDefault();  // Lấy phần tử đầu tiên sau khi sắp xếp
                ProcessGrantChart prc = new ProcessGrantChart(minBTMaxAT.id,minBTMaxAT.arrivalTime,minBTMaxAT.cpuBrustTime,arrivalTime);
                arr[index++] = prc;   
                minBTMaxAT.cpuBrustTime--;
                arrivalTime++;
            }

            for (int i = 0; i < index; i++)
                Console.WriteLine("Process" + arr[i].id+" Second " + arr[i].seconds);
            Console.ReadKey();
     
        }
        static bool check(List<Process> p)
        {
            foreach(Process process in p)
            {
                if (process.cpuBrustTime > 0)
                    return true;
            }
                return false;
                
        }
      }
    public class ProcessGrantChart
    {
        public int arrivalTime;
        public int cpuBrustTime;
        public int? seconds;
        public int id;
        public ProcessGrantChart(int id, int arrivalTime, int cpuBrustTime, int? seconds)
        {
            this.id = id;
            this.arrivalTime = arrivalTime;
            this.cpuBrustTime = cpuBrustTime;
            this.seconds = seconds;
        }
    }
    public class Process
    {
        public int id;
        public int arrivalTime;
        public int cpuBrustTime;
        public Process(int id, int arrivalTime, int cpuBrustTime)
        {
            this.id = id;
            this.arrivalTime = arrivalTime;
            this.cpuBrustTime = cpuBrustTime;
   
        }
    }
}
