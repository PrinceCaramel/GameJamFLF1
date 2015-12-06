using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PopupSimple : MonoBehaviour {

    public RectTransform popup;
    public Text txt_message;

    public List<string> msgs = new List<string>();

    public float duration;
    float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > Time.time)
            return;
        else
        {
            if (msgs.Count > 1)
            {
                //Open next
                msgs.RemoveAt(0);
                OpenPopup();
            }
            else
                ClosePopup();
        }
   	}

    public void OpenPopup ()
    {
        if (msgs.Count == 0)
            return;

        txt_message.text = msgs[0];
        timer = Time.time + duration;
        popup.gameObject.SetActive(true);
    }

    public void ClosePopup()
    {
        popup.gameObject.SetActive(false);
    }
}
