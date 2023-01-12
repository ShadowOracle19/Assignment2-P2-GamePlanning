using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEncounter : ScriptableObject
{
    public virtual void StartEncounter()
    {
        Debug.Log("Base Encounter");
        return;
    }
}
