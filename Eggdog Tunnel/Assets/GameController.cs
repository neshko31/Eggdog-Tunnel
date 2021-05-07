using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    protected GameController() { }

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

    public float Health
    {
        get { return mHealth; }
    }
}
