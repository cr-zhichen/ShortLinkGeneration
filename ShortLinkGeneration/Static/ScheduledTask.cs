using System.Collections.Concurrent;

namespace ShortLinkGeneration.Static
{
    public class ScheduledAction
    {
        public Action Action { get; set; }
        public int IntervalSeconds { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
    }


    public static class ScheduledTask
    {
        public static ConcurrentDictionary<string, ScheduledAction> _actions =
            new ConcurrentDictionary<string, ScheduledAction>();

        public static void Add(string id, Action action, int intervalSeconds)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            _actions[id] = new ScheduledAction()
            {
                Action = action,
                IntervalSeconds = intervalSeconds,
                CancellationTokenSource = cancellationTokenSource
            };

            Task.Run(async () =>
            {
                while (true)
                {
                    if (cancellationTokenSource.Token.IsCancellationRequested)
                        break;

                    action();
                    await Task.Delay(TimeSpan.FromSeconds(intervalSeconds), cancellationTokenSource.Token);
                }
            }, cancellationTokenSource.Token);
        }

        public static void Stop(string id)
        {
            if (_actions.TryRemove(id, out var scheduledAction))
            {
                scheduledAction.CancellationTokenSource.Cancel();
            }
        }
    }
}