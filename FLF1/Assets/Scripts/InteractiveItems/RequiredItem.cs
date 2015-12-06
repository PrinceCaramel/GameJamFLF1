using UnityEngine;
using System.Collections;

public class RequiredItem : ItemContainer
{
	public ItemManager.Items MyItem;

	protected override void initialize()
	{
		this._relativeItem = MyItem;
		base.initialize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
