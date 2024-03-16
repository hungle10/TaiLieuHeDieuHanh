using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJFnonPre
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of Processes");
            int soLuong = int.Parse(Console.ReadLine());
            //Process[] processes = new Process[10];     
            List<Process> list = new List<Process>();

            for (int i = 0; i < soLuong; i++)
            {
                Console.WriteLine("Enter the arrival time for process " + i);
                int arrivalTime = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the CPU brust time for process " + i);
                int brustTime = int.Parse(Console.ReadLine());
                Process pc = new Process(i, arrivalTime, brustTime);
                list.Add(pc);
            }
            Process[] arr = new Process[list.Count];
            int lowestCpuBurstTime=10000;

            int iDelete = 0;
            foreach (Process p in list)
            {
                if (p.arrivalTime == 0)
                {
                    p.completionTime = p.cpuBrustTime;
                    p.turnAroundTime = p.completionTime - p.arrivalTime;
                    p.waitingTime = p.turnAroundTime - p.cpuBrustTime;
                    p.startingTime = 0;
                    p.finishTime = p.cpuBrustTime;
                    lowestCpuBurstTime = p.cpuBrustTime;
                    arr[0] = p;
                    iDelete = p.id;
                }
            }
            list.RemoveAt(iDelete);
            list.Sort(new ProcessComparer());
            int index = 1; // Biến đếm để theo dõi vị trí hiện tại trong mảng arr
            foreach (Process m in list)
            {
                arr[index] = m; // Thêm phần tử m vào mảng arr
                index++; // Tăng biến đếm
            }
            for(int i = 1; i < arr.Length; i++)
            {
                arr[i].startingTime = arr[i - 1].finishTime;
                arr[i].finishTime = arr[i].startingTime + arr[i].cpuBrustTime;
                arr[i].completionTime = arr[i].finishTime;
                arr[i].turnAroundTime= arr[i].completionTime - arr[i].arrivalTime;
                arr[i].waitingTime=arr[i].turnAroundTime - arr[i].cpuBrustTime; 
            }
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("Process" + arr[i].id +" | "+ "StartingTime" + arr[i].startingTime +" | " + "FisnishTime" + arr[i].finishTime + "|" + "CompletionTime" + arr[i].completionTime +  "|" + "TurnAroundTime" + arr[i].turnAroundTime +" | " + "WaitingTime" + arr[i].waitingTime );
            }
            Console.ReadKey();
        }
    }
    public class Process
    {
        public int id;
        public int arrivalTime;
        public int cpuBrustTime;
        public int completionTime;
        public int startingTime;
        public int finishTime;
        public int turnAroundTime;
        public int waitingTime;
        public Process(int id, int arrivalTime, int cpuBrustTime)
        {
            this.id = id;
            this.arrivalTime = arrivalTime;
            this.cpuBrustTime = cpuBrustTime;
        }
    }
    public class ProcessComparer : IComparer<Process>
    {
        public int Compare(Process x, Process y)
        {
            // So sánh Burst Time
            int compareByBurstTime = x.cpuBrustTime.CompareTo(y.cpuBrustTime);
            if (compareByBurstTime != 0)
            {
                // Nếu Burst Time khác nhau, sắp xếp theo Burst Time từ nhỏ đến lớn
                return compareByBurstTime;
            }
            else
            {
                // Nếu Burst Time bằng nhau, sắp xếp theo Arrival Time từ nhỏ đến lớn
                return x.arrivalTime.CompareTo(y.arrivalTime);
            }
        }
    }

}

