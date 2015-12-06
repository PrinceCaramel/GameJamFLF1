using UnityEngine;
using System.Collections;

public class RequiredSubitem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			this.transform.parent.GetComponent<RequiredItem>().Activate();
		}		
	}
}
