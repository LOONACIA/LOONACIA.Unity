using System;
using System.Threading.Tasks;

namespace LOONACIA.Unity.Threading
{
    public readonly struct ThreadContextSwitchOperation : IAwaitable<ThreadContextSwitchOperation>, IAwaiter
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
                Task.Run(continuation);
            }
        }
        
        public void GetResult()
        {
        }
    }
}