using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnActionToken : MonoBehaviour
{
    private MoveRight movementScript;

    public ConveyorManager manager;
    public Dropper drop;

    public void OnMouseDown()
    {
        movementScript.enabled = false;
        manager.spawnedActionTokens.Remove(gameObject);
        transform.position = drop.gameObject.transform.position;
        drop.DropToken(GetComponent<ReadTokenValue>());
    }


    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<MoveRight>();
    }
}
