using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyActions
{
    Attack,
    Defend
}

public class EnemyStats : DataStats
{
    public int ATBSpeed = 1;

    public EnemyActions currentAction;
    public Image indicator;
    public Sprite attackIndicator;
    public Sprite defendIndicator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = currentHealth + "/" + maxHealth;
    }

}
