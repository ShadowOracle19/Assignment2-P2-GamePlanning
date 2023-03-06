using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region GameManager instance
    public static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is Null");

            return _instance;
        }
    }
    #endregion


    [Header("Map")]
    public GameObject playerMoveSprite;//this sprite is only shown on the map to move to each node
    public GameObject map;
    public MapNode node;

    [Header("Dialogue")]
    public Dialogue dialogueManager;
    public GameObject dialogueUI;

    [Header("Combat")]
    public TurnBasedManager combatManager;
    public GameObject combatUI;
    public PlayerStats player;
    public List<ActionTokens> actionTokens;

    [Header("UI")]
    public GameObject endEncounterUI;
    public GameObject pausePanelUI;
    public GameObject shopMenuUI;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI rationsAmountText;
    public TextMeshProUGUI medkitAmountText;
    public TextMeshProUGUI capsAmountText;
    public GameObject rewardBasePopup;
    public Transform popupParent;

    [Header("Supplies")]
    public int amountOfRations = 5;
    public int amountOfMedkits = 2;
    public int caps = 99;
    public int amountOfTimeWithoutRations = 0;

    public bool isGamePaused = false;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player.currentHealth = player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = "Health: " + player.currentHealth.ToString() + "/" + player.maxHealth.ToString();
        rationsAmountText.text = "Rations Available: " + amountOfRations.ToString();
        capsAmountText.text = "Caps: " + caps.ToString();
        medkitAmountText.text = "X " + amountOfMedkits.ToString();
    }

    public void StartCombatEncounter(CombatEncounter encounter)
    {
        combatUI.SetActive(true);
        map.SetActive(false);

        combatManager.StartEncounter(encounter.encounteredEnemies, node);
    }
    public void StartDialogueEncounter(DialogueEncounter encounter)
    {
        dialogueUI.SetActive(true);
        map.SetActive(false);

        dialogueManager.currentConversation = encounter.desiredConversation;
        dialogueManager.currentNode = node;
        dialogueManager.StartDialogue();
    }

    public void StartShopEncounter()
    {
        shopMenuUI.SetActive(true);
        map.SetActive(false);
    }

    public void EndEncounter()
    {
        node.encounter.GiveReward();
    }

    public void RewardPopup()
    {

    }

    public void UseMedkit()
    {
        if (player.currentHealth == player.maxHealth || amountOfMedkits <= 0) return; //if player has max health or no medkits do nothing

        amountOfMedkits -= 1;
        player.currentHealth += (int)(player.maxHealth / 2);//the player will heal 50% of their max health
        player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);
    }

}
