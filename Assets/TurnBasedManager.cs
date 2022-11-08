using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedManager : MonoBehaviour
{
    private bool isPlayersTurn = true;

    public int playerHealth = 10;
    private int playerMaxHealth = 10;

    public int enemyHealth = 10;
    private int enemyMaxHealth = 10;

    public int playerAttack = 2;
    public int maxPlayerAttack = 2;
    public int enemyAttack = 2;
    public int maxEnemyAttack = 2;

    //ai
    public float attackPercent = 50; //increase this number to defend more often. decrease to attack more often
    public float attackChance; //this number will be random and will determine if the enemy attacks or defends

    //ui
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemyHealthText;

    public Image enemyAttackIndicator;

    // Start is called before the first frame update
    void Start()
    {
        DisplayNextEnemyMove();
    }

    // Update is called once per frame
    void Update()
    {
        //clamp player and enemy health so it wont go below zero
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
        enemyHealth = Mathf.Clamp(enemyHealth, 0, enemyMaxHealth);

        //display player and enemy current health 
        playerHealthText.text = playerHealth + "/" + playerMaxHealth;
        enemyHealthText.text = enemyHealth + "/" + enemyMaxHealth;

        if(!isPlayersTurn)
        {
            //attack will activate if number is above 50
            if(attackChance > attackPercent)
            {
                playerHealth -= enemyAttack;
                enemyAttack = maxEnemyAttack;
                isPlayersTurn = true;
                DisplayNextEnemyMove();
            }
            else //defend
            {
                playerAttack -= 1;
                isPlayersTurn = true;
                DisplayNextEnemyMove();
            }
        }

        if(playerHealth == 0 || enemyHealth == 0)
        {
            playerHealth = playerMaxHealth;
            enemyHealth = enemyMaxHealth;
        }
    }

    public void Attack()
    {
        enemyHealth -= playerAttack;
        playerAttack = maxPlayerAttack;
        isPlayersTurn = false;
    }

    public void Defend()
    {
        enemyAttack -= 1;
        isPlayersTurn = false;
    }

    public void DisplayNextEnemyMove()
    {
        attackChance = Random.Range(0, 100);
        //attack will activate if number is above 50
        if (attackChance > attackPercent)
        {
            enemyAttackIndicator.color = Color.red;
        }
        else //defend
        {
            enemyAttackIndicator.color = Color.blue;
        }
    }
}
