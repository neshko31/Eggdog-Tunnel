using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public GameObject txt;
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

    public void SetDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            GameDone();
        }
    }

    public float Health
    {
        get { return currentHealth; }
        //set { currentHealth = value; }
    }

    public void GameDone()
    {
        txt.SetActive(true);
        Time.timeScale = 0f;
        Invoke(nameof(Load), 1f);
    }
    void Load()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
