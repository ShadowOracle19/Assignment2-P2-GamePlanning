using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
