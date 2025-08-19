using System;
using UnityEngine;

namespace LOONACIA.Unity.Annotations
{
    /// <summary>
    /// An attribute that make int field a layer selection field
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class LayerAttribute : PropertyAttribute
    {
    }
}