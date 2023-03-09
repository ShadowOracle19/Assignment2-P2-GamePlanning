using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public Option option;
    public TextMeshProUGUI choiceTextBox;

    public Dialogue dialogueManager;

    // Update is called once per frame
    void Update()
    {
        choiceTextBox.text = option.text;    
    }

    public void OnChoiceSelect()
    {
        TelemetryLogger.Log(this, $"Choice Selected from {dialogueManager.currentConversation.name}: {option.text}");
        dialogueManager.LoadDialogueOption(option);
    }
}
