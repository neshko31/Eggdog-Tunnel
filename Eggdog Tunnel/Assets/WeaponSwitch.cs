using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun2.SetActive(false);
            gun1.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gun1.SetActive(false);
            gun2.SetActive(true);
        }
    }
}
