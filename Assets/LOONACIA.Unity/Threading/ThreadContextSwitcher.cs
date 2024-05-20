namespace LOONACIA.Unity.Threading
{
    public static class ThreadContextSwitcher
    {
        public static ThreadContextSwitchOperation SwitchToMainThreadAsync() => new(true);
        
        public static ThreadContextSwitchOperation SwitchToBackgroundThreadAsync() => new(false);
    }
}
