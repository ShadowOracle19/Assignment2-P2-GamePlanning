using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    private float speed = 0.5f;
    public float speedMax = 0.5f;

    public GameObject itemInFront;
    private bool itemBlocking = false;


    // Start is called before the first frame update
    void Start()
    {
        speed = speedMax;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speed = 0;
        if(!itemBlocking)
        {
            itemInFront = collision.gameObject;
            itemBlocking = true;

            if(itemInFront.GetComponent<ActionType>().type == GetComponent<ActionType>().type)
            {
                //Combine
                itemInFront.GetComponent<ActionType>().CombineActionType(GetComponent<ActionType>().effectNum);
                FindObjectOfType<ConveyorManager>().spawnedActionTokens.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == itemInFront)
        {
            speed = speedMax;
            itemBlocking = false;
        }
    }
}
