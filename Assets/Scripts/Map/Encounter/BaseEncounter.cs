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
            popup.GetComponent<RewardPopup>().whatRewardTextSays = reward.numberOfAddedMedkits + " Medkits";
            popup.GetComponent<RewardPopup>().rewardIcon.sprite = reward.medkitSprite;
        }
        if (reward.numberOfAddedCaps > 0)
        {
            GameManager.Instance.caps += reward.numberOfAddedCaps;
            var popup = Instantiate(GameManager.Instance.rewardBasePopup, GameManager.Instance.popupParent);
            popup.GetComponent<RewardPopup>().whatRewardTextSays = reward.numberOfAddedMedkits + " Caps";
            popup.GetComponent<RewardPopup>().rewardIcon.sprite = reward.capSprite;
        }
        if (reward.numberOfAddedRations > 0)
        {
            GameManager.Instance.amountOfRations += reward.numberOfAddedRations;
            var popup = Instantiate(GameManager.Instance.rewardBasePopup, GameManager.Instance.popupParent);
            popup.GetComponent<RewardPopup>().whatRewardTextSays = reward.numberOfAddedMedkits + " Rations";
            popup.GetComponent<RewardPopup>().rewardIcon.sprite = reward.rationSprite;
        }
        if(reward.tokensToModify.Length > 0)
        {
            foreach (var token in reward.tokensToModify)
            {
                var popup = Instantiate(GameManager.Instance.rewardBasePopup, GameManager.Instance.popupParent);
                popup.GetComponent<RewardPopup>().whatRewardTextSays = token.tokenToModify.name;
                token.tokenToModify.damageAmount += token.newDamageAmount;
                token.tokenToModify.healingAmount += token.newHealingAmount;
                token.tokenToModify.defendAmount += token.newDefendAmount;

                if(token.newDamageAmount > 0)
                {
                    popup.GetComponent<RewardPopup>().whatRewardTextSays += " +" + token.newDamageAmount + " damage";
                }
                if(token.newHealingAmount > 0)
                {
                    popup.GetComponent<RewardPopup>().whatRewardTextSays += " +" + token.newHealingAmount + " healing";

                }
                if(token.newHealingAmount > 0)
                {
                    popup.GetComponent<RewardPopup>().whatRewardTextSays += " +" + token.newDefendAmount + " defend";

                }

                if (token.newUser != null) token.tokenToModify.character = token.newUser;
                if (token.newSprite != null) token.tokenToModify.icon = token.newSprite;

                 token.tokenToModify.icon = popup.GetComponent<RewardPopup>().rewardIcon.sprite;
            }
        }
    }
}
