using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public ActionType currentType;

    public GameObject actionButton;

    public TurnBasedManager turnBasedManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentType != null)
        {
            actionButton.SetActive(true);
        }
        else
        {
            actionButton.SetActive(false);
        }
    }

    public void UseActionToken()
    {
        switch (currentType.type)
        {
            case ActionTypeEnum.Attack:
                Debug.Log("Attack!");
                turnBasedManager.Attack();
                break;

            case ActionTypeEnum.Special:
                Debug.Log("Special move!");
                turnBasedManager.Special();
                break;

            case ActionTypeEnum.Defend:
                Debug.Log("Defend!");
                turnBasedManager.Defend();
                break;

            case ActionTypeEnum.Heal:
                Debug.Log("I need healing!");
                turnBasedManager.Heal();
                break;

            default:
                break;
        }
        Destroy(currentType.gameObject);
    }
}
