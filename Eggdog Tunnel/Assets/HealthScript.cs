using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    private float maxHealth = 100;
    private float currentHealth = 100;

    public bool RestoreHealth(float health)
    {
        if (maxHealth == currentHealth)
        {
            return false;
        }
        if (currentHealth + health >= 100)
        {
            currentHealth = 100;
            return true;
        }
        else
        {
            currentHealth += health;
            return true;
        }
    }

    public float Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
}
