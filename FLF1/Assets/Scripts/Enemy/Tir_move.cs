using UnityEngine;
using System.Collections;

public class Tir_move : MonoBehaviour {

	public float direction= 1f;
	public float speed = 3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (direction * speed* Time.deltaTime,0f,0f);
	}

	void OnTriggerEnter2D(Collider2D otherObj) {
		if (otherObj.gameObject.tag == "Player") {
			print ("joueur touché");
			Destroy(this.gameObject);
			AnimationManager.Instance.SetAction(AnimationManager.ActionAnimation.DEATH);
        }
    }
}
