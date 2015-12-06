﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainUI : MonoBehaviour {

	public GameObject linePrefab, periodPrefab;
	public Transform TimelineParent;

	public GameObject ItemRequiredPrefab;
	public Transform ItemRequiredParent;

	public Text Timer;
	private float _timeForLevel;


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


	IEnumerator StartProcess()
	{
		yield return new WaitForEndOfFrame();
		RefreshTimeline(TimeManager.Instance.NumberOfEras);

	}
}