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
    public EnemyScriptable currentEnemy;

    public int ATBSpeed = 1;

    public EnemyActions currentAction;
    public EnemyStates currentState;

    private EnemyStates highHealth;
    private EnemyStates midHealth;
    private EnemyStates lowHealth;

    private float attackChance;
    private float attackPercent = 50.0f;


    public Image enemyImage;
    public Image indicator;
    public Sprite attackIndicator;
    public Sprite defendIndicator;

    public TextMeshProUGUI enemyName;

    public bool enemyIsTargeted = false;
    public bool enemyIsHovered = false;
    public Image targetIcon;

    // Start is called before the first frame update
    void Start()
    {
        targetIcon.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = currentHealth + "/" + maxHealth;
        healthSlider.value = currentHealth;

        if(enemyIsTargeted)
        {
            targetIcon.gameObject.SetActive(true);
            targetIcon.color = Color.red;
        }
        else if(!enemyIsTargeted && !enemyIsHovered)
        {
            targetIcon.gameObject.SetActive(false);
            targetIcon.color = Color.white;
        }

        if(currentHealth/maxHealth >= 0.7f)//enemy at max/high health
        {
            currentState = highHealth;
        }
        else if(currentHealth / maxHealth >= 0.4f)//enemy at mid health
        {
            currentState = midHealth;
        }
        else if(currentHealth / maxHealth <= 0.3f)//enemy at low health
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


        enemyName.text = currentEnemy.enemyName;

        enemyImage.sprite = currentEnemy.enemySprite;

        maxHealth = currentEnemy.maxHealth;
        currentHealth = currentEnemy.maxHealth;

        attack = currentEnemy.attack;

        defend = currentEnemy.defend;
        healthSlider.maxValue = maxHealth;

        ATBSpeed = currentEnemy.ATBSpeed;
        ATBSlider.value = 0;

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
