using System.Collections;
using UnityEngine;

public class TreeRadar : MonoBehaviour {

    private AncientTree script;

	// Use this for initialization
	void Start () {
        script = (AncientTree)GetComponentInParent(typeof(AncientTree));
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MainChar")
        {
            script.lostPlayer = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            script.lostPlayer = true;
            script.canWalk = true;
        }
    }

}
