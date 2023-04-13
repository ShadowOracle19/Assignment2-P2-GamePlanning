using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Option option;
    public TextMeshProUGUI choiceTextBox;

    public Dialogue dialogueManager;
    public bool isHovered = false;

    // Update is called once per frame
    void Update()
    {
        choiceTextBox.text = option.text;    
    }

    public void OnChoiceSelect()
    {
        //TelemetryLogger.Log(this, $"Choice Selected from {dialogueManager.currentConversation.name}", option.text);
        dialogueManager.LoadDialogueOption(option);
        choiceTextBox.color = Color.white;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHovered)
        {
            isHovered = true;
            GetComponent<AudioSource>().Play();
            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x + 100, GetComponent<RectTransform>().sizeDelta.y);

        }
        choiceTextBox.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHovered)
        {
            isHovered = false;
            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x - 100, GetComponent<RectTransform>().sizeDelta.y);

        }
        choiceTextBox.color = Color.white;
    }
}
