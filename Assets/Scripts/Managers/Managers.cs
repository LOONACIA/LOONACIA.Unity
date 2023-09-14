using UnityEngine;

public class Managers : MonoBehaviour
{
	private static Managers s_Instance;

	private PoolManager _pool = new();

	private ResourceManager _resource = new();

	public static Managers Instance
	{
		get
		{
			Init();
			return s_Instance;
		}
	}

	public static PoolManager Pool => Instance._pool;

	public static ResourceManager Resource => Instance._resource;

	private static void Init()
	{
		if (s_Instance == null)
		{
			if (GameObject.Find("@Managers") is not GameObject managersRoot)
			{
				managersRoot = new() { name = "@Managers" };
			}

			s_Instance = managersRoot.GetOrAddComponent<Managers>();
			DontDestroyOnLoad(s_Instance);
		}
	}
}