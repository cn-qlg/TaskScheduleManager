using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduleManager
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var manager = new TaskScheduleManager())
            {
                var tasks = manager.FilterTasksByPath(@"\");
                foreach (var task in tasks)
                {
                    Console.WriteLine($"{task.Folder}, {task.Name}, {task.LastRunTime}, {task.LastTaskResult}, {manager.GetMsgFromLastTaskResult(task.LastTaskResult)}, {task.NextRunTime}, {task.State} ");
                }
                Console.ReadKey();
            }
        }
    }
}
