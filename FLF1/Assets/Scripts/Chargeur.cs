using UnityEngine;
using System.Collections;

public class Chargeur : MonoBehaviour {

    private GameObject target;
    private bool charging;

    public float speed = 10f;
    public float range = 5f;
    public float waitBeforeCharge = 10f;

    public int direction = -1;

    private Animator anim;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        charging = false;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!charging)
        {
            if (checkPlayer())
            {
                charging = true;
                StartCoroutine("charge");
            }
        }
	}

    IEnumerator charge()
    {
        anim.SetInteger("status", 2);
        yield return new WaitForSeconds(waitBeforeCharge);
        if (!checkPlayer())
        {
            anim.SetInteger("status", 1);
            charging = false;
        }
        else
        {
            anim.SetInteger("status", 3);
            yield return new WaitForSeconds(0.3f);
            //On court jusqu'à trouver un obstacle
            Collider2D obstacle;
            while ((obstacle = Physics2D.Raycast(transform.position, Vector2.right * direction, 1f).collider) == null)
            {
                transform.Translate(new Vector2(direction * speed * Time.deltaTime, 0));
                
                yield return null;
            }

            if (obstacle.gameObject.tag == "Player")
            {
                AnimationManager.Instance.SetAction(AnimationManager.ActionAnimation.DEATH);
            }

            // Fin du déplacement et animation de fin
            anim.SetInteger("status", 4);
            while (Physics2D.Raycast(transform.position, Vector2.right * direction, 0.5f).collider == null)
            {
                transform.Translate(new Vector2(direction * speed * Time.deltaTime, 0));

                yield return null;
            }
        }
    }

    //Recherche du player
    private bool checkPlayer()
    {
        return Vector3.Distance(transform.position, target.transform.position) < range;
    }

}
