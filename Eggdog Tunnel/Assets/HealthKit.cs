using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    float healthRestore = 20;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (GameController.Instance.RestoreHealth(healthRestore))
            {
                Destroyy();
            }
        }
    }
    private void Destroyy ()
    {
        Destroy(gameObject);
    }
}
