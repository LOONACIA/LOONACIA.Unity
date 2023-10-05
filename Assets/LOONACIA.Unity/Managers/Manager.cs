using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LOONACIA.Unity.Managers
{
	public class Manager : MonoBehaviour
	{
		public enum ManagerOperationMode
		{
			Always,
			DestroyWhenSceneLoaded
		}
		
		private static Manager s_instance;

		private readonly InputManager _input = new();

		private readonly PoolManager _pool = new();

		private readonly ResourceManager _resource = new();

		private readonly UIManager _ui = new();

		public static Manager Instance
		{
			get
			{
				Init();
				return s_instance;
			}
		}

		public ManagerOperationMode OperationMode { get; set; }

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

			s_instance = managersRoot.GetOrAddComponent<Manager>();
			DontDestroyOnLoad(s_instance);
			SceneManagerEx.OnSceneChanging += s_instance.OnSceneChanging;
			
			s_instance._input.Init();
			s_instance._pool.Init();
			s_instance._ui.Init();
		}

		private void OnSceneChanging(Scene obj)
		{
			switch (OperationMode)
			{
				case ManagerOperationMode.Always:
					Clear(false);
					break;
				case ManagerOperationMode.DestroyWhenSceneLoaded:
					SceneManagerEx.OnSceneChanging -= OnSceneChanging;
					Clear(true);
					Destroy(gameObject);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public static void Clear(bool destroyAssociatedObject = false)
		{
			UI.Clear(destroyAssociatedObject);
			Pool.Clear(destroyAssociatedObject);
		}
	}
}