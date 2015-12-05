using UnityEngine;
using System.Collections;

public class TimelineUI : MonoBehaviour {

	public GameObject linePrefab, periodPrefab;

	public Transform TimelineParent;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine("StartProcess");
	}
	
	// Update is called once per frame
	void Update () {
	
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
//		int tmp = TimeManager.Instance.CurrentTime;
		yield return new WaitForEndOfFrame();
		RefreshTimeline(TimeManager.Instance.NumberOfEras);

	}
}
