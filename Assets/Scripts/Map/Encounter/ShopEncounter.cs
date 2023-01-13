using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter", menuName = "ScriptableObjects/Encounters/Shop")]
public class ShopEncounter : BaseEncounter
{
    public override void StartEncounter()
    {
        GameManager.Instance.StartShopEncounter();
    }
}
