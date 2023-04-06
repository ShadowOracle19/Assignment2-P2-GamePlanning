using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : DataStats
{
    
    public int ATBSpeed = 10;

    public void SpriteSwap()
    {
        TurnBasedManager.Instance.SwapSprites();
    }

    public void Attack()
    {
        TurnBasedManager.Instance.AngrySpriteSwap();
    }

    // Update is called once per frame
    void Update()
    {

        healthText.text = currentHealth + "/" + maxHealth;
    }
}
