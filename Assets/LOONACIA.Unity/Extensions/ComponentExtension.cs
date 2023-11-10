using UnityEngine;

// ReSharper disable once CheckNamespace
namespace LOONACIA.Unity
{
	public static class ComponentExtension
	{
		public static T FindChild<T>(this Component component, string name = null, bool recursive = true)
			where T : Object
		{
			return component.gameObject.FindChild<T>(name, recursive);
		}
	}
}
