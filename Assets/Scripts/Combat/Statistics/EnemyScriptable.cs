using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates
{
    Normal,
    Aggressive,
    Defensive
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public Sprite enemySprite;
    public Sprite targetedEnemySprite;
    public string enemyName;

    [Header("Stats")]
    public int maxHealth = 10;
    public int attack = 2;
    public int defend = 2;
    public int ATBSpeed = 1;

    [Header("High Health State")]
    public EnemyStates highHealth;

    [Header("Mid Health State")]
    public EnemyStates midHealth;
    
    [Header("Low Health State")]
    public EnemyStates lowHealth;
}
