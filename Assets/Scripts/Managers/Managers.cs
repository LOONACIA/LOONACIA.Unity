using UnityEngine;

public class Managers : MonoBehaviour
{
	private static Managers s_Instance;

	private InputManager _input = new();

	private PoolManager _pool = new();

	private ResourceManager _resource = new();

	private UIManager _ui = new();

	public static Managers Instance
	{
		get
		{
			Init();
			return s_Instance;
		}
	}

	public static InputManager Input => Instance._input;

	public static PoolManager Pool => Instance._pool;

	public static ResourceManager Resource => Instance._resource;

	public static UIManager UI => Instance._ui;

	private static void Init()
	{
		if (s_Instance != null)
		{
			return;
		}
		
		if (GameObject.Find("@Managers") is not { } managersRoot)
		{
			managersRoot = new() { name = "@Managers" };
		}

		s_Instance = managersRoot.GetOrAddComponent<Managers>();
		DontDestroyOnLoad(s_Instance);
			
		s_Instance._input.Init();
	}
}