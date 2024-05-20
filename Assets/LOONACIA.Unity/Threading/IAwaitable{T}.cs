namespace LOONACIA.Unity.Threading
{
    public interface IAwaitable<out TAwaiter, out TResult>
        where TAwaiter : IAwaiter<TResult>
    {
        TAwaiter GetAwaiter();
    }
}