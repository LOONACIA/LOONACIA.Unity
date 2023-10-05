using UnityEngine;

namespace LOONACIA.Unity.Managers
{
	public class ManagerHost : MonoBehaviour
	{
		private static ManagerHost s_instance;

		private readonly InputManager _input = new();

		private readonly PoolManager _pool = new();

		private readonly ResourceManager _resource = new();

		private readonly UIManager _ui = new();

		public static ManagerHost Instance
		{
			get
			{
				Init();
				return s_instance;
			}
		}

		public static InputManager Input => Instance._input;

		public static PoolManager Pool => Instance._pool;

		public static ResourceManager Resource => Instance._resource;

		public static UIManager UI => Instance._ui;

		private static void Init()
		{
			if (s_instance != null)
			{
				return;
			}
			
			if (GameObject.Find("@Managers") is not { } managersRoot)
			{
				managersRoot = new() { name = "@Managers" };
			}

			s_instance = managersRoot.GetOrAddComponent<ManagerHost>();
			DontDestroyOnLoad(s_instance);
			
			s_instance._input.Init();
		}
	}
}