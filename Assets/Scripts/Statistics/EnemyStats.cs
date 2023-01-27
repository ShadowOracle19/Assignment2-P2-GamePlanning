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
    public EnemyHealthStates currentState;
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
        Debug.Log("Current health percent for " + gameObject.name + ": " + currentHealth / maxHealth);

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
            currentState = EnemyHealthStates.HighHealth;
        }
        else if(currentHealth / maxHealth >= 0.4f)//enemy at mid health
        {
            currentState = EnemyHealthStates.MediumHealth;
        }
        else if(currentHealth / maxHealth <= 0.3f)//enemy at low health
        {
            currentState = EnemyHealthStates.LowHealth;
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
