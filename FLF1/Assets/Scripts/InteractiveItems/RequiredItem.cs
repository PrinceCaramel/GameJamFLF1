using UnityEngine;
using System.Collections;

public class RequiredItem : ItemContainer
{
	public ItemManager.Items MyItem;

	protected override void initialize()
	{
		this._relativeItem = MyItem;
		this.isRequired = true;
		base.initialize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Activate()
	{
		UIManager.Instance.GetCanvas(UIManager.UIObjects.MAIN).GetComponent<MainUI>().OnItemActivated(this._relativeItem);
		Destroy(this.gameObject);
	}
}
