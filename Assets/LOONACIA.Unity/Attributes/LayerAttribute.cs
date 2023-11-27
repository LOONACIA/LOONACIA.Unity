using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace LOONACIA.Unity
{
    /// <summary>
    /// An attribute that make int field a layer selection field
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class LayerAttribute : PropertyAttribute
    {
    }
}