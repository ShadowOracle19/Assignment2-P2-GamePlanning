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

public class EnemyStats : DataStats, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public EnemyScriptable currentEnemy;

    public int ATBSpeed = 1;

    public EnemyActions currentAction;
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
