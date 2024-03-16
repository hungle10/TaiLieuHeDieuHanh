using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundRobin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Process> processes = new List<Process>();
            Console.WriteLine("Enter the number of processes");
            int soluong = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the QuantumTime");
            int quantumTime = int.Parse(Console.ReadLine());
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
            Queue<Process> queue = new Queue<Process>();
            int maxArrivalTime = 0;
            for (int i = 0; i < processes.Count; i++)
            {
                maxArrivalTime = maxArrivalTime + processes[i].burstTime;
            }
      
            int time = 0;
            /* while (time <= maxArrivalTime)
             {
                 foreach (Process pr in processes)
                 {
                    bool containsElement = queue.Contains(pr);
                    if (pr.arrivalTime == time && containsElement != true)
                    {
                        queue.Enqueue(pr);
                    }

                 }
                    Process tmp = queue.Dequeue();
                    int dem = 0;
                    while (dem < quantumTime)
                    {
                        if (tmp.burstTime > 0)
                        {
                            ProcessGrantChart tp = new ProcessGrantChart(tmp.id, tmp.arrivalTime, tmp.burstTime, time);
                            arr[time++] = tp;
                            foreach (Process tpc in processes)
                            {
                                bool containsElement1 = queue.Contains(tpc);
                                if (tpc.arrivalTime == time && containsElement1 != true)
                                    queue.Enqueue(tpc);
                            }
                            dem++;
                            tmp.burstTime--;
                        }
                        else
                        {
                            processes.Remove(tmp);
                            break;
                        }

                    }

                }*/
            while (time <= maxArrivalTime || queue.Count > 0)
            {
                foreach (Process pr in processes)
                {
                    bool containsElement = queue.Contains(pr);
                    if (pr.arrivalTime == time && !containsElement)
                    {
                        queue.Enqueue(pr);
                    }
                }

                if (queue.Count > 0)
                {
                    Process tmp = queue.Dequeue();
                    int dem = 0;
                    while (dem < quantumTime && tmp.burstTime > 0)
                    {
                        ProcessGrantChart tp = new ProcessGrantChart(tmp.id, tmp.arrivalTime, tmp.burstTime, time);
                        arr[time++] = tp;
                        foreach (Process tpc in processes)
                        {
                            bool containsElement1 = queue.Contains(tpc);
                            if (tpc.arrivalTime == time && !containsElement1)
                                queue.Enqueue(tpc);
                        }
                        dem++;
                        tmp.burstTime--;
                    }
                    time--;
                    if (tmp.burstTime > 0) // Nếu còn thời gian burst, thêm lại vào hàng đợi
                    {
                        queue.Enqueue(tmp);
                    }
                }

                time++; // Tiến thời gian
            }



            for (int i = 0; i < maxArrivalTime; i++)
                if (arr[i]!=null)
                Console.WriteLine("Process :" + arr[i].id + "Second" + arr[i].seconds);
            Console.ReadKey();
        }
        static bool check(List<Process> p)
        {
            foreach (Process process in p)
            {
                if (process.burstTime > 0)
                    return true;
            }
            return false;

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
            public int burstTime;
            public Process(int id, int arrivalTime, int burstTime)
            {
                this.id = id;
                this.arrivalTime = arrivalTime;
                this.burstTime = burstTime;
            }
        }
    }
}
