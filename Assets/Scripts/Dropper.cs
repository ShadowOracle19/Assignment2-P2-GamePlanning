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
        if(currentToken != null)
        {
            
            if((TurnBasedManager.Instance.currentOnScreenCharacter != currentToken.currentToken.character))
            {
                TurnBasedManager.Instance.nextPlayerSprite.sprite = currentToken.currentToken.character.neutral;
                TurnBasedManager.Instance.currentPlayerName.text = currentToken.currentToken.character.name;
                TurnBasedManager.Instance.nextScreenCharacter = currentToken.currentToken.character;
                TurnBasedManager.Instance.combatAnim.SetBool("SwapSprite", true);
            }

            TurnBasedManager.Instance.player.ATBSlider.value += Time.deltaTime * TurnBasedManager.Instance.player.ATBSpeed;

        }
        if(TurnBasedManager.Instance.player.ATBSlider.value == TurnBasedManager.Instance.player.ATBSlider.maxValue)
        {
            if (TurnBasedManager.Instance.targetedEnemy != null)
            {
                
                TurnBasedManager.Instance.player.Attack(currentToken.currentToken.damageAmount, TurnBasedManager.Instance.currentPlayerSprite.GetComponent<Animator>(), TurnBasedManager.Instance.targetedEnemy, currentToken.currentToken.isAoe);
            }
            TurnBasedManager.Instance.player.Defend(currentToken.currentToken.defendAmount, TurnBasedManager.Instance.player);
            TurnBasedManager.Instance.player.Heal(TurnBasedManager.Instance.player, currentToken.currentToken.healingAmount);
            if(currentToken.currentToken.isChangeStance)
            {
                conveyorManager.isPiercing = !conveyorManager.isPiercing;
            }
            DestroyToken();
        }
        
    }

    public void DestroyToken()
    {
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
    }
}
