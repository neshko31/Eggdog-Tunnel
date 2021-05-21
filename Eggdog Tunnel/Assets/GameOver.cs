using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject txt;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameDone();
        }
    }
    void Load ()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GameDone ()
    {
        txt.SetActive(true);
        Time.timeScale = 0f;
        Invoke(nameof(Load), 1f);
    }
}
