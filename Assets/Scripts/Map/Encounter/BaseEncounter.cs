using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEncounter : ScriptableObject
{
    [Header("Rewards")]
    public int moneyReward;
    public int rationReward;
    public int medkitReward;

    public virtual void StartEncounter()
    {
        Debug.Log("Base Encounter");
        return;
    }
}
