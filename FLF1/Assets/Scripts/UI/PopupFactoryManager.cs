using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PopupFactoryManager : MonoBehaviour {

	public enum IconType { VALIDATE, WARNING, ERROR, NONE }
	public enum AnimationEffectType { FADE, SIMPLE_SLIDE }
	
	public int EnablePopupCounter = 0;
	
	public static List<StandardPopupAttributes> PopupPile = new List<StandardPopupAttributes>();

	public StandardPopup OkWindow;
    public PopupSimple AchievePopup;

    public Texture ValidateIcon, WarningIcon, ErrorIcon;

	// instance of singleton
	private static PopupFactoryManager _instance = null;
	
	// getting the singleton
	public static PopupFactoryManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<PopupFactoryManager>();
			}
			return _instance;
		}
	}
	
	// constructor in private to avoid a construction
	private PopupFactoryManager(){}
	
	// killing the singleton
	public static void Kill()
	{
		_instance = null;
	}

//	void Update()
//	{
//		if(Input.GetKeyDown(KeyCode.P))
//		{
//			InvokeSimplePopup("TEST","dsmq dlsqjdlqdj lqsdj qlsd jql djqlsd");
//		}
//
//		if(Input.GetKeyDown(KeyCode.O))
//		{
//			InvokeYesNoPopup("TEST2","dsmq dlsqjdlqdj lqsdj qlsd jql djqlsd", TestEvent);
//		}
//	}

	void Start()
	{
		this.OkWindow.gameObject.SetActive(false);
        this.AchievePopup.gameObject.SetActive(false);
    }

	void TestEvent()
	{
		Debug.Log("TestEvent !!!!");
	}

	public void InvokeOKPopup(string innerText, StandardPopupAttributes.OkEvent EventMethod = null, AnimationEffectType effectType = AnimationEffectType.FADE)
	{
		StandardPopupAttributes attributes = new StandardPopupAttributes();
		
		attributes.TypeOfPopup = StandardPopupAttributes.PopupType.OK_WINDOW;
		attributes.Content = innerText;
		attributes.EffectType = effectType;
		attributes.OkClickEvent += EventMethod;

		if(!IsAPopupActive())
		{
			OkWindow.gameObject.SetActive(true);
			
			OkWindow.Attributes = attributes;
		}
		
		if(PopupPile.Count == 0)
		{
			OkWindow.LaunchPopup();
			EnablePopupCounter++;
		}
		PopupPile.Add(attributes);
	}
    


	/// <summary>
	/// Launchs the popup from factory.
	/// </summary>
	/// <param name="attributes">Attributes.</param>
	public void LaunchPopupFromFactory(StandardPopupAttributes attributes)
	{
		StandardPopup popup = GetPopupFromType(attributes.TypeOfPopup);

		popup.gameObject.SetActive(true);

		popup.Attributes = attributes;
		popup.LaunchPopup();
	}

	public void ValidateCurrentPopup()
	{
		OkWindow.OkButtonActivation();
	}


	/// <summary>
	/// Get a popup from his type.
	/// </summary>
	/// <returns>The popup from type.</returns>
	/// <param name="type">Type.</param>
	public StandardPopup GetPopupFromType(StandardPopupAttributes.PopupType type)
	{
		StandardPopup returnPopup = OkWindow;

		switch(type)
		{
			case StandardPopupAttributes.PopupType.OK_WINDOW : returnPopup = OkWindow;
				break;
		}

		return returnPopup;
	}

	/// <summary>
	/// Determines if we have a popup active.
	/// </summary>
	/// <returns><c>true</c> if this instance have a popup active; otherwise, <c>false</c>.</returns>
	public bool IsAPopupActive()
	{
		bool isActive = false;

		if(OkWindow.gameObject.activeSelf)
		{
			isActive = true;
		}

		return isActive;
	}

	public void CloseStandardPopup(StandardPopup popup)
	{
		popup.gameObject.SetActive(false);
		
		EnablePopupCounter--;
		
		PopupPile.RemoveAt(0);
		
		if(PopupPile.Count != 0)
		{
			LaunchPopupFromFactory(PopupPile[0]);
			EnablePopupCounter++;
		}
	}
}
