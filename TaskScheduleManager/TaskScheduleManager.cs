using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SchedulerTask = Microsoft.Win32.TaskScheduler.Task;

namespace TaskScheduleManager
{
    public class TaskScheduleManager : IDisposable
    {
        private TaskService _taskService;
        public TaskScheduleManager()
        {
            _taskService = TaskService.Instance;
        }

        public void Dispose()
        {
            _taskService?.Dispose();
        }

        public IEnumerable<SchedulerTask> FilterTasksByPath(string path)
        {
            return _taskService.AllTasks.Where(task => task.Folder.ToString().StartsWith(path));
        }

        public IEnumerable<SchedulerTask> FilterTasksByState(TaskState state)
        {
            return _taskService.AllTasks.Where(task => task.State == state);
        }

        public string GetMsgFromLastTaskResult(int lastTaskResult)
        {
            if (lastTaskResult == 0)
            {
                return "操作成功完成。(0x0)";
            }
            var exc = new Win32Exception(lastTaskResult);
            if (string.IsNullOrEmpty(exc.Message))
                return $"(0x{lastTaskResult:X})";
            return exc.Message.IndexOf(lastTaskResult.ToString("X"), StringComparison.InvariantCultureIgnoreCase) == -1
                ? string.Format("{1} (0x{0:X})", lastTaskResult, exc.Message)
                : exc.Message;
        }
    }

    class ScheduleTaskInfo
    {
        public string Folder { get; set; }
        public string Name { get; set; }
        public string LastRunTime { get; set; }
        public string LastTaskResult { get; set; }
        public string NextRunTime { get; set; }
        public string State { get; set; }

    }
}
