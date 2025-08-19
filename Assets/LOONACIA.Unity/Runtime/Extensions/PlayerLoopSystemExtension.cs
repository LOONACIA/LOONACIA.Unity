using System;
using UnityEngine.LowLevel;

namespace LOONACIA.Unity
{
    public static class PlayerLoopSystemExtension
    {
        public static ref PlayerLoopSystem FindSubSystem<T>(this ref PlayerLoopSystem root)
        {
            for (var i = 0; i < root.subSystemList.Length; i++)
            {
                if (root.subSystemList[i].type == typeof(T))
                {
                    return ref root.subSystemList[i];
                }
            }
            
            throw new InvalidOperationException($"SubSystem of type {typeof(T)} not found.");
        }
    }
}
