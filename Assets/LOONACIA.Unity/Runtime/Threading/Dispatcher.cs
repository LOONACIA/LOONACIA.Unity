using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace LOONACIA.Unity.Threading
{
    public sealed class Dispatcher
    {
        private static readonly Lazy<Dispatcher> s_lazyInstance = new(() => new());
        
        private static bool s_isInitialized;
    
        private readonly ConcurrentQueue<Action> _dispatcherQueue = new();
        
        private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        private Dispatcher()
        {
            Register();
            Application.quitting += Unregister;
        }
    
        public static Dispatcher Current => s_lazyInstance.Value;
        
        public bool IsMainThread => SynchronizationContext.Current == _synchronizationContext;
    
        public void Enqueue(Action action)
        {
            _dispatcherQueue.Enqueue(action);
        }
    
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (s_isInitialized)
            {
                return;
            }

            _ = Current;

            s_isInitialized = true;
        }

        private void Register()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            ref var system = ref playerLoop.FindSubSystem<Update>();
            system.updateDelegate += Update;
            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        private void Unregister()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            ref var system = ref playerLoop.FindSubSystem<Update>();
            system.updateDelegate -= Update;
            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        private void Update()
        {
            while (_dispatcherQueue.TryDequeue(out var action))
            {
                action?.Invoke();
            }
        }
    }
}