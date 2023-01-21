using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public ActionType currentType;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentType != null)
        {
            TurnBasedManager.Instance.player.ATBSlider.value += Time.deltaTime * TurnBasedManager.Instance.player.ATBSpeed;

        }
        if(TurnBasedManager.Instance.player.ATBSlider.value == TurnBasedManager.Instance.player.ATBSlider.maxValue)
        {
            switch (currentType.type)
            {
                case ActionTypeEnum.Attack:
                    if (TurnBasedManager.Instance.targetedEnemy == null) return;
                    TurnBasedManager.Instance.player.Attack(currentType.effectNum, TurnBasedManager.Instance.targetedEnemy);
                    DestroyToken();


                    break;

                case ActionTypeEnum.Special:
                    if (TurnBasedManager.Instance.targetedEnemy == null) return;
                    TurnBasedManager.Instance.player.Attack(currentType.effectNum, TurnBasedManager.Instance.targetedEnemy);
                    DestroyToken();


                    break;

                case ActionTypeEnum.Defend:
                    TurnBasedManager.Instance.player.Defend(currentType.effectNum, TurnBasedManager.Instance.player);
                    DestroyToken();


                    break;

                case ActionTypeEnum.Heal:
                    TurnBasedManager.Instance.player.Heal(TurnBasedManager.Instance.player, currentType.effectNum);
                    DestroyToken();

                    break;

                default:
                    TurnBasedManager.Instance.player.ATBSlider.value = TurnBasedManager.Instance.player.ATBSlider.minValue;
                    break;
            }
        }
        
    }

    public void DestroyToken()
    {
        Destroy(currentType.gameObject);
        currentType = null;
        TurnBasedManager.Instance.player.ATBSlider.value = TurnBasedManager.Instance.player.ATBSlider.minValue;

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
