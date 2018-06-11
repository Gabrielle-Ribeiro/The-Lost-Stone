using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {
    // Metodo criado para poder destruir a Pool a partir da animação
    void destroy()
    {
        DestroyObject(gameObject);
    }
}
