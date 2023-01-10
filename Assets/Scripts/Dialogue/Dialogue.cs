using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TextSpeed
{
    Slow,
    Medium,
    Fast
}

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI speakerName;

    public Image leftSpeaker;
    public Image rightSpeaker;

    public Conversation currentConversation;


    public float textSpeed;

    public TextSpeed speedSelect;


    public int index;

    public currentCharacter character;

    

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        speakerName.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        character = currentConversation.lines[index].speakerSide;
        

        if(Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == currentConversation.lines[index].text)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = currentConversation.lines[index].text;
            }
        }
    }

    void StartDialogue()
    {
        index = 0;

        leftSpeaker.sprite = currentConversation.speakerleft.portrait;
        rightSpeaker.sprite = currentConversation.speakerRight.portrait;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //whichever side is speaking change color to 255 and the other side to 70
        if (currentConversation.lines[index].speakerSide == currentCharacter.LEFT)//Character left is speaking
        {
            rightSpeaker.color = new Color(0.6f, 0.6f, 0.6f);
            leftSpeaker.color = new Color(1, 1, 1);
        }
        else if(currentConversation.lines[index].speakerSide == currentCharacter.RIGHT)//character right is speaking
        {
            rightSpeaker.color = new Color(1, 1, 1);
            leftSpeaker.color = new Color(0.6f, 0.6f, 0.6f);
        }

        //type each character 1 by 1 
        foreach (char c in currentConversation.lines[index].text.ToCharArray())
        {
            speakerName.text = currentConversation.lines[index].character.fullName;
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < currentConversation.lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
