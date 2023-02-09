using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Token", menuName = "ScriptableObjects/ActionTokens")]
public class ActionTokens : ScriptableObject
{
    public Sprite icon;

    public bool isAoe = false;

    //if this is set to false it will be set to slashing. if set to trye it will be piercing
    public bool isPiercing = false;

    public int healingAmount = 0;
    public int damageAmount = 0;
    public int defendAmount = 0;

    public Sprite neutral;
    public Sprite angry;
}
