using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image healthBar;

    float maxHealth;

    public void setMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void setHealth(float health)
    {
        healthBar.fillAmount = health / maxHealth;
    }
}
