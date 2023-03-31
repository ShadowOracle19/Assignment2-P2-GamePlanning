using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSquare : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Token")
        {
            Destroy(other.gameObject);
        }
    }
}
