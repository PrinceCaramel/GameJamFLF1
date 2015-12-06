using UnityEngine;
using System.Collections;
//using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {

	private static ItemManager _instance;
	public static ItemManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<ItemManager>();
			}
			if (_instance == null)
			{
				_instance = new GameObject().AddComponent<ItemManager>();
			}
			return _instance;
		}
	}

	public enum Items { NONE, POMME, CAROTTE }

    public Dictionary<Items, List<ItemContainer>> _itemsInScene; 
	public Dictionary<Items, List<RequiredItem>> _requiredItems; 

	void Awake()
	{
		_itemsInScene = new Dictionary<Items, List<ItemContainer>>();
		_requiredItems = new Dictionary<Items, List<RequiredItem>>();
	}

	public void RegisterItem(Items item, ItemContainer go, bool isRequired = false)
	{
		if (!this._itemsInScene.ContainsKey(item))
		{
			this._itemsInScene.Add(item, new List<ItemContainer>());
		}
		this._itemsInScene[item].Add(go);

		//if item is required to end level, then add it to the dictionnary (create space for it if needed)
		if (isRequired)
		{
			if (!this._requiredItems.ContainsKey(item))
			{
				this._requiredItems.Add(item, new List<RequiredItem>());
			}
			this._requiredItems[item].Add((RequiredItem)go);
		}
	}

	void Start()
	{
		StartCoroutine("StartProcess");
	}
	
	IEnumerator StartProcess()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();

		UIManager.Instance.GetCanvas(UIManager.UIObjects.MAIN).GetComponent<MainUI>().RefreshRequiredItems(this._requiredItems);
	}
}
