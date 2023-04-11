using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFirstDialogue : MonoBehaviour
{
    public DialogueEncounter dialogueEncounter;
    public MapNode _node;
    
    public void LoadDialogue()
    {
        GameManager.Instance.StartDialogueEncounter(dialogueEncounter);
        GameManager.Instance.node = _node;
    }
}
