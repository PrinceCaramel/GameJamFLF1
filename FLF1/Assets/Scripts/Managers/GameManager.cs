using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System.IO;

public class GameManager : MonoBehaviour {

    #region Instance && Awake()
	// instance of singleton
	private static GameManager _instance = null;
	
	// getting the singleton
	public static GameManager Instance
	{
		get
		{
			// check if the instance is null but we shouldn't reach this point
			if(_instance == null)
			{
				_instance = new GameObject("GameManager").AddComponent<GameManager>();
			}
			
			return _instance;
		}
	}
	
	// constructor in private to avoid a construction
    private GameManager()
	{
		
	}
	
	// killing the singleton
	public static void Kill()
	{
		_instance = null;
	}
	
	void Awake ()
	{
		// retrieve the correct gameobject
        Object[] instance = GameObject.FindObjectsOfType(typeof(GameManager));
		if(instance.Length > 1)
		{
			// destroy it if there is more than one easyaccessresources
			Destroy(gameObject);
		}
		else
		{
			// set the _instance variable
            _instance = (GameManager)instance[0];
			DontDestroyOnLoad(this.gameObject);
		}
	}
	#endregion

    static List<string> sceneList;
    static int currentScene;

	// Use this for initialization
	void Start () {
	    sceneList = GetSceneNames();
	    
        foreach(string scene in sceneList){
            Debug.Log(scene);
        }
        currentScene = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private static List<string> GetSceneNames()
    {
        // Collect the names of all levels in the build settings.
        return (from buildSettingsScene in EditorBuildSettings.scenes
                where buildSettingsScene.enabled
                select buildSettingsScene.path.Substring(buildSettingsScene.path.LastIndexOf(Path.AltDirectorySeparatorChar) + 1)
                    into name
                    select name.Substring(0, name.Length - 6)).ToList();
    }

    public static void NextScene()
    {
        if (currentScene < sceneList.Count)
        {
            currentScene += 1;
        }
        else
        {
            currentScene = 0;
        }

        Application.LoadLevel(currentScene);

        PlayerMove.Instance.Reset();
    }
}
