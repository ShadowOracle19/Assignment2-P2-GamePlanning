using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataStats : MonoBehaviour
{
    [Header("Stats")]
    public float currentHealth;
    public float maxHealth = 10;
    public float attack = 2;
    public float defend = 2;
    public float defendHealth = 0;

    [Header("UI")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI defendHealthText;
    public GameObject defendHealthParent;
    public Slider healthSlider;
    public Slider ATBSlider;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(float damage, DataStats victim)
    {
        if(victim.defendHealth > 0)
        {
            var tempDamage = damage;
            damage -= victim.defendHealth;
            victim.defendHealth -= tempDamage;
            victim.defendHealthText.text = victim.defendHealth.ToString();
            
            if(!(damage <= 0))
            {
                victim.defendHealthParent.SetActive(false);
                victim.currentHealth -= damage;
                victim.healthSlider.value = victim.currentHealth;
                return;
            }
            else if(victim.defendHealth <= 0)
            {
                defendHealth = 0;
                victim.defendHealthParent.SetActive(false);
            }
        }
        else
        {
            victim.currentHealth -= damage;
            victim.healthSlider.value = victim.currentHealth;
            return;
        }
    }

    public void Defend(int num, DataStats user)
    {
        user.defendHealthParent.SetActive(true);
        user.defendHealth += num;
        user.defendHealthText.text = defendHealth.ToString();
    }

    public void Heal(DataStats user, int heal)
    {
        user.currentHealth += heal;
    }
}
