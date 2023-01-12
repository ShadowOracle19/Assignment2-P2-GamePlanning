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

    public MapNode currentNode;

    public ConveyorManager conveyorManager;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    DisplayNextEnemyMove();
    //    player.currentHealth = player.maxHealth;
    //    enemy.currentHealth = enemy.maxHealth;
    //}

    // Update is called once per frame
    void Update()
    {
        enemy.currentHealth = Mathf.Clamp(enemy.currentHealth, 0, enemy.maxHealth);
        player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);

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


        if(enemy.currentHealth == 0) //win/lose state
        {
            //end encounter
            FinishEncounter();
        }
        else if(player.currentHealth == 0)
        {
            //activate player death state here
        }
    }


    public void StartEncounter(EnemyScriptable encounteredEnemy, MapNode _currentNode)
    {
        enemy.SetStats(encounteredEnemy);
        player.currentHealth = player.maxHealth;
        enemy.currentHealth = enemy.maxHealth;
        DisplayNextEnemyMove();

        StartCoroutine(conveyorManager.SpawnActionTokens());
        currentNode = _currentNode;
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

    public void FinishEncounter()
    {
        conveyorManager.DestroyTokens();
        currentNode.finishedEncounter = true;
        GameManager.Instance.map.SetActive(true);
        GameManager.Instance.combatUI.SetActive(false);

    }
}
