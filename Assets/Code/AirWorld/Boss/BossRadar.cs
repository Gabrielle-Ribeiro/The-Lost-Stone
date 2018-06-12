using System.Collections;
using UnityEngine;

public class BossRadar : MonoBehaviour
{

    private SecBoss script;

    // Use this for initialization
    void Start()
    {
        script = (SecBoss)GetComponentInParent(typeof(SecBoss));
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
        if (collision.gameObject.name == "MainChar")
        {
            script.BacktoHome();
            script.lostPlayer = true;
        }
    }

}
