using UnityEngine;
using System.Collections;

public class RequiredItem : ItemContainer
{
	protected override void initialize()
	{
		this.isRequired = true;
		base.initialize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Activate()
	{
		UIManager.Instance.GetCanvas(UIManager.UIObjects.MAIN).GetComponent<MainUI>().OnItemActivated(this.RelativeItem);
		Destroy(this.gameObject);
	}
}
