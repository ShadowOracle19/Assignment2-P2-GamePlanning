using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EnemyActions
{
    Attack,
    Defend
}

public class EnemyStats : DataStats
{
    public EnemyScriptable currentEnemy;

    public int ATBSpeed = 1;

    public EnemyActions currentAction;
    public Image enemyImage;
    public Image indicator;
    public Sprite attackIndicator;
    public Sprite defendIndicator;

    public TextMeshProUGUI enemyName;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = currentHealth + "/" + maxHealth;
        healthSlider.value = currentHealth;
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

        ATBSpeed = currentEnemy.ATBSpeed;
        ATBSlider.value = 0;
    }

}
