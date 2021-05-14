using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text mTxtHealth;
    public Text mTxtAmmo;
    public HealthScript p;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mTxtHealth.text = "+ " + p.Health.ToString();//GameController.Instance.Health.ToString();

        GunScript[] armyUnits = (GunScript[])FindObjectsOfType(typeof(GunScript));

        foreach (var item in armyUnits)
        {
            if (item.name == "SciFiGunLightBlue")
            {
                mTxtAmmo.text = item.CurrentAmmo + "/" + item.RestOfAmmo;
                break;
            }
        }
    }
}
