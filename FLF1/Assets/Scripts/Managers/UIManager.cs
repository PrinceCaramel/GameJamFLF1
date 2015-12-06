using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

		this._canvas = new Dictionary<UIObjects, GameObject>();
		
		for (int i =0; i< this.transform.childCount; i++)
		{
			this.transform.GetChild(i).gameObject.SetActive(true);
		}
	}
	#endregion

	public enum UIObjects { MAIN }

	//LIST ALL CANVAS HERE
	private Dictionary<UIObjects, GameObject> _canvas;


	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void RegisterCanvas(UIObjects obj, GameObject go)
	{
		this._canvas.Add(obj, go);
	}

	public GameObject GetCanvas(UIObjects obj)
	{
		if (this._canvas.ContainsKey(obj))
		{
			return this._canvas[obj];
		}

		return null;
	}
	
}
