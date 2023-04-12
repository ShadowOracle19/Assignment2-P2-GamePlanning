using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter", menuName = "ScriptableObjects/Encounters/Dialogue")]
public class DialogueEncounter : BaseEncounter
{
    public Conversation desiredConversation;

    public override void StartEncounter()
    {
        GameManager.Instance.StartDialogueEncounter(this);
        
    }
}
