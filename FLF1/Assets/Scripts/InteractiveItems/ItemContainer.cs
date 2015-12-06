using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemContainer : MonoBehaviour {

	[System.Serializable]
	public struct ItemPeriode{public int Periode;public GameObject Item;}
	public ItemPeriode[] PeriodItems;

	//can be used as an identifier
	public ItemManager.Items RelativeItem;

	protected bool isRequired = false;


	// Use this for initialization
	void Start () {
		TimeManager.OnTimeChange += ChangingTime;
		ChangingTime(0);

		initialize();

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void ChangingTime(int newPeriod)
	{

		for (int i=0; i< PeriodItems.Length; i++)
		{
			if(PeriodItems[i].Item != null)
				PeriodItems[i].Item.SetActive(PeriodItems[i].Periode == TimeManager.Instance.CurrentTime);
		}
	}

	protected virtual void initialize()
	{
		//this must be done AFTER sub-class set its _relativeMenu identifier
		if (RelativeItem != ItemManager.Items.NONE)
		{
			ItemManager.Instance.RegisterItem(RelativeItem, this, isRequired);
		}
	}


}
