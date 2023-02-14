using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Option
{
    public Conversation conversationBranch;

    public RewardSystem rewardSystem;
    public CombatEncounter combatEncounter;

    [TextArea(2, 5)]
    public string text;
}

[CreateAssetMenu(fileName = "New Choices", menuName = "ScriptableObjects/ChoiceCreator")]
public class Choice : ScriptableObject
{
    public Option[] options = new Option[3];
}
