using System.Runtime.CompilerServices;

namespace LOONACIA.Unity.Threading
{
    public interface IAwaiter<out T> : INotifyCompletion
    {
        bool IsCompleted { get; }

        T GetResult();
    }
}