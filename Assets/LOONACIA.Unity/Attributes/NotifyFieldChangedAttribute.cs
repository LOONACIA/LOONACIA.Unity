using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace LOONACIA.Unity
{
	/// <summary>
	/// Invoke method when field changed in inspector<br/>
	/// <para>
	/// Method signature must be one of these:<br/>
	/// 1. MethodName()<br/>
	/// 2. MethodName(object oldValue) - newValue is target field value<br/>
	/// 3. MethodName(object oldValue, object newValue)<br/>
	/// <br/>
	/// This attribute can be used as follows:
	/// <code>
	/// public class MyComponent : MonoBehaviour
	/// {
	///		[SerializeField]
	///		[NotifyFieldChanged(nameof(OnValueChanged))]
	///		private int m_value;
	///
	///		private void OnValueChanged(int oldValue, int newValue)
	///		{
	///		}
	/// }
	/// </code>
	/// </para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class NotifyFieldChangedAttribute : PropertyAttribute
	{
		/// <summary>
		/// Initialize a new instance of the <see cref="NotifyFieldChangedAttribute"/> class.
		/// </summary>
		/// <param name="methodName">The name of method to invoke.</param>
		public NotifyFieldChangedAttribute(string methodName)
		{
			MethodName = methodName;
		}

		/// <summary>
		/// Gets the name of method to invoke.
		/// </summary>
		public string MethodName { get; }
	}
}
