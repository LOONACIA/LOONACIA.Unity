using System.Runtime.CompilerServices;

namespace LOONACIA.Unity.Threading
{
    public interface IAwaiter : INotifyCompletion
    {
        bool IsCompleted { get; }
        
        void GetResult();
    }
}
