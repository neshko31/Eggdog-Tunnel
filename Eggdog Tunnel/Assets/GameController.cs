using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    protected GameController() { }

    private float maxHealth = 100;
    private float mHealth = 100;

    public void SetDamage(float damage)
    {
        mHealth -= damage;

        if (mHealth <= 0)
        {
            mHealth = 0;
        }

        Debug.Log(mHealth);
    }

    public bool RestoreHealth(float health)
    {
        if (maxHealth == mHealth)
        {
            return false;
        }
        if (mHealth+health>=100)
        {
            mHealth = 100;
            return true;
        }
        else
        {
            mHealth += health;
            return true;
        }
    }

    public float Health
    {
        get { return mHealth; }
    }
}
