using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("Shop")]
    public ShopManager shop;

    [Header("Tutorials")]
    public GameObject tutorialCanvas;
    public GameObject combatTutorial;
    public GameObject mapTutorial;
    public GameObject eventTutorial;

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

    [Header("Timer")]
    float timer = 0.0f;
    public int seconds;
    bool gameFinished = false;

    [Header("Volume Setting")]
    public Scrollbar music;
    public TextMeshProUGUI musicTextNum;

    public Scrollbar soundFX;
    public TextMeshProUGUI soundFXTextNum;

    [Header("Game finished")]
    public GameObject sceneRoot;
    public GameObject endGameScreen;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player.currentHealth = player.maxHealth;
        gameFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        AdjustVolume();
        playerHealthText.text = "Health: " + player.currentHealth.ToString() + "/" + player.maxHealth.ToString();
        rationsAmountText.text = "Rations Available: " + amountOfRations.ToString();
        capsAmountText.text = "Caps: " + caps.ToString();
        medkitAmountText.text = "X " + amountOfMedkits.ToString();

        if(!gameFinished)
        {
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);
        }
    }

    public void AdjustVolume()
    {
        musicTextNum.text = ((int)(music.value * 100)).ToString();
        soundFXTextNum.text = ((int)(soundFX.value * 100)).ToString();
        SoundEffectManager.Instance.mapSFX.volume = music.value / 10;
        SoundEffectManager.Instance.combatSFX.volume = music.value / 10;
        SoundEffectManager.Instance.shopSFX.volume = music.value / 10;

        SoundEffectManager.Instance.weaponSFX.volume = music.value;
        SoundEffectManager.Instance.rewardSFX.volume = music.value / 10;
        SoundEffectManager.Instance.stanceChangeMeleeSFX.volume = music.value;
        SoundEffectManager.Instance.stanceChangeRangedSFX.volume = music.value;
    }

    public void StartCombatEncounter(CombatEncounter encounter)
    {
        combatUI.SetActive(true);
        map.SetActive(false);

        SoundEffectManager.Instance.mapSFX.Pause();
        SoundEffectManager.Instance.combatSFX.Play();

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


        SoundEffectManager.Instance.mapSFX.Pause();
        SoundEffectManager.Instance.shopSFX.Play();

        shop.currentNode = node;
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

    public void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void EndGame()
    {
        gameFinished = true;
        TelemetryLogger.Log(this, "Time taken to finish game", seconds);
        endGameScreen.SetActive(true);
        sceneRoot.SetActive(false);
        combatUI.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
