using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private MoveRight movementScript;

    public bool isDragging;
    private bool isTouching;

    private Collider2D _collider;

    public ConveyorManager manager;

    public void OnMouseDown()
    {
        isDragging = true;
        movementScript.enabled = false;
        _collider.enabled = false;
        manager.spawnedActionTokens.Remove(gameObject);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        _collider.enabled = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<MoveRight>();   
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Dropper")
        {
            transform.position = collision.transform.position;
            transform.parent = collision.transform;

            if(collision.GetComponent<Dropper>().currentType != null)
            {
                Destroy(collision.GetComponent<Dropper>().currentType.gameObject);
            }

            collision.GetComponent<Dropper>().currentType = gameObject.GetComponent<ActionType>() ;
        }
    }

}
