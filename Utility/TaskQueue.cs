using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Utility
{
    public class TaskQueue
    {
        private readonly ConcurrentQueue<TaskQueueModel> _taskQueue = new ConcurrentQueue<TaskQueueModel>();

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private readonly Task _pumpTask;

        public TaskQueue()
        {
            _pumpTask = Task.Run(() => Pump(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
        }

        public bool AddTask(Task task)
        {
            _taskQueue.Enqueue(new TaskQueueModel(task));
            return true;
        }
        public bool ReTry(TaskQueueModel task)
        {
            if (task.RetryCount < 5)
            {
                task.RetryCount++;
                _taskQueue.Enqueue(task);
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task Pump(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_taskQueue.TryDequeue(out TaskQueueModel task))
                {
                    try
                    {
                        await task.Task.ConfigureAwait(false);
                    }
                    catch
                    {
                        ReTry(task);
                        // Ignore exceptions for now
                    }
                }
                else
                {
                    await Task.Delay(100, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public Task ShutdownAsync()
        {
            _cancellationTokenSource.Cancel();
            return _pumpTask;
        }

        public class TaskQueueModel
        {
            public TaskQueueModel(Task task)
            {
                Task = task;
                RetryCount = 0;
                QueueID = Guid.NewGuid();
            }
            public Task Task { get; set; }
            public int RetryCount { get; set; }
            public Guid QueueID { get; set; }
        }
    }
}
