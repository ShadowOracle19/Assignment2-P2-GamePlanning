using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ModifyToken
{
    public ActionTokens tokenToModify;

    public Sprite newSprite;

    public int newHealingAmount;
    public int newDamageAmount;
    public int newDefendAmount;

    public Character newUser;
}

[CreateAssetMenu(fileName = "New Reward Set", menuName = "ScriptableObjects/Reward Creator")]
public class RewardSystem : ScriptableObject
{
    public int numberOfAddedRations;
    public int numberOfAddedMedkits;
    public int numberOfAddedCaps;

    public ModifyToken[] tokensToModify;
}
