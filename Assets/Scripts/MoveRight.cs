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

        if(itemInFront != null)
        {
            speed = 0;
        }
        else
        {
            speed = speedMax;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!itemBlocking)
        {
            itemInFront = collision.gameObject;
            itemBlocking = true;

            if (!itemInFront.CompareTag("Token"))
                return;

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
            //speed = speedMax;
            itemBlocking = false;
            itemInFront = null;
        }
    }
}
