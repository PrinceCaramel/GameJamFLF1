using UnityEngine;
using System.Collections;

public class ItemContainer : MonoBehaviour {

	public GameObject[] PeriodItems;

	// Use this for initialization
	void Start () {
		TimeManager.OnTimeChange += ChangingTime;

		ChangingTime(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ChangingTime(int newPeriod)
	{
		for (int i=0; i< PeriodItems.Length; i++)
		{
			PeriodItems[i].SetActive(i==TimeManager.Instance.CurrentTime);
		}
	}
}
