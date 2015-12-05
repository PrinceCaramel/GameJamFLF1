using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	#region Instance && Awake()
	// instance of singleton
	private static TimeManager _instance = null;
	
	// getting the singleton
	public static TimeManager Instance
	{
		get
		{
			// check if the instance is null but we shouldn't reach this point
			if(_instance == null)
			{
				_instance = new GameObject("TimeManager").AddComponent<TimeManager>();
			}
			
			return _instance;
		}
	}
	
	// constructor in private to avoid a construction
	private TimeManager()
	{
		this._currentTime = 0;
	}
	
	// killing the singleton
	public static void Kill()
	{
		_instance = null;
	}
	
	void Awake ()
	{
		// retrieve the correct gameobject
		Object[] instance = GameObject.FindObjectsOfType(typeof(TimeManager));
		if(instance.Length > 1)
		{
			// destroy it if there is more than one easyaccessresources
			Destroy(gameObject);
		}
		else
		{
			// set the _instance variable
			_instance = (TimeManager)instance[0];
			DontDestroyOnLoad(this.gameObject);
		}
	}
	#endregion

	public const int NUMBER_OF_ERAS_AT_START = 1;

	public delegate void TimeChange(int newEre);
	public static event TimeChange OnTimeChange;

	private int _currentTime = 0;
	private int _numberOfEras;

	public int CurrentTime { get { return this._currentTime; } }
	public int NumberOfEras { get { return this._numberOfEras; } }


	// Use this for initialization
	void Start ()
	{
		_currentTime = 0;
		_numberOfEras = NUMBER_OF_ERAS_AT_START;
	}
	
	public void NextTime()
	{
		_currentTime--;
		if (_currentTime < 0)
		{
			_currentTime = _numberOfEras -1;
		}
	
		if (OnTimeChange != null)	{ OnTimeChange(_currentTime); }

		Debug.Log("going on next Era : " + this._currentTime);

	}

	public void PreviousTime()
	{
		_currentTime++;
		if (_currentTime >= _numberOfEras)
		{
			_currentTime = 0;
		}

		if (OnTimeChange != null)	{ OnTimeChange(_currentTime); }

		Debug.Log("going on previous Era : " + this._currentTime);
	}

	//FIXME usefull ?
	public void AddEra()
	{
		_numberOfEras++;
		UIManager.Instance.Timeline.GetComponent<TimelineUI>().RefreshTimeline(this._numberOfEras);
	}

	//FIXME usefull ?
	public void SetNumberOfEras(int count)
	{
		_numberOfEras = count;
		UIManager.Instance.Timeline.GetComponent<TimelineUI>().RefreshTimeline(this._numberOfEras);
	}

	// FIXME TEMPFIX
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.W))
		{
			AddEra();
		}
	}
}
