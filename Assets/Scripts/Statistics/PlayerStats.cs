using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : DataStats
{
    public int special = 4;
    public int heal = 2;
    public int ATBSpeed = 10;
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
