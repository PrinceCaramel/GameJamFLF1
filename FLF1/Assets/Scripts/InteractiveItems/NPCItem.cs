using UnityEngine;
using System.Collections;

public class NPCItem : ItemContainer {

	protected override void initialize()
	{
		this.isRequired = false;
		base.initialize();
	}
	 
}
