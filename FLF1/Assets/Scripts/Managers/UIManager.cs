using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	#region Instance && Awake()
	// instance of singleton
	private static UIManager _instance = null;
	
	// getting the singleton
	public static UIManager Instance
	{
		get
		{
			// check if the instance is null but we shouldn't reach this point
			if(_instance == null)
			{
				if (GameObject.FindObjectOfType<UIManager>() == null)
				{
					throw new UnityException("No UIManager in scene");
				}
				else
				{
					_instance = GameObject.FindObjectOfType<UIManager>();
				}
			}
			
			return _instance;
		}
	}

	
	// killing the singleton
	public static void Kill()
	{
		_instance = null;
	}
	
	void Awake ()
	{
		// retrieve the correct gameobject
		Object[] instance = GameObject.FindObjectsOfType(typeof(UIManager));
		if(instance.Length > 1)
		{
			// destroy it if there is more than one easyaccessresources
			Destroy(gameObject);
		}
		else
		{
			// set the _instance variable
			_instance = (UIManager)instance[0];
			DontDestroyOnLoad(this.gameObject);
		}
	}
	#endregion

	//LIST ALL CANVAS HERE
	public GameObject Timeline;

	// Use this for initialization
	void Start () {
		Timeline.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
