using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : DataStats
{
    
    public int ATBSpeed = 10;

    // Update is called once per frame
    void Update()
    {

        healthText.text = currentHealth + "/" + maxHealth;
    }
}
