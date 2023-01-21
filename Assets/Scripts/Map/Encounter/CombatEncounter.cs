using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter", menuName = "ScriptableObjects/Encounters/Combat")]
public class CombatEncounter : BaseEncounter
{
    public List<EnemyScriptable> encounteredEnemies;

    public override void StartEncounter()
    {
        GameManager.Instance.StartCombatEncounter(this);
    }
}
