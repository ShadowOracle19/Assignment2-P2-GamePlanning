using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEncounter : ScriptableObject
{
    public RewardSystem reward;

    public virtual void StartEncounter()
    {
        Debug.Log("Base Encounter");
        return;
    }

    public virtual void GiveReward()
    {
       
        if (reward.numberOfAddedMedkits > 0)
        {
            GameManager.Instance.amountOfMedkits += reward.numberOfAddedMedkits;
            var popup = Instantiate(GameManager.Instance.rewardBasePopup, GameManager.Instance.popupParent);
            popup.GetComponent<RewardPopup>().whatRewardTextSays = ""
        }
        if (reward.numberOfAddedCaps > 0)
        {
            GameManager.Instance.caps += reward.numberOfAddedCaps;
        }
        if (reward.numberOfAddedRations > 0)
        {
            GameManager.Instance.amountOfRations += reward.numberOfAddedRations;
        }
        if(reward.tokensToModify.Length > 0)
        {
            foreach (var token in reward.tokensToModify)
            {
                token.tokenToModify.damageAmount += token.newDamageAmount;
                token.tokenToModify.healingAmount += token.newHealingAmount;
                token.tokenToModify.defendAmount += token.newDefendAmount;

                if (token.newUser != null) token.tokenToModify.character = token.newUser;
                if (token.newSprite != null) token.tokenToModify.icon = token.newSprite;
            }
        }
    }
}
