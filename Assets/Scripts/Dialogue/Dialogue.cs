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
    public Image middleSpeaker;
    public Conversation currentConversation;
    private float textSpeed;
    public TextSpeed speedSelect;
    public int index;

    public TextMeshProUGUI checkTextSize;

    public MapNode currentNode;

    [Header("Choices")]
    public Choice choices;
    public GameObject choiceButtons;
    public List<GameObject> choiceButtonsList;
    public CombatEncounter dialogueCombat;

    // Start is called before the first frame update
    void Start()
    {
        choiceButtons.SetActive(false);
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        switch (speedSelect)//variable text speed
        {
            case TextSpeed.Slow:
                textSpeed = 0.15f;
                break;
            case TextSpeed.Medium:
                textSpeed = 0.1f;
                break;
            case TextSpeed.Fast:
                textSpeed = 0.05f;
                break;
            default:
                textSpeed = 0.1f;
                break;
        }
    }

    void StartDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;
        speakerName.text = string.Empty;



        //if there is no speaker set it to false
        if (currentConversation.speakerleft == null)
        {
            leftSpeaker.gameObject.SetActive(false);
        }
        else
        {
            leftSpeaker.gameObject.SetActive(true);
        }
        if(currentConversation.speakerRight == null)
        {
            rightSpeaker.gameObject.SetActive(false);
        }
        else
        {
            rightSpeaker.gameObject.SetActive(true);
        }
        if (currentConversation.speakerMiddle == null)
        {
            middleSpeaker.gameObject.SetActive(false);
        }
        else
        {
            middleSpeaker.gameObject.SetActive(true);
        }

        //if there is a speaker set it active and set the portait 
        if (currentConversation.speakerleft != null)
        {
            leftSpeaker.gameObject.SetActive(true);
            leftSpeaker.sprite = currentConversation.speakerleft.neutral;
        }
        if (currentConversation.speakerRight != null)
        {
            Debug.Log("um hello?");
            rightSpeaker.gameObject.SetActive(true);
            rightSpeaker.sprite = currentConversation.speakerRight.neutral;
        }
        if (currentConversation.speakerMiddle != null)
        {
            middleSpeaker.gameObject.SetActive(true);
            middleSpeaker.sprite = currentConversation.speakerMiddle.neutral;
        }



        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        checkTextSize.text = currentConversation.lines[index].text;
        yield return new WaitForSeconds(0.1f);
        //whichever side is speaking change color to 255 and the other side to 70
        if (currentConversation.lines[index].speakerSide == currentCharacter.LEFT)//Character left is speaking
        {
            rightSpeaker.color = new Color(0.6f, 0.6f, 0.6f);
            middleSpeaker.color = new Color(0.6f, 0.6f, 0.6f);
            leftSpeaker.color = new Color(1, 1, 1);
            ChangeCharacterEmotion(leftSpeaker, currentConversation.speakerleft, currentConversation.lines[index].currentEmotion);
        }
        else if (currentConversation.lines[index].speakerSide == currentCharacter.MIDDLE)//character right is speaking
        {
            rightSpeaker.color = new Color(0.6f, 0.6f, 0.6f);
            middleSpeaker.color = new Color(1, 1, 1);
            leftSpeaker.color = new Color(0.6f, 0.6f, 0.6f);
            ChangeCharacterEmotion(middleSpeaker, currentConversation.speakerMiddle, currentConversation.lines[index].currentEmotion);
        }
        else if(currentConversation.lines[index].speakerSide == currentCharacter.RIGHT)//character right is speaking
        {
            rightSpeaker.color = new Color(1, 1, 1);
            middleSpeaker.color = new Color(0.6f, 0.6f, 0.6f);
            leftSpeaker.color = new Color(0.6f, 0.6f, 0.6f);
            ChangeCharacterEmotion(rightSpeaker, currentConversation.speakerRight, currentConversation.lines[index].currentEmotion);
        }
        else
        {
            rightSpeaker.color = new Color(1, 1, 1);
            middleSpeaker.color = new Color(1, 1, 1);
            leftSpeaker.color = new Color(1, 1, 1);
        }

        //type each character 1 by 1 
        foreach (char c in currentConversation.lines[index].text.ToCharArray())
        {
            speakerName.text = currentConversation.lines[index].character.fullName;
            textComponent.fontSize = checkTextSize.fontSize;
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
        else//finish dialogue
        {
            if(currentConversation.choice != null)
            {
                LoadChoices(currentConversation.choice);
                Debug.Log("Choice found");
            }
            else
            {
                if(dialogueCombat != null)
                {
                    GameManager.Instance.StartCombatEncounter(dialogueCombat);
                    dialogueCombat = null;
                    GameManager.Instance.dialogueUI.SetActive(false);
                    return;
                }
                currentNode.encounter.GiveReward();
                currentNode.finishedEncounter = true;
                GameManager.Instance.map.SetActive(true);
                GameManager.Instance.dialogueUI.SetActive(false);
            }
        }
    }

    public void LoadDialogueOption(Option chosenOption)
    {
        if(chosenOption.combatEncounter != null)
        {
            dialogueCombat = chosenOption.combatEncounter; if (chosenOption.combatEncounter.reward != null)
            {
                currentNode.encounter.reward = chosenOption.combatEncounter.reward;
            }
        }
        if(chosenOption.rewardSystem != null )
        {
            currentNode.encounter.reward = chosenOption.rewardSystem;
        }
        choiceButtons.SetActive(false);
        currentConversation = chosenOption.conversationBranch;
        StartDialogue();
    }

    public void LoadChoices(Choice currentChoices)
    {
        choiceButtons.SetActive(true);

        for (int i = 0; i < choiceButtonsList.Count; i++)
        {
            if (currentChoices.options[i].conversationBranch == null)
            {
                Debug.Log(choiceButtonsList[i].gameObject.name + " Hidden");
                choiceButtonsList[i].gameObject.SetActive(false);
                continue;
            }
            choiceButtonsList[i].gameObject.SetActive(true);
            choiceButtonsList[i].GetComponent<ChoiceButton>().option = currentChoices.options[i];
        }
    }

    public void SetTextSpeed(int num)//0 - slow, 1 - medium, 2 - fast
    {
        switch (num)
        {
            case 0:
                speedSelect = TextSpeed.Slow;
                break;
            case 1:
                speedSelect = TextSpeed.Medium;
                break;
            case 2:
                speedSelect = TextSpeed.Fast;
                break;
            default:
                speedSelect = TextSpeed.Medium;
                break;
        }
    }

    public void DialogueClick()
    {
        if (textComponent.text == currentConversation.lines[index].text)
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = currentConversation.lines[index].text;
        }
    }

    public void ChangeCharacterEmotion(Image speakerImage, Character currentSpeakingCharacter, characterEmotion currentEmotion)
    {
        switch (currentEmotion)
        {
            case characterEmotion.NEUTRAL:
                speakerImage.sprite = currentSpeakingCharacter.neutral;
                break;

            case characterEmotion.HAPPY:
                speakerImage.sprite = currentSpeakingCharacter.happy;
                break;

            case characterEmotion.SAD:
                speakerImage.sprite = currentSpeakingCharacter.sad;
                break;

            case characterEmotion.ANGRY:
                speakerImage.sprite = currentSpeakingCharacter.angry;
                break;

            case characterEmotion.SUPRISED:
                speakerImage.sprite = currentSpeakingCharacter.suprised;
                break;

            default:
                break;
        }
    }
}
