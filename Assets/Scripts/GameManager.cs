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
    public float mapMoveSpeed = 1;

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
    public Sprite rationsSprite;
    public Sprite capSprite;
    public Sprite medkitSprite;

    public bool isGamePaused = false;

    [Header("Timer")]
    float timer = 0.0f;
    public int seconds;
    bool gameFinished = false;

    [Header("Volume Setting")]
    public Scrollbar music;
    public TextMeshProUGUI musicTextNum;
    public TextMeshProUGUI musicTextNumTitle;

    public Scrollbar soundFX;
    public TextMeshProUGUI soundFXTextNum;
    public TextMeshProUGUI soundFXTextNumTitle;

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
        playerHealthText.text = $"{player.currentHealth}/{player.maxHealth}";
        rationsAmountText.text = amountOfRations.ToString();
        capsAmountText.text = caps.ToString();
        medkitAmountText.text = amountOfMedkits.ToString();

        if(!gameFinished)
        {
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);
        }
    }

    public void AdjustVolume()
    {
        musicTextNum.text = ((int)(music.value * 100)).ToString();
        musicTextNumTitle.text = ((int)(music.value * 100)).ToString();
        soundFXTextNum.text = ((int)(soundFX.value * 100)).ToString();
        soundFXTextNumTitle.text = ((int)(soundFX.value * 100)).ToString();
        SoundEffectManager.Instance.mapSFX.volume = music.value / 10;
        SoundEffectManager.Instance.combatSFX.volume = music.value / 10;
        SoundEffectManager.Instance.shopSFX.volume = music.value / 10;
        SoundEffectManager.Instance.TitleSFX.volume = music.value / 10;

        SoundEffectManager.Instance.weaponSFX.volume = soundFX.value;
        SoundEffectManager.Instance.rewardSFX.volume = soundFX.value / 10;
        SoundEffectManager.Instance.stanceChangeMeleeSFX.volume = soundFX.value;
        SoundEffectManager.Instance.stanceChangeRangedSFX.volume = soundFX.value;
    }

    public void StartCombatEncounter(CombatEncounter encounter)
    {
        

        combatUI.SetActive(true);
        map.SetActive(false);

        SoundEffectManager.Instance.mapSFX.Pause();
        SoundEffectManager.Instance.combatSFX.Play();
        combatManager.encounterRunning = true;
        combatManager.currentCombat = encounter;
        combatManager.StartEncounter(encounter.encounteredEnemies, node);

        if (encounter.isTutorial)
        {
            combatTutorial.SetActive(true);
            PauseGame();
        }
    }
    public void StartDialogueEncounter(DialogueEncounter encounter)
    {
        dialogueUI.SetActive(true);
        map.SetActive(false);

        dialogueManager.currentConversation = encounter.desiredConversation;


        dialogueManager.currentNode = node;
        dialogueManager.StartDialogue();

        if (encounter.isTutorial)
        {
            eventTutorial.SetActive(true);
            PauseGame();
        }
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
        node.finishedEncounter = true;
        map.SetActive(true);
        combatUI.SetActive(false);
        shopMenuUI.SetActive(false);
        dialogueUI.SetActive(false);

        SoundEffectManager.Instance.mapSFX.Play();
        SoundEffectManager.Instance.shopSFX.Pause();
        SoundEffectManager.Instance.combatSFX.Pause();

    }

    public void RewardPopup()
    {
        if (node.encounter.reward == null) EndEncounter();
        node.encounter.GiveReward();
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

    public void EndGameDeath()
    {
        gameFinished = true;
        TelemetryLogger.Log(this, "Death state achieved", seconds);
        endGameScreen.SetActive(true);
        sceneRoot.SetActive(false);
        combatUI.SetActive(false);
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
