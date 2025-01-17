using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum EnemyActions
{
    Attack,
    Defend
}

public enum EnemyHealthStates
{
    HighHealth,
    MediumHealth,
    LowHealth
}



public class EnemyStats : DataStats, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Enemy Stats")]
    public EnemyScriptable currentEnemy;

    public int ATBSpeed = 1;


    
    [Header("Enemy states")]
    public EnemyActions currentAction;
    public EnemyStates currentState;

    private EnemyStates highHealth;
    private EnemyStates midHealth;
    private EnemyStates lowHealth;

    private float attackChance;
    private float attackPercent = 50.0f;

    [Header("Enemy Sprites")]
    public Image enemyImage;
    public Image indicator;
    public Sprite attackIndicator;
    public Sprite defendIndicator;

    [Header("Target objects")]
    public bool enemyIsTargeted = false;
    public bool enemyIsHovered = false;
    public Image targetIcon;
    public Image targettedSprite;

    // Start is called before the first frame update
    void Start()
    {
        targetIcon.gameObject.SetActive(false);
        targettedSprite.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CombatFunction();
        
    }

    private void CombatFunction()
    {
        //When the action time bar fills to max it will use the desired action being either attack or defend        
        if (ATBSlider.value == ATBSlider.maxValue)
        {
            GetComponent<AudioSource>().clip = currentEnemy.attackSound;
            GetComponent<AudioSource>().Play();
            switch (currentAction)
            {
                case EnemyActions.Attack:
                    Attack(currentEnemy.attack, TurnBasedManager.Instance.player);
                    TurnBasedManager.Instance.combatAnim.SetTrigger("Attacked");
                    ATBSlider.value = 0;
                    break;
                case EnemyActions.Defend:
                    Defend(currentEnemy.defend, this);
                    ATBSlider.value = 0;
                    break;
                default:
                    break;
            }

        }

        //Display the health left of the enemy
        healthText.text = currentHealth + "/" + maxHealth;
        healthSlider.value = currentHealth;

        //If the enemy is targeted set all the target stuff to true
        if (enemyIsTargeted)
        {
            enemyImage.gameObject.SetActive(false);
            targetIcon.gameObject.SetActive(true);
            targettedSprite.gameObject.SetActive(true);
            targetIcon.color = Color.red;
        }
        else if (!enemyIsTargeted && !enemyIsHovered)
        {
            enemyImage.gameObject.SetActive(true);
            targetIcon.gameObject.SetActive(false);
            targettedSprite.gameObject.SetActive(false);
            targetIcon.color = Color.white;
        }

        if (currentHealth / maxHealth >= 0.7f)//enemy at max/high health
        {
            currentState = highHealth;
        }
        else if (currentHealth / maxHealth >= 0.4f)//enemy at mid health
        {
            currentState = midHealth;
        }
        else if (currentHealth / maxHealth <= 0.3f)//enemy at low health
        {
            currentState = lowHealth;
        }

        switch (currentState)
        {
            case EnemyStates.Normal:
                attackPercent = 50;
                break;
            case EnemyStates.Aggressive:
                attackPercent = 25;
                break;
            case EnemyStates.Defensive:
                attackPercent = 75;
                break;
            default:
                break;
        }
    }

    public void SetStats(EnemyScriptable encounteredEnemy)
    {
        currentEnemy = encounteredEnemy;

        enemyImage.sprite = currentEnemy.enemySprite;
        targettedSprite.sprite = currentEnemy.targetedEnemySprite;

        maxHealth = currentEnemy.maxHealth;
        currentHealth = currentEnemy.maxHealth;

        attack = currentEnemy.attack;

        defend = currentEnemy.defend;
        healthSlider.maxValue = maxHealth;

        ATBSpeed = currentEnemy.ATBSpeed;
        ATBSlider.value = Random.Range(0, currentEnemy.ATBSpeed);

        highHealth = currentEnemy.highHealth;
        midHealth = currentEnemy.midHealth;
        lowHealth = currentEnemy.lowHealth;

    }

    public void DisplayNextEnemyMove()
    {
        attackChance = Random.Range(0, 100);

        //attack will activate if number is higher then attack
        //attack will happen the lower attack percent is set
        if (attackChance > attackPercent)
        {
            indicator.sprite = attackIndicator;
            currentAction = EnemyActions.Attack;
        }
        else //defend
        {
            indicator.sprite = defendIndicator;
            currentAction = EnemyActions.Defend;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        TurnBasedManager.Instance.TargetNewEnemy(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetIcon.gameObject.SetActive(true);
        targetIcon.color = Color.white;
        enemyIsHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetIcon.gameObject.SetActive(false);
        enemyIsHovered = false;
    }
}
