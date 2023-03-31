using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reserve : MonoBehaviour
{
    public ReadTokenValue currentToken;

    public GameObject square;
    public Sprite noToken;
    public Sprite hasToken;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentToken != null)
        {
            square.GetComponent<SpriteRenderer>().sprite = hasToken;

        }
        else
        {
            square.GetComponent<SpriteRenderer>().sprite = noToken;
        }
    }
}
