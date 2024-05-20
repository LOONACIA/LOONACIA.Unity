using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace LOONACIA.Unity.Threading
{
    public readonly struct ThreadContextSwitchOperation : IAwaitable<ThreadContextSwitchOperation>, IAwaiter, ICriticalNotifyCompletion
    {
        private readonly bool _isSwitchingToMainThread;
        
        public ThreadContextSwitchOperation(bool isSwitchingToMainThread)
        {
            _isSwitchingToMainThread = isSwitchingToMainThread;
        }
        
        public bool IsCompleted => Dispatcher.Current.IsMainThread && _isSwitchingToMainThread;

        public ThreadContextSwitchOperation GetAwaiter() => this;

        public void OnCompleted(Action continuation)
        {
            if (_isSwitchingToMainThread)
            {
                Dispatcher.Current.Enqueue(continuation);
            }
            else
            {
                ThreadPool.QueueUserWorkItem(obj => continuation(), null);
            }
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            if (_isSwitchingToMainThread)
            {
                using var flowControl = ExecutionContext.SuppressFlow();
                Dispatcher.Current.Enqueue(continuation);
            }
            else
            {
                ThreadPool.UnsafeQueueUserWorkItem(obj => continuation(), null);
            }
        }

        public void GetResult()
        {
        }
    }
}