using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubNPC : MonoBehaviour {

	public GameObject infoBulle;
	public List<string> dialogs;
	public string passiveDialog;
	private bool hasAlreadySpoken = false;

	private int currentDialog = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			PlayerMove.PlayerAction += OnAction;
			infoBulle.SetActive(true);
		}		
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			PlayerMove.PlayerAction -= OnAction;
			infoBulle.SetActive(false);
		}		
	}


	private void OnAction()
	{
		if (hasAlreadySpoken || dialogs.Count==0)
		{
			if (passiveDialog == "")
			{
				throw new UnityException("NPC with no dialog ! " + this.transform.parent.name);
            }
			else
			{
				PopupFactoryManager.Instance.InvokeOKPopup(passiveDialog, null, PopupFactoryManager.AnimationEffectType.FADE);
            }
		}
		else
		{
			currentDialog = 0;
			hasAlreadySpoken = true;

			PopupFactoryManager.Instance.InvokeOKPopup(dialogs[currentDialog], OnNextDialog, PopupFactoryManager.AnimationEffectType.FADE);
		}

	}

	private void OnNextDialog()
	{

		currentDialog++;
		Debug.Log(currentDialog);
		if (currentDialog < dialogs.Count)
		{
			if (dialogs[currentDialog].StartsWith("COMMAND_"))
			{
				Debug.Log ("SPECIAL ACTION : " + dialogs[currentDialog].Substring(8));
				switch (dialogs[currentDialog].Substring(8))
				{
				case "ADD_PERIOD" :
					TimeManager.Instance.AddEra();
					break;
				case "PREVIOUS_ERA" :
					TimeManager.Instance.PreviousTime();
					break;
				case "NEXT_ERA" :
					TimeManager.Instance.NextTime();
					break;
				}

				OnNextDialog();
			}
			else
			{
				PopupFactoryManager.Instance.InvokeOKPopup(dialogs[currentDialog], OnNextDialog, PopupFactoryManager.AnimationEffectType.FADE);
			}
		}
	}
}
