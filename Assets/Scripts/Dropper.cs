using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public ActionType currentType;


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
            turnBasedManager.player.ATBSlider.value += Time.deltaTime * turnBasedManager.player.ATBSpeed;

        }
        if(turnBasedManager.player.ATBSlider.value == turnBasedManager.player.ATBSlider.maxValue)
        {
            switch (currentType.type)
            {
                case ActionTypeEnum.Attack:
                    turnBasedManager.player.Attack(turnBasedManager.player.attack, turnBasedManager.enemy);
                    DestroyToken();


                    break;

                case ActionTypeEnum.Special:
                    turnBasedManager.player.Attack(turnBasedManager.player.attack * 2, turnBasedManager.enemy);
                    DestroyToken();


                    break;

                case ActionTypeEnum.Defend:
                    turnBasedManager.player.Defend(turnBasedManager.player);
                    DestroyToken();


                    break;

                case ActionTypeEnum.Heal:
                    turnBasedManager.player.Heal(turnBasedManager.player, turnBasedManager.player.heal);
                    DestroyToken();

                    break;

                default:
                    turnBasedManager.player.ATBSlider.value = turnBasedManager.player.ATBSlider.minValue;
                    break;
            }
        }
        
    }

    public void DestroyToken()
    {
        Destroy(currentType.gameObject);
        currentType = null;
        turnBasedManager.player.ATBSlider.value = turnBasedManager.player.ATBSlider.minValue;

    }

    public void DropToken(ActionType token)
    {
        if(currentType != null)
        {
            DestroyToken();
        }
        currentType = token;
    }
}
