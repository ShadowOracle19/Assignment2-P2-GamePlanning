using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedManager : MonoBehaviour
{
    //ai
    public float attackPercent = 50; //increase this number to defend more often. decrease to attack more often
    public float attackChance; //this number will be random and will determine if the enemy attacks or defends

    public PlayerStats player;
    public EnemyStats enemy;


    // Start is called before the first frame update
    void Start()
    {
        DisplayNextEnemyMove();
        player.currentHealth = player.maxHealth;
        enemy.currentHealth = enemy.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        enemy.ATBSlider.value += Time.deltaTime * enemy.ATBSpeed;

        if(enemy.ATBSlider.value == enemy.ATBSlider.maxValue)
        {
            Debug.Log("Action");
            if(enemy.currentAction == EnemyActions.Attack)
            {

                enemy.Attack(enemy.attack, player);
            }
            if(enemy.currentAction == EnemyActions.Defend)
            {
                enemy.Defend(Random.Range(1, 4), enemy);
            }

            enemy.ATBSlider.value = enemy.ATBSlider.minValue;
            DisplayNextEnemyMove();
        }


        //if(playerHealth == 0 || enemyHealth == 0) //win/lose state
        //{
        //    playerHealth = playerMaxHealth;
        //    enemyHealth = enemyMaxHealth;
        //}
    }


    public void DisplayNextEnemyMove()
    {
        attackChance = Random.Range(0, 100);
        //attack will activate if number is above 50
        if (attackChance > attackPercent)
        {
            enemy.indicator.sprite = enemy.attackIndicator;
            enemy.currentAction = EnemyActions.Attack;
        }
        else //defend
        {
            enemy.indicator.sprite = enemy.defendIndicator;
            enemy.currentAction = EnemyActions.Defend;
        }
    }
}
