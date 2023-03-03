using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public ReadTokenValue currentToken;
    public ConveyorManager conveyorManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentToken != null)
        {
            if((TurnBasedManager.Instance.currentOnScreenCharacter != currentToken.currentToken.character))
            {
                TurnBasedManager.Instance.nextPlayerSprite.sprite = currentToken.currentToken.character.neutral;
                TurnBasedManager.Instance.nextPlayerNameplate.sprite = currentToken.currentToken.character.namePlate;
                TurnBasedManager.Instance.currentPlayerName.text = currentToken.currentToken.character.name;
                TurnBasedManager.Instance.nextScreenCharacter = currentToken.currentToken.character;
                TurnBasedManager.Instance.combatAnim.SetBool("SwapSprite", true);//SwapSprite
            }

            TurnBasedManager.Instance.player.ATBSlider.value += Time.deltaTime * TurnBasedManager.Instance.player.ATBSpeed;

        }
        if(TurnBasedManager.Instance.player.ATBSlider.value == TurnBasedManager.Instance.player.ATBSlider.maxValue)
        {
            if ((TurnBasedManager.Instance.targetedEnemy != null || currentToken.currentToken.isAoe) && currentToken.currentToken.damageAmount > 0)
            {
                TurnBasedManager.Instance.combatAnim.SetBool("Attacking", true);
                TurnBasedManager.Instance.player.Attack(currentToken.currentToken.damageAmount, TurnBasedManager.Instance.targetedEnemy, currentToken.currentToken.isAoe);
                //TurnBasedManager.Instance.combatAnim.SetBool("Attacking", false);
            }
            TurnBasedManager.Instance.player.Defend(currentToken.currentToken.defendAmount, TurnBasedManager.Instance.player);
            TurnBasedManager.Instance.player.Heal(TurnBasedManager.Instance.player, currentToken.currentToken.healingAmount);
            if(currentToken.currentToken.isChangeStance)
            {

                conveyorManager.DestroyTokens();
                conveyorManager.isPiercing = !conveyorManager.isPiercing;
            }
            DestroyToken();
        }
        
    }

    public void DestroyToken()
    {
        TelemetryLogger.Log(this, "Token Used", currentToken.currentToken.name);
        Destroy(currentToken.gameObject);
        currentToken = null;
        TurnBasedManager.Instance.player.ATBSlider.value = TurnBasedManager.Instance.player.ATBSlider.minValue;


    }

    public void DropToken(ReadTokenValue token)
    {
        if(currentToken != null)
        {
            DestroyToken();
        }
        currentToken = token;
        TurnBasedManager.Instance.player.ATBSlider.value = TurnBasedManager.Instance.player.ATBSlider.minValue;
        TurnBasedManager.Instance.combatAnim.SetBool("SwapSprite", false);//SwapSprite
    }
}
