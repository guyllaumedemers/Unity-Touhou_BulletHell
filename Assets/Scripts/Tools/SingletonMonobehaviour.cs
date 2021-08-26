using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : Component
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				var objs = FindObjectsOfType(typeof(T)) as T[];
				if (objs.Length > 0)
					instance = objs[0];
				if (objs.Length > 1)
				{
					Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
				}
				if (instance == null)
				{
					GameObject obj = new GameObject();
					obj.hideFlags = HideFlags.HideAndDontSave;
					instance = obj.AddComponent<T>();
				}
			}
			return instance;
		}
	}
}


public class SingletonMonoPersistent<T> : MonoBehaviour where T : Component
{
	public static T Instance { get; private set; }

	public virtual void Awake()
	{
		if (Instance == null)
		{
			Instance = this as T;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
