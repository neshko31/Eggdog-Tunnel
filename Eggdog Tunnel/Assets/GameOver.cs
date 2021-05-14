using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Invoke(nameof(Load), 2f);
        }
    }
    void Load ()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
