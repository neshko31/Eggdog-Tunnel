using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoKit : MonoBehaviour
{
    int ammoRestore = 10;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GunScript[] allGuns = UnityEngine.Object.FindObjectsOfType<GunScript>();
            foreach (var item in allGuns)
            {
                item.RestoreAmmo(ammoRestore);
            }
            Destroyy();
        }
    }
    private void Destroyy()
    {
        Destroy(gameObject);
    }
}
