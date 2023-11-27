using System;
using LOONACIA.Unity;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace LOONACIA.Unity
{
    /// <summary>
    /// An attribute that make a field read-only in the inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
        /// <summary>
        /// The mode of read-only.
        /// </summary>
        public enum ReadOnlyMode
        {
            /// <summary>
            /// A field is always read-only.
            /// </summary>
            Always,
            
            /// <summary>
            /// A field is read-only only in runtime.
            /// </summary>
            RuntimeOnly
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyAttribute"/> class.
        /// </summary>
        /// <param name="mode">The mode of read-only.</param>
        public ReadOnlyAttribute(ReadOnlyMode mode = ReadOnlyMode.Always)
        {
            Mode = mode;
        }
        
        /// <summary>
        /// Gets the mode of read-only.
        /// </summary>
        public ReadOnlyMode Mode { get; }
    }
}