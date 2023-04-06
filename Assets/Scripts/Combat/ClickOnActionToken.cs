using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnActionToken : MonoBehaviour
{
    private MoveRight movementScript;

    public ConveyorManager manager;
    public Dropper drop;
    public Reserve reserve;

    public GameObject toolTip;

    //public void OnMouseDown()
    //{
    //    movementScript.enabled = false;
    //    manager.spawnedActionTokens.Remove(gameObject);
    //    transform.position = drop.gameObject.transform.position;
    //    drop.DropToken(GetComponent<ReadTokenValue>());
    //}

    public void OnMouseOver()
    {
        if (GameManager.Instance.isGamePaused) return;
        toolTip.gameObject.SetActive(true);

        if(Input.GetMouseButtonDown(1))
        {
            movementScript.enabled = false;
            manager.spawnedActionTokens.Remove(gameObject);
            transform.position = reserve.gameObject.transform.position;

            if(reserve.currentToken != null)
            {
                Destroy(reserve.currentToken.gameObject);
                reserve.currentToken = null;
            }

            reserve.currentToken = GetComponent<ReadTokenValue>();
        }
        else if(Input.GetMouseButtonDown(0))
        {
            movementScript.enabled = false;
            manager.spawnedActionTokens.Remove(gameObject);
            transform.position = drop.gameObject.transform.position;
            drop.DropToken(GetComponent<ReadTokenValue>());
        }
    }

    private void OnMouseExit()
    {
        toolTip.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<MoveRight>();
    }

    private void Update()
    {
        toolTip.GetComponent<ToolTipPopup>().tokenText.sprite = GetComponent<ReadTokenValue>().currentToken.tokentext;
        toolTip.GetComponent<ToolTipPopup>().tokenStroke.sprite = GetComponent<ReadTokenValue>().currentToken.tokenStroke;
        toolTip.GetComponent<ToolTipPopup>().tokenDescription.text = GetComponent<ReadTokenValue>().currentToken.tooltipInfo;
    }
}
