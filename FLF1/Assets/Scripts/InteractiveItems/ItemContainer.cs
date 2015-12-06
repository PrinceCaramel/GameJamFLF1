using UnityEngine;
using System.Collections;

public class ItemContainer : MonoBehaviour {

	public GameObject[] PeriodItems;

	//can be used as an identifier
	protected ItemManager.Items _relativeItem;
	public ItemManager.Items RelativeItem { get { return _relativeItem; } }


	// Use this for initialization
	void Start () {
		TimeManager.OnTimeChange += ChangingTime;
		ChangingTime(0);

		initialize();

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

	protected virtual void initialize()
	{
		//this must be done AFTER sub-class set its _relativeMenu identifier
		if (_relativeItem != ItemManager.Items.NONE)
		{
			ItemManager.Instance.RegisterItem(_relativeItem, this);
		}
	}
}
