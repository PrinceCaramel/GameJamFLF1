﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainUI : MonoBehaviour {

	public GameObject linePrefab, periodPrefab;
	public Transform TimelineParent;

	public GameObject ItemRequiredPrefab;
	public Transform ItemRequiredParent;

	public Text Timer;
	private float _timeForLevel;

	private Dictionary<ItemManager.Items, RequiredItemLine> _requiredObjects;

	// Use this for initialization
	void Start ()
	{
		_timeForLevel = 91f;
		this.Timer.text = getTime(_timeForLevel);

		UIManager.Instance.RegisterCanvas(UIManager.UIObjects.MAIN, this.gameObject);
		StartCoroutine("StartProcess");
	}
	
	// Update is called once per frame
	void Update ()
	{
		_timeForLevel -= Time.deltaTime;
		this.Timer.text = getTime(_timeForLevel);
	}

	string getTime(float timer)
	{
		int min = (int)timer / 60;
		int secondes = (int)timer %60;

		string res = (min<10 ? "0" : "") + min + ":" + (secondes<10 ? "0" : "") + secondes;
		return res;
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
			GameObject.FindObjectOfType<PlayerMove>().EndOfLevel();
		}
	}
}
