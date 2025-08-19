namespace LOONACIA.Unity.Threading
{
    public interface IAwaitable<out TAwaiter>
        where TAwaiter : IAwaiter
    {
        TAwaiter GetAwaiter();
    }
}