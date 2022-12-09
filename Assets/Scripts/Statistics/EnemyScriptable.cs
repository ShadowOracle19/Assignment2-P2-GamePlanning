using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public Sprite enemySprite;
    public string enemyName;

    [Header("Stats")]
    public int maxHealth = 10;
    public int attack = 2;
    public int defend = 2;
    public int ATBSpeed = 1;
}
