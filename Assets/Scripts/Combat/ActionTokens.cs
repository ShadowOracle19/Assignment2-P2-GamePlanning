using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Token", menuName = "ScriptableObjects/ActionTokens")]
public class ActionTokens : ScriptableObject
{
    public Sprite icon;
    public string tokenName;
    public AudioClip sfx;

    public bool isAoe = false;

    //if this is set to false it will be set to slashing. if set to trye it will be piercing
    public bool isPiercing = false;

    public bool isChangeStance = false; //only set to true if you want to change player stance

    public int healingAmount = 0;
    public int damageAmount = 0;
    public int defendAmount = 0;


    public Character character;

    public string tooltipInfo;
}
