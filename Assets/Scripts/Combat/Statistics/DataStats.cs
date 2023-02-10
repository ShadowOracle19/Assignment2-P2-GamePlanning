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

    public void Attack(float damage,  DataStats victim, bool isAoe)
    {
        if(damage <= 0) return;
        
        float totalDamage = damage;
        if(isAoe)//aoe only works for player attacking enemy
        {
            List<EnemyStats> targets = TurnBasedManager.Instance.enemies;
            for (int i = 0; i < targets.Count; i++)
            {
                damage = totalDamage;
                if (targets[i].defendHealth > 0)
                {
                    var tempDamage = damage;
                    damage -= targets[i].defendHealth;
                    targets[i].defendHealth -= tempDamage;
                    targets[i].defendHealthText.text = targets[i].defendHealth.ToString();

                    if (!(damage <= 0))
                    {
                        targets[i].defendHealthParent.SetActive(false);
                        targets[i].currentHealth -= damage;
                        targets[i].healthSlider.value = targets[i].currentHealth;
                        continue;
                    }
                    else if (targets[i].defendHealth <= 0)
                    {
                        defendHealth = 0;
                        targets[i].defendHealthParent.SetActive(false);
                    }
                }
                else
                {
                    targets[i].currentHealth -= damage;
                    targets[i].healthSlider.value = targets[i].currentHealth;
                    continue;
                }
            }
            return;
        }

        if (victim.defendHealth > 0)
        {
            var tempDamage = damage;
            damage -= victim.defendHealth;
            victim.defendHealth -= tempDamage;
            victim.defendHealthText.text = victim.defendHealth.ToString();

            if (!(damage <= 0))
            {
                victim.defendHealthParent.SetActive(false);
                victim.currentHealth -= damage;
                victim.healthSlider.value = victim.currentHealth;
                return;
            }
            else if (victim.defendHealth <= 0)
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
        if (num <= 0) return;
        user.defendHealthParent.SetActive(true);
        user.defendHealth += num;
        user.defendHealthText.text = defendHealth.ToString();
    }

    public void Heal(DataStats user, int heal)
    {
        if (heal <= 0) return;
        user.currentHealth += heal;
    }
}
