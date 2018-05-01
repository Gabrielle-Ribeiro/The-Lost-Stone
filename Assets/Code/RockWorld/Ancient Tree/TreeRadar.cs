using System.Collections;
using UnityEngine;

public class TreeRadar : MonoBehaviour {

    private TreeIA script;

	// Use this for initialization
	void Start () {
        script = (TreeIA)GetComponentInParent(typeof(TreeIA));
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MainChar")
        {
            script.lostPlayer = false;
            script.canWalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MainChar")
        {
            script.BacktoHome();
            script.lostPlayer = true;
            script.canWalk = true;	
        }
    }

}
