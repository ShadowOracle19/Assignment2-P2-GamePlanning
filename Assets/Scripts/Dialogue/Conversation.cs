using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum currentCharacter
{
    LEFT,
    RIGHT,
    MIDDLE,
    NONE
}

public enum characterEmotion
{
    NEUTRAL,
    HAPPY,
    SAD,
    ANGRY,
    SUPRISED
}

[System.Serializable]
public struct Line
{
    public Character character;

    
    public currentCharacter speakerSide;

    public characterEmotion currentEmotion;

    [TextArea(2, 5)]
    public string text;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "ScriptableObjects/DialogueCreator")]
public class Conversation : ScriptableObject
{
    public Character speakerleft;
    public Character speakerRight;
    public Character speakerMiddle;
    public Line[] lines;

    public Choice choice;
    public CombatEncounter combat;
 
}


