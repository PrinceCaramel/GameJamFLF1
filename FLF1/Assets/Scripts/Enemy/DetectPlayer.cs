using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour {

    GameObject enemy;

	// Use this for initialization
	void Start () {
        enemy = gameObject.transform.parent.FindChild("Enemy").gameObject;

	}

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemy.GetComponent<EnemyMove>().target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemy.GetComponent<EnemyMove>().target = null;
        } 
    }
}
