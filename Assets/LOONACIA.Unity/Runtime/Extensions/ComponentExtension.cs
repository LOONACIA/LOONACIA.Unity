// ReSharper disable once CheckNamespace
namespace UnityEngine
{
	public static class ComponentExtension
	{
		public static T FindChild<T>(this Component component, string name = null, bool recursive = true)
			where T : Object
		{
			return component.gameObject.FindChild<T>(name, recursive);
		}
		
		public static T GetOrAddComponent<T>(this Component component)
			where T : Component
		{
			return component.gameObject.GetOrAddComponent<T>();
		}
	}
}