using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct CurrentTokenUsage
{
    public int bandage;
    public int chainsaw;
    public int changeStance;
    public int knife;
    public int pistol;
    public int shield;
    public int shotgun;
    public int sledgehamer;
}

public class TurnBasedManager : MonoBehaviour
{
    #region Turn base manager instance
    public static TurnBasedManager _instance;

    public static TurnBasedManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("TurnBased Manager is Null");

            return _instance;
        }
    }
    #endregion

    public bool encounterRunning = false;

    //ai
    public float attackPercent = 50; //increase this number to defend more often. decrease to attack more often
    public float attackChance; //this number will be random and will determine if the enemy attacks or defends

    public PlayerStats player;

    public List<EnemyStats> enemies;
    public List<EnemyScriptable> enemyBacklog;
    public Transform enemyParent;

    public EnemyStats targetedEnemy;

    public MapNode currentNode;
    public CombatEncounter currentCombat;

    public ConveyorManager conveyorManager;

    public Animator combatAnim;
    public Image currentPlayerSprite;
    public Image nextPlayerSprite;

    public Image currentPlayerNameplate;
    public Image nextPlayerNameplate;

    public TextMeshProUGUI currentPlayerName;


    public Character currentOnScreenCharacter;
    public Character nextScreenCharacter;


    public CurrentTokenUsage tokenUsage;

    private void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!encounterRunning) return;
        player.healthSlider.value = player.currentHealth;
        player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);

        foreach(EnemyStats enemy in enemies)
        {
            if(!enemy.gameObject.activeInHierarchy)
            {
                enemies.Remove(enemy);
            }

            if(targetedEnemy == null)
            {
                TargetNewEnemy(enemy);
            }

            enemy.currentHealth = Mathf.Clamp(enemy.currentHealth, 0, enemy.maxHealth);
            enemy.ATBSlider.value += Time.deltaTime * enemy.ATBSpeed;

            if(enemy.currentHealth == 0)
            {
                if(enemyBacklog.Count != 0)
                {
                    enemy.SetStats(enemyBacklog[0]);
                    enemyBacklog.RemoveAt(0);
                }
                else if(enemyBacklog.Count == 0)
                {
                    if (targetedEnemy == enemy) targetedEnemy = null;

                    enemy.gameObject.SetActive(false);
                    enemies.Remove(enemy);
                }
            }
        }

        if(enemies.Count == 0) //win/lose state
        {
            //end encounter

            if (currentCombat.dialogueAfterCombat != null)
            {
                GameManager.Instance.StartDialogueEncounter(currentCombat.dialogueAfterCombat);
                conveyorManager.DestroyTokens();
                StopAllCoroutines();
                GameManager.Instance.combatUI.SetActive(false);
                return;
            }


            FinishEncounter();
        }
        else if(player.currentHealth == 0)
        {
            //activate player death state here
            GameManager.Instance.EndGameDeath();
        }
    }



    public void StartEncounter(List<EnemyScriptable> enemiesToEncounter, MapNode _currentNode)
    {
        foreach (Transform child in enemyParent)
        {
            enemies.Add(child.gameObject.GetComponent<EnemyStats>());
        }

        LoadEnemies(enemiesToEncounter);

        player.healthSlider.value = player.currentHealth;
        player.healthSlider.maxValue = player.maxHealth;

        StartCoroutine(conveyorManager.SpawnActionTokens());
        currentNode = _currentNode;
    }

    private void LoadEnemies(List<EnemyScriptable> enemiesToEncounter)
    {
        for (int i = 0; i < enemiesToEncounter.Count; i++)
        {
            if(i > 2)
            {
                enemyBacklog.Add(enemiesToEncounter[i]);
                continue;
            }
            enemies[i].gameObject.SetActive(true);
            enemies[i].SetStats(enemiesToEncounter[i]);
            TargetNewEnemy(enemies[i]);
        }
    }

    public void FinishEncounter()
    {
        encounterRunning = false;
        //TelemetryLogger.Log(this, $"Token's used through combat {currentNode.encounter.name}", tokenUsage);

        tokenUsage.bandage = 0;
        tokenUsage.chainsaw = 0;
        tokenUsage.changeStance = 0;
        tokenUsage.knife = 0;
        tokenUsage.pistol = 0;
        tokenUsage.shield = 0;
        tokenUsage.shotgun = 0;
        tokenUsage.sledgehamer = 0;


        conveyorManager.DestroyTokens();
        StopAllCoroutines();
        GameManager.Instance.RewardPopup();

    }

    public void TargetNewEnemy(EnemyStats newEnemyTarget)
    {
        if(targetedEnemy != null) targetedEnemy.enemyIsTargeted = false;

        targetedEnemy = newEnemyTarget;
        targetedEnemy.enemyIsTargeted = true;
    }

    public void SwapSprites()
    {
        combatAnim.ResetTrigger("SwapSprite");
        combatAnim.ResetTrigger("Attacking");
        combatAnim.ResetTrigger("Attacked");
        currentPlayerSprite.sprite = nextPlayerSprite.sprite;

        currentPlayerNameplate.sprite = nextPlayerNameplate.sprite;
        currentOnScreenCharacter = nextScreenCharacter;
        currentPlayerSprite.sprite = currentOnScreenCharacter.neutral;
        currentPlayerSprite.color = Color.white;
    }

    public void AngrySpriteSwap()
    {
        currentPlayerSprite.sprite = currentOnScreenCharacter.angry;
    }
}
