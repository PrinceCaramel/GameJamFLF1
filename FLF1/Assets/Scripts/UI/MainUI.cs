using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainUI : MonoBehaviour {
	
	public GameObject linePrefab, periodPrefab;
	public Transform TimelineParent;

	public GameObject ItemRequiredPrefab;
	public Transform ItemRequiredParent;

	public Text Timer;
	public Image TimerIcon;
	private float _timeForLevel;

	public const int TIME_FOR_LEVEL = 120;

	private Dictionary<ItemManager.Items, RequiredItemLine> _requiredObjects;

	// Use this for initialization
	void Start ()
	{
		_timeForLevel = TIME_FOR_LEVEL;
		this.setTime(_timeForLevel);

		UIManager.Instance.RegisterCanvas(UIManager.UIObjects.MAIN, this.gameObject);
		StartCoroutine("StartProcess");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (PlayerMove.Instance.CurrentPlayerState != PlayerMove.PlayerState.DEATH
		    && PlayerMove.Instance.CurrentPlayerState != PlayerMove.PlayerState.WIN)
		{
			this.setTime(_timeForLevel);

			_timeForLevel -= Time.deltaTime;
	        if (this._timeForLevel <= 0)
	        {
				AnimationManager.Instance.SetAction(AnimationManager.ActionAnimation.DEATH);
				this._timeForLevel = 0;
	        }
		}
	}

	void setTime(float timer)
	{
		int min = (int)timer / 60;
		int secondes = (int)timer %60;

		string res = (min<10 ? "0" : "") + min + ":" + (secondes<10 ? "0" : "") + secondes;
		this.Timer.text = res;
		this.Timer.color = new Color( 1, Mathf.Min(1f, timer/30f), Mathf.Min(1f, timer/30f));
		this.TimerIcon.color = new Color( 1, Mathf.Min(1f, timer/30f), Mathf.Min(1f, timer/30f));
	}

	
	IEnumerator StartProcess()
	{
		yield return new WaitForEndOfFrame();
		RefreshTimeline(TimeManager.Instance.NumberOfEras);
		
	}


	public void RefreshTimeline(int count)
	{
		// Destroy old timeline
		for (int cnt = 0; cnt < TimelineParent.childCount; cnt++)
		{
			GameObject.Destroy(TimelineParent.GetChild(cnt).gameObject);
		}

		//create new timeline

		for (int i = count-1 ; i >0; i--)
		{
			GameObject period =	GameObject.Instantiate(periodPrefab);
			period.transform.SetParent(TimelineParent, false);

			GameObject line = GameObject.Instantiate(linePrefab);
			line.transform.SetParent(TimelineParent, false);
		}

		//special case for last period, no line
		{
			GameObject period = GameObject.Instantiate(periodPrefab);
			period.transform.SetParent(TimelineParent, false);
		}
	}



	public void RefreshRequiredItems(Dictionary<ItemManager.Items, List<RequiredItem>> allItems)
	{
        // Destroy old required items
		this._requiredObjects = new Dictionary<ItemManager.Items, RequiredItemLine>();
		for (int cnt = 0; cnt < ItemRequiredParent.childCount; cnt++)
        {
            GameObject.Destroy(ItemRequiredParent.GetChild(cnt).gameObject);
        }

		Debug.Log(allItems.Keys.Count+ " items to collect to end the level");
		foreach(ItemManager.Items key in allItems.Keys)
		{
			GameObject requiredLine = GameObject.Instantiate(ItemRequiredPrefab);
			requiredLine.transform.SetParent(ItemRequiredParent, false);
			requiredLine.GetComponent<RequiredItemLine>().Init(Resources.Load<Sprite>("ItemsIcons/" + key.ToString()), allItems[key].Count);

			this._requiredObjects.Add(key, requiredLine.GetComponent<RequiredItemLine>());
		}
	}

	public void OnItemActivated(ItemManager.Items item)
	{
		if (!this._requiredObjects.ContainsKey(item))
		{
			Debug.LogWarning("Not item " + item.ToString() + " in requiredItems");
			return;
		}

		this._requiredObjects[item].Increment();

		//for each new item activated we try to know if all items have been collected
		bool isComplete = true;
		foreach (RequiredItemLine obj in this._requiredObjects.Values)
		{
			isComplete &= obj.IsComplete;
		}

		if (isComplete)
		{
			AnimationManager.Instance.SetAction(AnimationManager.ActionAnimation.WIN);
			GameObject.FindObjectOfType<PlayerMove>().EndOfLevel();

			this.Timer.color = new Color( 0, 1f, 0);
			this.TimerIcon.color = new Color( 0, 1f, 0);
		}
	}

	public void Reset()
	{
		this._timeForLevel = TIME_FOR_LEVEL;
	}
}