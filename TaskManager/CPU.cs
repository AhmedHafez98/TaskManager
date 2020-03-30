using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class CPU
    {
        Process p;
        int pid;
        private  DateTime lastTime;
        private  TimeSpan lastTotalProcessorTime;
        private  DateTime curTime;
        private  TimeSpan curTotalProcessorTime;
        public CPU(int PID)
        {

            p = Process.GetProcessById(PID);
            lastTime = DateTime.Now;
            try
            {
                lastTotalProcessorTime = p.TotalProcessorTime;
            }
            catch
            {
                
            }
            pid = PID;
        }
        public double getUsage()
        {
            curTime = DateTime.Now;
            try
            {
                curTotalProcessorTime = p.TotalProcessorTime;
            }
            catch
            {
                return -1.0;
            }
            double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
            lastTime = curTime;
            lastTotalProcessorTime = curTotalProcessorTime;
            return CPUUsage * 100;
        }
    }
}
