using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ActionTypeEnum
{
    Attack,
    Special,
    Defend,
    Heal
}

public class ActionType : MonoBehaviour
{
    public ActionTypeEnum type;

    public int effectNum = 0; //holds the variable of how much the action type will do a thing

    public TextMeshProUGUI num;

    private void Start()
    {
        
    }

    public void CombineActionType(int otherEffectNum)
    {
        effectNum += otherEffectNum;
    }
}
